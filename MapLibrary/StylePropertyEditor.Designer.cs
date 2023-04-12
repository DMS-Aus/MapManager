namespace DMS.MapLibrary
{
    partial class StylePropertyEditor
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
            this.components = new System.ComponentModel.Container();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.checkBoxAntialias = new System.Windows.Forms.CheckBox();
            this.labelOpacityPercent = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.textBoxOffsetY = new System.Windows.Forms.TextBox();
            this.textBoxOffsetX = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxMaxWidth = new System.Windows.Forms.TextBox();
            this.textBoxMinWidth = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxMaxSize = new System.Windows.Forms.TextBox();
            this.textBoxMinSize = new System.Windows.Forms.TextBox();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAngle = new System.Windows.Forms.TextBox();
            this.checkBoxAutoAngle = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGap = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.textBoxPattern = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelMaxZoom = new System.Windows.Forms.Label();
            this.labelMinZoom = new System.Windows.Forms.Label();
            this.checkBoxQueryable = new System.Windows.Forms.CheckBox();
            this.textBoxMaxZoom = new System.Windows.Forms.TextBox();
            this.textBoxMinZoom = new System.Windows.Forms.TextBox();
            this.checkBoxVisible = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBoxSample = new System.Windows.Forms.PictureBox();
            this.buttonMaxScale = new System.Windows.Forms.Button();
            this.buttonMinScale = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxGeomTransform = new System.Windows.Forms.ComboBox();
            this.styleBindingControllerAngle = new DMS.MapLibrary.StyleBindingController();
            this.styleBindingControllerOutlineColor = new DMS.MapLibrary.StyleBindingController();
            this.colorPickerOutlineColor = new DMS.MapLibrary.ColorPicker();
            this.styleBindingControllerColor = new DMS.MapLibrary.StyleBindingController();
            this.colorPickerColor = new DMS.MapLibrary.ColorPicker();
            this.styleBindingControllerWidth = new DMS.MapLibrary.StyleBindingController();
            this.styleBindingControllerSize = new DMS.MapLibrary.StyleBindingController();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxAntialias
            // 
            this.checkBoxAntialias.AutoSize = true;
            this.checkBoxAntialias.Location = new System.Drawing.Point(436, 298);
            this.checkBoxAntialias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxAntialias.Name = "checkBoxAntialias";
            this.checkBoxAntialias.Size = new System.Drawing.Size(80, 20);
            this.checkBoxAntialias.TabIndex = 29;
            this.checkBoxAntialias.Text = "Antialias";
            this.checkBoxAntialias.UseVisualStyleBackColor = true;
            this.checkBoxAntialias.CheckedChanged += new System.EventHandler(this.ValueChanging);
            // 
            // labelOpacityPercent
            // 
            this.labelOpacityPercent.AutoSize = true;
            this.labelOpacityPercent.Location = new System.Drawing.Point(365, 299);
            this.labelOpacityPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOpacityPercent.Name = "labelOpacityPercent";
            this.labelOpacityPercent.Size = new System.Drawing.Size(40, 16);
            this.labelOpacityPercent.TabIndex = 28;
            this.labelOpacityPercent.Text = "100%";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(68, 299);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 16);
            this.label13.TabIndex = 24;
            this.label13.Text = "Opacity:";
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.AutoSize = false;
            this.trackBarOpacity.Location = new System.Drawing.Point(120, 295);
            this.trackBarOpacity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarOpacity.Maximum = 100;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(243, 33);
            this.trackBarOpacity.TabIndex = 25;
            this.trackBarOpacity.TickFrequency = 10;
            this.trackBarOpacity.Value = 100;
            this.trackBarOpacity.Scroll += new System.EventHandler(this.trackBarOpacity_Scroll);
            this.trackBarOpacity.ValueChanged += new System.EventHandler(this.trackBarOpacity_Scroll);
            this.trackBarOpacity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarOpacity_MouseUp);
            // 
            // textBoxOffsetY
            // 
            this.textBoxOffsetY.Location = new System.Drawing.Point(699, 174);
            this.textBoxOffsetY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxOffsetY.Name = "textBoxOffsetY";
            this.textBoxOffsetY.Size = new System.Drawing.Size(43, 22);
            this.textBoxOffsetY.TabIndex = 23;
            this.textBoxOffsetY.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxOffsetY.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxOffsetY.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxOffsetX
            // 
            this.textBoxOffsetX.Location = new System.Drawing.Point(699, 145);
            this.textBoxOffsetX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxOffsetX.Name = "textBoxOffsetX";
            this.textBoxOffsetX.Size = new System.Drawing.Size(43, 22);
            this.textBoxOffsetX.TabIndex = 21;
            this.textBoxOffsetX.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxOffsetX.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxOffsetX.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(639, 180);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "Y Offset:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(637, 149);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 16);
            this.label11.TabIndex = 20;
            this.label11.Text = "X Offset:";
            // 
            // textBoxMaxWidth
            // 
            this.textBoxMaxWidth.Location = new System.Drawing.Point(300, 124);
            this.textBoxMaxWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMaxWidth.Name = "textBoxMaxWidth";
            this.textBoxMaxWidth.Size = new System.Drawing.Size(45, 22);
            this.textBoxMaxWidth.TabIndex = 11;
            this.textBoxMaxWidth.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMaxWidth.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxMaxWidth.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxMinWidth
            // 
            this.textBoxMinWidth.Location = new System.Drawing.Point(131, 124);
            this.textBoxMinWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMinWidth.Name = "textBoxMinWidth";
            this.textBoxMinWidth.Size = new System.Drawing.Size(45, 22);
            this.textBoxMinWidth.TabIndex = 9;
            this.textBoxMinWidth.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinWidth.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxMinWidth.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(131, 96);
            this.textBoxWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(215, 22);
            this.textBoxWidth.TabIndex = 7;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxWidth.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxWidth.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxMaxSize
            // 
            this.textBoxMaxSize.Location = new System.Drawing.Point(300, 152);
            this.textBoxMaxSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMaxSize.Name = "textBoxMaxSize";
            this.textBoxMaxSize.Size = new System.Drawing.Size(45, 22);
            this.textBoxMaxSize.TabIndex = 5;
            this.textBoxMaxSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMaxSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxMaxSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxMinSize
            // 
            this.textBoxMinSize.Location = new System.Drawing.Point(131, 152);
            this.textBoxMinSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMinSize.Name = "textBoxMinSize";
            this.textBoxMinSize.Size = new System.Drawing.Size(45, 22);
            this.textBoxMinSize.TabIndex = 3;
            this.textBoxMinSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxMinSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(131, 39);
            this.textBoxSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(215, 22);
            this.textBoxSize.TabIndex = 1;
            this.textBoxSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Max. Width:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Min. Width:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 156);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Max. Scale Size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 156);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Min. Scale Size:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(641, 10);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "Preview:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(365, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Category:";
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(131, 67);
            this.textBoxAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(214, 22);
            this.textBoxAngle.TabIndex = 13;
            this.textBoxAngle.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxAngle.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxAngle.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // checkBoxAutoAngle
            // 
            this.checkBoxAutoAngle.AutoSize = true;
            this.checkBoxAutoAngle.Location = new System.Drawing.Point(539, 298);
            this.checkBoxAutoAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxAutoAngle.Name = "checkBoxAutoAngle";
            this.checkBoxAutoAngle.Size = new System.Drawing.Size(94, 20);
            this.checkBoxAutoAngle.TabIndex = 30;
            this.checkBoxAutoAngle.Text = "Auto Angle";
            this.checkBoxAutoAngle.UseVisualStyleBackColor = true;
            this.checkBoxAutoAngle.CheckedChanged += new System.EventHandler(this.checkBoxAutoAngle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 245);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "Geom. Transform.:";
            // 
            // textBoxGap
            // 
            this.textBoxGap.Location = new System.Drawing.Point(699, 203);
            this.textBoxGap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxGap.Name = "textBoxGap";
            this.textBoxGap.Size = new System.Drawing.Size(43, 22);
            this.textBoxGap.TabIndex = 35;
            this.textBoxGap.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxGap.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxGap.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(661, 208);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 16);
            this.label7.TabIndex = 34;
            this.label7.Text = "Gap:";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Font = new System.Drawing.Font("Arial Narrow", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(369, 34);
            this.listView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(264, 200);
            this.listView.TabIndex = 36;
            this.listView.TileSize = new System.Drawing.Size(36, 36);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(436, 4);
            this.comboBoxCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(197, 24);
            this.comboBoxCategory.TabIndex = 38;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // textBoxPattern
            // 
            this.textBoxPattern.AcceptsTab = true;
            this.textBoxPattern.Location = new System.Drawing.Point(131, 268);
            this.textBoxPattern.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPattern.Name = "textBoxPattern";
            this.textBoxPattern.Size = new System.Drawing.Size(215, 22);
            this.textBoxPattern.TabIndex = 40;
            this.textBoxPattern.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxPattern.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPattern_Validating);
            this.textBoxPattern.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 272);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 39;
            this.label8.Text = "Line Pattern:";
            // 
            // labelMaxZoom
            // 
            this.labelMaxZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMaxZoom.AutoSize = true;
            this.labelMaxZoom.Location = new System.Drawing.Point(-22, 90);
            this.labelMaxZoom.MinimumSize = new System.Drawing.Size(120, 0);
            this.labelMaxZoom.Name = "labelMaxZoom";
            this.labelMaxZoom.Size = new System.Drawing.Size(120, 13);
            this.labelMaxZoom.TabIndex = 4;
            this.labelMaxZoom.Text = "Farthest scale: 1:";
            this.labelMaxZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMinZoom
            // 
            this.labelMinZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMinZoom.AutoSize = true;
            this.labelMinZoom.Location = new System.Drawing.Point(-22, 56);
            this.labelMinZoom.MinimumSize = new System.Drawing.Size(120, 0);
            this.labelMinZoom.Name = "labelMinZoom";
            this.labelMinZoom.Size = new System.Drawing.Size(120, 13);
            this.labelMinZoom.TabIndex = 2;
            this.labelMinZoom.Text = "Closest scale: 1:";
            this.labelMinZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxQueryable
            // 
            this.checkBoxQueryable.AutoSize = true;
            this.checkBoxQueryable.Location = new System.Drawing.Point(104, 21);
            this.checkBoxQueryable.Name = "checkBoxQueryable";
            this.checkBoxQueryable.Size = new System.Drawing.Size(74, 17);
            this.checkBoxQueryable.TabIndex = 1;
            this.checkBoxQueryable.Text = "Queryable";
            this.checkBoxQueryable.UseVisualStyleBackColor = true;
            // 
            // textBoxMaxZoom
            // 
            this.textBoxMaxZoom.Location = new System.Drawing.Point(480, 270);
            this.textBoxMaxZoom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMaxZoom.Name = "textBoxMaxZoom";
            this.textBoxMaxZoom.Size = new System.Drawing.Size(153, 22);
            this.textBoxMaxZoom.TabIndex = 44;
            this.textBoxMaxZoom.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMaxZoom.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // textBoxMinZoom
            // 
            this.textBoxMinZoom.Location = new System.Drawing.Point(480, 240);
            this.textBoxMinZoom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMinZoom.Name = "textBoxMinZoom";
            this.textBoxMinZoom.Size = new System.Drawing.Size(152, 22);
            this.textBoxMinZoom.TabIndex = 42;
            this.textBoxMinZoom.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinZoom.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // checkBoxVisible
            // 
            this.checkBoxVisible.AutoSize = true;
            this.checkBoxVisible.Location = new System.Drawing.Point(15, 21);
            this.checkBoxVisible.Name = "checkBoxVisible";
            this.checkBoxVisible.Size = new System.Drawing.Size(56, 17);
            this.checkBoxVisible.TabIndex = 0;
            this.checkBoxVisible.Text = "Visible";
            this.checkBoxVisible.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(371, 244);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 16);
            this.label10.TabIndex = 45;
            this.label10.Text = "Closest scale 1:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(367, 273);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 16);
            this.label15.TabIndex = 46;
            this.label15.Text = "Farthest scale 1:";
            // 
            // pictureBoxSample
            // 
            this.pictureBoxSample.BackColor = System.Drawing.Color.White;
            this.pictureBoxSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSample.Location = new System.Drawing.Point(641, 34);
            this.pictureBoxSample.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxSample.Name = "pictureBoxSample";
            this.pictureBoxSample.Size = new System.Drawing.Size(101, 81);
            this.pictureBoxSample.TabIndex = 47;
            this.pictureBoxSample.TabStop = false;
            // 
            // buttonMaxScale
            // 
            this.buttonMaxScale.Image = global::MapLibrary.Properties.Resources.Map;
            this.buttonMaxScale.Location = new System.Drawing.Point(641, 267);
            this.buttonMaxScale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonMaxScale.Name = "buttonMaxScale";
            this.buttonMaxScale.Size = new System.Drawing.Size(32, 30);
            this.buttonMaxScale.TabIndex = 49;
            this.buttonMaxScale.UseVisualStyleBackColor = true;
            this.buttonMaxScale.Click += new System.EventHandler(this.buttonMaxScale_Click);
            // 
            // buttonMinScale
            // 
            this.buttonMinScale.Image = global::MapLibrary.Properties.Resources.Map;
            this.buttonMinScale.Location = new System.Drawing.Point(641, 236);
            this.buttonMinScale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonMinScale.Name = "buttonMinScale";
            this.buttonMinScale.Size = new System.Drawing.Size(32, 30);
            this.buttonMinScale.TabIndex = 48;
            this.buttonMinScale.UseVisualStyleBackColor = true;
            this.buttonMinScale.Click += new System.EventHandler(this.buttonMinScale_Click);
            // 
            // comboBoxGeomTransform
            // 
            this.comboBoxGeomTransform.FormattingEnabled = true;
            this.comboBoxGeomTransform.Location = new System.Drawing.Point(131, 239);
            this.comboBoxGeomTransform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxGeomTransform.Name = "comboBoxGeomTransform";
            this.comboBoxGeomTransform.Size = new System.Drawing.Size(215, 24);
            this.comboBoxGeomTransform.TabIndex = 50;
            this.comboBoxGeomTransform.TextChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // styleBindingControllerAngle
            // 
            this.styleBindingControllerAngle.ActiveLinkColor = System.Drawing.Color.RosyBrown;
            this.styleBindingControllerAngle.AutoSize = true;
            this.styleBindingControllerAngle.BindingState = false;
            this.styleBindingControllerAngle.LinkColor = System.Drawing.Color.Blue;
            this.styleBindingControllerAngle.Location = new System.Drawing.Point(80, 71);
            this.styleBindingControllerAngle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.styleBindingControllerAngle.Name = "styleBindingControllerAngle";
            this.styleBindingControllerAngle.Size = new System.Drawing.Size(45, 16);
            this.styleBindingControllerAngle.StyleBinding = OSGeo.MapServer.MS_STYLE_BINDING_ENUM.MS_STYLE_BINDING_ANGLE;
            this.styleBindingControllerAngle.TabIndex = 12;
            this.styleBindingControllerAngle.TabStop = true;
            this.styleBindingControllerAngle.TargetControl = this.textBoxAngle;
            this.styleBindingControllerAngle.Text = "Angle:";
            this.styleBindingControllerAngle.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // styleBindingControllerOutlineColor
            // 
            this.styleBindingControllerOutlineColor.AutoSize = true;
            this.styleBindingControllerOutlineColor.BindingState = false;
            this.styleBindingControllerOutlineColor.Location = new System.Drawing.Point(29, 213);
            this.styleBindingControllerOutlineColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.styleBindingControllerOutlineColor.Name = "styleBindingControllerOutlineColor";
            this.styleBindingControllerOutlineColor.Size = new System.Drawing.Size(93, 16);
            this.styleBindingControllerOutlineColor.StyleBinding = OSGeo.MapServer.MS_STYLE_BINDING_ENUM.MS_STYLE_BINDING_OUTLINECOLOR;
            this.styleBindingControllerOutlineColor.TabIndex = 18;
            this.styleBindingControllerOutlineColor.TabStop = true;
            this.styleBindingControllerOutlineColor.TargetControl = this.colorPickerOutlineColor;
            this.styleBindingControllerOutlineColor.Text = "Outline Colour:";
            this.styleBindingControllerOutlineColor.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // colorPickerOutlineColor
            // 
            this.colorPickerOutlineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerOutlineColor.Context = null;
            this.colorPickerOutlineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerOutlineColor.Location = new System.Drawing.Point(131, 211);
            this.colorPickerOutlineColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorPickerOutlineColor.Name = "colorPickerOutlineColor";
            this.colorPickerOutlineColor.ReadOnly = false;
            this.colorPickerOutlineColor.Size = new System.Drawing.Size(216, 22);
            this.colorPickerOutlineColor.TabIndex = 19;
            this.colorPickerOutlineColor.Text = "White";
            this.colorPickerOutlineColor.Value = System.Drawing.Color.White;
            this.colorPickerOutlineColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // styleBindingControllerColor
            // 
            this.styleBindingControllerColor.AutoSize = true;
            this.styleBindingControllerColor.BindingState = false;
            this.styleBindingControllerColor.Location = new System.Drawing.Point(76, 186);
            this.styleBindingControllerColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.styleBindingControllerColor.Name = "styleBindingControllerColor";
            this.styleBindingControllerColor.Size = new System.Drawing.Size(49, 16);
            this.styleBindingControllerColor.StyleBinding = OSGeo.MapServer.MS_STYLE_BINDING_ENUM.MS_STYLE_BINDING_COLOR;
            this.styleBindingControllerColor.TabIndex = 14;
            this.styleBindingControllerColor.TabStop = true;
            this.styleBindingControllerColor.TargetControl = this.colorPickerColor;
            this.styleBindingControllerColor.Text = "Colour:";
            this.styleBindingControllerColor.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // colorPickerColor
            // 
            this.colorPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerColor.Context = null;
            this.colorPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerColor.Location = new System.Drawing.Point(131, 182);
            this.colorPickerColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorPickerColor.Name = "colorPickerColor";
            this.colorPickerColor.ReadOnly = false;
            this.colorPickerColor.Size = new System.Drawing.Size(216, 22);
            this.colorPickerColor.TabIndex = 15;
            this.colorPickerColor.Text = "White";
            this.colorPickerColor.Value = System.Drawing.Color.White;
            this.colorPickerColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // styleBindingControllerWidth
            // 
            this.styleBindingControllerWidth.ActiveLinkColor = System.Drawing.Color.RosyBrown;
            this.styleBindingControllerWidth.AutoSize = true;
            this.styleBindingControllerWidth.BindingState = false;
            this.styleBindingControllerWidth.Location = new System.Drawing.Point(49, 99);
            this.styleBindingControllerWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.styleBindingControllerWidth.Name = "styleBindingControllerWidth";
            this.styleBindingControllerWidth.Size = new System.Drawing.Size(72, 16);
            this.styleBindingControllerWidth.StyleBinding = OSGeo.MapServer.MS_STYLE_BINDING_ENUM.MS_STYLE_BINDING_WIDTH;
            this.styleBindingControllerWidth.TabIndex = 6;
            this.styleBindingControllerWidth.TabStop = true;
            this.styleBindingControllerWidth.TargetControl = this.textBoxWidth;
            this.styleBindingControllerWidth.Text = "Line Width:";
            this.styleBindingControllerWidth.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // styleBindingControllerSize
            // 
            this.styleBindingControllerSize.ActiveLinkColor = System.Drawing.Color.RosyBrown;
            this.styleBindingControllerSize.AutoSize = true;
            this.styleBindingControllerSize.BindingState = false;
            this.styleBindingControllerSize.Location = new System.Drawing.Point(40, 43);
            this.styleBindingControllerSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.styleBindingControllerSize.Name = "styleBindingControllerSize";
            this.styleBindingControllerSize.Size = new System.Drawing.Size(85, 16);
            this.styleBindingControllerSize.StyleBinding = OSGeo.MapServer.MS_STYLE_BINDING_ENUM.MS_STYLE_BINDING_SIZE;
            this.styleBindingControllerSize.TabIndex = 0;
            this.styleBindingControllerSize.TabStop = true;
            this.styleBindingControllerSize.TargetControl = this.textBoxSize;
            this.styleBindingControllerSize.Text = "Symbol Size:";
            this.styleBindingControllerSize.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // StylePropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxGeomTransform);
            this.Controls.Add(this.buttonMaxScale);
            this.Controls.Add(this.buttonMinScale);
            this.Controls.Add(this.pictureBoxSample);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxMaxZoom);
            this.Controls.Add(this.textBoxMinZoom);
            this.Controls.Add(this.textBoxPattern);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.textBoxGap);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxAutoAngle);
            this.Controls.Add(this.checkBoxAntialias);
            this.Controls.Add(this.styleBindingControllerAngle);
            this.Controls.Add(this.styleBindingControllerOutlineColor);
            this.Controls.Add(this.styleBindingControllerColor);
            this.Controls.Add(this.styleBindingControllerWidth);
            this.Controls.Add(this.styleBindingControllerSize);
            this.Controls.Add(this.textBoxAngle);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.labelOpacityPercent);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.colorPickerOutlineColor);
            this.Controls.Add(this.trackBarOpacity);
            this.Controls.Add(this.textBoxOffsetY);
            this.Controls.Add(this.textBoxOffsetX);
            this.Controls.Add(this.colorPickerColor);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxMaxWidth);
            this.Controls.Add(this.textBoxMinWidth);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.textBoxMaxSize);
            this.Controls.Add(this.textBoxMinSize);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "StylePropertyEditor";
            this.Size = new System.Drawing.Size(747, 332);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label labelOpacityPercent;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.TextBox textBoxOffsetY;
        private System.Windows.Forms.TextBox textBoxOffsetX;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxAntialias;
        private System.Windows.Forms.TextBox textBoxMaxWidth;
        private System.Windows.Forms.TextBox textBoxMinWidth;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxMaxSize;
        private System.Windows.Forms.TextBox textBoxMinSize;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ColorPicker colorPickerOutlineColor;
        private ColorPicker colorPickerColor;
        private System.Windows.Forms.Label label14;
        private StyleBindingController styleBindingControllerSize;
        private StyleBindingController styleBindingControllerWidth;
        private StyleBindingController styleBindingControllerColor;
        private StyleBindingController styleBindingControllerOutlineColor;
        private System.Windows.Forms.Label label1;
        private StyleBindingController styleBindingControllerAngle;
        private System.Windows.Forms.TextBox textBoxAngle;
        private System.Windows.Forms.CheckBox checkBoxAutoAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGap;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelMaxZoom;
        private System.Windows.Forms.Label labelMinZoom;
        private System.Windows.Forms.CheckBox checkBoxQueryable;
        private System.Windows.Forms.TextBox textBoxMaxZoom;
        private System.Windows.Forms.TextBox textBoxMinZoom;
        private System.Windows.Forms.CheckBox checkBoxVisible;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.PictureBox pictureBoxSample;
        private System.Windows.Forms.Button buttonMaxScale;
        private System.Windows.Forms.Button buttonMinScale;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox comboBoxGeomTransform;
    }
}
