namespace DMS.MapManager
{
    partial class TileManagerForm
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
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.lblSavePath = new System.Windows.Forms.Label();
            this.dlgSaveFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelSaveFolder = new System.Windows.Forms.Panel();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.cmbNumLevels = new System.Windows.Forms.ComboBox();
            this.lblLevels = new System.Windows.Forms.Label();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.cmbNumTest = new System.Windows.Forms.ComboBox();
            this.txtTileSize = new System.Windows.Forms.TextBox();
            this.txtTPM = new System.Windows.Forms.TextBox();
            this.btnGenerateStats = new System.Windows.Forms.Button();
            this.lblAveSize = new System.Windows.Forms.Label();
            this.lblTPM = new System.Windows.Forms.Label();
            this.dgLevels = new System.Windows.Forms.DataGridView();
            this.dgLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgNumTiles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTimeEst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.lblActiveProcess = new System.Windows.Forms.Label();
            this.btnStopGen = new System.Windows.Forms.Button();
            this.grpBoxSettings = new System.Windows.Forms.GroupBox();
            this.lblNumProcesses = new System.Windows.Forms.Label();
            this.lblOverwrite = new System.Windows.Forms.Label();
            this.cmbOverwrite = new System.Windows.Forms.ComboBox();
            this.cmbImageFormat = new System.Windows.Forms.ComboBox();
            this.lblImageFormat = new System.Windows.Forms.Label();
            this.cmbImageBuffer = new System.Windows.Forms.ComboBox();
            this.lblImageBuffer = new System.Windows.Forms.Label();
            this.cmbImageSize = new System.Windows.Forms.ComboBox();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.cmbProcesses = new System.Windows.Forms.ComboBox();
            this.cmbLevels = new System.Windows.Forms.ComboBox();
            this.cmbSnapWorld = new System.Windows.Forms.ComboBox();
            this.lblNumLevels = new System.Windows.Forms.Label();
            this.lblCustom = new System.Windows.Forms.Label();
            this.txtPreMaxY = new System.Windows.Forms.TextBox();
            this.lblMaxY = new System.Windows.Forms.Label();
            this.txtPreMinY = new System.Windows.Forms.TextBox();
            this.lblMinY = new System.Windows.Forms.Label();
            this.txtPreMaxX = new System.Windows.Forms.TextBox();
            this.lblMaxX = new System.Windows.Forms.Label();
            this.txtPreMinX = new System.Windows.Forms.TextBox();
            this.lblMinX = new System.Windows.Forms.Label();
            this.lbl000 = new System.Windows.Forms.Label();
            this.btnBaseLayerText = new System.Windows.Forms.Button();
            this.imagePreview = new System.Windows.Forms.PictureBox();
            this.btnGenerateTiles = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineGoogleH = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineGoogleV = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.tabTileManager = new System.Windows.Forms.TabControl();
            this.cmbPreConfig = new System.Windows.Forms.ComboBox();
            this.lblConfiguration = new System.Windows.Forms.Label();
            this.btnDeleteConfig = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.timerProcessScan = new System.Windows.Forms.Timer(this.components);
            this.panelSaveFolder.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLevels)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.grpBoxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            this.tabTileManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(179, 10);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(490, 23);
            this.txtSavePath.TabIndex = 6;
            this.txtSavePath.Leave += new System.EventHandler(this.txtSavePath_Leave);
            // 
            // lblSavePath
            // 
            this.lblSavePath.AutoSize = true;
            this.lblSavePath.Location = new System.Drawing.Point(87, 13);
            this.lblSavePath.Name = "lblSavePath";
            this.lblSavePath.Size = new System.Drawing.Size(67, 15);
            this.lblSavePath.TabIndex = 16;
            this.lblSavePath.Text = "Save Folder";
            // 
            // dlgSaveFolder
            // 
            this.dlgSaveFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(676, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(28, 28);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelSaveFolder
            // 
            this.panelSaveFolder.Controls.Add(this.txtSavePath);
            this.panelSaveFolder.Controls.Add(this.lblSavePath);
            this.panelSaveFolder.Controls.Add(this.btnSave);
            this.panelSaveFolder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSaveFolder.Location = new System.Drawing.Point(0, 505);
            this.panelSaveFolder.Name = "panelSaveFolder";
            this.panelSaveFolder.Size = new System.Drawing.Size(804, 49);
            this.panelSaveFolder.TabIndex = 34;
            // 
            // tabStatistics
            // 
            this.tabStatistics.BackColor = System.Drawing.Color.Transparent;
            this.tabStatistics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabStatistics.Controls.Add(this.cmbNumLevels);
            this.tabStatistics.Controls.Add(this.lblLevels);
            this.tabStatistics.Controls.Add(this.grpStats);
            this.tabStatistics.Controls.Add(this.dgLevels);
            this.tabStatistics.Location = new System.Drawing.Point(4, 33);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatistics.Size = new System.Drawing.Size(796, 468);
            this.tabStatistics.TabIndex = 1;
            this.tabStatistics.Text = "Statistics";
            // 
            // cmbNumLevels
            // 
            this.cmbNumLevels.DropDownHeight = 100;
            this.cmbNumLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumLevels.FormattingEnabled = true;
            this.cmbNumLevels.IntegralHeight = false;
            this.cmbNumLevels.Location = new System.Drawing.Point(675, 56);
            this.cmbNumLevels.Name = "cmbNumLevels";
            this.cmbNumLevels.Size = new System.Drawing.Size(50, 23);
            this.cmbNumLevels.TabIndex = 43;
            this.cmbNumLevels.SelectedIndexChanged += new System.EventHandler(this.cmbNumLevels_SelectedIndexChanged);
            // 
            // lblLevels
            // 
            this.lblLevels.AutoSize = true;
            this.lblLevels.Location = new System.Drawing.Point(676, 35);
            this.lblLevels.Name = "lblLevels";
            this.lblLevels.Size = new System.Drawing.Size(39, 15);
            this.lblLevels.TabIndex = 30;
            this.lblLevels.Text = "Levels";
            // 
            // grpStats
            // 
            this.grpStats.Controls.Add(this.cmbNumTest);
            this.grpStats.Controls.Add(this.txtTileSize);
            this.grpStats.Controls.Add(this.txtTPM);
            this.grpStats.Controls.Add(this.btnGenerateStats);
            this.grpStats.Controls.Add(this.lblAveSize);
            this.grpStats.Controls.Add(this.lblTPM);
            this.grpStats.Location = new System.Drawing.Point(16, 20);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(744, 77);
            this.grpStats.TabIndex = 28;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Tile Generation Statistics";
            // 
            // cmbNumTest
            // 
            this.cmbNumTest.FormattingEnabled = true;
            this.cmbNumTest.Items.AddRange(new object[] {
            "250",
            "500",
            "1000",
            "2000"});
            this.cmbNumTest.Location = new System.Drawing.Point(126, 34);
            this.cmbNumTest.Name = "cmbNumTest";
            this.cmbNumTest.Size = new System.Drawing.Size(60, 23);
            this.cmbNumTest.TabIndex = 43;
            this.cmbNumTest.Text = "500";
            // 
            // txtTileSize
            // 
            this.txtTileSize.Enabled = false;
            this.txtTileSize.Location = new System.Drawing.Point(553, 36);
            this.txtTileSize.Name = "txtTileSize";
            this.txtTileSize.Size = new System.Drawing.Size(80, 23);
            this.txtTileSize.TabIndex = 42;
            // 
            // txtTPM
            // 
            this.txtTPM.Enabled = false;
            this.txtTPM.Location = new System.Drawing.Point(331, 35);
            this.txtTPM.Name = "txtTPM";
            this.txtTPM.Size = new System.Drawing.Size(80, 23);
            this.txtTPM.TabIndex = 41;
            // 
            // btnGenerateStats
            // 
            this.btnGenerateStats.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGenerateStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateStats.Location = new System.Drawing.Point(20, 30);
            this.btnGenerateStats.Name = "btnGenerateStats";
            this.btnGenerateStats.Size = new System.Drawing.Size(100, 30);
            this.btnGenerateStats.TabIndex = 40;
            this.btnGenerateStats.Text = "Calculate";
            this.btnGenerateStats.UseVisualStyleBackColor = false;
            this.btnGenerateStats.Click += new System.EventHandler(this.btnGenerateStats_Click);
            // 
            // lblAveSize
            // 
            this.lblAveSize.AutoSize = true;
            this.lblAveSize.Location = new System.Drawing.Point(424, 38);
            this.lblAveSize.Name = "lblAveSize";
            this.lblAveSize.Size = new System.Drawing.Size(98, 15);
            this.lblAveSize.TabIndex = 24;
            this.lblAveSize.Text = "Average Tile Size:";
            // 
            // lblTPM
            // 
            this.lblTPM.AutoSize = true;
            this.lblTPM.Location = new System.Drawing.Point(213, 38);
            this.lblTPM.Name = "lblTPM";
            this.lblTPM.Size = new System.Drawing.Size(95, 15);
            this.lblTPM.TabIndex = 25;
            this.lblTPM.Text = "Tiles Per Minute:";
            // 
            // dgLevels
            // 
            this.dgLevels.AllowUserToAddRows = false;
            this.dgLevels.AllowUserToDeleteRows = false;
            this.dgLevels.AllowUserToResizeColumns = false;
            this.dgLevels.AllowUserToResizeRows = false;
            this.dgLevels.ColumnHeadersHeight = 30;
            this.dgLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgLevels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgLevel,
            this.dgScale,
            this.dgNumTiles,
            this.dgFileSize,
            this.dgTimeEst});
            this.dgLevels.Location = new System.Drawing.Point(16, 106);
            this.dgLevels.Name = "dgLevels";
            this.dgLevels.RowTemplate.Height = 24;
            this.dgLevels.Size = new System.Drawing.Size(744, 346);
            this.dgLevels.TabIndex = 44;
            // 
            // dgLevel
            // 
            this.dgLevel.HeaderText = "Level";
            this.dgLevel.Name = "dgLevel";
            this.dgLevel.ReadOnly = true;
            this.dgLevel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgScale
            // 
            this.dgScale.HeaderText = "Scale";
            this.dgScale.Name = "dgScale";
            this.dgScale.ReadOnly = true;
            this.dgScale.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgScale.Width = 150;
            // 
            // dgNumTiles
            // 
            this.dgNumTiles.HeaderText = "Number of Tiles";
            this.dgNumTiles.Name = "dgNumTiles";
            this.dgNumTiles.ReadOnly = true;
            this.dgNumTiles.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgNumTiles.Width = 150;
            // 
            // dgFileSize
            // 
            this.dgFileSize.HeaderText = "Estimated Disk Size";
            this.dgFileSize.Name = "dgFileSize";
            this.dgFileSize.ReadOnly = true;
            this.dgFileSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgFileSize.Width = 150;
            // 
            // dgTimeEst
            // 
            this.dgTimeEst.HeaderText = "Estimated Time";
            this.dgTimeEst.Name = "dgTimeEst";
            this.dgTimeEst.ReadOnly = true;
            this.dgTimeEst.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgTimeEst.Width = 150;
            // 
            // tabSettings
            // 
            this.tabSettings.BackColor = System.Drawing.Color.Transparent;
            this.tabSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabSettings.Controls.Add(this.lblActiveProcess);
            this.tabSettings.Controls.Add(this.btnStopGen);
            this.tabSettings.Controls.Add(this.grpBoxSettings);
            this.tabSettings.Controls.Add(this.txtPreMaxY);
            this.tabSettings.Controls.Add(this.lblMaxY);
            this.tabSettings.Controls.Add(this.txtPreMinY);
            this.tabSettings.Controls.Add(this.lblMinY);
            this.tabSettings.Controls.Add(this.txtPreMaxX);
            this.tabSettings.Controls.Add(this.lblMaxX);
            this.tabSettings.Controls.Add(this.txtPreMinX);
            this.tabSettings.Controls.Add(this.lblMinX);
            this.tabSettings.Controls.Add(this.lbl000);
            this.tabSettings.Controls.Add(this.btnBaseLayerText);
            this.tabSettings.Controls.Add(this.imagePreview);
            this.tabSettings.Controls.Add(this.btnGenerateTiles);
            this.tabSettings.Controls.Add(this.shapeContainer1);
            this.tabSettings.Location = new System.Drawing.Point(4, 33);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(796, 468);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "Settings";
            // 
            // lblActiveProcess
            // 
            this.lblActiveProcess.AutoSize = true;
            this.lblActiveProcess.Location = new System.Drawing.Point(312, 433);
            this.lblActiveProcess.Name = "lblActiveProcess";
            this.lblActiveProcess.Size = new System.Drawing.Size(187, 15);
            this.lblActiveProcess.TabIndex = 71;
            this.lblActiveProcess.Text = "No TileGenerator Processes Active";
            // 
            // btnStopGen
            // 
            this.btnStopGen.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnStopGen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopGen.Location = new System.Drawing.Point(504, 393);
            this.btnStopGen.Name = "btnStopGen";
            this.btnStopGen.Size = new System.Drawing.Size(170, 30);
            this.btnStopGen.TabIndex = 70;
            this.btnStopGen.Text = "Stop Tile Generation";
            this.btnStopGen.UseVisualStyleBackColor = false;
            this.btnStopGen.Click += new System.EventHandler(this.btnStopGen_Click);
            // 
            // grpBoxSettings
            // 
            this.grpBoxSettings.Controls.Add(this.lblNumProcesses);
            this.grpBoxSettings.Controls.Add(this.lblOverwrite);
            this.grpBoxSettings.Controls.Add(this.cmbOverwrite);
            this.grpBoxSettings.Controls.Add(this.cmbImageFormat);
            this.grpBoxSettings.Controls.Add(this.lblImageFormat);
            this.grpBoxSettings.Controls.Add(this.cmbImageBuffer);
            this.grpBoxSettings.Controls.Add(this.lblImageBuffer);
            this.grpBoxSettings.Controls.Add(this.cmbImageSize);
            this.grpBoxSettings.Controls.Add(this.lblImageSize);
            this.grpBoxSettings.Controls.Add(this.cmbProcesses);
            this.grpBoxSettings.Controls.Add(this.cmbLevels);
            this.grpBoxSettings.Controls.Add(this.cmbSnapWorld);
            this.grpBoxSettings.Controls.Add(this.lblNumLevels);
            this.grpBoxSettings.Controls.Add(this.lblCustom);
            this.grpBoxSettings.Location = new System.Drawing.Point(42, 16);
            this.grpBoxSettings.Name = "grpBoxSettings";
            this.grpBoxSettings.Size = new System.Drawing.Size(310, 278);
            this.grpBoxSettings.TabIndex = 69;
            this.grpBoxSettings.TabStop = false;
            this.grpBoxSettings.Text = "Generation Settings";
            // 
            // lblNumProcesses
            // 
            this.lblNumProcesses.AutoSize = true;
            this.lblNumProcesses.Location = new System.Drawing.Point(17, 33);
            this.lblNumProcesses.Name = "lblNumProcesses";
            this.lblNumProcesses.Size = new System.Drawing.Size(119, 15);
            this.lblNumProcesses.TabIndex = 1;
            this.lblNumProcesses.Text = "Number of Processes";
            // 
            // lblOverwrite
            // 
            this.lblOverwrite.AutoSize = true;
            this.lblOverwrite.Location = new System.Drawing.Point(17, 101);
            this.lblOverwrite.Name = "lblOverwrite";
            this.lblOverwrite.Size = new System.Drawing.Size(99, 15);
            this.lblOverwrite.TabIndex = 14;
            this.lblOverwrite.Text = "Overwrite Images";
            // 
            // cmbOverwrite
            // 
            this.cmbOverwrite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOverwrite.FormattingEnabled = true;
            this.cmbOverwrite.Location = new System.Drawing.Point(191, 98);
            this.cmbOverwrite.Name = "cmbOverwrite";
            this.cmbOverwrite.Size = new System.Drawing.Size(100, 23);
            this.cmbOverwrite.TabIndex = 22;
            // 
            // cmbImageFormat
            // 
            this.cmbImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageFormat.FormattingEnabled = true;
            this.cmbImageFormat.Location = new System.Drawing.Point(191, 200);
            this.cmbImageFormat.Name = "cmbImageFormat";
            this.cmbImageFormat.Size = new System.Drawing.Size(100, 23);
            this.cmbImageFormat.TabIndex = 25;
            // 
            // lblImageFormat
            // 
            this.lblImageFormat.AutoSize = true;
            this.lblImageFormat.Location = new System.Drawing.Point(17, 203);
            this.lblImageFormat.Name = "lblImageFormat";
            this.lblImageFormat.Size = new System.Drawing.Size(81, 15);
            this.lblImageFormat.TabIndex = 10;
            this.lblImageFormat.Text = "Image Format";
            // 
            // cmbImageBuffer
            // 
            this.cmbImageBuffer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageBuffer.FormattingEnabled = true;
            this.cmbImageBuffer.Location = new System.Drawing.Point(191, 166);
            this.cmbImageBuffer.Name = "cmbImageBuffer";
            this.cmbImageBuffer.Size = new System.Drawing.Size(100, 23);
            this.cmbImageBuffer.TabIndex = 24;
            // 
            // lblImageBuffer
            // 
            this.lblImageBuffer.AutoSize = true;
            this.lblImageBuffer.Location = new System.Drawing.Point(17, 169);
            this.lblImageBuffer.Name = "lblImageBuffer";
            this.lblImageBuffer.Size = new System.Drawing.Size(120, 15);
            this.lblImageBuffer.TabIndex = 8;
            this.lblImageBuffer.Text = "Image Buffer in Pixels";
            // 
            // cmbImageSize
            // 
            this.cmbImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageSize.FormattingEnabled = true;
            this.cmbImageSize.Location = new System.Drawing.Point(191, 132);
            this.cmbImageSize.Name = "cmbImageSize";
            this.cmbImageSize.Size = new System.Drawing.Size(100, 23);
            this.cmbImageSize.TabIndex = 23;
            // 
            // lblImageSize
            // 
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.Location = new System.Drawing.Point(17, 135);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(108, 15);
            this.lblImageSize.TabIndex = 6;
            this.lblImageSize.Text = "Image Size in Pixels";
            // 
            // cmbProcesses
            // 
            this.cmbProcesses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcesses.FormattingEnabled = true;
            this.cmbProcesses.Location = new System.Drawing.Point(191, 30);
            this.cmbProcesses.Name = "cmbProcesses";
            this.cmbProcesses.Size = new System.Drawing.Size(100, 23);
            this.cmbProcesses.TabIndex = 20;
            // 
            // cmbLevels
            // 
            this.cmbLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevels.FormattingEnabled = true;
            this.cmbLevels.Location = new System.Drawing.Point(191, 64);
            this.cmbLevels.Name = "cmbLevels";
            this.cmbLevels.Size = new System.Drawing.Size(100, 23);
            this.cmbLevels.TabIndex = 21;
            this.cmbLevels.SelectedIndexChanged += new System.EventHandler(this.cmbLevels_SelectedIndexChanged);
            // 
            // cmbSnapWorld
            // 
            this.cmbSnapWorld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSnapWorld.FormattingEnabled = true;
            this.cmbSnapWorld.Location = new System.Drawing.Point(191, 234);
            this.cmbSnapWorld.Name = "cmbSnapWorld";
            this.cmbSnapWorld.Size = new System.Drawing.Size(100, 23);
            this.cmbSnapWorld.TabIndex = 26;
            this.cmbSnapWorld.SelectedIndexChanged += new System.EventHandler(this.cmbSnapWorldChanged);
            // 
            // lblNumLevels
            // 
            this.lblNumLevels.AutoSize = true;
            this.lblNumLevels.Location = new System.Drawing.Point(17, 67);
            this.lblNumLevels.Name = "lblNumLevels";
            this.lblNumLevels.Size = new System.Drawing.Size(100, 15);
            this.lblNumLevels.TabIndex = 3;
            this.lblNumLevels.Text = "Number of Levels";
            // 
            // lblCustom
            // 
            this.lblCustom.AutoSize = true;
            this.lblCustom.Location = new System.Drawing.Point(17, 237);
            this.lblCustom.Name = "lblCustom";
            this.lblCustom.Size = new System.Drawing.Size(80, 15);
            this.lblCustom.TabIndex = 29;
            this.lblCustom.Text = "Snap to world";
            // 
            // txtPreMaxY
            // 
            this.txtPreMaxY.Enabled = false;
            this.txtPreMaxY.Location = new System.Drawing.Point(533, 351);
            this.txtPreMaxY.Name = "txtPreMaxY";
            this.txtPreMaxY.Size = new System.Drawing.Size(150, 23);
            this.txtPreMaxY.TabIndex = 30;
            // 
            // lblMaxY
            // 
            this.lblMaxY.AutoSize = true;
            this.lblMaxY.Location = new System.Drawing.Point(419, 354);
            this.lblMaxY.Name = "lblMaxY";
            this.lblMaxY.Size = new System.Drawing.Size(74, 15);
            this.lblMaxY.TabIndex = 67;
            this.lblMaxY.Text = "Extent Max Y";
            // 
            // txtPreMinY
            // 
            this.txtPreMinY.Enabled = false;
            this.txtPreMinY.Location = new System.Drawing.Point(533, 314);
            this.txtPreMinY.Name = "txtPreMinY";
            this.txtPreMinY.Size = new System.Drawing.Size(150, 23);
            this.txtPreMinY.TabIndex = 29;
            // 
            // lblMinY
            // 
            this.lblMinY.AutoSize = true;
            this.lblMinY.Location = new System.Drawing.Point(419, 317);
            this.lblMinY.Name = "lblMinY";
            this.lblMinY.Size = new System.Drawing.Size(73, 15);
            this.lblMinY.TabIndex = 65;
            this.lblMinY.Text = "Extent Min Y";
            // 
            // txtPreMaxX
            // 
            this.txtPreMaxX.Enabled = false;
            this.txtPreMaxX.Location = new System.Drawing.Point(212, 351);
            this.txtPreMaxX.Name = "txtPreMaxX";
            this.txtPreMaxX.Size = new System.Drawing.Size(150, 23);
            this.txtPreMaxX.TabIndex = 28;
            // 
            // lblMaxX
            // 
            this.lblMaxX.AutoSize = true;
            this.lblMaxX.Location = new System.Drawing.Point(94, 354);
            this.lblMaxX.Name = "lblMaxX";
            this.lblMaxX.Size = new System.Drawing.Size(74, 15);
            this.lblMaxX.TabIndex = 63;
            this.lblMaxX.Text = "Extent Max X";
            // 
            // txtPreMinX
            // 
            this.txtPreMinX.Enabled = false;
            this.txtPreMinX.Location = new System.Drawing.Point(212, 314);
            this.txtPreMinX.Name = "txtPreMinX";
            this.txtPreMinX.Size = new System.Drawing.Size(150, 23);
            this.txtPreMinX.TabIndex = 27;
            // 
            // lblMinX
            // 
            this.lblMinX.AutoSize = true;
            this.lblMinX.Location = new System.Drawing.Point(94, 317);
            this.lblMinX.Name = "lblMinX";
            this.lblMinX.Size = new System.Drawing.Size(73, 15);
            this.lblMinX.TabIndex = 61;
            this.lblMinX.Text = "Extent Min X";
            // 
            // lbl000
            // 
            this.lbl000.AutoSize = true;
            this.lbl000.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl000.Location = new System.Drawing.Point(528, 12);
            this.lbl000.Name = "lbl000";
            this.lbl000.Size = new System.Drawing.Size(101, 15);
            this.lbl000.TabIndex = 34;
            this.lbl000.Text = "0/0/0 Tile Preview";
            // 
            // btnBaseLayerText
            // 
            this.btnBaseLayerText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBaseLayerText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaseLayerText.Location = new System.Drawing.Point(128, 393);
            this.btnBaseLayerText.Name = "btnBaseLayerText";
            this.btnBaseLayerText.Size = new System.Drawing.Size(170, 30);
            this.btnBaseLayerText.TabIndex = 31;
            this.btnBaseLayerText.Text = "Base Map Text";
            this.btnBaseLayerText.UseVisualStyleBackColor = false;
            this.btnBaseLayerText.Click += new System.EventHandler(this.btnBaseLayerText_Click);
            // 
            // imagePreview
            // 
            this.imagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePreview.Location = new System.Drawing.Point(450, 35);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(256, 256);
            this.imagePreview.TabIndex = 21;
            this.imagePreview.TabStop = false;
            // 
            // btnGenerateTiles
            // 
            this.btnGenerateTiles.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGenerateTiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateTiles.Location = new System.Drawing.Point(316, 393);
            this.btnGenerateTiles.Name = "btnGenerateTiles";
            this.btnGenerateTiles.Size = new System.Drawing.Size(170, 30);
            this.btnGenerateTiles.TabIndex = 32;
            this.btnGenerateTiles.Text = "Generate Tiles";
            this.btnGenerateTiles.UseVisualStyleBackColor = false;
            this.btnGenerateTiles.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineGoogleH,
            this.lineGoogleV});
            this.shapeContainer1.Size = new System.Drawing.Size(788, 460);
            this.shapeContainer1.TabIndex = 33;
            this.shapeContainer1.TabStop = false;
            // 
            // lineGoogleH
            // 
            this.lineGoogleH.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lineGoogleH.Name = "lineGoogleH";
            this.lineGoogleH.X1 = 447;
            this.lineGoogleH.X2 = 703;
            this.lineGoogleH.Y1 = 160;
            this.lineGoogleH.Y2 = 160;
            // 
            // lineGoogleV
            // 
            this.lineGoogleV.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lineGoogleV.Name = "lineGoogleV";
            this.lineGoogleV.X1 = 578;
            this.lineGoogleV.X2 = 578;
            this.lineGoogleV.Y1 = 32;
            this.lineGoogleV.Y2 = 288;
            // 
            // tabTileManager
            // 
            this.tabTileManager.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabTileManager.Controls.Add(this.tabSettings);
            this.tabTileManager.Controls.Add(this.tabStatistics);
            this.tabTileManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTileManager.HotTrack = true;
            this.tabTileManager.Location = new System.Drawing.Point(0, 0);
            this.tabTileManager.Name = "tabTileManager";
            this.tabTileManager.Padding = new System.Drawing.Point(6, 6);
            this.tabTileManager.SelectedIndex = 0;
            this.tabTileManager.Size = new System.Drawing.Size(804, 505);
            this.tabTileManager.TabIndex = 31;
            this.tabTileManager.SelectedIndexChanged += new System.EventHandler(this.tabTileManager_SelectedIndexChanged);
            // 
            // cmbPreConfig
            // 
            this.cmbPreConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreConfig.FormattingEnabled = true;
            this.cmbPreConfig.Location = new System.Drawing.Point(292, 5);
            this.cmbPreConfig.Name = "cmbPreConfig";
            this.cmbPreConfig.Size = new System.Drawing.Size(245, 23);
            this.cmbPreConfig.TabIndex = 2;
            this.cmbPreConfig.SelectedIndexChanged += new System.EventHandler(this.cmbPreConfig_SelectedIndexChanged);
            // 
            // lblConfiguration
            // 
            this.lblConfiguration.AutoSize = true;
            this.lblConfiguration.Location = new System.Drawing.Point(195, 9);
            this.lblConfiguration.Name = "lblConfiguration";
            this.lblConfiguration.Size = new System.Drawing.Size(81, 15);
            this.lblConfiguration.TabIndex = 52;
            this.lblConfiguration.Text = "Configuration";
            // 
            // btnDeleteConfig
            // 
            this.btnDeleteConfig.FlatAppearance.BorderSize = 0;
            this.btnDeleteConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteConfig.Image = global::MapManager.Properties.Resources.Delete24;
            this.btnDeleteConfig.Location = new System.Drawing.Point(591, 3);
            this.btnDeleteConfig.Name = "btnDeleteConfig";
            this.btnDeleteConfig.Size = new System.Drawing.Size(24, 24);
            this.btnDeleteConfig.TabIndex = 5;
            this.btnDeleteConfig.UseVisualStyleBackColor = false;
            this.btnDeleteConfig.Click += new System.EventHandler(this.btnDeleteConfig_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.FlatAppearance.BorderSize = 0;
            this.btnSaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConfig.Image = global::MapManager.Properties.Resources.Save24;
            this.btnSaveConfig.Location = new System.Drawing.Point(558, 3);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(24, 24);
            this.btnSaveConfig.TabIndex = 4;
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // timerProcessScan
            // 
            this.timerProcessScan.Tick += new System.EventHandler(this.timerProcessScan_Tick);
            // 
            // TileManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 554);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.cmbPreConfig);
            this.Controls.Add(this.btnDeleteConfig);
            this.Controls.Add(this.lblConfiguration);
            this.Controls.Add(this.tabTileManager);
            this.Controls.Add(this.panelSaveFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::MapManager.Properties.Resources.MapManager;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TileManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tile Manager";
            this.Activated += new System.EventHandler(this.TileManagerForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TileManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.TileManagerForm_Load);
            this.panelSaveFolder.ResumeLayout(false);
            this.panelSaveFolder.PerformLayout();
            this.tabStatistics.ResumeLayout(false);
            this.tabStatistics.PerformLayout();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLevels)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.grpBoxSettings.ResumeLayout(false);
            this.grpBoxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).EndInit();
            this.tabTileManager.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Label lblSavePath;
        private System.Windows.Forms.FolderBrowserDialog dlgSaveFolder;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelSaveFolder;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.ComboBox cmbNumLevels;
        private System.Windows.Forms.Label lblLevels;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.TextBox txtTileSize;
        private System.Windows.Forms.TextBox txtTPM;
        private System.Windows.Forms.Button btnGenerateStats;
        private System.Windows.Forms.Label lblAveSize;
        private System.Windows.Forms.Label lblTPM;
        private System.Windows.Forms.DataGridView dgLevels;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgNumTiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTimeEst;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TextBox txtPreMaxY;
        private System.Windows.Forms.Label lblMaxY;
        private System.Windows.Forms.TextBox txtPreMinY;
        private System.Windows.Forms.Label lblMinY;
        private System.Windows.Forms.TextBox txtPreMaxX;
        private System.Windows.Forms.Label lblMaxX;
        private System.Windows.Forms.TextBox txtPreMinX;
        private System.Windows.Forms.Label lblMinX;
        private System.Windows.Forms.Label lbl000;
        private System.Windows.Forms.Button btnBaseLayerText;
        private System.Windows.Forms.ComboBox cmbSnapWorld;
        private System.Windows.Forms.Label lblCustom;
        private System.Windows.Forms.PictureBox imagePreview;
        private System.Windows.Forms.Label lblNumProcesses;
        private System.Windows.Forms.Button btnGenerateTiles;
        private System.Windows.Forms.Label lblNumLevels;
        private System.Windows.Forms.ComboBox cmbLevels;
        private System.Windows.Forms.ComboBox cmbProcesses;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.ComboBox cmbImageSize;
        private System.Windows.Forms.Label lblImageBuffer;
        private System.Windows.Forms.ComboBox cmbImageBuffer;
        private System.Windows.Forms.Label lblImageFormat;
        private System.Windows.Forms.ComboBox cmbImageFormat;
        private System.Windows.Forms.ComboBox cmbOverwrite;
        private System.Windows.Forms.Label lblOverwrite;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineGoogleH;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineGoogleV;
        private System.Windows.Forms.TabControl tabTileManager;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.ComboBox cmbPreConfig;
        private System.Windows.Forms.Label lblConfiguration;
        private System.Windows.Forms.Button btnDeleteConfig;
        private System.Windows.Forms.GroupBox grpBoxSettings;
        private System.Windows.Forms.ComboBox cmbNumTest;
        private System.Windows.Forms.Label lblActiveProcess;
        private System.Windows.Forms.Button btnStopGen;
        private System.Windows.Forms.Timer timerProcessScan;
    }
}