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
using System.Runtime.InteropServices;

namespace DMS.MapManager
{
    public partial class TileManagerForm : Form
    {
        // main form reference
        MainForm mainForm;

        // is form loaded
        bool loaded = false;

        // tile settings loaded
        public TileSettings loadedSettings = null;

        // map variables
        string mapfilepath;
        MapControl mapControl;
        mapObj map = null;

        // config manager text
        TileManager.BaseLayerTextDialog baseLayerTextDialog;
        string configManager = "";

        // number of tiles to generate
        int numTiles;
        int numTestTiles;

        // stats table variables
        int statsTPM = 0;
        int statsTileSize = 0;
        double statsNumber = 0.001425; // percent of tiles to test with: 0.001425 is roughly 500 random tiles from up to level 10
        
        
        // google parameters
        int z0;
        double googleMinX = -20037508.34;
        double googleMinY = -20037508.34;
        double googleMaxX = 20037508.34;
        double googleMaxY = 20037508.34;
        double worldSnapMinX;
        double worldSnapMinY;
        double worldSnapMaxX;
        double worldSnapMaxY;

        public TileManagerForm(MainForm mainFormRef, string mapfile, MapControl mapobject)
        {
            InitializeComponent();

            // references from main form
            mainForm = mainFormRef;
            mapfilepath = mapfile;
            mapControl = mapobject;
            map = mapControl.map;            
        }

        private void TileManagerForm_Load(object sender, EventArgs e)
        {            
            try
            {
                // scan for active TileGenerator instances
                timerProcessScan.Interval = 1000;
                timerProcessScan.Start();

                // remove scalebar from map if it is enabled warning
                if (map.scalebar.status > 0)
                {
                    MessageBox.Show("The Scalebar should be turned off else it may appear on every tile generated", "Scalebar Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // set base statisics
                txtTPM.Text = statsTPM.ToString();
                txtTileSize.Text = statsTileSize.ToString();

                // set custom extent
                cmbSnapWorld.Items.Add("Yes");
                cmbSnapWorld.Items.Add("No");
                cmbSnapWorld.SelectedIndex = 1;

                // set number of available processers
                for (int i = 1; i < Environment.ProcessorCount + 1; i++)
                {
                    cmbProcesses.Items.Add(i);
                }
                {
                    int defaultProcesses = (int)Math.Round(((double)cmbProcesses.Items.Count * 0.75), 0) - 1;
                    if (defaultProcesses > 0)
                    {
                        cmbProcesses.SelectedIndex = defaultProcesses;
                    }
                    else
                    {
                        cmbProcesses.SelectedIndex = 0;
                    }
                }

                // set number of levels
                int zoomSelected = 1;
                for (int i = 1; i < 22; i++)
                {
                    cmbLevels.Items.Add(i);
                    cmbNumLevels.Items.Add(i);
                    if ((map.scaledenom / Math.Pow(2, i) < 250) && zoomSelected == 1)
                    {
                        zoomSelected = i;
                    }
                }
                cmbLevels.SelectedIndex = zoomSelected;

                // set image sizes
                cmbImageSize.Items.Add("256");
                cmbImageSize.SelectedIndex = 0;

                // set image buffer
                cmbImageBuffer.Items.Add("Off");
                cmbImageBuffer.Items.Add("25");
                cmbImageBuffer.Items.Add("50");
                cmbImageBuffer.Items.Add("100");
                cmbImageBuffer.SelectedIndex = 2;

                // set image format
                cmbImageFormat.Items.Add("png");
                cmbImageFormat.Items.Add("jpg");
                cmbImageFormat.SelectedIndex = 0;

                // set overwrite
                cmbOverwrite.Items.Add("True");
                cmbOverwrite.Items.Add("False");
                cmbOverwrite.SelectedIndex = 0;

                // load pre-configred settings if available
                cmbPreConfig.Items.Add("< unsaved settings >");
                foreach (TileSettings tileSettings in mainForm.settings.tileSettingsArray)
                {
                    cmbPreConfig.Items.Add(tileSettings.settingsName);
                }
                btnDeleteConfig.Enabled = false;
                cmbPreConfig.SelectedIndex = 0;

                // load from metadata if available
                string key = map.getFirstMetaDataKey();
                while (key != null)
                {
                    switch (key)
                    {
                        case "TileManager_Config":
                            int i = 0;
                            foreach (TileSettings tileSettings in mainForm.settings.tileSettingsArray)
                            {
                                i++;
                                if (tileSettings.settingsName == map.getMetaData("TileManager_Config")) cmbPreConfig.SelectedIndex = i;
                            }
                            break;
                    }
                    key = map.getNextMetaDataKey(key);
                }

                // set up datagrid view
                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
                cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgLevels.ColumnHeadersDefaultCellStyle = cellStyle;
                dgLevels.DefaultCellStyle = cellStyle;

                // draw lines correctly on top of preview
                lineGoogleH.Visible = false;
                lineGoogleV.Visible = false;
                shapeContainer1.BringToFront();

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("TileManager form could not be loaded: " + Environment.NewLine + ex.ToString(), "Form Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdatePreview()
        {
            try
            {
                if (loadedSettings == null && cmbSnapWorld.SelectedIndex == 0) CalculateGoogleExtents();

                // save current map metadata
                int height = map.height;
                int width = map.width;
                double minx = map.extent.minx;
                double miny = map.extent.miny;
                double maxx = map.extent.maxx;
                double maxy = map.extent.maxy;

                // create preview
                map.setSize(256, 256);

                if (loadedSettings == null)
                {
                    if (cmbSnapWorld.SelectedIndex == 0)
                    {
                        double tempGap;
                        if (Math.Abs(worldSnapMinX - worldSnapMaxX) > Math.Abs(worldSnapMinY - worldSnapMaxY))
                        {
                            tempGap = Math.Abs(worldSnapMinX - worldSnapMaxX);
                        }
                        else
                        {
                            tempGap = Math.Abs(worldSnapMinY - worldSnapMaxY);
                        }
                        map.setExtent(worldSnapMinX, worldSnapMinY, worldSnapMinX + tempGap, worldSnapMinY + tempGap);
                    }
                    else
                    {
                        double tempGap;
                        if (Math.Abs(minx - maxx) > Math.Abs(miny - maxy))
                        {
                            tempGap = Math.Abs(minx - maxx);
                        }
                        else
                        {
                            tempGap = Math.Abs(miny -maxy);
                        }
                        map.setExtent(minx, miny, minx + tempGap, miny + tempGap);
                    }
                }
                else
                {
                    map.setExtent(
                            loadedSettings.startX,
                            loadedSettings.startY,
                            loadedSettings.startX + loadedSettings.initTileGap,
                            loadedSettings.startY + loadedSettings.initTileGap);
                }

                using (imageObj image = map.draw())
                {
                    Image mapImage;

                    byte[] img = image.getBytes();
                    using (MemoryStream ms = new MemoryStream(img))
                    {
                        mapImage = Image.FromStream(ms);
                        ms.Flush();

                        imagePreview.Image = mapImage;
                    }
                }

                // restore map metadata
                map.setSize(width, height);
                map.setExtent(minx, miny, maxx, maxy);

                // redraw line
                shapeContainer1.Invalidate();
                if (cmbSnapWorld.SelectedIndex   == 0)
                {
                    lineGoogleV.Visible = true;
                    lineGoogleH.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating preview image: " + Environment.NewLine + ex.ToString(), "Preview Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateTable()
        {
            this.SuspendLayout();
            dgLevels.Rows.Clear();

            // normal rows
            for (int i = 0; i < cmbLevels.SelectedIndex + 1; i++)
            {
                dgLevels.Rows.Add();

                dgLevels.Rows[i].Cells[0].Value = i;

                // save current map metadata
                int height = map.height;
                int width = map.width;
                double minx = map.extent.minx;
                double miny = map.extent.miny;
                double maxx = map.extent.maxx;
                double maxy = map.extent.maxy;

                // create preview
                map.setSize(256, 256);

                if (loadedSettings == null)
                {
                    if (cmbSnapWorld.SelectedIndex == 0)
                    {
                        double tempGap;
                        if (Math.Abs(worldSnapMinX - worldSnapMaxX) > Math.Abs(worldSnapMinY - worldSnapMaxY))
                        {
                            tempGap = Math.Abs(worldSnapMinX - worldSnapMaxX);
                        }
                        else
                        {
                            tempGap = Math.Abs(worldSnapMinY - worldSnapMaxY);
                        }
                        map.setExtent(worldSnapMinX, worldSnapMinY, worldSnapMinX + tempGap, worldSnapMinY + tempGap);
                    }
                    else
                    {
                        double tempGap;
                        if (Math.Abs(minx - maxx) > Math.Abs(miny - maxy))
                        {
                            tempGap = Math.Abs(minx - maxx);
                        }
                        else
                        {
                            tempGap = Math.Abs(miny - maxy);
                        }
                        map.setExtent(minx, miny, minx + tempGap, miny + tempGap);
                    }
                }
                else
                {
                    map.setExtent(
                            loadedSettings.startX,
                            loadedSettings.startY,
                            loadedSettings.startX + loadedSettings.initTileGap,
                            loadedSettings.startY + loadedSettings.initTileGap);
                }

                dgLevels.Rows[i].Cells[1].Value = "1 : " + Math.Floor(map.scaledenom / Math.Pow(2, i));

                // restore map metadata
                map.setSize(width, height);
                map.setExtent(minx, miny, maxx, maxy);

                double j = Math.Pow(4, i);
                if (j < 1000) dgLevels.Rows[i].Cells[2].Value = Math.Pow(4, i);
                if (j >= 1000 && j < 1000000) dgLevels.Rows[i].Cells[2].Value = Math.Floor(Math.Pow(4, i) / 1000) + "K";
                if (j >= 1000000 && j < 1000000000) dgLevels.Rows[i].Cells[2].Value = Math.Floor(Math.Pow(4, i) / 1000000) + "M";
                if (j >= 1000000000 && j < 1000000000000) dgLevels.Rows[i].Cells[2].Value = Math.Floor(Math.Pow(4, i) / 1000000000) + "B";
                if (j > 1000000000000) dgLevels.Rows[i].Cells[2].Value = Math.Floor(Math.Pow(4, i) / 1000000000000) + "T";

                if (statsTileSize != 0)
                {
                    double k = Math.Pow(4, i) * statsTileSize;
                    if (k < 1000) dgLevels.Rows[i].Cells[3].Value = Math.Floor(k) + "B";
                    if (k >= 1000 && k < 1000000) dgLevels.Rows[i].Cells[3].Value = Math.Floor(k / 1000) + "KB";
                    if (k >= 1000000 && k < 1000000000) dgLevels.Rows[i].Cells[3].Value = Math.Floor(k / 1000000) + "MB";
                    if (k >= 1000000000 && k < 1000000000000) dgLevels.Rows[i].Cells[3].Value = Math.Floor(k / 1000000000) + "GB";
                    if (k >= 1000000000000 && k < 1000000000000000) dgLevels.Rows[i].Cells[3].Value = Math.Floor(k / 1000000000000) + "TB";
                    if (k >= 1000000000000000) dgLevels.Rows[i].Cells[3].Value = Math.Floor(k / 1000000000000000) + "PB";
                }
                else
                {
                    dgLevels.Rows[i].Cells[3].Value = "...";
                }

                if (statsTPM != 0)
                {
                    double l = Math.Pow(4, i) / (statsTPM / 60);
                    if (l < 60) dgLevels.Rows[i].Cells[4].Value = "< 1m";
                    if (l >= 60 && l < 3600) dgLevels.Rows[i].Cells[4].Value = Math.Floor(l / 60) + "m";
                    if (l >= 3600 && l < 86400) dgLevels.Rows[i].Cells[4].Value = Math.Floor(l / 3600) + "h";
                    if (l >= 86400 && l < 604800) dgLevels.Rows[i].Cells[4].Value = Math.Floor(l / 86400) + "d";
                    if (l >= 604800 && l < 31449600) dgLevels.Rows[i].Cells[4].Value = Math.Floor(l / 604800) + "w";
                    if (l >= 31449600 && l < 3144960000) dgLevels.Rows[i].Cells[4].Value = Math.Floor(l / 31449600) + "y";
                    if (l >= 3144960000) dgLevels.Rows[i].Cells[4].Value = Math.Floor(l / 3144960000) + "c";
                }
                else
                {
                    dgLevels.Rows[i].Cells[4].Value = "...";
                }
            }

            // total row
            dgLevels.Rows.Add();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgLevels.Rows[cmbLevels.SelectedIndex + 1].DefaultCellStyle = cellStyle;

            dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[0].Value = "Total";

            double totalTiles = 0;
            for (int i = 0; i < cmbLevels.SelectedIndex + 1; i++)
            {
                totalTiles = totalTiles + Math.Pow(4, i);
            }
            if (totalTiles < 1000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[2].Value = totalTiles;
            if (totalTiles >= 1000 && totalTiles < 1000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[2].Value = Math.Floor(totalTiles / 1000) + "K";
            if (totalTiles >= 1000000 && totalTiles < 1000000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[2].Value = Math.Floor(totalTiles / 1000000) + "M";
            if (totalTiles >= 1000000000 && totalTiles < 1000000000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[2].Value = Math.Floor(totalTiles / 1000000000) + "B";
            if (totalTiles > 1000000000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[2].Value = Math.Floor(totalTiles / 1000000000000) + "T";

            dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[1].Value = "N/A";

            if (statsTileSize != 0)
            {
                double totalSize = 0;
                for (int i = 0; i < cmbLevels.SelectedIndex + 1; i++)
                {
                    totalSize = totalSize + Math.Pow(4, i) * statsTileSize;
                }
                if (totalSize < 1000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[3].Value = Math.Floor(totalSize) + "B";
                if (totalSize >= 1000 && totalSize < 1000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[3].Value = Math.Floor(totalSize / 1000) + "KB";
                if (totalSize >= 1000000 && totalSize < 1000000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[3].Value = Math.Floor(totalSize / 1000000) + "MB";
                if (totalSize >= 1000000000 && totalSize < 1000000000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[3].Value = Math.Floor(totalSize / 1000000000) + "GB";
                if (totalSize >= 1000000000000 && totalSize < 1000000000000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[3].Value = Math.Floor(totalSize / 1000000000000) + "TB";
                if (totalSize >= 1000000000000000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[3].Value = Math.Floor(totalSize / 1000000000000000) + "PB";
            }
            else
            {
                dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[3].Value = "...";
            }

            if (statsTPM != 0)
            {
                double totalTime = 0;
                for (int i = 0; i < cmbLevels.SelectedIndex + 1; i++)
                {
                    totalTime = totalTime + Math.Pow(4, i) / (statsTPM / 60);
                }
                if (totalTime < 60) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = "< 1m";
                if (totalTime >= 60 && totalTime < 3600) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = Math.Floor(totalTime / 60) + "m";
                if (totalTime >= 3600 && totalTime < 86400) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = Math.Floor(totalTime / 3600) + "h";
                if (totalTime >= 86400 && totalTime < 604800) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = Math.Floor(totalTime / 86400) + "d";
                if (totalTime >= 604800 && totalTime < 31449600) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = Math.Floor(totalTime / 604800) + "w";
                if (totalTime >= 31449600 && totalTime < 3144960000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = Math.Floor(totalTime / 31449600) + "y";
                if (totalTime >= 3144960000) dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = Math.Floor(totalTime / 3144960000) + "c";
            }
            else
            {
                dgLevels.Rows[cmbLevels.SelectedIndex + 1].Cells[4].Value = "...";
            }

            // resize control
            if (cmbLevels.SelectedIndex < 12)
            {
                dgLevels.Width = 744;
                dgLevels.Height = 32 + (24 * (cmbLevels.SelectedIndex + 2));
            }
            else
            {
                dgLevels.Width = 764;
                dgLevels.Height = 344;
            }
            this.ResumeLayout();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSaveFolder.ShowDialog() == DialogResult.OK)
            {
                txtSavePath.Text = dlgSaveFolder.SelectedPath;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtSavePath.Text == "")
            {
                MessageBox.Show("Please select a folder for saving tiles", "Select Save Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Enabled = false;
            try
            {
                SaveTileSettings(cmbPreConfig.Text, false);

                // check extent has not changed
                if (mapControl.GetInitialExtent().minx != map.extent.minx
                    || mapControl.GetInitialExtent().miny != map.extent.miny
                    || mapControl.GetInitialExtent().maxx != map.extent.maxx
                    || mapControl.GetInitialExtent().maxy != map.extent.maxy)
                    Program.frmMain.dirtyFlag = true;

                // check dirty flag
                if (Program.frmMain.dirtyFlag)
                {
                    if (MessageBox.Show("The map must be saved before running the tile generation tool." + Environment.NewLine + "Do you wish to save the map now and continue running?", "Save Map", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Program.frmMain.SaveMap();

                        SaveMetadata();
                        if (loadedSettings.snapWorld) CalculateGoogleExtents();
                        GenerateTileList(loadedSettings.depth, loadedSettings.savePath, loadedSettings.overwrite, loadedSettings.imageFormat);
                        GenerateSettingsFile(loadedSettings.savePath);
                        SplitTileList(false, loadedSettings.processes, loadedSettings.savePath);
                        StartTileGeneration(loadedSettings.processes, loadedSettings.savePath);
                    }
                }
                else
                {
                    SaveMetadata();
                    if (loadedSettings.snapWorld) CalculateGoogleExtents();
                    GenerateTileList(loadedSettings.depth, loadedSettings.savePath, loadedSettings.overwrite, loadedSettings.imageFormat);
                    GenerateSettingsFile(loadedSettings.savePath);
                    SplitTileList(false, loadedSettings.processes, loadedSettings.savePath);
                    StartTileGeneration(loadedSettings.processes, loadedSettings.savePath);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Tile generation process has been aborted", "Tile Generation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Enabled = true;
        }

        private void SaveMetadata()
        {
            // Steph if you know a better way then let me know, just calling set does not seem to overwrite values
            try
            {
                map.removeMetaData("TileManager_Config");
            }
            catch (Exception) { }
            map.setMetaData("TileManager_Config", cmbPreConfig.Text);
        }

        // saves the currently loaded tile settings and updates loadedSettings
        private void SaveTileSettings(string name, bool newConfig)
        {
            foreach (TileSettings tileSettings in mainForm.settings.tileSettingsArray)
            {
                if (tileSettings.settingsName == name)
                {
                    // form input variables
                    int processes;
                    int depth;
                    int imageSize;
                    int imageBuffer;
                    string imageFormat;
                    string savePath;
                    bool overwrite;
                    bool snapWorld;

                    numTiles = 0;
                    numTestTiles = 0;

                    processes = Convert.ToInt32(cmbProcesses.Text); // number of parallel processes

                    depth = Convert.ToInt32(cmbLevels.Text); // number of tile levels

                    imageSize = Convert.ToInt32(cmbImageSize.Text); // pixels

                    if (cmbImageBuffer.Text == "Off")
                    {
                        imageBuffer = 0; // pixels
                    }
                    else
                    {
                        imageBuffer = Convert.ToInt32(cmbImageBuffer.Text); // pixels
                    }

                    imageFormat = cmbImageFormat.Text; // image save format

                    savePath = txtSavePath.Text; // location to save generated tiles

                    if (cmbOverwrite.SelectedIndex == 0)
                    {
                        overwrite = true;
                    }
                    else
                    {
                        overwrite = false;
                    }

                    if (cmbSnapWorld.SelectedIndex == 0)
                    {
                        snapWorld = true;
                    }
                    else
                    {
                        snapWorld = false;
                    }


                    // 0/0/0 tile variables
                    double startPointX;
                    double startPointY;
                    if (snapWorld)
                    {
                        startPointX = worldSnapMinX;
                        startPointY = worldSnapMinY;
                    }
                    else
                    {
                        startPointX = map.extent.minx;
                        startPointY = map.extent.miny;
                    }

                    // inital distance between tiles, for level 0 is max meters - min meters
                    double initGap;
                    if (snapWorld)
                    {
                        initGap = Math.Abs(worldSnapMinX - worldSnapMaxX);
                    }
                    else
                    {
                        if ((map.extent.maxx - map.extent.minx) > (map.extent.maxy - map.extent.miny))
                        {
                            initGap = map.extent.maxx - map.extent.minx;
                        }
                        else
                        {
                            initGap = map.extent.maxy - map.extent.miny;
                        }
                    }

                    // meta-buffer to fix labeling as a multiplier
                    double buffer = 1 + ((double)imageBuffer / (double)imageSize);

                    tileSettings.depth = depth;
                    tileSettings.imageBuffer = imageBuffer;
                    tileSettings.imageFormat = imageFormat;
                    tileSettings.imageSize = imageSize;
                    tileSettings.overwrite = overwrite;
                    tileSettings.processes = processes;
                    tileSettings.savePath = savePath;
                    tileSettings.snapWorld = snapWorld;

                    if (newConfig)
                    {
                        tileSettings.startX = startPointX;
                        tileSettings.startY = startPointY;
                        tileSettings.initTileGap = initGap;
                    }

                    loadedSettings = tileSettings;
                }
            }
        }

        // saves list of tiles to generate to TilingList in the folder targeted on form
        private void GenerateTileList(int tileDepth, string tileSavePath, bool tileOverwrite, string tileFormat)
        {
            try
            {
                // tile parameter generation loops
                string tileDataFile = tileSavePath + "\\TilingList";
                if (File.Exists(tileDataFile))
                {
                    File.Delete(tileDataFile);
                }
                BinaryWriter bswriter = new BinaryWriter(File.Open(tileDataFile, FileMode.Create));

                for (int z = 0; z < tileDepth; z++)
                {
                    for (int x = 0; x < Math.Pow(2, z); x++)
                    {
                        for (int y = 0; y < Math.Pow(2, z); y++)
                        {
                            if (tileOverwrite)
                            {
                                bswriter.Write(z);
                                bswriter.Write(x);
                                bswriter.Write(y);
                                numTiles++;
                            }
                            else
                            {
                                // check if file exists if overwrite option is false
                                if (!(File.Exists(tileSavePath + "\\" + z + "\\" + x + "\\" + y + "." + tileFormat)))
                                {
                                    bswriter.Write(z);
                                    bswriter.Write(x);
                                    bswriter.Write(y);
                                    numTiles++;
                                }
                            }
                        }
                    }
                }
                bswriter.Flush();
                bswriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating the complete TilingList file: " + Environment.NewLine + ex.ToString(), "Tile List Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // called if snap to google tiles is set to yes
        private void CalculateGoogleExtents()
        {
            try
            {
                // temp extents
                double minX = 0, minY = 0, maxX = 0, maxY = 0;

                // calculate google min level
                double googleGap = Math.Abs(googleMinX - googleMaxX);
                double initGap;
                if (Math.Abs(map.extent.minx - map.extent.maxx) > (map.extent.miny - map.extent.maxy))
                {
                    initGap = Math.Abs(map.extent.minx - map.extent.maxx);
                }
                else
                {
                    initGap = Math.Abs(map.extent.miny - map.extent.maxy);
                }

                z0 = 0;
                while (googleGap > initGap)
                {
                    googleGap = googleGap / 2;
                    z0++;
                }
                z0--;
                googleGap = Math.Abs(googleMinX - googleMaxX);

                // calculate intersecting tiles at level
                for (int i = 0; i < Math.Pow(2, z0); i++)
                {
                    double calcMinX = googleMinX + ((googleGap / Math.Pow(2, z0)) * i);
                    double calcMaxX = googleMinX + ((googleGap / Math.Pow(2, z0)) * (i + 1));

                    // continue if the tiles left or right edges fall within the inital extent
                    if (!((calcMinX > map.extent.maxx) || (calcMaxX < map.extent.minx)))
                    {
                        for (int j = 0; j < Math.Pow(2, z0); j++)
                        {
                            double calcMinY = googleMinY + ((googleGap / Math.Pow(2, z0)) * j);
                            double calcMaxY = googleMinY + ((googleGap / Math.Pow(2, z0)) * (j + 1));

                            // continue if the tiles top or bottom edges fall within the initial extent
                            if (!((calcMinY > map.extent.maxy) || (calcMaxY < map.extent.miny)))
                            {
                                // set the new 0/0/0 tile based on tiles found
                                if ((calcMinX > googleMinX) && (calcMinX < map.extent.minx)) minX = calcMinX;
                                if ((calcMinY > googleMinY) && (calcMinY < map.extent.miny)) minY = calcMinY;
                                if ((calcMaxX < googleMaxX) && (calcMaxX > map.extent.maxx)) maxX = calcMaxX;
                                if ((calcMaxY < googleMaxY) && (calcMaxY > map.extent.maxy)) maxY = calcMaxY;
                            }
                        }
                    }
                }
                worldSnapMinX = minX;
                worldSnapMinY = minY;
                worldSnapMaxX = maxX;
                worldSnapMaxY = maxY;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating extents snapped to Google projection: " + Environment.NewLine + ex.ToString(), "Google Extents Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void NewTileSettings(string name)
        {
            mainForm.settings.AddToTilePreset(new TileSettings(name));
        }
    
        // generates the file used by all processes called TilingSettings in targeted folder
        private void GenerateSettingsFile(string savePath)
        {
            try
            {
                // save TileSettings file
                string tileSettingsFile = savePath + "\\TilingSettings";
                if (File.Exists(tileSettingsFile))
                {
                    File.Delete(tileSettingsFile);
                }
                FileStream fswriter = new FileStream(tileSettingsFile, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fswriter, loadedSettings);
                fswriter.Flush();
                fswriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating TilingSettings file: " + Environment.NewLine + ex.ToString(), "Tile Settings File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // generates the text which defines the tile set as a baselayer in ConfigManager
        public void GenerateBaselayerText(bool snapWorld, int depth, string imageFormat, int imageSize)
        {
            try
            {
                string proj4;
                int epsg;
                MapUtils.FindProjection(map.getProjection(), out proj4, out epsg);

                if (snapWorld)
                {
                    configManager = (
                        "{" + Environment.NewLine +
                        "    \"layers\": [{" + Environment.NewLine +
                        "        \"type\": \"tile\"," + Environment.NewLine +
                        "            \"url\": \"<enter your hosted tile folder here>\"," + Environment.NewLine +
                        "            \"tilesExtent\": {" + Environment.NewLine +
                        "                \"x1\": " + loadedSettings.startX + "," + Environment.NewLine +
                        "                \"y1\": " + loadedSettings.startY + "," + Environment.NewLine +
                        "                \"x2\": " + (loadedSettings.startX + loadedSettings.initTileGap) + "," + Environment.NewLine +
                        "                \"y2\": " + (loadedSettings.startY + loadedSettings.initTileGap) + "," + Environment.NewLine +
                        "                \"projection\": \"EPSG:" + epsg + "\"" + Environment.NewLine +
                        "            }," + Environment.NewLine +
                        "            \"numZoomLevels\": " + depth + "," + Environment.NewLine +
                        "            \"zoomOffset\": " + z0 + "," + Environment.NewLine +
                        "            \"imageFormat\": \"" + imageFormat + "\"," + Environment.NewLine +
                        "            \"tileSize\": " + imageSize + "," + Environment.NewLine +
                        "            \"opacity\": 1," + Environment.NewLine +
                        "            \"buffer\": 1" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        "    ]" + Environment.NewLine +
                        "}"
                        );
                }
                else
                {
                    configManager = (
                        "{" + Environment.NewLine +
                        "    \"layers\": [{" + Environment.NewLine +
                        "        \"type\": \"tile\"," + Environment.NewLine +
                        "            \"url\": \"<enter your hosted tile folder here>\"," + Environment.NewLine +
                        "            \"tilesExtent\": {" + Environment.NewLine +
                        "                \"x1\": " + loadedSettings.startX + "," + Environment.NewLine +
                        "                \"y1\": " + loadedSettings.startY + "," + Environment.NewLine +
                        "                \"x2\": " + (loadedSettings.startX + loadedSettings.initTileGap) + "," + Environment.NewLine +
                        "                \"y2\": " + (loadedSettings.startY + loadedSettings.initTileGap) + "," + Environment.NewLine +
                        "                \"projection\": \"EPSG:" + epsg + "\"" + Environment.NewLine +
                        "            }," + Environment.NewLine +
                        "            \"numZoomLevels\": " + depth + "," + Environment.NewLine +
                        "            \"imageFormat\": \"" + imageFormat + "\"," + Environment.NewLine +
                        "            \"tileSize\": " + imageSize + "," + Environment.NewLine +
                        "            \"opacity\": 1," + Environment.NewLine +
                        "            \"buffer\": 1" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        "    ]" + Environment.NewLine +
                        "}"
                );
                }

                if (baseLayerTextDialog != null) baseLayerTextDialog.Close();
                if (configManager != "")
                {
                    baseLayerTextDialog = new TileManager.BaseLayerTextDialog(mapfilepath, configManager);
                    baseLayerTextDialog.Owner = this;
                    baseLayerTextDialog.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating base layer text for config manager: " + Environment.NewLine + ex.ToString(), "Base Layer Text Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // splits the TilingList generated previously in to smaller files for each process to consume
        private void SplitTileList(bool testRun, int processes, string savePath)
        {
            try
            {
                long tilesPerProcess;
                if (testRun)
                {
                    tilesPerProcess = numTestTiles / processes;
                }
                else
                {
                    tilesPerProcess = numTiles / processes;
                }

                // all except last file
                string tileDataFile = savePath + "\\TilingList";
                BinaryReader bsreader = new BinaryReader(File.Open(tileDataFile, FileMode.Open));
                for (int i = 1; i < processes; i++)
                {
                    BinaryWriter bswriter = new BinaryWriter(File.Open(tileDataFile + i, FileMode.Create));
                    for (int j = 0; j < tilesPerProcess; j++)
                    {
                        bswriter.Write(bsreader.ReadInt32());
                        bswriter.Write(bsreader.ReadInt32());
                        bswriter.Write(bsreader.ReadInt32());
                    }
                    bswriter.Flush();
                    bswriter.Close();
                }

                // final file including leftovers
                BinaryWriter bswriterF = new BinaryWriter(File.Open(tileDataFile + processes, FileMode.Create));
                for (int j = 0; j < tilesPerProcess + (numTiles % processes); j++)
                {
                    bswriterF.Write(bsreader.ReadInt32());
                    bswriterF.Write(bsreader.ReadInt32());
                    bswriterF.Write(bsreader.ReadInt32());
                }
                bswriterF.Flush();
                bswriterF.Close();

                bsreader.Close();
                File.Delete(tileDataFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error splitting the TilingList file: " + Environment.NewLine + ex.ToString(), "Split Tile List Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // MapManager calls itself which will begin a new process with the TilingSettings/TilingList and map file as input
        private void StartTileGeneration(int numProcesses, string savePath)
        {
            try
            {
                for (int id = 1; id <= numProcesses; id++)
                {
                    //new TileGenerator(mapfilepath, savePath, id.ToString());
                    Process.Start(Application.StartupPath + "\\TileGenerator.exe", " /tile " + "\"" + mapfilepath + "\" " + "\"" + savePath + "\" " + id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting new MapManager processes for tile generation: " + Environment.NewLine + ex.ToString(), "Start Tile Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void TileManagerForm_Activated(object sender, EventArgs e)
        {
            UpdatePreview();
            UpdateTable();
        }

        private void cmbLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNumLevels.SelectedIndex = cmbLevels.SelectedIndex;
            if (loaded) UpdateTable();
        }

        private void btnGenerateStats_Click(object sender, EventArgs e)
        {
            if (cmbPreConfig.SelectedIndex == 0)
            {
                MessageBox.Show("Please save or select a configuration before calculating statistics", "Save Configuration First", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (loadedSettings != null) SaveTileSettings(cmbPreConfig.Text, false);

            if (txtSavePath.Text == "")
            {
                MessageBox.Show("Please select a folder for saving tiles", "Select Save Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Enabled = false;
            btnGenerateStats.Text = "Calculating...";
            Refresh();

            statsNumber = (Convert.ToDouble(cmbNumTest.Text) / 500) * 0.001425;

            try
            {
                StatisticsTest(statsNumber);
            }
            catch (Exception) { }

            UpdateTable();

            btnGenerateStats.Text = "Calculate Statistics";
            this.Enabled = true;
        }

        private void StatisticsTest(double testSize)
        {
            try
            {
                Random random = new Random();

                if (loadedSettings.snapWorld) CalculateGoogleExtents();

                string trueSavePath = loadedSettings.savePath;
                loadedSettings.savePath = loadedSettings.savePath + "\\StatsTest";
                Directory.CreateDirectory(loadedSettings.savePath);

                GenerateSettingsFile(loadedSettings.savePath);

                // tile parameter generation loops
                string tileDataFile = loadedSettings.savePath + "\\TilingList";
                if (File.Exists(tileDataFile))
                {
                    File.Delete(tileDataFile);
                }
                BinaryWriter bswriter = new BinaryWriter(File.Open(tileDataFile, FileMode.Create));

                for (int z = 0; z < 10; z++)
                {
                    for (int x = 0; x < Math.Pow(2, z); x++)
                    {
                        for (int y = 0; y < Math.Pow(2, z); y++)
                        {
                            if (random.NextDouble() <= testSize)
                            {
                                bswriter.Write(z);
                                bswriter.Write(x);
                                bswriter.Write(y);
                                numTestTiles++;
                            }
                        }
                    }
                }
                bswriter.Flush();
                bswriter.Close();

                SplitTileList(true, loadedSettings.processes, loadedSettings.savePath);
                StartTileGeneration(loadedSettings.processes, loadedSettings.savePath);

                DateTime startTime = DateTime.Now;

                int procRemaining = loadedSettings.processes;
                while (procRemaining > 0)
                {
                    procRemaining = 0;
                    foreach (string file in Directory.GetFiles(loadedSettings.savePath))
                    {
                        if (file.Contains("TilingList")) procRemaining++;
                    }
                    TimeSpan runTime = DateTime.Now - startTime;
                    Application.DoEvents();
                }

                // save and display stats
                DateTime endTime = DateTime.Now;
                TimeSpan timeTaken = endTime - startTime;
                DirectoryInfo dirInfo = new DirectoryInfo(loadedSettings.savePath);

                statsTPM = Convert.ToInt32(Math.Floor(((double)numTestTiles / timeTaken.TotalMinutes)));
                double dirSize = (double)getDirectorySize(dirInfo);
                if (dirSize > 0)
                {
                    statsTileSize = Convert.ToInt32(Math.Floor(dirSize / numTestTiles));
                }

                txtTPM.Text = statsTPM.ToString();
                txtTileSize.Text = statsTileSize.ToString();

                Directory.Delete(loadedSettings.savePath, true);
                loadedSettings.savePath = trueSavePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running calculate statistics function: " + Environment.NewLine + ex.ToString(), "Statistics Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public long getDirectorySize (DirectoryInfo dirInfo)
        {
            try
            {
                string lpRootPathName = loadedSettings.savePath.Substring(0, 3);
                uint lpSectorsPerCluster;
                uint lpBytesPerSector;
                uint lpNumberOfFreeClusters;
                uint lpTotalNumberOfClusters;

                GetDiskFreeSpace(lpRootPathName, out lpSectorsPerCluster, out lpBytesPerSector, out lpNumberOfFreeClusters, out lpTotalNumberOfClusters);
                uint clusterSize = lpSectorsPerCluster * lpBytesPerSector;

                long total = 0;
                foreach (System.IO.FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                {
                    total += Convert.ToInt64(Math.Ceiling((double)file.Length / clusterSize) * clusterSize);
                }
                return total;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Using default value of 4000 bytes because there was an error calculating precise tile size in directory" + Environment.NewLine + ex.ToString(), "Tile Size Calculation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool GetDiskFreeSpace(string lpRootPathName,
            out uint lpSectorsPerCluster,
            out uint lpBytesPerSector,
            out uint lpNumberOfFreeClusters,
            out uint lpTotalNumberOfClusters);

        private void btnBaseLayerText_Click(object sender, EventArgs e)
        {
            if (loadedSettings.snapWorld) CalculateGoogleExtents();
            SaveTileSettings(cmbPreConfig.Text, false);
            GenerateBaselayerText(loadedSettings.snapWorld, loadedSettings.depth, loadedSettings.imageFormat, loadedSettings.imageSize);
        }

        private void cmbNumLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLevels.SelectedIndex = cmbNumLevels.SelectedIndex;
        }

        private void tabTileManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabTileManager.SelectedIndex == 1) cmbNumLevels.Focus();
        }

        private void TileManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.tileManagerForm = null;
        }

        private void cmbPreConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPreConfig.Text == "< unsaved settings >")
            {
                loadedSettings = null;

                btnDeleteConfig.Enabled = false;
                btnGenerateTiles.Enabled = false;
                btnBaseLayerText.Enabled = false;
                cmbSnapWorld.Enabled = true;

                txtPreMinX.Text = "";
                txtPreMinY.Text = "";
                txtPreMaxX.Text = "";
                txtPreMaxY.Text = "";
            }
            else
            {
                btnDeleteConfig.Enabled = true;
                btnGenerateTiles.Enabled = true;
                btnBaseLayerText.Enabled = true;
                cmbSnapWorld.Enabled = false;

                foreach (TileSettings tileSettings in mainForm.settings.tileSettingsArray)
                {
                    if (tileSettings.settingsName == cmbPreConfig.Text)
                    {
                        txtPreMinX.Text = tileSettings.startX.ToString();
                        txtPreMinY.Text = tileSettings.startY.ToString();
                        txtPreMaxX.Text = (Convert.ToDouble(txtPreMinX.Text) + tileSettings.initTileGap).ToString();
                        txtPreMaxY.Text = (Convert.ToDouble(txtPreMinY.Text) + tileSettings.initTileGap).ToString();

                        cmbProcesses.SelectedIndex = tileSettings.processes - 1;
                        cmbLevels.SelectedIndex = tileSettings.depth - 1;
                        if (tileSettings.overwrite)
                        {
                            cmbOverwrite.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbOverwrite.SelectedIndex = 1;
                        }
                        if (tileSettings.snapWorld)
                        {
                            cmbSnapWorld.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbSnapWorld.SelectedIndex = 1;
                        }
                        if (tileSettings.imageBuffer == 0) cmbImageBuffer.SelectedIndex = 0;
                        if (tileSettings.imageBuffer == 25) cmbImageBuffer.SelectedIndex = 1;
                        if (tileSettings.imageBuffer == 50) cmbImageBuffer.SelectedIndex = 2;
                        if (tileSettings.imageBuffer == 100) cmbImageBuffer.SelectedIndex = 3;

                        if (tileSettings.imageFormat == "png") cmbImageFormat.SelectedIndex = 0;
                        if (tileSettings.imageFormat == "jpg") cmbImageFormat.SelectedIndex = 1;

                        if (tileSettings.imageSize == 256) cmbImageSize.SelectedIndex = 0;

                        txtSavePath.Text = tileSettings.savePath;

                        loadedSettings = tileSettings;
                    }
                }
            }
            if (loaded) UpdatePreview();
            if (loaded) UpdateTable();
        }

        private void btnDeleteConfig_Click(object sender, EventArgs e)
        {
            mainForm.settings.RemoveTilePreset(cmbPreConfig.Text);
            cmbPreConfig.Items.Remove(cmbPreConfig.Text);
            cmbPreConfig.SelectedIndex = 0;
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            if (cmbPreConfig.SelectedIndex == 0)
            {
                TileManager.NewPresetConfigDialog newPresetConfigDialog = new TileManager.NewPresetConfigDialog();
                newPresetConfigDialog.Owner = this;
                if (newPresetConfigDialog.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (TileSettings tileSettings in mainForm.settings.tileSettingsArray)
                    {
                        if (tileSettings.settingsName == newPresetConfigDialog.txtNewPreset.Text)
                        {
                            MessageBox.Show("Cancelled save as the name already exists", "Name Exists Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    NewTileSettings(newPresetConfigDialog.txtNewPreset.Text);
                    SaveTileSettings(newPresetConfigDialog.txtNewPreset.Text, true);

                    // reload combo box
                    cmbPreConfig.Items.Add(loadedSettings.settingsName);
                    cmbPreConfig.SelectedIndex = cmbPreConfig.Items.Count - 1;
                }
                newPresetConfigDialog.Dispose();
            }
            else
            {
                SaveTileSettings(cmbPreConfig.Text, false);
            }

            mainForm.SaveSettings();

            this.Enabled = true;
        }

        private void cmbSnapWorldChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                string proj4;
                int epsg;
                MapUtils.FindProjection(map.getProjection(), out proj4, out epsg);

                if (cmbSnapWorld.SelectedIndex == 0 && epsg != 3785)
                {
                    cmbSnapWorld.SelectedIndex = 1;
                    MessageBox.Show("Your map must be set to google projection EPSG:3785 to use this function", "Invalid Map Projection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (cmbPreConfig.Text != "< unsaved settings >") SaveTileSettings(cmbPreConfig.Text, false);
                UpdatePreview();
                UpdateTable();

                if (cmbSnapWorld.SelectedIndex == 0)
                {
                    lineGoogleV.Visible = true;
                    lineGoogleH.Visible = true;
                }
                else
                {
                    lineGoogleV.Visible = false;
                    lineGoogleH.Visible = false;
                }
            }
        }

        private void txtSavePath_Leave(object sender, EventArgs e)
        {
            if (!(txtSavePath.Text == ""))
            {
                if (!(Directory.Exists(txtSavePath.Text)))
                {
                    MessageBox.Show("Directory entered was invalid", "Save Path Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSavePath.Text = "";
                }
            }
        }

        private void btnStopGen_Click(object sender, EventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("TileGenerator");
            if (proc.Length > 0)
            {
                if (MessageBox.Show("Are you sure you want to end all tile generation processes?", "End Generation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < proc.Length; i++)
                    {
                        proc[i].Kill();
                    }
                }
            }
        }

        private void timerProcessScan_Tick(object sender, EventArgs e)
        {
            try
            {
                Process[] proc = Process.GetProcessesByName("TileGenerator");
                if (proc.Length > 0)
                {
                    lblActiveProcess.Text = proc.Length.ToString() + " TileGenerator Processes Active";
                }
                else
                {

                    lblActiveProcess.Text = "No TileGenerator Processes Active";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
