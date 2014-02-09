using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using OSGeo.MapServer;
using OSGeo.GDAL;
using OSGeo.OGR;
using DMS.MapLibrary;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;
using MapManager;

namespace DMS.MapManager
{
    /// <summary>
    /// The main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private string fileName;
        private int scrollPos;
        private int caretPos;
        public AppSettings settings;
        public bool dirtyFlag;
        bool textChanged;
        bool isTemplate;
        bool fileHasChanged;
        IPropertyEditor selectedEditor;
        SelectListForm selectListForm;
        CheckBox checkBoxRender;
        public TileManagerForm tileManagerForm;

        /// <summary>
        /// Locate the plugin dependencies by the application
        /// </summary>
        private void LocateDependencies()
        {
            //if (File.Exists(Path.Combine(Environment.CurrentDirectory, "gdalplugins\\ogr_OCI.dll")) && !MapUtils.FindLibrary("oci.dll"))
            //    Environment.Exit(1);

            //if ((File.Exists(Path.Combine(Environment.CurrentDirectory, "gdalplugins\\ogr_SDE.dll")) ||
            //    File.Exists(Path.Combine(Environment.CurrentDirectory, "gdalplugins\\gdal_SDE.dll"))) &&
            //    !MapUtils.FindLibrary("sde91.dll"))
            //     Environment.Exit(1);

            try
            {
                // try to load all the dependencies at startup
                string version = mapscript.msGetVersion();

                // gdal plugin path
                if (Directory.Exists(Environment.CurrentDirectory + "\\gdalplugins"))
                    Gdal.SetConfigOption("GDAL_DRIVER_PATH", Environment.CurrentDirectory + "\\gdalplugins");

                Gdal.AllRegister();
                Ogr.RegisterAll();
                mapscript.SetEnvironmentVariable("CURL_CA_BUNDLE=" + Environment.CurrentDirectory + "\\curl-ca-bundle.crt");
                // load scintilla config from file
                scintillaControl.ConfigurationManager.Language = "user";
                ScintillaNet.Configuration.Configuration config = new ScintillaNet.Configuration.Configuration(Environment.CurrentDirectory + "\\MapfileConfig.xml", "user", true);
                scintillaControl.ConfigurationManager.Configure(config);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The MapManager application failed to load, " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }


        /// <summary>
        /// Constructs the main form.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            MapUtils.SetPROJ_LIB(Environment.CurrentDirectory + "\\ProjLib");

            Gdal.SetConfigOption("GDAL_DATA", Environment.CurrentDirectory);
            LocateDependencies();

            textChanged = false;
            scrollPos = 0;
            caretPos = 0;

            dirtyFlag = false;
            isTemplate = false;
            fileHasChanged = false;
            selectedEditor = null;
            LoadSettings();

            selectListForm = new SelectListForm();
            selectListForm.VisibleChanged += new EventHandler(selectListForm_VisibleChanged);
            selectListForm.HelpRequested += new HelpEventHandler(editor_HelpRequested);

            EventProvider.EventMessage += new EventProvider.EventMessageEventHandler(EventProvider_EventMessage);

            this.splitContainer1.Panel1.Click += new EventHandler(LeftPanel_Click);
            this.splitContainer1.Panel2.Click += new EventHandler(LeftPanel_Click);

            // add render checkbox to status bar
            checkBoxRender = new CheckBox();
            checkBoxRender.Text = "";
            checkBoxRender.Checked = true;
            checkBoxRender.CheckedChanged += checkBoxRender_CheckedChanged;
            ToolStripControlHost cbItem = new ToolStripControlHost(checkBoxRender);
            //statusStripMain.Items.Add(cbItem);
            statusStripMain.Items.Insert(1, cbItem);

            UpdateMRU();
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxRender control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void checkBoxRender_CheckedChanged(object sender, EventArgs e)
        {
            mapControl.EnableRendering = checkBoxRender.Checked;
        }

        /// <summary>
        /// Click event handler of the split container left panel.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void LeftPanel_Click(object sender, EventArgs e)
        {
            if (tabControlContents.SelectedIndex == 1)
                tabControlContents.SelectedIndex = 0;  // switch back to the Map tab
        }

        /// <summary>
        /// EventMessage event handler of the EventProvider control.
        /// </summary>
        /// <param name="sender">The source object of the event</param>
        /// <param name="Message">The event message</param>
        /// <param name="type">The event type</param>
        void EventProvider_EventMessage(object sender, string Message, EventProvider.EventTypes type)
        {
            if (sender.GetType() == typeof(LayerPropertyEditor) && type == EventProvider.EventTypes.Error)
            {
                MessageBox.Show(Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (sender.GetType() == typeof(MapPropertyEditor) && type == EventProvider.EventTypes.Error)
            {
                MessageBox.Show(Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (sender.GetType() == typeof(MapControl) && type == EventProvider.EventTypes.Error)
            {
                MessageBox.Show(Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saving the main settings to the property bag.
        /// </summary>
        private void SaveWindowState()
        {
            settings.MainFormSettings.Save(this, null);
        }

        /// <summary>
        /// Loading the style library
        /// </summary>
        private void LoadStyleLibrary()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string styleLibFile = Application.StartupPath + "\\templates\\mmstyles.map";
                if (!File.Exists(styleLibFile))
                {
                    styleLibFile = Environment.CurrentDirectory + "\\mmstyles.map";
                    if (!File.Exists(styleLibFile))
                        return;
                }

                StyleLibrary.Load(styleLibFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Loading the main settings from the property bag.
        /// </summary>
        private void LoadSettings()
        {

            string appdataFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MapManager20\\MapManager.xml";
            // loading the application settings
            try
            {
                if (File.Exists(appdataFile))
                {
                    System.Xml.Serialization.XmlSerializer loader = new System.Xml.Serialization.XmlSerializer(typeof(AppSettings));
                    using (FileStream fs = File.OpenRead(appdataFile))
                    {
                        settings = (AppSettings)loader.Deserialize(fs);
                        if (settings.ColorRampList != null)
                            DMS.MapLibrary.ColorRampConverter.ColorRampList = settings.ColorRampList;
                    }
                }
            }
            catch (Exception)
            {

            }
            if (settings == null)
                settings = new AppSettings();

            if (settings.MainFormSettings == null)
                settings.MainFormSettings = new FormSettings();
            else
                settings.MainFormSettings.Load(this, null);

            LoadStyleLibrary();

            UpdateAppSettings();

            splitContainer.SplitterDistance = splitContainer.Panel1MinSize;
        }

        /// <summary>
        /// Updating the state of the form according to the property bag.
        /// </summary>
        private void UpdateAppSettings()
        {
            layerControl.BackColor = settings.LayerControlBackColor;
            layerControl.ShowCheckBoxes = settings.LayerControlShowCheckBoxes;
            layerControl.ShowClasses = settings.LayerControlShowClasses;
            layerControl.ShowRootObject = settings.LayerControlShowRootNode;
            layerControl.ShowStyles = settings.LayerControlShowStyles;
            layerControl.ShowLabels = settings.LayerControlShowLabels;
            layerControl.RefreshView();
        }

        /// <summary>
        /// Saving the settings to the MapManager.xml file.
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MapManager20";

                settings.ColorRampList = DMS.MapLibrary.ColorRampConverter.ColorRampList;

                // saving the application settings
                System.Xml.Serialization.XmlSerializer saver = new System.Xml.Serialization.XmlSerializer(typeof(AppSettings));

                // creating the directory if not exists
                if (!Directory.Exists(appdataFolder))
                    Directory.CreateDirectory(appdataFolder);

                using (FileStream fs = File.Open(appdataFolder + "\\MapManager.xml", FileMode.Create))
                {
                    saver.Serialize(fs, settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Loading the recently used map or the map specified at the command line.
        /// </summary>
        private void LoadRecentMap()
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1 && args[1] != "/replace")
                {
                    if (args[1].EndsWith(".map") && File.Exists(args[1]))
                    {
                        OpenMap(args[1], false);
                    }
                    else
                        MessageBox.Show("Error opening file: " + args[1],
                            "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (settings.AutoLoadRecent)
                {
                    string recent = settings.GetRecentMap();
                    if (recent != null && File.Exists(recent))
                    {
                        OpenMap(recent, false);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load the recent map: " + ex.Message,
                            "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            NewMap();
        }

        /// <summary>
        /// Updating the Most Recent Unit (MRU) list according to the currently opened file. 
        /// </summary>
        private void UpdateMRU()
        {
            this.recentMapsToolStripMenuItem.DropDownItems.Clear();
            // Adding the files in a LIFO fashion (ticket #4408)
            for (int i = settings.MRU.Count - 1; i >= 0; i--)
            {
                string s = settings.MRU[i].ToString();
                if (File.Exists(s))
                {
                    // filter out the new.map files (ticket #4423)
                    if (!s.ToLower().EndsWith("templates\\new.map"))
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem(s);
                        this.recentMapsToolStripMenuItem.DropDownItems.Add(s, null, this.MRUMenuItem_Click);
                    }
                }
            }
            UpdateMenuState();
        }

        /// <summary>
        /// Updating the title according to the currently opened file.
        /// </summary>
        private void UpdateTitle()
        {
            StringBuilder s = new StringBuilder("MapManager");
            if (fileName != null)
            {
                s.Append(" (");
                s.Append(fileName);
                if (dirtyFlag)
                    s.Append("*");
                s.Append(")");
            }
            this.Text = s.ToString();
        }

        /// <summary>
        /// Sets the modify flag of the map.
        /// </summary>
        /// <param name="dirty">The modify flag to be set.</param>
        private void SetDirty(bool dirty)
        {
            if (dirty != dirtyFlag)
            {
                dirtyFlag = dirty;
            }
            UpdateTitle();
        }

        /// <summary>
        /// Updating the checked and Enabled state of the menu items.
        /// </summary>
        private void UpdateMenuState()
        {
            saveAsToolStripMenuItem.Enabled = (mapControl.Target != null);
            clearSelectionToolStripMenuItem.Enabled = mapControl.QueryMode;
            saveToolStripMenuItem.Enabled = toolStripButtonSave.Enabled = (this.fileName != null);
            recentMapsToolStripMenuItem.Enabled = (settings.MRU.Count > 0);
            zoomToolStripMenuItem.Checked = toolStripButtonZoomIn.Checked =
                (mapControl.InputMode == MapControl.InputModes.ZoomIn);
            zoomOutToolStripMenuItem.Checked = toolStripButtonZoomOut.Checked =
                (mapControl.InputMode == MapControl.InputModes.ZoomOut);
            panToolStripMenuItem.Checked = toolStripButtonPan.Checked =
                (mapControl.InputMode == MapControl.InputModes.Pan);
            selectItemToolStripMenuItem.Checked = toolStripButtonSelect.Checked =
                (mapControl.InputMode == MapControl.InputModes.Select);
            selectByRectangleToolStripMenuItem.Checked = toolStripButtonSelectRect.Checked =
                (mapControl.InputMode == MapControl.InputModes.TrackRectangle);
            selectByPolygonToolStripMenuItem.Checked = toolStripButtonSelectPoly.Checked =
                (mapControl.InputMode == MapControl.InputModes.TrackPolygon);
            selectByLineToolStripMenuItem.Checked = toolStripButtonSelectLine.Checked =
                (mapControl.InputMode == MapControl.InputModes.TrackLine);
            zoomToolStripMenuItem.Enabled = toolStripButtonZoomIn.Enabled =
                zoomOutToolStripMenuItem.Enabled = toolStripButtonZoomOut.Enabled =
                panToolStripMenuItem.Enabled = toolStripButtonPan.Enabled =
                   redrawMapToolStripMenuItem.Enabled = toolStripButtonRedraw.Enabled =
                saveMapImageToolStripMenuItem.Enabled =
                mapPropertiesToolStripMenuItem.Enabled = (mapControl.Target != null);

            openFileExternalToolStripMenuItem.Enabled = (this.fileName != null && !isTemplate);

            setInitialExtentToolStripMenuItem.Enabled = false;
            if (!isTemplate)
                setInitialExtentToolStripMenuItem.Enabled = true;

            layerControl.Enabled = true;
            layerPropertyEditor.Enabled = true;
            mapPropertyEditor.Enabled = true;

            selectItemToolStripMenuItem.Enabled = toolStripButtonSelect.Enabled =
            selectByRectangleToolStripMenuItem.Enabled = toolStripButtonSelectRect.Enabled =
            selectByPolygonToolStripMenuItem.Enabled = toolStripButtonSelectPoly.Enabled =
            selectByLineToolStripMenuItem.Enabled = toolStripButtonSelectLine.Enabled = true;

            checkMapFileToolStripMenuItem.Enabled = false;

            if (tabControlContents.SelectedIndex == 1)
            {
                layerControl.Enabled = false;
                layerPropertyEditor.Enabled = false;
                mapPropertyEditor.Enabled = false;

                zoomToolStripMenuItem.Enabled = toolStripButtonZoomIn.Enabled =
                zoomOutToolStripMenuItem.Enabled = toolStripButtonZoomOut.Enabled =
                panToolStripMenuItem.Enabled = toolStripButtonPan.Enabled =
                redrawMapToolStripMenuItem.Enabled = toolStripButtonRedraw.Enabled =
                mapPropertiesToolStripMenuItem.Enabled = false;

                selectItemToolStripMenuItem.Enabled = toolStripButtonSelect.Enabled =
                selectByRectangleToolStripMenuItem.Enabled = toolStripButtonSelectRect.Enabled =
                selectByPolygonToolStripMenuItem.Enabled = toolStripButtonSelectPoly.Enabled =
                selectByLineToolStripMenuItem.Enabled = toolStripButtonSelectLine.Enabled = false;
                checkMapFileToolStripMenuItem.Enabled = true;
            }

            UpdateTitle();
        }

        /// <summary>
        /// Set up the file monitor based of the specified file
        /// </summary>
        /// <param name="file">The name of the file to be monitored</param>
        private void UpdateFileMonitor()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
            fileHasChanged = false;

            if (!isTemplate && fileName != null && File.Exists(fileName))
            {
                // register the file with filesystemwatcher
                fileSystemWatcher.Path = Path.GetDirectoryName(fileName);
                fileSystemWatcher.Filter = Path.GetFileName(fileName);
                fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Opening a map by showing a FileOpen dialog box.
        /// </summary>
        /// <param name="file">The name of the map file to open.</param>
        /// <returns>True if the file have been opened, otherwise false</returns>
        private bool OpenMap(string file, bool isTemplate)
        {
            // check if a Tile Manager window is open and close it if it is
            if (tileManagerForm != null) tileManagerForm.Close();
            tileManagerForm = null;

            if (dirtyFlag || buttonApply.Enabled)
            {
                DialogResult result = MessageBox.Show("The map has been edited, would you like to save the changes?", "MapManager",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    SaveMap();
                else if (result == DialogResult.Cancel)
                    return false;
            }

            buttonApply.Enabled = false;

            fileName = file;

            this.isTemplate = isTemplate;

            scrollPos = 0;
            MapfileConverter c = new MapfileConverter();
            try
            {
                c.Parse(File.ReadAllText(file, Encoding.Default), false);
                MapObjectHolder mapH;
                mapObj map;
                if (c.HasToConvert())
                {
                    MapFileConvertForm form = new MapFileConvertForm(c.GetChangeLog());
                    
                    if (form.ShowDialog(this) == DialogResult.Yes)
                    {
                        map = mapscript.msLoadMapFromString(c.GetMapFile(), null);
                        mapH = new MapObjectHolder(map, null);
                    }
                    else
                        throw new Exception ("Map file conversion aborted");
                }
                else
                {
                    mapH = MapUtils.OpenMap(file);
                    map = mapH;
                }

                if (map.extent.maxx == -1 && map.extent.minx == -1 && map.extent.maxy == -1 && map.extent.miny == -1)
                    map.setExtent(0, 0, 10, 10);

                mapH.PropertyChanged += new System.EventHandler(this.MainForm_PropertyChanged);
                mapH.PropertyChanging += new EventHandler(MainForm_PropertyChanging);
                mapH.SelectionChanged += new EventHandler(MainForm_SelectionChanged);
                mapH.ZoomChanged += new MapObjectHolder.ZoomChangedEventHandler(MainForm_ZoomChanged);

                mapControl.Target = mapH;
                layerControl_ItemSelect(this, null);
                layerControl.Target = mapControl.Target;
                selectListForm.selectList.Target = mapControl.Target;

                LoadTextContents();
                SetDirty(false);

                if (c.HasToConvert())
                    SetDirty(true); // conversion happened

                if (map.symbolset.filename != null && !File.Exists(map.symbolset.filename))
                {
                    // override the symbolset if that points to incorrect location
                    map.setSymbolSet(Application.StartupPath + "\\templates\\symbols.sym");
                }

                if (map.fontset.filename != null && !File.Exists(map.fontset.filename))
                {
                    // override the fontset if that points to incorrect location
                    map.setFontSet(Application.StartupPath + "\\templates\\font.list");
                }

                if (MapUtils.RenameDuplicatedNames(map))
                {
                    MessageBox.Show("Duplicated layer names detected in this mapfile, which is not supported by IntraMaps. The layers will be renamed to unique names.", "MapManager",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    layerControl.RefreshView();
                    SetDirty(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ". Please correct the issue in the text tab.", "MapManager",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDirty(false);
                mapControl.Target = null;
                layerControl_ItemSelect(this, null);
                layerControl.Target = null;
                selectListForm.selectList.Target = null;

                if (c.HasToConvert())
                    scintillaControl.Text = c.GetMapFile();
                else
                    scintillaControl.Text = File.ReadAllText(fileName, Encoding.Default);
                SetMargins();
                tabControlContents.SelectedIndex = 1;
            }

            if (!isTemplate)
                settings.AddToMRU(fileName); // template is not added to MRU (#4423)
            UpdateMRU();

            UpdateMenuState();

            UpdateFileMonitor();

            // clear the undo buffer of the control
            scintillaControl.UndoRedo.EmptyUndoBuffer();

            return true;
        }

        /// <summary>
        /// Click event handler of the MRUMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MRUMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenMap(sender.ToString(), false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + fileName + ", " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Create a new map or load the template if exists
        /// </summary>
        private void NewMap()
        {
            try
            {
                // opening the template file if exists
                if (File.Exists(Application.StartupPath + "\\templates\\new.map"))
                {
                    if (!OpenMap(Application.StartupPath + "\\templates\\new.map", true))
                        return;
                }
                else
                {
                    if (dirtyFlag || buttonApply.Enabled)
                    {
                        DialogResult result = MessageBox.Show("The map has been edited, would you like to save the changes?", "MapManager",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                            SaveMap();
                        else if (result == DialogResult.Cancel)
                            return;
                    }

                    buttonApply.Enabled = false;

                    MapObjectHolder mapH = MapUtils.CreateMap();
                    mapObj map = mapH;
                    if (map.extent.maxx == -1 && map.extent.minx == -1 && map.extent.maxy == -1 && map.extent.miny == -1)
                        map.setExtent(0, 0, 10, 10);

                    mapControl.Target = mapH;

                    mapH.PropertyChanged += new System.EventHandler(this.MainForm_PropertyChanged);
                    mapH.PropertyChanging += new EventHandler(MainForm_PropertyChanging);
                    mapH.SelectionChanged += new EventHandler(MainForm_SelectionChanged);
                    mapH.ZoomChanged += new MapObjectHolder.ZoomChangedEventHandler(MainForm_ZoomChanged);

                    layerControl_ItemSelect(this, null);
                    layerControl.Target = mapControl.Target;
                    selectListForm.selectList.Target = mapControl.Target;
                    layerControl.InitialExtentSet += new EventHandler(layerControl_InitialExtentSet);
                }

                fileName = "new.map";
                UpdateMenuState();
                UpdateFileMonitor();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating map, " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Click event handler of the newToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMap();
        }

        /// <summary>
        /// Singnals that the initial extent have been set for the map
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void layerControl_InitialExtentSet(object sender, EventArgs e)
        {
            mapControl.StoreInitialExtent();
        }

        /// <summary>
        /// Click event handler of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click event handler of the openToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogMain.Filter = "MapServer mapfiles|*.map|All files|*.*";
                if (openFileDialogMain.ShowDialog() == DialogResult.OK)
                {
                    OpenMap(openFileDialogMain.FileName, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + fileName + ", " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Copy the dependent files with relative locations in the mapfile.
        /// </summary>
        /// <param name="oldFile">The old file name</param>
        /// <param name="newFile">The new file name</param>
        private void CopyDependentFiles(string oldFile, string newFile)
        {
            if (oldFile == "new.map")
                oldFile = Application.StartupPath + "\\templates\\new.map"; // template

            string oldPath = oldFile.Substring(0, oldFile.LastIndexOf('\\'));
            string newPath = newFile.Substring(0, newFile.LastIndexOf('\\'));
            mapObj map = mapControl.Target;
            string file = map.fontset.filename;
            if (file != null && !File.Exists(file) && !File.Exists(Path.Combine(newPath, file)) && File.Exists(Path.Combine(oldPath, file)))
                File.Copy(Path.Combine(oldPath, file), Path.Combine(newPath, file));

            file = map.symbolset.filename;
            if (file != null && !File.Exists(file) && !File.Exists(Path.Combine(newPath, file)) && File.Exists(Path.Combine(oldPath, file)))
            {
                File.Copy(Path.Combine(oldPath, file), Path.Combine(newPath, file));
                // copy the images defined in the symbol file
                for (int i = 0; i < map.symbolset.numsymbols; i++)
                {
                    symbolObj sym = map.symbolset.getSymbol(i);
                    if (sym.type == (int)MS_SYMBOL_TYPE.MS_SYMBOL_PIXMAP && sym.imagepath != null)
                    {
                        file = sym.imagepath;
                        if (!File.Exists(file) && !File.Exists(Path.Combine(newPath, file)) && File.Exists(Path.Combine(oldPath, file)))
                            File.Copy(Path.Combine(oldPath, file), Path.Combine(newPath, file));
                    }
                }
            }
        }

        /// <summary>
        /// Update the version information in the mapfile
        /// </summary>
        /// <param name="map"></param>
        private void UpdateVersionInfo(mapObj map)
        {
            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // update the version information.
            map.setMetaData("mapmanager_version", currentVersion);
        }

        /// <summary>
        /// Saving the map to the specified file.
        /// </summary>
        private void SaveMapAs()
        {
            saveFileDialogMain.Filter = "MapServer mapfiles|*.map|All files|*.*";
            saveFileDialogMain.OverwritePrompt = true;
            saveFileDialogMain.FileName = "";
            if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                if (tabControlContents.SelectedIndex == 1 && !ValidateTextContents())
                    return;
                CopyDependentFiles(fileName, saveFileDialogMain.FileName);
                fileName = saveFileDialogMain.FileName;
                // checking the extension to be added
                if (string.Compare(Path.GetExtension(fileName), ".map", true) != 0)
                    fileName += ".map";
                UpdateVersionInfo((mapObj)mapControl.Target);
                fileSystemWatcher.EnableRaisingEvents = false;
                MapUtils.SaveMap((mapObj)mapControl.Target, fileName);
                //Stephane: set the FSW on again, to avoid event being ignore if file is updated from an external text editor 
                if (tabControlContents.SelectedIndex == 1)
                    LoadTextContents();

                if (fileSystemWatcher.Path != "")
                    fileSystemWatcher.EnableRaisingEvents = true;
                settings.AddToMRU(fileName);
                UpdateMRU();
                SetDirty(false);
                isTemplate = false;
                mapControl.StoreInitialExtent();
                UpdateMenuState();
                UpdateFileMonitor();
            }
        }

        /// <summary>
        /// Saving the map. Displays a SaveFile dialgog for the newly created maps.
        /// </summary>
        public void SaveMap()
        {
            ConfirmChanges();

            try
            {
                if (fileName == null || isTemplate)
                    SaveMapAs();
                else
                {
                    if (tabControlContents.SelectedIndex == 1 && !ValidateTextContents())
                        return;
                    UpdateVersionInfo((mapObj)mapControl.Target);
                    fileSystemWatcher.EnableRaisingEvents = false;
                    MapUtils.SaveMap((mapObj)mapControl.Target, fileName);
                    //Stephane: set the FSW on again, to avoid event being ignore if file is updated from an external text editor 
                    if (tabControlContents.SelectedIndex == 1)
                        LoadTextContents();

                    fileSystemWatcher.EnableRaisingEvents = true;
                    SetDirty(false);
                    isTemplate = false;
                    mapControl.StoreInitialExtent();
                    UpdateMenuState();
                    //Stephane: no longer required 
                    //UpdateFileMonitor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving map, " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saving the map image to the specified file.
        /// </summary>
        private void SaveMapImageAs()
        {
            mapObj map = (mapObj)mapControl.Target;

            saveFileDialogMain.FileName = "";
            saveFileDialogMain.Filter = map.imagetype + " files|*." + map.outputformat.extension;
            saveFileDialogMain.OverwritePrompt = true;
            if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                mapControl.SaveImage(saveFileDialogMain.FileName);
            }
        }

        /// <summary>
        /// Click event handler of the saveAsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmChanges();

            try
            {
                SaveMapAs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving map, " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Click event handler of the saveMapImageToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void saveMapImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //File.WriteAllBytes("test.png", MapUtils.ExportLegend((mapObj)mapControl.Target));
            //return;

            try
            {
                SaveMapImageAs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving map image, " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Click event handler of the saveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMap();
        }

        /// <summary>
        /// Click event handler of the toolStripButtonSave control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the panToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void panToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.InputMode = MapControl.InputModes.Pan;
            UpdateMenuState();
        }

        /// <summary>
        /// Click event handler of the toolStripButtonPan control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonPan_Click(object sender, EventArgs e)
        {
            panToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the zoomToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.InputMode = MapControl.InputModes.ZoomIn;
            UpdateMenuState();
        }

        /// <summary>
        /// Click event handler of the selectItemToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void selectItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.InputMode = MapControl.InputModes.Select;
            UpdateMenuState();
        }

        /// <summary>
        /// Click event handler of the toolStripButtonSelect control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            selectItemToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the selectByRectangleToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void selectByRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.InputMode = MapControl.InputModes.TrackRectangle;
            UpdateMenuState();
        }

        /// <summary>
        /// Click event handler of the toolStripButtonSelectRect control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonSelectRect_Click(object sender, EventArgs e)
        {
            selectByRectangleToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the selectByPolygonToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void selectByPolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.InputMode = MapControl.InputModes.TrackPolygon;
            UpdateMenuState();
        }

        /// <summary>
        /// Click event handler of the toolStripButtonSelectPoly control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonSelectPoly_Click(object sender, EventArgs e)
        {
            selectByPolygonToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the selectByLineToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void selectByLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.InputMode = MapControl.InputModes.TrackLine;
            UpdateMenuState();
        }

        /// <summary>
        /// Click event handler of the toolStripButtonSelectLine control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonSelectLine_Click(object sender, EventArgs e)
        {
            selectByLineToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the toolStripButtonZoomIn control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            zoomToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the zoomOutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.InputMode = MapControl.InputModes.ZoomOut;
            UpdateMenuState();
        }

        /// <summary>
        /// Click event handler of the toolStripButtonZoomOut control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            zoomOutToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the setInitialExtentToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void setInitialExtentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.SetInitialExtent();
        }

        /// <summary>
        /// ZoomChanged event handler of the target object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="zoom">The current zoom width in map coordinates.</param>
        /// <param name="scale">The current map scale denom.</param>
        private void MainForm_ZoomChanged(object sender, double zoom, double scale)
        {
            StringBuilder s = new StringBuilder("Zoom: ");
            s.Append(zoom);
            s.Append(" ");
            s.Append(mapControl.GetUnitName());
            toolStripStatusLabelZoom.Text = s.ToString();
            toolStripStatusLabelScale.Text = "Scale: 1:" + Convert.ToInt64(scale);
        }

        /// <summary>
        /// CursorMove event handler of the map control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="x">The x position of the cursor in map coordinates.</param>
        /// <param name="y">The y position of the cursor in map coordinates.</param>
        private void mapControl_CursorMove(object sender, double x, double y)
        {
            toolStripStatusLabelPos.Text = "(" + x + "  " + y + ")";
        }

        /// <summary>
        /// Click event handler of the toolbarToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolbarToolStripMenuItem.Checked = !toolbarToolStripMenuItem.Checked;
            toolStripMain.Visible = toolbarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Click event handler of the statusbarToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusbarToolStripMenuItem.Checked = !statusbarToolStripMenuItem.Checked;
            statusStripMain.Visible = statusbarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Click event handler of the layerPanelToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void layerPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layerPanelToolStripMenuItem.Checked = !layerPanelToolStripMenuItem.Checked;
            splitContainer.Panel1Collapsed = !layerPanelToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Click event handler of the overviewPanelToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void overviewPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            overviewPanelToolStripMenuItem.Checked = !overviewPanelToolStripMenuItem.Checked;
            splitContainer1.Panel2Collapsed = !overviewPanelToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Click event handler of the redrawMapToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void redrawMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.RefreshView();
        }

        void toolStripButtonHelp_Click(object sender, System.EventArgs e)
        {
            Help.ShowHelp(this, "MapManager.chm", HelpNavigator.TableOfContents);
        }
        /// <summary>
        /// Click event handler of the toolStripButtonRedraw control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonRedraw_Click(object sender, EventArgs e)
        {
            redrawMapToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.HelpRequested += new HelpEventHandler(editor_HelpRequested);
            aboutBox.ShowDialog(this);
        }

        /// <summary>
        /// Click event handler of the optionsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSettingsForm settingsForm = new AppSettingsForm(settings);
            settingsForm.HelpRequested += new HelpEventHandler(editor_HelpRequested);
            if (settingsForm.ShowDialog(this) == DialogResult.OK)
            {
                SaveSettings();
                UpdateAppSettings();
            }
        }

        /// <summary>
        /// The Load event handler of the main form.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadRecentMap();

            buttonApply.Enabled = false;
        }

        /// <summary>
        /// The FormClosing event handler of the main form.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (dirtyFlag || buttonApply.Enabled)
            {
                DialogResult result = MessageBox.Show("The map has been edited, would you like to save the changes?", "MapManager",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    SaveMap();
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            SaveWindowState();
            SaveSettings();
        }

        /// <summary>
        /// The KeyUp event handler of the main form.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void MainForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        /// <summary>
        /// The FormClosed event handler of the main form.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        /// <summary>
        /// Click event handler of the mapPropertiesToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void mapPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapPropertyEditorForm mapPropertyEditor = new MapPropertyEditorForm(mapControl.Target);
            mapPropertyEditor.HelpRequested += new HelpEventHandler(editor_HelpRequested);
            mapPropertyEditor.ShowDialog(this);
        }

        /// <summary>
        /// HelpRequested event handler of the PropertyEditor control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="hlpevent">The parameters of the help event.</param>
        void editor_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (sender.GetType() == typeof(MapPropertyEditor))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\MapPropertyEditor.htm");
            if (sender.GetType() == typeof(LayerPropertyEditor))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\LayerPropertyEditor.htm");
            if (sender.GetType() == typeof(ClassPropertyEditor))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\ClassPropertyEditor.htm");
            if (sender.GetType() == typeof(StylePropertyEditor))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\StylePropertyEditor.htm");
            if (sender.GetType() == typeof(LabelPropertyEditor))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\LabelPropertyEditor.htm");
            if (sender.GetType() == typeof(SelectListForm))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\SelectedFeaturesDialog.htm");
            if (sender.GetType() == typeof(ThemeWizardForm))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\ThemeWizards.htm");
            if (sender.GetType() == typeof(AboutBox))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\AboutBox.htm");
            if (sender.GetType() == typeof(AppSettingsForm))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\AppSettings.htm");
            if (sender.GetType() == typeof(IndividualValuesTheme))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\IndividualValuesThemeWizard.htm");
            if (sender.GetType() == typeof(ProjectionBrowserDialog))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\ProjectionBrowserDialog.htm");
            if (sender.GetType() == typeof(MapControl))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\Mapwindow.htm");
            if (sender.GetType() == typeof(LayerControl))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\Layerpanel.htm");
            if (sender.GetType() == typeof(Menu))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\Menu.htm");
            if (sender.GetType() == typeof(QueryMapPropertyEditor))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\QueryMapPropertyEditor.htm");
            if (sender.GetType() == typeof(ScalebarPropertyEditor))
                Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Topic, "General\\ScalebarPropertyEditor.htm");
            hlpevent.Handled = true;
        }

        /// <summary>
        /// The PropertyChanged event handler of the map object editors.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void MainForm_PropertyChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = false;
            mapControl.RefreshView();
            if (sender.GetType() != typeof(LayerControl))
                layerControl.RefreshView();
            SetDirty(true);

            LoadTextContents();
        }

        /// <summary>
        /// The PropertyChanging event handler of the map object editors.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void MainForm_PropertyChanging(object sender, EventArgs e)
        {
            if (selectedEditor != null)
                buttonApply.Enabled = selectedEditor.IsDirty();
        }

        /// <summary>
        /// The SelectionChanged event handler of the map object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void MainForm_SelectionChanged(object sender, EventArgs e)
        {
            if (!selectedFeaturesToolStripMenuItem.Checked)
                selectedFeaturesToolStripMenuItem_Click(sender, e);
            UpdateMenuState();
        }

        /// <summary>
        /// The EditProperties event handler of the layer control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="target">The object to be edited.</param>
        private void layerControl_EditProperties(object sender, MapObjectHolder target)
        {
            try
            {
                MapPropertyEditorForm mapPropertyEditor = new MapPropertyEditorForm(target);
                mapPropertyEditor.HelpRequested += new HelpEventHandler(editor_HelpRequested);
                mapPropertyEditor.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// The BeforeRefresh event handler of the map control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The object to be edited.</param>
        private void mapControl_BeforeRefresh(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (tileManagerForm != null) tileManagerForm.UpdateTable();
            if (tileManagerForm != null) tileManagerForm.UpdatePreview();
        }

        /// <summary>
        /// The AfterRefresh event handler of the map control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The object to be edited.</param>
        private void mapControl_AfterRefresh(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Confirm canceling or applying the pending changes.
        /// </summary>
        private void ConfirmChanges()
        {
            if (selectedEditor != null && buttonApply.Enabled)
            {
                if (selectedEditor.GetType() == typeof(LayerPropertyEditor))
                {
                    if (MessageBox.Show("Do you wish to Apply the changes you have made to the layer settings?", "MapManager",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        selectedEditor.UpdateValues();
                    else
                        selectedEditor.RefreshView();
                }
                else if (selectedEditor.GetType() == typeof(MapPropertyEditor))
                {
                    if (MessageBox.Show("Do you wish to Apply the changes you have made to the map settings?", "MapManager",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        selectedEditor.UpdateValues();
                    else
                        selectedEditor.RefreshView();
                }
                buttonApply.Enabled = false;
            }
        }

        /// <summary>
        /// The ItemSelect event handler of the layer control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="target">The object which have been selected.</param>
        private void layerControl_ItemSelect(object sender, MapObjectHolder target)
        {
            ConfirmChanges();

            layerPropertyEditor.Visible = false;
            mapPropertyEditor.Visible = false;
            selectedEditor = null;
            if (target == null)
            {

            }
            else if (target.GetType() == typeof(mapObj))
            {
                mapPropertyEditor.Target = target;
                mapPropertyEditor.Visible = true;
                selectedEditor = mapPropertyEditor;
            }
            else if (target.GetType() == typeof(layerObj) ||
                     target.GetType() == typeof(classObj) ||
                     target.GetType() == typeof(styleObj) ||
                     target.GetType() == typeof(labelObj))
            {
                layerPropertyEditor.Target = target;
                layerPropertyEditor.Visible = true;
                selectedEditor = layerPropertyEditor;
            }
        }

        /// <summary>
        /// Event handler to edit a sub-object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="target">The object to be edited.</param>
        void layerPropertyEditor_EditProperties(object sender, MapObjectHolder target)
        {
            try
            {
                MapPropertyEditorForm mapPropertyEditor = new MapPropertyEditorForm(target);
                mapPropertyEditor.HelpRequested += new HelpEventHandler(editor_HelpRequested);
                mapPropertyEditor.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Event handler for the apply button.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void buttonApply_Click(object sender, EventArgs e)
        {
            buttonApply.Enabled = false;

            if (layerPropertyEditor.Visible)
                layerPropertyEditor.UpdateValues();
            else if (mapPropertyEditor.Visible)
                mapPropertyEditor.UpdateValues();
        }

        /// <summary>
        /// Click Event handler for the selectedFeaturesToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void selectedFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedFeaturesToolStripMenuItem.Checked = !selectedFeaturesToolStripMenuItem.Checked;

            if (selectedFeaturesToolStripMenuItem.Checked)
            {
                selectListForm.Location = new Point(this.Left + this.Width - selectListForm.Width, this.Top + 40);
                selectListForm.Show(this);
            }
            else
                selectListForm.Hide();
        }

        /// <summary>
        /// Click Event handler for the styleLibraryToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void styleLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StyleLibraryForm styleLibraryForm = new StyleLibraryForm(mapControl.Target);
            styleLibraryForm.ShowDialog(this);
            if (styleLibraryForm.SymbolsetSaved && mapControl.Target != null)
            {
                // need to reload the in memory mapfile with the new symbolset
                mapObj map = mapControl.Target;
                string txt = ((mapObj)mapControl.Target).convertToString();
                map = mapscript.msLoadMapFromString(txt, null);
            }
        }

        /// <summary>
        /// VisibleChanged Event handler for the selectListForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        void selectListForm_VisibleChanged(object sender, EventArgs e)
        {
            selectedFeaturesToolStripMenuItem.Checked = selectListForm.Visible;
            if (!selectListForm.Visible)
                mapControl.ClearSelection();
        }

        /// <summary>
        /// Click Event handler for the clearSelectionToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapControl.ClearSelection();
        }

        /// <summary>
        /// Click Event handler for the helpContentsToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void helpContentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Display the index for the help file.
            Help.ShowHelp(this, "MapManager.chm", HelpNavigator.TableOfContents);
        }

        /// <summary>
        /// Click Event handler for the helpIndexToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void helpIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Display the index for the help file.
            Help.ShowHelpIndex(this, "MapManager.chm");
        }

        /// <summary>
        /// Click Event handler for the searchToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Display help using the provided keyword.
            Help.ShowHelp(this, "MapManager.chm", HelpNavigator.Find, "");
        }

        /// <summary>
        /// Click Event handler for the changeViewToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void changeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeViewForm form = new ChangeViewForm();
            form.Target = mapControl.Target;
            form.ShowDialog(this);
        }

        /// <summary>
        /// Changed Event handler for the fileSystemWatcher object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            fileHasChanged = true;
        }

        /// <summary>
        /// Raised when the MainForm gots focus
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (fileHasChanged)
            {
                fileHasChanged = false;

                StringBuilder msg = new StringBuilder();
                msg.Append("The file ");
                msg.Append(fileName);
                msg.Append(" has been modified outside of MapManager. Do you want to reload it");
                if (dirtyFlag)
                    msg.Append(" and loose the changes made in MapManager");
                msg.Append("?");

                if (MessageBox.Show(msg.ToString(), "MapManager",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dirtyFlag = false;
                    buttonApply.Enabled = false;
                    int pos = scintillaControl.CurrentPos;
                    OpenMap(fileName, isTemplate);
                    //scintillaControl.CurrentPos = pos;
                }
            }
        }

        /// <summary>
        /// Clic Event handler for the openFileExternalToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void openFileExternalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ScriptConsoleForm form = new ScriptConsoleForm(mapControl.Target);
            //form.Show(this);
            //return;

            if (dirtyFlag && MessageBox.Show("Would you like to save the changes before opening the file in the external editor?", "MapManager",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveMap();
            }

            try
            {
                if (settings.TextEditor != null && File.Exists(settings.TextEditor))
                    Process.Start(settings.TextEditor, fileName);
                else
                    Process.Start(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file, " + ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Set margins of the text editor according to the contents
        /// </summary>
        private void SetMargins()
        {
            // adjust margin width (text width + padding)
            scintillaControl.Margins.Margin0.Width = scintillaControl.NativeInterface.TextWidth(33, scintillaControl.Lines.Count.ToString()) + 6;
            scintillaControl.Margins.Margin1.Width = 0;
            scintillaControl.Margins.Margin2.Width = 20;
        }

        /// <summary>
        /// Loading the text contents based on the current map configuration
        /// </summary>
        private void LoadTextContents()
        {
            // saving the map into a temporary file
            if ((mapObj)mapControl.Target != null)
            {
                string txt = ((mapObj)mapControl.Target).convertToString();

                if (scintillaControl.Text != txt)
                {
                    scintillaControl.Text = txt;
                    SetMargins();

                    if (caretPos > 0)
                    {
                        scintillaControl.Selection.Start = caretPos;
                        scintillaControl.Caret.Position = caretPos;
                    }
                    if (scrollPos > 0)
                        scintillaControl.Scrolling.ScrollBy(0, scrollPos);
                    textChanged = false;
                }
            }
        }

        /// <summary>
        /// Applying the text contents based on the current map configuration
        /// </summary>
        private void ApplyTextContents()
        {
            mapObj map = mapscript.msLoadMapFromString(scintillaControl.Text, null);
            MapObjectHolder mapH = new MapObjectHolder(map, null);

            if (MapUtils.RenameDuplicatedNames(map))
            {
                MessageBox.Show("Duplicated layer names detected in this mapfile, which is not supported by IntraMaps. The layers will be renamed to unique names.", "MapManager",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetDirty(true);
            }

            mapH.PropertyChanged += new System.EventHandler(this.MainForm_PropertyChanged);
            mapH.PropertyChanging += new EventHandler(MainForm_PropertyChanging);
            mapH.SelectionChanged += new EventHandler(MainForm_SelectionChanged);
            mapH.ZoomChanged += new MapObjectHolder.ZoomChangedEventHandler(MainForm_ZoomChanged);

            mapControl.Target = mapH;
            layerControl_ItemSelect(this, null);
            layerControl.Target = mapControl.Target;
            selectListForm.selectList.Target = mapControl.Target;
        }

        /// <summary>
        /// Make sure the changes in the text tab can be parsed correctly
        /// </summary>
        /// <returns>True if the text is correct</returns>
        private bool ValidateTextContents()
        {
            if (textChanged || mapControl.Target == null)
            {
                try
                {
                    ApplyTextContents();
                    textChanged = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ". Please correct the issue in the text tab.", "MapManager",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);

                    mapControl.Target = null;
                    layerControl_ItemSelect(this, null);
                    layerControl.Target = null;
                    selectListForm.selectList.Target = null;

                    tabControlContents.SelectedIndex = 1;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the tabControlContents control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void tabControlContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlContents.SelectedIndex == 0)
            {
                scrollPos = scintillaControl.Lines.FirstVisible.Number;
                caretPos = scintillaControl.Caret.Position;

                ValidateTextContents();
            }
            else if (tabControlContents.SelectedIndex == 1)
            {
                ConfirmChanges();

                LoadTextContents();
                scintillaControl.Focus();
                textChanged = false;
            }

            UpdateMenuState();
        }

        /// <summary>
        /// TextInserted event handler of the scintillaControl control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void scintillaControl_TextInserted(object sender, ScintillaNet.TextModifiedEventArgs e)
        {
            SetMargins();
            textChanged = true;
            SetDirty(true);
        }

        /// <summary>
        /// TextDeleted event handler of the scintillaControl control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void scintillaControl_TextDeleted(object sender, ScintillaNet.TextModifiedEventArgs e)
        {
            SetMargins();
            textChanged = true;
            SetDirty(true);
        }

        /// <summary>
        /// GoToLayerText event handler of the layerControl control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void layerControl_GoToLayerText(object sender, layerObj layer, int classindex)
        {
            if (layer == null)
                return;

            scintillaControl.Text = "";
            scrollPos = 0;
            caretPos = 0;
            tabControlContents.SelectedIndex = 1;
            int pos = 0;
            int layerindex = layer.index;

            using (StringReader reader = new StringReader(scintillaControl.Text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim() == "LAYER")
                    {
                        if (layerindex < 0) // class not found
                            break;
                        --layerindex;
                    }
                    else if (layerindex < 0 && classindex >= 0 && line.Trim() == "CLASS")
                    {
                        --classindex;
                    }
                    if (layerindex < 0 && classindex < 0)
                        break;

                    ++pos;
                }
            }
            scrollPos = pos;
            scintillaControl.Scrolling.ScrollBy(0, scrollPos);
        }

        /// <summary>
        /// ZoomChanged event handler of the scintillaControl control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void scintillaControl_ZoomChanged(object sender, EventArgs e)
        {
            // adjust the margins when the zoom has changed
            SetMargins();
        }

        /// <summary>
        /// Click event handler of the findToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlContents.SelectedIndex = 1;
            scintillaControl.FindReplace.ShowFind();
        }

        /// <summary>
        /// Click event handler of the replaceToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlContents.SelectedIndex = 1;
            scintillaControl.FindReplace.ShowReplace();
        }

        /// <summary>
        /// Loads the TileManagerForm
        /// </summary>
        private void tileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tileManagerForm != null) tileManagerForm.Close();
            if (fileName != "new.map")
            {
                tileManagerForm = new TileManagerForm(this, fileName, mapControl);
                tileManagerForm.Owner = this;
                tileManagerForm.Show();
            }
        }

        /// <summary>
        /// Click event handler of the replaceToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkMapFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapfileConverter c = new MapfileConverter();
            try
            {
                c.Parse(scintillaControl.Text, true);
                MapObjectHolder mapH;
                mapObj map;
                if (c.HasToConvert())
                {
                    MapFileConvertForm form = new MapFileConvertForm(c.GetChangeLog());

                    if (form.ShowDialog(this) == DialogResult.Yes)
                    {
                        scintillaControl.Text = c.GetMapFile();
                    }
                }
                else
                {
                    MessageBox.Show("Map file check was successful.",
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}