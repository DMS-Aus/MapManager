namespace DMS.MapLibrary
{
    partial class MapPropertyEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.buttonImagepath = new System.Windows.Forms.Button();
            this.textBoxImagepath = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.buttonFontset = new System.Windows.Forms.Button();
            this.buttonSymbolset = new System.Windows.Forms.Button();
            this.buttonShapePath = new System.Windows.Forms.Button();
            this.textBoxFontset = new System.Windows.Forms.TextBox();
            this.textBoxSymbolset = new System.Windows.Forms.TextBox();
            this.textBoxShapePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageExtent = new System.Windows.Forms.TabPage();
            this.labelUnit2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.labelUnit1 = new System.Windows.Forms.Label();
            this.textBoxZoomWidth = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPageProjection = new System.Windows.Forms.TabPage();
            this.buttonProjection = new System.Windows.Forms.Button();
            this.comboBoxUnits = new System.Windows.Forms.ComboBox();
            this.textBoxProjection = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.checkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxResolution = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonQueryMap = new System.Windows.Forms.Button();
            this.buttonScalebar = new System.Windows.Forms.Button();
            this.comboBoxImageType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.colorPickerBackColor = new DMS.MapLibrary.ColorPicker();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageExtent.SuspendLayout();
            this.tabPageProjection.SuspendLayout();
            this.tabPageImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageExtent);
            this.tabControl1.Controls.Add(this.tabPageProjection);
            this.tabControl1.Controls.Add(this.tabPageImage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(320, 203);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.buttonImagepath);
            this.tabPageGeneral.Controls.Add(this.textBoxImagepath);
            this.tabPageGeneral.Controls.Add(this.label18);
            this.tabPageGeneral.Controls.Add(this.buttonFontset);
            this.tabPageGeneral.Controls.Add(this.buttonSymbolset);
            this.tabPageGeneral.Controls.Add(this.buttonShapePath);
            this.tabPageGeneral.Controls.Add(this.textBoxFontset);
            this.tabPageGeneral.Controls.Add(this.textBoxSymbolset);
            this.tabPageGeneral.Controls.Add(this.textBoxShapePath);
            this.tabPageGeneral.Controls.Add(this.label4);
            this.tabPageGeneral.Controls.Add(this.label3);
            this.tabPageGeneral.Controls.Add(this.label2);
            this.tabPageGeneral.Controls.Add(this.textBoxName);
            this.tabPageGeneral.Controls.Add(this.label1);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(312, 177);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // buttonImagepath
            // 
            this.buttonImagepath.Location = new System.Drawing.Point(279, 137);
            this.buttonImagepath.Name = "buttonImagepath";
            this.buttonImagepath.Size = new System.Drawing.Size(25, 23);
            this.buttonImagepath.TabIndex = 13;
            this.buttonImagepath.Text = "...";
            this.buttonImagepath.UseVisualStyleBackColor = true;
            this.buttonImagepath.Click += new System.EventHandler(this.buttonImagepath_Click);
            // 
            // textBoxImagepath
            // 
            this.textBoxImagepath.Location = new System.Drawing.Point(72, 140);
            this.textBoxImagepath.Name = "textBoxImagepath";
            this.textBoxImagepath.Size = new System.Drawing.Size(201, 20);
            this.textBoxImagepath.TabIndex = 12;
            this.textBoxImagepath.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(2, 143);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 13);
            this.label18.TabIndex = 11;
            this.label18.Text = "Image Path:";
            // 
            // buttonFontset
            // 
            this.buttonFontset.Location = new System.Drawing.Point(279, 107);
            this.buttonFontset.Name = "buttonFontset";
            this.buttonFontset.Size = new System.Drawing.Size(25, 23);
            this.buttonFontset.TabIndex = 10;
            this.buttonFontset.Text = "...";
            this.buttonFontset.UseVisualStyleBackColor = true;
            this.buttonFontset.Click += new System.EventHandler(this.buttonFontset_Click);
            // 
            // buttonSymbolset
            // 
            this.buttonSymbolset.Location = new System.Drawing.Point(279, 78);
            this.buttonSymbolset.Name = "buttonSymbolset";
            this.buttonSymbolset.Size = new System.Drawing.Size(25, 23);
            this.buttonSymbolset.TabIndex = 7;
            this.buttonSymbolset.Text = "...";
            this.buttonSymbolset.UseVisualStyleBackColor = true;
            this.buttonSymbolset.Click += new System.EventHandler(this.buttonSymbolset_Click);
            // 
            // buttonShapePath
            // 
            this.buttonShapePath.Location = new System.Drawing.Point(279, 47);
            this.buttonShapePath.Name = "buttonShapePath";
            this.buttonShapePath.Size = new System.Drawing.Size(25, 23);
            this.buttonShapePath.TabIndex = 4;
            this.buttonShapePath.Text = "...";
            this.buttonShapePath.UseVisualStyleBackColor = true;
            this.buttonShapePath.Click += new System.EventHandler(this.buttonShapePath_Click);
            // 
            // textBoxFontset
            // 
            this.textBoxFontset.Location = new System.Drawing.Point(72, 110);
            this.textBoxFontset.Name = "textBoxFontset";
            this.textBoxFontset.Size = new System.Drawing.Size(201, 20);
            this.textBoxFontset.TabIndex = 9;
            this.textBoxFontset.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // textBoxSymbolset
            // 
            this.textBoxSymbolset.Location = new System.Drawing.Point(72, 80);
            this.textBoxSymbolset.Name = "textBoxSymbolset";
            this.textBoxSymbolset.Size = new System.Drawing.Size(201, 20);
            this.textBoxSymbolset.TabIndex = 6;
            this.textBoxSymbolset.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // textBoxShapePath
            // 
            this.textBoxShapePath.Location = new System.Drawing.Point(72, 49);
            this.textBoxShapePath.Name = "textBoxShapePath";
            this.textBoxShapePath.Size = new System.Drawing.Size(201, 20);
            this.textBoxShapePath.TabIndex = 3;
            this.textBoxShapePath.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Fontset:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Symbolset:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ShapePath:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(72, 19);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(201, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map Name:";
            // 
            // tabPageExtent
            // 
            this.tabPageExtent.Controls.Add(this.labelUnit2);
            this.tabPageExtent.Controls.Add(this.label6);
            this.tabPageExtent.Controls.Add(this.textBoxY);
            this.tabPageExtent.Controls.Add(this.label5);
            this.tabPageExtent.Controls.Add(this.textBoxX);
            this.tabPageExtent.Controls.Add(this.label7);
            this.tabPageExtent.Controls.Add(this.label8);
            this.tabPageExtent.Controls.Add(this.textBoxScale);
            this.tabPageExtent.Controls.Add(this.label16);
            this.tabPageExtent.Controls.Add(this.labelUnit1);
            this.tabPageExtent.Controls.Add(this.textBoxZoomWidth);
            this.tabPageExtent.Controls.Add(this.label17);
            this.tabPageExtent.Location = new System.Drawing.Point(4, 22);
            this.tabPageExtent.Name = "tabPageExtent";
            this.tabPageExtent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExtent.Size = new System.Drawing.Size(312, 177);
            this.tabPageExtent.TabIndex = 1;
            this.tabPageExtent.Text = "Map Extent";
            this.tabPageExtent.UseVisualStyleBackColor = true;
            // 
            // labelUnit2
            // 
            this.labelUnit2.Location = new System.Drawing.Point(272, 116);
            this.labelUnit2.Name = "labelUnit2";
            this.labelUnit2.Size = new System.Drawing.Size(36, 13);
            this.labelUnit2.TabIndex = 11;
            this.labelUnit2.Text = "m";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(102, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Y:";
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(124, 125);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(139, 20);
            this.textBoxY.TabIndex = 10;
            this.textBoxY.TextChanged += new System.EventHandler(this.ExtentChanging);
            this.textBoxY.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "X:";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(124, 101);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(139, 20);
            this.textBoxX.TabIndex = 8;
            this.textBoxX.TextChanged += new System.EventHandler(this.ExtentChanging);
            this.textBoxX.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Centre of window:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(102, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "1:";
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(124, 67);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(139, 20);
            this.textBoxScale.TabIndex = 5;
            this.textBoxScale.TextChanged += new System.EventHandler(this.ExtentChanging);
            this.textBoxScale.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScale_Validating);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Map scale:";
            // 
            // labelUnit1
            // 
            this.labelUnit1.Location = new System.Drawing.Point(272, 35);
            this.labelUnit1.Name = "labelUnit1";
            this.labelUnit1.Size = new System.Drawing.Size(36, 13);
            this.labelUnit1.TabIndex = 2;
            this.labelUnit1.Text = "m";
            // 
            // textBoxZoomWidth
            // 
            this.textBoxZoomWidth.Location = new System.Drawing.Point(124, 32);
            this.textBoxZoomWidth.Name = "textBoxZoomWidth";
            this.textBoxZoomWidth.Size = new System.Drawing.Size(139, 20);
            this.textBoxZoomWidth.TabIndex = 1;
            this.textBoxZoomWidth.TextChanged += new System.EventHandler(this.ExtentChanging);
            this.textBoxZoomWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxZoomWidth_Validating);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 35);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(113, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Zoom (Window width):";
            // 
            // tabPageProjection
            // 
            this.tabPageProjection.Controls.Add(this.buttonProjection);
            this.tabPageProjection.Controls.Add(this.comboBoxUnits);
            this.tabPageProjection.Controls.Add(this.textBoxProjection);
            this.tabPageProjection.Controls.Add(this.label12);
            this.tabPageProjection.Controls.Add(this.label11);
            this.tabPageProjection.Location = new System.Drawing.Point(4, 22);
            this.tabPageProjection.Name = "tabPageProjection";
            this.tabPageProjection.Size = new System.Drawing.Size(312, 177);
            this.tabPageProjection.TabIndex = 2;
            this.tabPageProjection.Text = "Coordinate Space";
            this.tabPageProjection.UseVisualStyleBackColor = true;
            // 
            // buttonProjection
            // 
            this.buttonProjection.Location = new System.Drawing.Point(279, 23);
            this.buttonProjection.Name = "buttonProjection";
            this.buttonProjection.Size = new System.Drawing.Size(25, 23);
            this.buttonProjection.TabIndex = 2;
            this.buttonProjection.Text = "...";
            this.buttonProjection.UseVisualStyleBackColor = true;
            this.buttonProjection.Click += new System.EventHandler(this.buttonProjection_Click);
            // 
            // comboBoxUnits
            // 
            this.comboBoxUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnits.FormattingEnabled = true;
            this.comboBoxUnits.Location = new System.Drawing.Point(69, 106);
            this.comboBoxUnits.Name = "comboBoxUnits";
            this.comboBoxUnits.Size = new System.Drawing.Size(204, 21);
            this.comboBoxUnits.TabIndex = 4;
            this.comboBoxUnits.SelectedIndexChanged += new System.EventHandler(this.comboBoxUnits_SelectedIndexChanged);
            // 
            // textBoxProjection
            // 
            this.textBoxProjection.Location = new System.Drawing.Point(69, 23);
            this.textBoxProjection.Multiline = true;
            this.textBoxProjection.Name = "textBoxProjection";
            this.textBoxProjection.ReadOnly = true;
            this.textBoxProjection.Size = new System.Drawing.Size(204, 67);
            this.textBoxProjection.TabIndex = 1;
            this.textBoxProjection.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Map Units:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Projection:";
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.checkBoxTransparent);
            this.tabPageImage.Controls.Add(this.label15);
            this.tabPageImage.Controls.Add(this.textBoxResolution);
            this.tabPageImage.Controls.Add(this.label14);
            this.tabPageImage.Controls.Add(this.buttonQueryMap);
            this.tabPageImage.Controls.Add(this.buttonScalebar);
            this.tabPageImage.Controls.Add(this.comboBoxImageType);
            this.tabPageImage.Controls.Add(this.label10);
            this.tabPageImage.Controls.Add(this.label9);
            this.tabPageImage.Controls.Add(this.colorPickerBackColor);
            this.tabPageImage.Location = new System.Drawing.Point(4, 22);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Size = new System.Drawing.Size(312, 177);
            this.tabPageImage.TabIndex = 3;
            this.tabPageImage.Text = "Image Details";
            this.tabPageImage.UseVisualStyleBackColor = true;
            // 
            // checkBoxTransparent
            // 
            this.checkBoxTransparent.AutoSize = true;
            this.checkBoxTransparent.Location = new System.Drawing.Point(194, 79);
            this.checkBoxTransparent.Name = "checkBoxTransparent";
            this.checkBoxTransparent.Size = new System.Drawing.Size(115, 17);
            this.checkBoxTransparent.TabIndex = 9;
            this.checkBoxTransparent.Text = "Transparent Image";
            this.checkBoxTransparent.UseVisualStyleBackColor = true;
            this.checkBoxTransparent.CheckedChanged += new System.EventHandler(this.checkBoxTransparent_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(149, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "DPI";
            // 
            // textBoxResolution
            // 
            this.textBoxResolution.Location = new System.Drawing.Point(92, 77);
            this.textBoxResolution.Name = "textBoxResolution";
            this.textBoxResolution.Size = new System.Drawing.Size(51, 20);
            this.textBoxResolution.TabIndex = 5;
            this.textBoxResolution.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxResolution.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 80);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Resolution:";
            // 
            // buttonQueryMap
            // 
            this.buttonQueryMap.Location = new System.Drawing.Point(91, 134);
            this.buttonQueryMap.Name = "buttonQueryMap";
            this.buttonQueryMap.Size = new System.Drawing.Size(127, 23);
            this.buttonQueryMap.TabIndex = 8;
            this.buttonQueryMap.Text = "QueryMap Properties...";
            this.buttonQueryMap.UseVisualStyleBackColor = true;
            this.buttonQueryMap.Click += new System.EventHandler(this.buttonQueryMap_Click);
            // 
            // buttonScalebar
            // 
            this.buttonScalebar.Location = new System.Drawing.Point(91, 105);
            this.buttonScalebar.Name = "buttonScalebar";
            this.buttonScalebar.Size = new System.Drawing.Size(125, 23);
            this.buttonScalebar.TabIndex = 7;
            this.buttonScalebar.Text = "Scalebar Properties...";
            this.buttonScalebar.UseVisualStyleBackColor = true;
            this.buttonScalebar.Click += new System.EventHandler(this.buttonScalebar_Click);
            // 
            // comboBoxImageType
            // 
            this.comboBoxImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImageType.FormattingEnabled = true;
            this.comboBoxImageType.Location = new System.Drawing.Point(91, 48);
            this.comboBoxImageType.Name = "comboBoxImageType";
            this.comboBoxImageType.Size = new System.Drawing.Size(203, 21);
            this.comboBoxImageType.TabIndex = 3;
            this.comboBoxImageType.SelectedIndexChanged += new System.EventHandler(this.comboBoxImageType_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Image Type:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Map Background Colour:";
            // 
            // colorPickerBackColor
            // 
            this.colorPickerBackColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerBackColor.Context = null;
            this.colorPickerBackColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerBackColor.Location = new System.Drawing.Point(134, 20);
            this.colorPickerBackColor.Name = "colorPickerBackColor";
            this.colorPickerBackColor.ReadOnly = false;
            this.colorPickerBackColor.Size = new System.Drawing.Size(159, 20);
            this.colorPickerBackColor.TabIndex = 1;
            this.colorPickerBackColor.Text = "White";
            this.colorPickerBackColor.Value = System.Drawing.Color.White;
            this.colorPickerBackColor.ValueChanged += new System.EventHandler(this.colorPickerBackColor_ValueChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // MapPropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "MapPropertyEditor";
            this.Size = new System.Drawing.Size(320, 203);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageExtent.ResumeLayout(false);
            this.tabPageExtent.PerformLayout();
            this.tabPageProjection.ResumeLayout(false);
            this.tabPageProjection.PerformLayout();
            this.tabPageImage.ResumeLayout(false);
            this.tabPageImage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageExtent;
        private System.Windows.Forms.TabPage tabPageProjection;
        private System.Windows.Forms.TabPage tabPageImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSymbolset;
        private System.Windows.Forms.Button buttonShapePath;
        private System.Windows.Forms.TextBox textBoxFontset;
        private System.Windows.Forms.TextBox textBoxSymbolset;
        private System.Windows.Forms.TextBox textBoxShapePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonFontset;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonProjection;
        private System.Windows.Forms.ComboBox comboBoxUnits;
        private System.Windows.Forms.TextBox textBoxProjection;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox comboBoxImageType;
        private ColorPicker colorPickerBackColor;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonScalebar;
        private System.Windows.Forms.Button buttonQueryMap;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxResolution;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelUnit2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxScale;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labelUnit1;
        private System.Windows.Forms.TextBox textBoxZoomWidth;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonImagepath;
        private System.Windows.Forms.TextBox textBoxImagepath;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox checkBoxTransparent;

    }
}
