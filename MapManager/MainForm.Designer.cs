namespace DMS.MapManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setInitialExtentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectByRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectByPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectByLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.redrawMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mapPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overviewPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileExternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.styleLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkMapFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpContentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelectRect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelectPoly = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelectLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRedraw = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.imageListTabs = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialogMain = new System.Windows.Forms.SaveFileDialog();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.layerControl = new DMS.MapLibrary.LayerControl();
            this.buttonApply = new System.Windows.Forms.Button();
            this.layerPropertyEditor = new DMS.MapLibrary.LayerPropertyEditor();
            this.mapPropertyEditor = new DMS.MapLibrary.MapPropertyEditor();
            this.tabControlContents = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mapControl = new DMS.MapLibrary.MapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.scintillaControl = new ScintillaNet.Scintilla();
            this.selectByShapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlContents.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scintillaControl)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStripMain.Size = new System.Drawing.Size(1577, 28);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            this.menuStripMain.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.editor_HelpRequested);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveMapImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.recentMapsToolStripMenuItem,
            this.toolStripSeparator2,
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(201, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::MapManager.Properties.Resources.Save16;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveMapImageToolStripMenuItem
            // 
            this.saveMapImageToolStripMenuItem.Name = "saveMapImageToolStripMenuItem";
            this.saveMapImageToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.saveMapImageToolStripMenuItem.Text = "Save Map &Image...";
            this.saveMapImageToolStripMenuItem.Click += new System.EventHandler(this.saveMapImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // recentMapsToolStripMenuItem
            // 
            this.recentMapsToolStripMenuItem.Name = "recentMapsToolStripMenuItem";
            this.recentMapsToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.recentMapsToolStripMenuItem.Text = "&Recent Maps";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.findToolStripMenuItem.Text = "&Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.replaceToolStripMenuItem.Text = "&Replace...";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(201, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panToolStripMenuItem,
            this.zoomToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.setInitialExtentToolStripMenuItem,
            this.toolStripMenuItem1,
            this.selectItemToolStripMenuItem,
            this.selectByRectangleToolStripMenuItem,
            this.selectByPolygonToolStripMenuItem,
            this.selectByLineToolStripMenuItem,
            this.selectByShapeToolStripMenuItem,
            this.clearSelectionToolStripMenuItem,
            this.toolStripMenuItem3,
            this.redrawMapToolStripMenuItem,
            this.toolStripMenuItem2,
            this.mapPropertiesToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.mapToolStripMenuItem.Text = "&Map";
            // 
            // panToolStripMenuItem
            // 
            this.panToolStripMenuItem.Image = global::MapManager.Properties.Resources.Pan16;
            this.panToolStripMenuItem.Name = "panToolStripMenuItem";
            this.panToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.panToolStripMenuItem.Text = "&Pan";
            this.panToolStripMenuItem.Click += new System.EventHandler(this.panToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Image = global::MapManager.Properties.Resources.Zoomin_16;
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.zoomToolStripMenuItem.Text = "Zoom &In";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Image = global::MapManager.Properties.Resources.Zoomout_16;
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.zoomOutToolStripMenuItem.Text = "Zoom &Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // setInitialExtentToolStripMenuItem
            // 
            this.setInitialExtentToolStripMenuItem.Name = "setInitialExtentToolStripMenuItem";
            this.setInitialExtentToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.setInitialExtentToolStripMenuItem.Text = "Zoom to Initial &Extent";
            this.setInitialExtentToolStripMenuItem.Click += new System.EventHandler(this.setInitialExtentToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(225, 6);
            // 
            // selectItemToolStripMenuItem
            // 
            this.selectItemToolStripMenuItem.Name = "selectItemToolStripMenuItem";
            this.selectItemToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectItemToolStripMenuItem.Text = "&Select Item";
            this.selectItemToolStripMenuItem.Click += new System.EventHandler(this.selectItemToolStripMenuItem_Click);
            // 
            // selectByRectangleToolStripMenuItem
            // 
            this.selectByRectangleToolStripMenuItem.Name = "selectByRectangleToolStripMenuItem";
            this.selectByRectangleToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectByRectangleToolStripMenuItem.Text = "Select By &Rectangle";
            this.selectByRectangleToolStripMenuItem.Click += new System.EventHandler(this.selectByRectangleToolStripMenuItem_Click);
            // 
            // selectByPolygonToolStripMenuItem
            // 
            this.selectByPolygonToolStripMenuItem.Name = "selectByPolygonToolStripMenuItem";
            this.selectByPolygonToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectByPolygonToolStripMenuItem.Text = "Select By Po&lygon";
            this.selectByPolygonToolStripMenuItem.Click += new System.EventHandler(this.selectByPolygonToolStripMenuItem_Click);
            // 
            // selectByLineToolStripMenuItem
            // 
            this.selectByLineToolStripMenuItem.Name = "selectByLineToolStripMenuItem";
            this.selectByLineToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectByLineToolStripMenuItem.Text = "Select By &Line";
            this.selectByLineToolStripMenuItem.Visible = false;
            this.selectByLineToolStripMenuItem.Click += new System.EventHandler(this.selectByLineToolStripMenuItem_Click);
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.clearSelectionToolStripMenuItem.Text = "&Clear Selection";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(225, 6);
            // 
            // redrawMapToolStripMenuItem
            // 
            this.redrawMapToolStripMenuItem.Image = global::MapManager.Properties.Resources.Refresh16;
            this.redrawMapToolStripMenuItem.Name = "redrawMapToolStripMenuItem";
            this.redrawMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.redrawMapToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.redrawMapToolStripMenuItem.Text = "Re&draw Map";
            this.redrawMapToolStripMenuItem.Click += new System.EventHandler(this.redrawMapToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(225, 6);
            // 
            // mapPropertiesToolStripMenuItem
            // 
            this.mapPropertiesToolStripMenuItem.Name = "mapPropertiesToolStripMenuItem";
            this.mapPropertiesToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.mapPropertiesToolStripMenuItem.Text = "&Map Properties...";
            this.mapPropertiesToolStripMenuItem.Click += new System.EventHandler(this.mapPropertiesToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarToolStripMenuItem,
            this.statusbarToolStripMenuItem,
            this.layerPanelToolStripMenuItem,
            this.overviewPanelToolStripMenuItem,
            this.selectedFeaturesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolbarToolStripMenuItem
            // 
            this.toolbarToolStripMenuItem.Checked = true;
            this.toolbarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolbarToolStripMenuItem.Name = "toolbarToolStripMenuItem";
            this.toolbarToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.toolbarToolStripMenuItem.Text = "&Toolbar";
            this.toolbarToolStripMenuItem.Click += new System.EventHandler(this.toolbarToolStripMenuItem_Click);
            // 
            // statusbarToolStripMenuItem
            // 
            this.statusbarToolStripMenuItem.Checked = true;
            this.statusbarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusbarToolStripMenuItem.Name = "statusbarToolStripMenuItem";
            this.statusbarToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.statusbarToolStripMenuItem.Text = "&Statusbar";
            this.statusbarToolStripMenuItem.Click += new System.EventHandler(this.statusbarToolStripMenuItem_Click);
            // 
            // layerPanelToolStripMenuItem
            // 
            this.layerPanelToolStripMenuItem.Checked = true;
            this.layerPanelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.layerPanelToolStripMenuItem.Name = "layerPanelToolStripMenuItem";
            this.layerPanelToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.layerPanelToolStripMenuItem.Text = "&Layer Panel";
            this.layerPanelToolStripMenuItem.Click += new System.EventHandler(this.layerPanelToolStripMenuItem_Click);
            // 
            // overviewPanelToolStripMenuItem
            // 
            this.overviewPanelToolStripMenuItem.Checked = true;
            this.overviewPanelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overviewPanelToolStripMenuItem.Name = "overviewPanelToolStripMenuItem";
            this.overviewPanelToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.overviewPanelToolStripMenuItem.Text = "&Property Panel";
            this.overviewPanelToolStripMenuItem.Click += new System.EventHandler(this.overviewPanelToolStripMenuItem_Click);
            // 
            // selectedFeaturesToolStripMenuItem
            // 
            this.selectedFeaturesToolStripMenuItem.Name = "selectedFeaturesToolStripMenuItem";
            this.selectedFeaturesToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.selectedFeaturesToolStripMenuItem.Text = "Selected &Features...";
            this.selectedFeaturesToolStripMenuItem.Click += new System.EventHandler(this.selectedFeaturesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.openFileExternalToolStripMenuItem,
            this.styleLibraryToolStripMenuItem,
            this.tileManagerToolStripMenuItem,
            this.checkMapFileToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // openFileExternalToolStripMenuItem
            // 
            this.openFileExternalToolStripMenuItem.Image = global::MapManager.Properties.Resources.Edit;
            this.openFileExternalToolStripMenuItem.Name = "openFileExternalToolStripMenuItem";
            this.openFileExternalToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.openFileExternalToolStripMenuItem.Text = "Open Mapfile in External &Editor";
            this.openFileExternalToolStripMenuItem.Click += new System.EventHandler(this.openFileExternalToolStripMenuItem_Click);
            // 
            // styleLibraryToolStripMenuItem
            // 
            this.styleLibraryToolStripMenuItem.Name = "styleLibraryToolStripMenuItem";
            this.styleLibraryToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.styleLibraryToolStripMenuItem.Text = "St&yle Library...";
            this.styleLibraryToolStripMenuItem.Click += new System.EventHandler(this.styleLibraryToolStripMenuItem_Click);
            // 
            // tileManagerToolStripMenuItem
            // 
            this.tileManagerToolStripMenuItem.Image = global::MapManager.Properties.Resources.Basemap;
            this.tileManagerToolStripMenuItem.Name = "tileManagerToolStripMenuItem";
            this.tileManagerToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.tileManagerToolStripMenuItem.Text = "Tile Manager";
            this.tileManagerToolStripMenuItem.Click += new System.EventHandler(this.tileManagerToolStripMenuItem_Click);
            // 
            // checkMapFileToolStripMenuItem
            // 
            this.checkMapFileToolStripMenuItem.Name = "checkMapFileToolStripMenuItem";
            this.checkMapFileToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.checkMapFileToolStripMenuItem.Text = "Check Map File...";
            this.checkMapFileToolStripMenuItem.Click += new System.EventHandler(this.checkMapFileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpContentsToolStripMenuItem,
            this.helpIndexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpContentsToolStripMenuItem
            // 
            this.helpContentsToolStripMenuItem.Image = global::MapManager.Properties.Resources.Help16;
            this.helpContentsToolStripMenuItem.Name = "helpContentsToolStripMenuItem";
            this.helpContentsToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.helpContentsToolStripMenuItem.Text = "Help &Contents...";
            this.helpContentsToolStripMenuItem.Click += new System.EventHandler(this.helpContentsToolStripMenuItem_Click);
            // 
            // helpIndexToolStripMenuItem
            // 
            this.helpIndexToolStripMenuItem.Name = "helpIndexToolStripMenuItem";
            this.helpIndexToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.helpIndexToolStripMenuItem.Text = "Help &Index...";
            this.helpIndexToolStripMenuItem.Click += new System.EventHandler(this.helpIndexToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.searchToolStripMenuItem.Text = "&Search...";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.aboutToolStripMenuItem.Text = "&About MapManager...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelZoom,
            this.toolStripStatusLabelScale,
            this.toolStripStatusLabelPos});
            this.statusStripMain.Location = new System.Drawing.Point(0, 860);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStripMain.Size = new System.Drawing.Size(1577, 25);
            this.statusStripMain.TabIndex = 1;
            this.statusStripMain.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.editor_HelpRequested);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(56, 20);
            this.toolStripStatusLabel1.Text = "Render";
            // 
            // toolStripStatusLabelZoom
            // 
            this.toolStripStatusLabelZoom.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabelZoom.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabelZoom.Name = "toolStripStatusLabelZoom";
            this.toolStripStatusLabelZoom.Size = new System.Drawing.Size(52, 20);
            this.toolStripStatusLabelZoom.Text = "Zoom:";
            // 
            // toolStripStatusLabelScale
            // 
            this.toolStripStatusLabelScale.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabelScale.Name = "toolStripStatusLabelScale";
            this.toolStripStatusLabelScale.Size = new System.Drawing.Size(47, 20);
            this.toolStripStatusLabelScale.Text = "Scale:";
            // 
            // toolStripStatusLabelPos
            // 
            this.toolStripStatusLabelPos.Name = "toolStripStatusLabelPos";
            this.toolStripStatusLabelPos.Size = new System.Drawing.Size(43, 20);
            this.toolStripStatusLabelPos.Text = "(0  0)";
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripSeparator6,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut,
            this.toolStripButtonPan,
            this.toolStripSeparator4,
            this.toolStripButtonSelect,
            this.toolStripButtonSelectRect,
            this.toolStripButtonSelectPoly,
            this.toolStripButtonSelectLine,
            this.toolStripSeparator5,
            this.toolStripButtonRedraw,
            this.toolStripButtonHelp});
            this.toolStripMain.Location = new System.Drawing.Point(0, 28);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1577, 31);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStripMain";
            this.toolStripMain.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.editor_HelpRequested);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::MapManager.Properties.Resources.Save24;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSave.Text = "toolStripButton1";
            this.toolStripButtonSave.ToolTipText = "Save Map File";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonZoomIn
            // 
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomIn.Image = global::MapManager.Properties.Resources.Zoomin_24;
            this.toolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            this.toolStripButtonZoomIn.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonZoomIn.Text = "toolStripButton1";
            this.toolStripButtonZoomIn.ToolTipText = "Zoom In";
            this.toolStripButtonZoomIn.Click += new System.EventHandler(this.toolStripButtonZoomIn_Click);
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomOut.Image = global::MapManager.Properties.Resources.Zoomout_24;
            this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonZoomOut.Text = "toolStripButton1";
            this.toolStripButtonZoomOut.ToolTipText = "Zoom Out";
            this.toolStripButtonZoomOut.Click += new System.EventHandler(this.toolStripButtonZoomOut_Click);
            // 
            // toolStripButtonPan
            // 
            this.toolStripButtonPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPan.Image = global::MapManager.Properties.Resources.Pan24;
            this.toolStripButtonPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPan.Name = "toolStripButtonPan";
            this.toolStripButtonPan.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPan.Text = "toolStripButton1";
            this.toolStripButtonPan.ToolTipText = "Pan";
            this.toolStripButtonPan.Click += new System.EventHandler(this.toolStripButtonPan_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelect.Image = global::MapManager.Properties.Resources.Select;
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSelect.Text = "toolStripButton1";
            this.toolStripButtonSelect.ToolTipText = "Select Item";
            this.toolStripButtonSelect.Click += new System.EventHandler(this.toolStripButtonSelect_Click);
            // 
            // toolStripButtonSelectRect
            // 
            this.toolStripButtonSelectRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelectRect.Image = global::MapManager.Properties.Resources.SelectRect;
            this.toolStripButtonSelectRect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectRect.Name = "toolStripButtonSelectRect";
            this.toolStripButtonSelectRect.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSelectRect.Text = "toolStripButton1";
            this.toolStripButtonSelectRect.ToolTipText = "Select By Rectangle";
            this.toolStripButtonSelectRect.Click += new System.EventHandler(this.toolStripButtonSelectRect_Click);
            // 
            // toolStripButtonSelectPoly
            // 
            this.toolStripButtonSelectPoly.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelectPoly.Image = global::MapManager.Properties.Resources.SelectPoly;
            this.toolStripButtonSelectPoly.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectPoly.Name = "toolStripButtonSelectPoly";
            this.toolStripButtonSelectPoly.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSelectPoly.Text = "toolStripButton2";
            this.toolStripButtonSelectPoly.ToolTipText = "Select By Polygon";
            this.toolStripButtonSelectPoly.Click += new System.EventHandler(this.toolStripButtonSelectPoly_Click);
            // 
            // toolStripButtonSelectLine
            // 
            this.toolStripButtonSelectLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelectLine.Image = global::MapManager.Properties.Resources.SelectLine;
            this.toolStripButtonSelectLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectLine.Name = "toolStripButtonSelectLine";
            this.toolStripButtonSelectLine.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSelectLine.Text = "toolStripButton1";
            this.toolStripButtonSelectLine.ToolTipText = "Select By Line";
            this.toolStripButtonSelectLine.Visible = false;
            this.toolStripButtonSelectLine.Click += new System.EventHandler(this.toolStripButtonSelectLine_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonRedraw
            // 
            this.toolStripButtonRedraw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedraw.Image = global::MapManager.Properties.Resources.Refresh24;
            this.toolStripButtonRedraw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedraw.Name = "toolStripButtonRedraw";
            this.toolStripButtonRedraw.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRedraw.Text = "toolStripButton1";
            this.toolStripButtonRedraw.ToolTipText = "Redraw Map";
            this.toolStripButtonRedraw.Click += new System.EventHandler(this.toolStripButtonRedraw_Click);
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHelp.Image = global::MapManager.Properties.Resources.Help24;
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonHelp.Text = "toolStripButton1";
            this.toolStripButtonHelp.ToolTipText = "Help";
            // 
            // imageListTabs
            // 
            this.imageListTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabs.ImageStream")));
            this.imageListTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabs.Images.SetKeyName(0, "Globe16.png");
            this.imageListTabs.Images.SetKeyName(1, "Edit.png");
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 59);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer.Panel1MinSize = 320;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControlContents);
            this.splitContainer.Size = new System.Drawing.Size(1577, 801);
            this.splitContainer.SplitterDistance = 426;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.layerControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonApply);
            this.splitContainer1.Panel2.Controls.Add(this.layerPropertyEditor);
            this.splitContainer1.Panel2.Controls.Add(this.mapPropertyEditor);
            this.splitContainer1.Size = new System.Drawing.Size(426, 801);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // layerControl
            // 
            this.layerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerControl.IsStyleLibraryControl = false;
            this.layerControl.LegendIconPadding = new System.Windows.Forms.Padding(5);
            this.layerControl.LegendIconSize = new System.Drawing.Size(30, 20);
            this.layerControl.Location = new System.Drawing.Point(0, 0);
            this.layerControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.layerControl.Name = "layerControl";
            this.layerControl.ShowCheckBoxes = true;
            this.layerControl.ShowClasses = true;
            this.layerControl.ShowLabels = false;
            this.layerControl.ShowRootObject = true;
            this.layerControl.ShowStyles = false;
            this.layerControl.ShowToolbar = true;
            this.layerControl.Size = new System.Drawing.Size(424, 518);
            this.layerControl.TabIndex = 0;
            this.layerControl.Target = null;
            this.layerControl.ItemSelect += new DMS.MapLibrary.LayerControl.ItemSelectEventHandler(this.layerControl_ItemSelect);
            this.layerControl.GoToLayerText += new DMS.MapLibrary.LayerControl.GoToLayerTextEventHandler(this.layerControl_GoToLayerText);
            this.layerControl.EditProperties += new DMS.MapLibrary.EditPropertiesEventHandler(this.layerControl_EditProperties);
            this.layerControl.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.editor_HelpRequested);
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(295, 289);
            this.buttonApply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(93, 28);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // layerPropertyEditor
            // 
            this.layerPropertyEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layerPropertyEditor.Location = new System.Drawing.Point(0, 0);
            this.layerPropertyEditor.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.layerPropertyEditor.Name = "layerPropertyEditor";
            this.layerPropertyEditor.Size = new System.Drawing.Size(424, 287);
            this.layerPropertyEditor.TabIndex = 0;
            this.layerPropertyEditor.Target = null;
            this.layerPropertyEditor.Visible = false;
            this.layerPropertyEditor.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.editor_HelpRequested);
            this.layerPropertyEditor.EditProperties += new DMS.MapLibrary.EditPropertiesEventHandler(this.layerPropertyEditor_EditProperties);
            // 
            // mapPropertyEditor
            // 
            this.mapPropertyEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPropertyEditor.Location = new System.Drawing.Point(0, 0);
            this.mapPropertyEditor.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mapPropertyEditor.Name = "mapPropertyEditor";
            this.mapPropertyEditor.Size = new System.Drawing.Size(424, 287);
            this.mapPropertyEditor.TabIndex = 1;
            this.mapPropertyEditor.Target = null;
            this.mapPropertyEditor.Visible = false;
            this.mapPropertyEditor.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.editor_HelpRequested);
            this.mapPropertyEditor.EditProperties += new DMS.MapLibrary.EditPropertiesEventHandler(this.layerPropertyEditor_EditProperties);
            // 
            // tabControlContents
            // 
            this.tabControlContents.Controls.Add(this.tabPage1);
            this.tabControlContents.Controls.Add(this.tabPage2);
            this.tabControlContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlContents.ImageList = this.imageListTabs;
            this.tabControlContents.Location = new System.Drawing.Point(0, 0);
            this.tabControlContents.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlContents.Name = "tabControlContents";
            this.tabControlContents.SelectedIndex = 0;
            this.tabControlContents.Size = new System.Drawing.Size(1144, 799);
            this.tabControlContents.TabIndex = 0;
            this.tabControlContents.SelectedIndexChanged += new System.EventHandler(this.tabControlContents_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mapControl);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1136, 770);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Map";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mapControl
            // 
            this.mapControl.Border = false;
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.EnableRendering = true;
            this.mapControl.Gap = 10;
            this.mapControl.InputMode = DMS.MapLibrary.MapControl.InputModes.Pan;
            this.mapControl.Location = new System.Drawing.Point(4, 4);
            this.mapControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(1128, 762);
            this.mapControl.TabIndex = 1;
            this.mapControl.Target = null;
            this.mapControl.CursorMove += new DMS.MapLibrary.CursorMoveEventHandler(this.mapControl_CursorMove);
            this.mapControl.BeforeRefresh += new System.EventHandler(this.mapControl_BeforeRefresh);
            this.mapControl.AfterRefresh += new System.EventHandler(this.mapControl_AfterRefresh);
            this.mapControl.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.editor_HelpRequested);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.scintillaControl);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1136, 770);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Text";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // scintillaControl
            // 
            this.scintillaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaControl.Lexing.Lexer = ScintillaNet.Lexer.Null;
            this.scintillaControl.Lexing.LexerName = "automatic";
            this.scintillaControl.Lexing.LineCommentPrefix = "";
            this.scintillaControl.Lexing.StreamCommentPrefix = "";
            this.scintillaControl.Lexing.StreamCommentSufix = "";
            this.scintillaControl.Location = new System.Drawing.Point(4, 4);
            this.scintillaControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scintillaControl.Name = "scintillaControl";
            this.scintillaControl.Size = new System.Drawing.Size(1128, 762);
            this.scintillaControl.Styles.BraceBad.FontName = "Verdana";
            this.scintillaControl.Styles.BraceLight.FontName = "Verdana";
            this.scintillaControl.Styles.ControlChar.FontName = "Verdana";
            this.scintillaControl.Styles.Default.FontName = "Verdana";
            this.scintillaControl.Styles.IndentGuide.FontName = "Verdana";
            this.scintillaControl.Styles.LastPredefined.FontName = "Verdana";
            this.scintillaControl.Styles.LineNumber.FontName = "Verdana";
            this.scintillaControl.Styles.Max.FontName = "Verdana";
            this.scintillaControl.TabIndex = 0;
            this.scintillaControl.TextInserted += new System.EventHandler<ScintillaNet.TextModifiedEventArgs>(this.scintillaControl_TextInserted);
            this.scintillaControl.TextDeleted += new System.EventHandler<ScintillaNet.TextModifiedEventArgs>(this.scintillaControl_TextDeleted);
            this.scintillaControl.ZoomChanged += new System.EventHandler(this.scintillaControl_ZoomChanged);
            // 
            // selectByShapeToolStripMenuItem
            // 
            this.selectByShapeToolStripMenuItem.Name = "selectByShapeToolStripMenuItem";
            this.selectByShapeToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectByShapeToolStripMenuItem.Text = "Select By Sha&pe";
            this.selectByShapeToolStripMenuItem.Click += new System.EventHandler(this.selectByShapeToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1577, 885);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Icon = global::MapManager.Properties.Resources.MapManager;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapManager";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlContents.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scintillaControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SaveFileDialog saveFileDialogMain;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setInitialExtentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZoom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelScale;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerPanelToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DMS.MapLibrary.LayerControl layerControl;
        private System.Windows.Forms.ToolStripMenuItem overviewPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem redrawMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPos;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonPan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedraw;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mapPropertiesToolStripMenuItem;
        private DMS.MapLibrary.LayerPropertyEditor layerPropertyEditor;
        private DMS.MapLibrary.MapPropertyEditor mapPropertyEditor;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.ToolStripMenuItem selectItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem selectByRectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectByPolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectRect;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectPoly;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectLine;
        private System.Windows.Forms.ToolStripMenuItem selectByLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpContentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.ToolStripMenuItem openFileExternalToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private DMS.MapLibrary.MapControl mapControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageListTabs;
        private ScintillaNet.Scintilla scintillaControl;
        private System.Windows.Forms.TabControl tabControlContents;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem styleLibraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem tileManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkMapFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectByShapeToolStripMenuItem;
    }
}

