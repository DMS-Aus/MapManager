using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a Form for changing the map view.
    /// </summary>
    public partial class StyleLibraryForm : Form
    {
        private bool propertyChanged;
        private MapObjectHolder selectedItem;
        private int scrollPos;
        private int caretPos;
        private bool styleLibraryChanged;
        private bool textChanged;
        private bool symbolsetChanged;
        private bool symbolsetSaved;
        private bool fontsetChanged;
        private bool fontsetSaved;
        private bool isTextValidating;
        private delegate void RefreshDelegate();
        private RefreshDelegate refresh;
        private bool isInit;
        private bool isInitStyleLibText;
        MapObjectHolder target;
        mapObj map;
        
        /// <summary>
        /// Constructs a new ChangeViewForm class.
        /// </summary>
        public StyleLibraryForm(MapObjectHolder target)
        {
            InitializeComponent();
            isTextValidating = false;
            refresh = new RefreshDelegate(this.Refresh);
            isInit = true;
            isInitStyleLibText = true;
            // saving the reference to the base mapObj
            // In certain cases we require to reload the modified symbolset on the base mapfile too.
            this.target = target;
            this.map = target;
        }

        /// <summary>
        /// Click event handler of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Set the modified flag for the style library (mmstyles.map)
        /// </summary>
        /// <param name="bModify">modified flag</param>
        private void SetStyleLibModified(bool bModify)
        {
            styleLibraryChanged = bModify;
            buttonSave.Enabled = bModify;
            
            if (bModify)
            {
                if (!tabPage1.Text.EndsWith("*"))
                    tabPage1.Text += "*";
                if (!tabPage2.Text.EndsWith("*"))
                    tabPage2.Text += "*";
            }
            else
            {
                tabPage1.Text = tabPage1.Text.Replace("*", "");
                tabPage2.Text = tabPage2.Text.Replace("*", "");
            }
        }

        /// <summary>
        /// Set the modified flag for the symbolset (symbols.sym)
        /// </summary>
        /// <param name="bModify">modified flag</param>
        private void SetSymbolsetModified(bool bModify)
        {
            symbolsetChanged = bModify;
            buttonSave.Enabled = bModify;
            tabPage3.Text = tabPage3.Text.Replace("*", "");

            if (bModify)
            {
                if (!tabPage3.Text.EndsWith("*"))
                    tabPage3.Text += "*";
            }
            else
            {
                tabPage3.Text = tabPage3.Text.Replace("*", "");
            }
        }

        /// <summary>
        /// Set the modified flag for the fontset (font.list)
        /// </summary>
        /// <param name="bModify">modified flag</param>
        private void SetFontsetModified(bool bModify)
        {
            fontsetChanged = bModify;
            buttonSave.Enabled = bModify;
            tabPage4.Text = tabPage4.Text.Replace("*", "");

            if (bModify)
            {
                if (!tabPage4.Text.EndsWith("*"))
                    tabPage4.Text += "*";
            }
            else
            {
                tabPage4.Text = tabPage4.Text.Replace("*", "");
            }
        }

        /// <summary>
        /// KeyDown event handler of the StyleLibraryForm control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void StyleLibraryForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Returns true is the symbolset has been saved
        /// </summary>
        public bool SymbolsetSaved
        {
            get
            {
                return symbolsetSaved;
            }
        }

        /// <summary>
        /// Returns true is the fontset has been saved
        /// </summary>
        public bool FontsetSaved
        {
            get
            {
                return fontsetSaved;
            }
        }

        /// <summary>
        /// Load event handler of the StyleLibraryForm control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void StyleLibraryForm_Load(object sender, EventArgs e)
        {
            textChanged = false;
            symbolsetChanged = false;
            symbolsetSaved = false;
            fontsetChanged = false;
            fontsetSaved = false;
            styleLibraryChanged = false;
            // load scintilla config from file
            scintillaControl.ConfigurationManager.Language = "user";
            ScintillaNet.Configuration.Configuration config = new ScintillaNet.Configuration.Configuration(Environment.CurrentDirectory + "\\MapfileConfig.xml", "user", true);
            scintillaControl.ConfigurationManager.Configure(config);

            scintillaControlSymbolset.ConfigurationManager.Language = "user";
            scintillaControlSymbolset.ConfigurationManager.Configure(config);

            LoadSymbolset();

            LoadFontset();

            timerRefresh.Enabled = true;
            buttonSave.Enabled = false;
        }

        /// <summary>
        /// PropertyChanged Event handler for the style librabry base object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        void Styles_PropertyChanged(object sender, EventArgs e)
        {
            SetStyleLibModified(true);
        }

        /// <summary>
        /// FormClosing Event handler for the StyleLibraryForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void StyleLibraryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (styleLibraryChanged)
            {
                DialogResult result = MessageBox.Show("The Style Library has been edited, would you like to save the changes?", "MapManager",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                try
                {
                    if (result == DialogResult.Yes)
                        SaveAll();
                    else if (result == DialogResult.No)
                        StyleLibrary.Refresh(); // reload
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Update the Sample image according to the selected element
        /// </summary>
        /// <param name="target">Selected element</param>
        private void UpdateSample(MapObjectHolder target)
        {
            layerObj layer = null;
            mapObj map = null;
            classObj classobj = null;
            if (target.GetType() == typeof(layerObj))
            {
                layer = target;
                map = layer.map;
                classobj = layer.getClass(0);
            }
            else if (target.GetType() == typeof(classObj))
            {
                classobj = target;
                if (classobj != null)
                    layer = classobj.layer;
                if (layer != null)
                    map = layer.map;
            }
            if (map == null || layer == null || classobj == null)
            {
                pictureBoxSample.Image = null;
                return;
            }

            styleObj style = classobj.getStyle(0);
            double size = style.size;

            // collect all fonts specified in the fontset file
            Hashtable fonts = new Hashtable();
            string key = null;
            while ((key = this.map.fontset.fonts.nextKey(key)) != null)
                fonts.Add(key, key);
            textBoxInfo.Text = "";
            if (style.symbol >= 0)
            {
                string font = ((mapObj)StyleLibrary.Styles).symbolset.getSymbol(style.symbol).font;
                if (font != null && !fonts.ContainsKey(font))
                {
                    textBoxInfo.Text = "WARNING: The fontset of the map doesn't contain " + font + " font. This symbol will not be selectable on the map.";
                }
            }
                
            if (layer.type == MS_LAYER_TYPE.MS_LAYER_POINT)
            {
                // apply magnification for point styles
                style.size = size * trackBarMagnify.Value / 4;
            }

            try
            {
                using (classObj def_class = new classObj(null)) // for drawing legend image
                {
                    using (imageObj image2 = def_class.createLegendIcon(map, layer,
                        pictureBoxSample.Width, pictureBoxSample.Height))
                    {
                        classobj.drawLegendIcon(map, layer,
                            pictureBoxSample.Width, pictureBoxSample.Height, image2, 5, 5);
                        byte[] img = image2.getBytes();
                        using (MemoryStream ms = new MemoryStream(img))
                        {
                            pictureBoxSample.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            finally
            {
                style.size = size;
            }
        }

        /// <summary>
        /// ItemSelect Event handler for the layerControlStyles object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void layerControlStyles_ItemSelect(object sender, MapObjectHolder target)
        {
            selectedItem = target;
            UpdateSample(target);
        }

        /// <summary>
        /// EditProperties Event handler for the layerControlStyles object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void layerControlStyles_EditProperties(object sender, MapObjectHolder target)
        {
            try
            {
                MapPropertyEditorForm mapPropertyEditor;
                if (target.GetType() == typeof(classObj))
                {
                    classObj classobj = target;

                    if (classobj.numstyles <= 0)
                    {
                        // adding an empty style to this class
                        styleObj style = new styleObj(classobj);
                    }

                    mapPropertyEditor = new MapPropertyEditorForm(
                        new MapObjectHolder(classobj.getStyle(0), target), new StyleLibraryPropertyEditor());
                }
                else if (target.GetType() == typeof(styleObj))
                    mapPropertyEditor = new MapPropertyEditorForm(target, new StyleLibraryPropertyEditor());
                else
                    return;
                propertyChanged = false;
                target.PropertyChanged += new EventHandler(target_PropertyChanged);
                mapPropertyEditor.ShowDialog(this);
                if (propertyChanged)
                    layerControlStyles.RefreshView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// PropertyChanged event handler for the edited target.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        void target_PropertyChanged(object sender, EventArgs e)
        {
            propertyChanged = true;
        }

        /// <summary>
        /// Saving both the Style Library and the Symbolset (if changed)
        /// </summary>
        private void SaveAll()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                StyleLibrary.Save();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// Click Event handler for the buttonSave object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedIndex == 3)
                {
                    SaveFontset();
                }
                else
                {
                    SaveAll();

                    MessageBox.Show("Style library was saved successfully!", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetStyleLibModified(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Scroll Event handler for the trackBarMagnify object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void trackBarMagnify_Scroll(object sender, EventArgs e)
        {
            labelPercent.Text = Convert.ToString(trackBarMagnify.Value * 25) + "%";
            if (selectedItem != null)
                UpdateSample(selectedItem);
        }

        /// <summary>
        /// Make sure the changes in the text tab can be parsed correctly
        /// </summary>
        /// <returns>True if the text is correct</returns>
        private bool ValidateTextContents()
        {
            if (textChanged || StyleLibrary.Styles == null)
            {
                try
                {
                    isTextValidating = true;
                    StyleLibrary.ApplyTextContents(scintillaControl.Text);
                    textChanged = false;
                    SetStyleLibModified(true);
                    timerRefresh.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ". Please correct the issue in the text tab.", "MapManager",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);

                    tabControl.SelectedIndex = 1;
                    return false;
                }
                finally
                {
                    isTextValidating = false;
                }
            }
            return true;
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
        /// Set margins of the text editor according to the contents
        /// </summary>
        private void SetMarginsSymbolset()
        {
            // adjust margin width (text width + padding)
            scintillaControlSymbolset.Margins.Margin0.Width = scintillaControlSymbolset.NativeInterface.TextWidth(33, scintillaControlSymbolset.Lines.Count.ToString()) + 6;
            scintillaControlSymbolset.Margins.Margin1.Width = 0;
            scintillaControlSymbolset.Margins.Margin2.Width = 20;
        }
        
        /// <summary>
        /// SelectedIndexChanged Event handler for the tabControl object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isTextValidating)
                return;

            buttonAddFont.Visible = false;
            buttonRemoveFont.Visible = false;
            
            if (tabControl.SelectedIndex == 0)
            {
                scrollPos = scintillaControl.Lines.FirstVisible.Number;
                caretPos = scintillaControl.Caret.Position;

                ValidateTextContents();
                buttonSave.Enabled = styleLibraryChanged;
            }
            else if (tabControl.SelectedIndex == 1)
            {
                string txt = StyleLibrary.LoadTextContents();
                if (scintillaControl.Text != txt)
                {
                    isInitStyleLibText = true;
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
                    isInitStyleLibText = false;
                }
                scintillaControl.Focus();
                textChanged = false;
                buttonSave.Enabled = styleLibraryChanged;
            }
            else if (tabControl.SelectedIndex == 2)
            {
                buttonSave.Enabled = false;
            }
            else if (tabControl.SelectedIndex == 3)
            {
                buttonSave.Enabled = false;
                buttonAddFont.Visible = true;
                buttonRemoveFont.Visible = true;
                buttonRemoveFont.Enabled = (listViewFonts.SelectedItems.Count > 0);
            }

            if (tabControl.SelectedIndex != 3 && fontsetChanged)
            {
                if (MessageBox.Show("Do you wish to save the modifications of the symbolset?", "MapManager", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveFontset();
                }
            }
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
            if (!isInitStyleLibText)
                SetStyleLibModified(true);
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
            if (!isInitStyleLibText)
                SetStyleLibModified(true);
        }

        /// <summary>
        /// TextInserted event handler of the scintillaControlSymbolset control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void scintillaControlSymbolset_TextInserted(object sender, ScintillaNet.TextModifiedEventArgs e)
        {
            SetMarginsSymbolset();
            SetSymbolsetModified(true);
        }

        /// <summary>
        /// TextDeleted event handler of the scintillaControlSymbolset control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void scintillaControlSymbolset_TextDeleted(object sender, ScintillaNet.TextModifiedEventArgs e)
        {
            SetMarginsSymbolset();
            SetSymbolsetModified(true);
        }

        /// <summary>
        /// GoToLayerText event handler of the layerControlStyles control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void layerControlStyles_GoToLayerText(object sender, layerObj layer, int classindex)
        {
            if (layer == null)
                return;

            scintillaControl.Text = "";
            scrollPos = 0;
            caretPos = 0;
            tabControl.SelectedIndex = 1;
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
        /// Loading the symbolset from file
        /// </summary>
        private void LoadSymbolset()
        {
            if (StyleLibrary.SymbolsetFileName != null)
                scintillaControlSymbolset.Text = File.ReadAllText(StyleLibrary.SymbolsetFileName);
            else
                scintillaControlSymbolset.Text = "";

            SetMarginsSymbolset();
            SetSymbolsetModified(false);
        }

        /// <summary>
        /// Loading the fontset from file
        /// </summary>
        private void LoadFontset()
        {
            listViewFonts.Items.Clear();
            if (StyleLibrary.FontsetFileName != null)
            {
                using (StringReader r = new StringReader(File.ReadAllText(StyleLibrary.FontsetFileName)))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        string[] vals = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (vals.Length >= 2)
                        {
                            ListViewItem item = new ListViewItem(vals[0]);
                            item.SubItems.Add(vals[1]);
                            listViewFonts.Items.Add(item);
                        }
                    }
                }
            }

            SetFontsetModified(false);
        }

        /// <summary>
        /// Saving the symbolset to file
        /// </summary>
        private void SaveSymbolset()
        {
            if (StyleLibrary.SymbolsetFileName != null && symbolsetChanged)
            {
                File.WriteAllText(StyleLibrary.SymbolsetFileName, scintillaControlSymbolset.Text);
                // reconstruct the in memory style lib with this new symbolset (without writing a file)
                string text = StyleLibrary.LoadTextContents();
                StyleLibrary.ApplyTextContents(text);
                layerControlStyles.Target = StyleLibrary.Styles;
                SetSymbolsetModified(false);
            }
        }

        /// <summary>
        /// Saving the fontset to file
        /// </summary>
        private void SaveFontset()
        {
            if (StyleLibrary.FontsetFileName != null && fontsetChanged)
            {
                StringBuilder fontsetContents = new StringBuilder();
                foreach (ListViewItem item in listViewFonts.Items)
                    fontsetContents.AppendLine(String.Format("{0}\t{1}", item.Text, item.SubItems[1].Text));      
                File.WriteAllText(StyleLibrary.FontsetFileName, fontsetContents.ToString());
                // reconstruct the in memory style lib with this new fontset (without writing a file)
                string text = StyleLibrary.LoadTextContents();
                StyleLibrary.ApplyTextContents(text);
                layerControlStyles.Target = StyleLibrary.Styles;
                SetFontsetModified(false);
            }
        }

        /// <summary>
        /// Refresh event handler of the timerRefresh control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void timerRefresh_Tick(object sender, EventArgs e)
        {         
            timerRefresh.Enabled = false;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                panelRefresh.Visible = true;
                Application.DoEvents();
                if (isInit)
                {
                    StyleLibrary.Refresh();
                    StyleLibrary.Styles.PropertyChanged += Styles_PropertyChanged;
                    isInit = false;
                }
                layerControlStyles.Target = StyleLibrary.Styles;

                // clear the undo buffer of the control
                scintillaControl.UndoRedo.EmptyUndoBuffer();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                panelRefresh.Visible = false;
            }
        }

        /// <summary>
        /// Validating event handler of the scintillaControlSymbolset control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void scintillaControlSymbolset_Validating(object sender, CancelEventArgs e)
        {
            if (symbolsetChanged)
            {
                // validating
                string fileName = Path.GetTempFileName();
                try
                {
                    File.WriteAllText(fileName, scintillaControlSymbolset.Text);
                    // test whether we can parse the symbolset
                    symbolSetObj s = new symbolSetObj(fileName);
                    // make sure we have all symbol references
                    Hashtable symbolNames = new Hashtable();
                    for (int i = 0; i < s.numsymbols; i++)
                    {
                        string symbolName = s.getSymbol(i).name;
                        if (symbolName != null && !symbolNames.ContainsKey(symbolName))
                            symbolNames.Add(symbolName, symbolName);
                    }
                    mapObj map = StyleLibrary.Styles;
                    s = map.symbolset;
                    for (int i = 0; i < s.numsymbols; i++)
                    {
                        symbolObj sym = s.getSymbol(i);
                        string symbolName = sym.name;
                        if (symbolName != null && sym.inmapfile == mapscript.MS_TRUE && !symbolNames.ContainsKey(symbolName))
                            symbolNames.Add(symbolName, symbolName);
                    }

                    for (int i = 0; i < map.numlayers; i++)
                    {
                        layerObj layer = map.getLayer(i);
                        for (int j = 0; j < layer.numclasses; j++)
                        {
                            classObj classobj = layer.getClass(j);
                            for (int k = 0; k < classobj.numstyles; k++)
                            {
                                string symbolName = classobj.getStyle(k).symbolname;
                                if (symbolName != null && !symbolNames.ContainsKey(symbolName))
                                    throw new Exception("Symbol name '" + symbolName + "' is missing from the symbolset file!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.Message + "\n\rPress OK to correct the errors or Cancel to ignore the changes!", "MapManager", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                        e.Cancel = true;
                    else
                    {
                        LoadSymbolset();
                    }

                    return;
                }
                finally
                {
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                }
                
                // validation ok, ask whether to save or not
                DialogResult res = MessageBox.Show("Do you wish to save the modifications of the symbolset?", "MapManager", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SaveSymbolset();
                    // the symbolset has changed, need to reload view
                    timerRefresh.Enabled = true;
                }
                else if (res == DialogResult.No)
                {
                    LoadSymbolset();
                }
                else if (res == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// AfterRefresh event handler of the layerControlStyles control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void layerControlStyles_AfterRefresh(object sender, EventArgs e)
        {
            panelRefresh.Visible = false;
        }

        /// <summary>
        /// BeforeRefresh event handler of the layerControlStyles control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void layerControlStyles_BeforeRefresh(object sender, EventArgs e)
        {
            panelRefresh.Visible = true;
            Application.DoEvents();
        }

        /// <summary>
        /// Click event handler of the buttonAddFont control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void buttonAddFont_Click(object sender, EventArgs e)
        {
            AddFontForm form = new AddFontForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(form.FontName);
                item.SubItems.Add(form.FontFile);
                listViewFonts.Items.Add(item);
                SetFontsetModified(true);
            }
        }

        /// <summary>
        /// Click event handler of the buttonRemoveFont control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void buttonRemoveFont_Click(object sender, EventArgs e)
        {
            if (listViewFonts.SelectedItems.Count > 0)
            {
                // check whether we have symbol references to this font
                string fontName = listViewFonts.SelectedItems[0].Text;
                if (map != null)
                {
                    symbolSetObj symbolset = map.symbolset;
                    for (int i = 0; i < symbolset.numsymbols; i++)
                    {
                        symbolObj sym = map.symbolset.getSymbol(i);
                        if (String.Compare(sym.font, fontName, true) == 0)
                        {
                            MessageBox.Show("Unable to remove the selected font! The map file contains symbol referring to this font: " + sym.name, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                if (StyleLibrary.Styles != null)
                {
                    symbolSetObj symbolset = ((mapObj)StyleLibrary.Styles).symbolset;
                    for (int i = 0; i < symbolset.numsymbols; i++)
                    {
                        symbolObj sym = map.symbolset.getSymbol(i);
                        if (String.Compare(sym.font, fontName, true) == 0)
                        {
                            MessageBox.Show("Unable to remove the selected font! The style library contains symbol referring to this font: " + sym.name, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                if (MessageBox.Show("Are you sure you want to delete the selected font?",
                    "MapManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                listViewFonts.Items.Remove(listViewFonts.SelectedItems[0]);
                SetFontsetModified(true);
            }
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the listViewFonts control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="layer">The selected layer.</param>
        private void listViewFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemoveFont.Enabled = true;
        }
    }
}