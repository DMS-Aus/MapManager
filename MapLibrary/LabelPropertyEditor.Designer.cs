namespace DMS.MapLibrary
{
    partial class LabelPropertyEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageRendering = new System.Windows.Forms.TabPage();
            this.textBoxEncoding = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.labelBindingControllerShadowSizeY = new DMS.MapLibrary.LabelBindingController();
            this.textBoxShadowSizeY = new System.Windows.Forms.TextBox();
            this.labelBindingControllerShadowSizeX = new DMS.MapLibrary.LabelBindingController();
            this.textBoxShadowSizeX = new System.Windows.Forms.TextBox();
            this.labelBindingControllerSize = new DMS.MapLibrary.LabelBindingController();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.labelBindingControllerFont = new DMS.MapLibrary.LabelBindingController();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.labelBindingControllerColor = new DMS.MapLibrary.LabelBindingController();
            this.colorPickerColor = new DMS.MapLibrary.ColorPicker();
            this.labelBindingControllerOutlineColor = new DMS.MapLibrary.LabelBindingController();
            this.colorPickerOutlineColor = new DMS.MapLibrary.ColorPicker();
            this.textBoxMaxSize = new System.Windows.Forms.TextBox();
            this.textBoxMinSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.colorPickerShadowColor = new DMS.MapLibrary.ColorPicker();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.buttonMaxScale = new System.Windows.Forms.Button();
            this.buttonMinScale = new System.Windows.Forms.Button();
            this.labelMaxZoom = new System.Windows.Forms.Label();
            this.labelMinZoom = new System.Windows.Forms.Label();
            this.textBoxMaxScale = new System.Windows.Forms.TextBox();
            this.textBoxMinScale = new System.Windows.Forms.TextBox();
            this.textBoxRepeatDistance = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMinFeatureSize = new System.Windows.Forms.TextBox();
            this.textBoxExpression = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxMinDistance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAutoMinFeatureSize = new System.Windows.Forms.CheckBox();
            this.checkBoxForce = new System.Windows.Forms.CheckBox();
            this.checkBoxPartials = new System.Windows.Forms.CheckBox();
            this.tabPagePosition = new System.Windows.Forms.TabPage();
            this.labelBindingControllerPosition = new DMS.MapLibrary.LabelBindingController();
            this.comboBoxPosition = new System.Windows.Forms.ComboBox();
            this.labelBindingControllerPriority = new DMS.MapLibrary.LabelBindingController();
            this.textBoxPriority = new System.Windows.Forms.TextBox();
            this.labelBindingControllerAngle = new DMS.MapLibrary.LabelBindingController();
            this.textBoxAngle = new System.Windows.Forms.TextBox();
            this.textBoxMaxLength = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxBuffer = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBoxAngleMode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxAlign = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxWrap = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxOffsetY = new System.Windows.Forms.TextBox();
            this.textBoxOffsetX = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPageStyles = new System.Windows.Forms.TabPage();
            this.buttonRemoveStyle = new System.Windows.Forms.Button();
            this.buttonMoveStyleDown = new System.Windows.Forms.Button();
            this.buttonMoveStyleUp = new System.Windows.Forms.Button();
            this.buttonEditStyle = new System.Windows.Forms.Button();
            this.buttonAddStyle = new System.Windows.Forms.Button();
            this.listViewStyles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mapControl = new DMS.MapLibrary.MapControl();
            this.tabControl1.SuspendLayout();
            this.tabPageRendering.SuspendLayout();
            this.tabPageDisplay.SuspendLayout();
            this.tabPagePosition.SuspendLayout();
            this.tabPageStyles.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sample:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageRendering);
            this.tabControl1.Controls.Add(this.tabPageDisplay);
            this.tabControl1.Controls.Add(this.tabPagePosition);
            this.tabControl1.Controls.Add(this.tabPageStyles);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(310, 243);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageRendering
            // 
            this.tabPageRendering.Controls.Add(this.textBoxEncoding);
            this.tabPageRendering.Controls.Add(this.label15);
            this.tabPageRendering.Controls.Add(this.textBoxText);
            this.tabPageRendering.Controls.Add(this.label13);
            this.tabPageRendering.Controls.Add(this.labelBindingControllerShadowSizeY);
            this.tabPageRendering.Controls.Add(this.labelBindingControllerShadowSizeX);
            this.tabPageRendering.Controls.Add(this.labelBindingControllerSize);
            this.tabPageRendering.Controls.Add(this.labelBindingControllerFont);
            this.tabPageRendering.Controls.Add(this.labelBindingControllerColor);
            this.tabPageRendering.Controls.Add(this.labelBindingControllerOutlineColor);
            this.tabPageRendering.Controls.Add(this.textBoxSize);
            this.tabPageRendering.Controls.Add(this.comboBoxFont);
            this.tabPageRendering.Controls.Add(this.textBoxMaxSize);
            this.tabPageRendering.Controls.Add(this.textBoxMinSize);
            this.tabPageRendering.Controls.Add(this.label4);
            this.tabPageRendering.Controls.Add(this.label3);
            this.tabPageRendering.Controls.Add(this.colorPickerOutlineColor);
            this.tabPageRendering.Controls.Add(this.colorPickerColor);
            this.tabPageRendering.Controls.Add(this.textBoxShadowSizeY);
            this.tabPageRendering.Controls.Add(this.textBoxShadowSizeX);
            this.tabPageRendering.Controls.Add(this.label17);
            this.tabPageRendering.Controls.Add(this.colorPickerShadowColor);
            this.tabPageRendering.Location = new System.Drawing.Point(4, 22);
            this.tabPageRendering.Name = "tabPageRendering";
            this.tabPageRendering.Size = new System.Drawing.Size(302, 217);
            this.tabPageRendering.TabIndex = 5;
            this.tabPageRendering.Text = "Rendering";
            this.tabPageRendering.UseVisualStyleBackColor = true;
            // 
            // textBoxEncoding
            // 
            this.textBoxEncoding.Location = new System.Drawing.Point(234, 75);
            this.textBoxEncoding.Name = "textBoxEncoding";
            this.textBoxEncoding.Size = new System.Drawing.Size(55, 20);
            this.textBoxEncoding.TabIndex = 25;
            this.textBoxEncoding.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(182, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Encoding:";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(81, 4);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(208, 20);
            this.textBoxText.TabIndex = 23;
            this.textBoxText.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxText.Validated += new System.EventHandler(this.textBoxText_Validated);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(48, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Text:";
            // 
            // labelBindingControllerShadowSizeY
            // 
            this.labelBindingControllerShadowSizeY.AutoSize = true;
            this.labelBindingControllerShadowSizeY.BindingState = false;
            this.labelBindingControllerShadowSizeY.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelBindingControllerShadowSizeY.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_SHADOWSIZEY;
            this.labelBindingControllerShadowSizeY.Location = new System.Drawing.Point(-1, 195);
            this.labelBindingControllerShadowSizeY.Name = "labelBindingControllerShadowSizeY";
            this.labelBindingControllerShadowSizeY.Size = new System.Drawing.Size(82, 13);
            this.labelBindingControllerShadowSizeY.TabIndex = 15;
            this.labelBindingControllerShadowSizeY.TabStop = true;
            this.labelBindingControllerShadowSizeY.TargetControl = this.textBoxShadowSizeY;
            this.labelBindingControllerShadowSizeY.Text = "Shadow Size Y:";
            this.labelBindingControllerShadowSizeY.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // textBoxShadowSizeY
            // 
            this.textBoxShadowSizeY.Location = new System.Drawing.Point(81, 191);
            this.textBoxShadowSizeY.Name = "textBoxShadowSizeY";
            this.textBoxShadowSizeY.Size = new System.Drawing.Size(208, 20);
            this.textBoxShadowSizeY.TabIndex = 5;
            this.textBoxShadowSizeY.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxShadowSizeY.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxShadowSizeY.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // labelBindingControllerShadowSizeX
            // 
            this.labelBindingControllerShadowSizeX.AutoSize = true;
            this.labelBindingControllerShadowSizeX.BindingState = false;
            this.labelBindingControllerShadowSizeX.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelBindingControllerShadowSizeX.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_SHADOWSIZEX;
            this.labelBindingControllerShadowSizeX.Location = new System.Drawing.Point(-1, 172);
            this.labelBindingControllerShadowSizeX.Name = "labelBindingControllerShadowSizeX";
            this.labelBindingControllerShadowSizeX.Size = new System.Drawing.Size(82, 13);
            this.labelBindingControllerShadowSizeX.TabIndex = 14;
            this.labelBindingControllerShadowSizeX.TabStop = true;
            this.labelBindingControllerShadowSizeX.TargetControl = this.textBoxShadowSizeX;
            this.labelBindingControllerShadowSizeX.Text = "Shadow Size X:";
            this.labelBindingControllerShadowSizeX.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // textBoxShadowSizeX
            // 
            this.textBoxShadowSizeX.Location = new System.Drawing.Point(81, 168);
            this.textBoxShadowSizeX.Name = "textBoxShadowSizeX";
            this.textBoxShadowSizeX.Size = new System.Drawing.Size(208, 20);
            this.textBoxShadowSizeX.TabIndex = 3;
            this.textBoxShadowSizeX.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxShadowSizeX.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxShadowSizeX.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // labelBindingControllerSize
            // 
            this.labelBindingControllerSize.AutoSize = true;
            this.labelBindingControllerSize.BindingState = false;
            this.labelBindingControllerSize.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelBindingControllerSize.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_SIZE;
            this.labelBindingControllerSize.Location = new System.Drawing.Point(48, 53);
            this.labelBindingControllerSize.Name = "labelBindingControllerSize";
            this.labelBindingControllerSize.Size = new System.Drawing.Size(33, 13);
            this.labelBindingControllerSize.TabIndex = 4;
            this.labelBindingControllerSize.TabStop = true;
            this.labelBindingControllerSize.TargetControl = this.textBoxSize;
            this.labelBindingControllerSize.Text = "Size: ";
            this.labelBindingControllerSize.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(81, 51);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(208, 20);
            this.textBoxSize.TabIndex = 5;
            this.textBoxSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // labelBindingControllerFont
            // 
            this.labelBindingControllerFont.AutoSize = true;
            this.labelBindingControllerFont.BindingState = false;
            this.labelBindingControllerFont.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelBindingControllerFont.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_FONT;
            this.labelBindingControllerFont.Location = new System.Drawing.Point(47, 30);
            this.labelBindingControllerFont.Name = "labelBindingControllerFont";
            this.labelBindingControllerFont.Size = new System.Drawing.Size(34, 13);
            this.labelBindingControllerFont.TabIndex = 2;
            this.labelBindingControllerFont.TabStop = true;
            this.labelBindingControllerFont.TargetControl = this.comboBoxFont;
            this.labelBindingControllerFont.Text = "Font: ";
            this.labelBindingControllerFont.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Location = new System.Drawing.Point(81, 27);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(208, 21);
            this.comboBoxFont.TabIndex = 3;
            this.comboBoxFont.SelectedIndexChanged += new System.EventHandler(this.comboBoxFont_SelectedIndexChanged);
            // 
            // labelBindingControllerColor
            // 
            this.labelBindingControllerColor.AutoSize = true;
            this.labelBindingControllerColor.BindingState = false;
            this.labelBindingControllerColor.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_COLOR;
            this.labelBindingControllerColor.Location = new System.Drawing.Point(40, 103);
            this.labelBindingControllerColor.Name = "labelBindingControllerColor";
            this.labelBindingControllerColor.Size = new System.Drawing.Size(40, 13);
            this.labelBindingControllerColor.TabIndex = 0;
            this.labelBindingControllerColor.TabStop = true;
            this.labelBindingControllerColor.TargetControl = this.colorPickerColor;
            this.labelBindingControllerColor.Text = "Colour:";
            this.labelBindingControllerColor.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // colorPickerColor
            // 
            this.colorPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerColor.Context = null;
            this.colorPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerColor.Location = new System.Drawing.Point(81, 99);
            this.colorPickerColor.Name = "colorPickerColor";
            this.colorPickerColor.ReadOnly = false;
            this.colorPickerColor.Size = new System.Drawing.Size(208, 20);
            this.colorPickerColor.TabIndex = 1;
            this.colorPickerColor.Text = "White";
            this.colorPickerColor.Value = System.Drawing.Color.White;
            this.colorPickerColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // labelBindingControllerOutlineColor
            // 
            this.labelBindingControllerOutlineColor.AutoSize = true;
            this.labelBindingControllerOutlineColor.BindingState = false;
            this.labelBindingControllerOutlineColor.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_OUTLINECOLOR;
            this.labelBindingControllerOutlineColor.Location = new System.Drawing.Point(4, 126);
            this.labelBindingControllerOutlineColor.Name = "labelBindingControllerOutlineColor";
            this.labelBindingControllerOutlineColor.Size = new System.Drawing.Size(76, 13);
            this.labelBindingControllerOutlineColor.TabIndex = 2;
            this.labelBindingControllerOutlineColor.TabStop = true;
            this.labelBindingControllerOutlineColor.TargetControl = this.colorPickerOutlineColor;
            this.labelBindingControllerOutlineColor.Text = "Outline Colour:";
            this.labelBindingControllerOutlineColor.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // colorPickerOutlineColor
            // 
            this.colorPickerOutlineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerOutlineColor.Context = null;
            this.colorPickerOutlineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerOutlineColor.Location = new System.Drawing.Point(81, 122);
            this.colorPickerOutlineColor.Name = "colorPickerOutlineColor";
            this.colorPickerOutlineColor.ReadOnly = false;
            this.colorPickerOutlineColor.Size = new System.Drawing.Size(208, 20);
            this.colorPickerOutlineColor.TabIndex = 3;
            this.colorPickerOutlineColor.Text = "White";
            this.colorPickerOutlineColor.Value = System.Drawing.Color.White;
            this.colorPickerOutlineColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // textBoxMaxSize
            // 
            this.textBoxMaxSize.Location = new System.Drawing.Point(156, 75);
            this.textBoxMaxSize.Name = "textBoxMaxSize";
            this.textBoxMaxSize.Size = new System.Drawing.Size(25, 20);
            this.textBoxMaxSize.TabIndex = 9;
            this.textBoxMaxSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMaxSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxMaxSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxMinSize
            // 
            this.textBoxMinSize.Location = new System.Drawing.Point(81, 75);
            this.textBoxMinSize.Name = "textBoxMinSize";
            this.textBoxMinSize.Size = new System.Drawing.Size(25, 20);
            this.textBoxMinSize.TabIndex = 7;
            this.textBoxMinSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxMinSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "MaxSize:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "MinSize:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(-1, 148);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Shadow Colour:";
            // 
            // colorPickerShadowColor
            // 
            this.colorPickerShadowColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerShadowColor.Context = null;
            this.colorPickerShadowColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerShadowColor.Location = new System.Drawing.Point(81, 145);
            this.colorPickerShadowColor.Name = "colorPickerShadowColor";
            this.colorPickerShadowColor.ReadOnly = false;
            this.colorPickerShadowColor.Size = new System.Drawing.Size(208, 20);
            this.colorPickerShadowColor.TabIndex = 1;
            this.colorPickerShadowColor.Text = "White";
            this.colorPickerShadowColor.Value = System.Drawing.Color.White;
            this.colorPickerShadowColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.buttonMaxScale);
            this.tabPageDisplay.Controls.Add(this.buttonMinScale);
            this.tabPageDisplay.Controls.Add(this.labelMaxZoom);
            this.tabPageDisplay.Controls.Add(this.labelMinZoom);
            this.tabPageDisplay.Controls.Add(this.textBoxMaxScale);
            this.tabPageDisplay.Controls.Add(this.textBoxMinScale);
            this.tabPageDisplay.Controls.Add(this.textBoxRepeatDistance);
            this.tabPageDisplay.Controls.Add(this.label14);
            this.tabPageDisplay.Controls.Add(this.label5);
            this.tabPageDisplay.Controls.Add(this.textBoxMinFeatureSize);
            this.tabPageDisplay.Controls.Add(this.textBoxExpression);
            this.tabPageDisplay.Controls.Add(this.label9);
            this.tabPageDisplay.Controls.Add(this.textBoxMinDistance);
            this.tabPageDisplay.Controls.Add(this.label2);
            this.tabPageDisplay.Controls.Add(this.checkBoxAutoMinFeatureSize);
            this.tabPageDisplay.Controls.Add(this.checkBoxForce);
            this.tabPageDisplay.Controls.Add(this.checkBoxPartials);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Size = new System.Drawing.Size(302, 217);
            this.tabPageDisplay.TabIndex = 2;
            this.tabPageDisplay.Text = "Display";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // buttonMaxScale
            // 
            this.buttonMaxScale.Image = global::MapLibrary.Properties.Resources.Map;
            this.buttonMaxScale.Location = new System.Drawing.Point(267, 121);
            this.buttonMaxScale.Name = "buttonMaxScale";
            this.buttonMaxScale.Size = new System.Drawing.Size(24, 24);
            this.buttonMaxScale.TabIndex = 33;
            this.buttonMaxScale.UseVisualStyleBackColor = true;
            this.buttonMaxScale.Click += new System.EventHandler(this.buttonMaxScale_Click);
            // 
            // buttonMinScale
            // 
            this.buttonMinScale.Image = global::MapLibrary.Properties.Resources.Map;
            this.buttonMinScale.Location = new System.Drawing.Point(267, 93);
            this.buttonMinScale.Name = "buttonMinScale";
            this.buttonMinScale.Size = new System.Drawing.Size(24, 24);
            this.buttonMinScale.TabIndex = 32;
            this.buttonMinScale.UseVisualStyleBackColor = true;
            this.buttonMinScale.Click += new System.EventHandler(this.buttonMinScale_Click);
            // 
            // labelMaxZoom
            // 
            this.labelMaxZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMaxZoom.AutoSize = true;
            this.labelMaxZoom.Location = new System.Drawing.Point(7, 126);
            this.labelMaxZoom.Name = "labelMaxZoom";
            this.labelMaxZoom.Size = new System.Drawing.Size(88, 13);
            this.labelMaxZoom.TabIndex = 30;
            this.labelMaxZoom.Text = "Farthest scale: 1:";
            this.labelMaxZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelMinZoom
            // 
            this.labelMinZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMinZoom.AutoSize = true;
            this.labelMinZoom.Location = new System.Drawing.Point(11, 98);
            this.labelMinZoom.Name = "labelMinZoom";
            this.labelMinZoom.Size = new System.Drawing.Size(84, 13);
            this.labelMinZoom.TabIndex = 28;
            this.labelMinZoom.Text = "Closest scale: 1:";
            this.labelMinZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMaxScale
            // 
            this.textBoxMaxScale.Location = new System.Drawing.Point(95, 123);
            this.textBoxMaxScale.Name = "textBoxMaxScale";
            this.textBoxMaxScale.Size = new System.Drawing.Size(166, 20);
            this.textBoxMaxScale.TabIndex = 31;
            this.textBoxMaxScale.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMaxScale.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            this.textBoxMaxScale.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxMinScale
            // 
            this.textBoxMinScale.Location = new System.Drawing.Point(95, 95);
            this.textBoxMinScale.Name = "textBoxMinScale";
            this.textBoxMinScale.Size = new System.Drawing.Size(166, 20);
            this.textBoxMinScale.TabIndex = 29;
            this.textBoxMinScale.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinScale.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            this.textBoxMinScale.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxRepeatDistance
            // 
            this.textBoxRepeatDistance.Location = new System.Drawing.Point(95, 67);
            this.textBoxRepeatDistance.Name = "textBoxRepeatDistance";
            this.textBoxRepeatDistance.Size = new System.Drawing.Size(58, 20);
            this.textBoxRepeatDistance.TabIndex = 25;
            this.textBoxRepeatDistance.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxRepeatDistance.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxRepeatDistance.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Repeat Distance:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Min. Feature Size:";
            // 
            // textBoxMinFeatureSize
            // 
            this.textBoxMinFeatureSize.Location = new System.Drawing.Point(95, 11);
            this.textBoxMinFeatureSize.Name = "textBoxMinFeatureSize";
            this.textBoxMinFeatureSize.Size = new System.Drawing.Size(58, 20);
            this.textBoxMinFeatureSize.TabIndex = 22;
            this.textBoxMinFeatureSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinFeatureSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxMinFeatureSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxExpression
            // 
            this.textBoxExpression.Location = new System.Drawing.Point(95, 151);
            this.textBoxExpression.Multiline = true;
            this.textBoxExpression.Name = "textBoxExpression";
            this.textBoxExpression.Size = new System.Drawing.Size(196, 42);
            this.textBoxExpression.TabIndex = 21;
            this.textBoxExpression.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Expression:";
            // 
            // textBoxMinDistance
            // 
            this.textBoxMinDistance.Location = new System.Drawing.Point(95, 39);
            this.textBoxMinDistance.Name = "textBoxMinDistance";
            this.textBoxMinDistance.Size = new System.Drawing.Size(58, 20);
            this.textBoxMinDistance.TabIndex = 11;
            this.textBoxMinDistance.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinDistance.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxMinDistance.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Min. Distance:";
            // 
            // checkBoxAutoMinFeatureSize
            // 
            this.checkBoxAutoMinFeatureSize.AutoSize = true;
            this.checkBoxAutoMinFeatureSize.Location = new System.Drawing.Point(172, 13);
            this.checkBoxAutoMinFeatureSize.Name = "checkBoxAutoMinFeatureSize";
            this.checkBoxAutoMinFeatureSize.Size = new System.Drawing.Size(133, 17);
            this.checkBoxAutoMinFeatureSize.TabIndex = 17;
            this.checkBoxAutoMinFeatureSize.Text = "Auto Min. Feature Size";
            this.checkBoxAutoMinFeatureSize.UseVisualStyleBackColor = true;
            this.checkBoxAutoMinFeatureSize.CheckedChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // checkBoxForce
            // 
            this.checkBoxForce.AutoSize = true;
            this.checkBoxForce.Location = new System.Drawing.Point(172, 66);
            this.checkBoxForce.Name = "checkBoxForce";
            this.checkBoxForce.Size = new System.Drawing.Size(53, 17);
            this.checkBoxForce.TabIndex = 16;
            this.checkBoxForce.Text = "Force";
            this.checkBoxForce.UseVisualStyleBackColor = true;
            this.checkBoxForce.CheckedChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // checkBoxPartials
            // 
            this.checkBoxPartials.AutoSize = true;
            this.checkBoxPartials.Location = new System.Drawing.Point(172, 40);
            this.checkBoxPartials.Name = "checkBoxPartials";
            this.checkBoxPartials.Size = new System.Drawing.Size(97, 17);
            this.checkBoxPartials.TabIndex = 15;
            this.checkBoxPartials.Text = "Display Partials";
            this.checkBoxPartials.UseVisualStyleBackColor = true;
            this.checkBoxPartials.CheckedChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // tabPagePosition
            // 
            this.tabPagePosition.Controls.Add(this.labelBindingControllerPosition);
            this.tabPagePosition.Controls.Add(this.labelBindingControllerPriority);
            this.tabPagePosition.Controls.Add(this.labelBindingControllerAngle);
            this.tabPagePosition.Controls.Add(this.textBoxPriority);
            this.tabPagePosition.Controls.Add(this.textBoxMaxLength);
            this.tabPagePosition.Controls.Add(this.label20);
            this.tabPagePosition.Controls.Add(this.textBoxBuffer);
            this.tabPagePosition.Controls.Add(this.label19);
            this.tabPagePosition.Controls.Add(this.comboBoxAngleMode);
            this.tabPagePosition.Controls.Add(this.label7);
            this.tabPagePosition.Controls.Add(this.comboBoxAlign);
            this.tabPagePosition.Controls.Add(this.label10);
            this.tabPagePosition.Controls.Add(this.textBoxWrap);
            this.tabPagePosition.Controls.Add(this.label8);
            this.tabPagePosition.Controls.Add(this.textBoxAngle);
            this.tabPagePosition.Controls.Add(this.textBoxOffsetY);
            this.tabPagePosition.Controls.Add(this.textBoxOffsetX);
            this.tabPagePosition.Controls.Add(this.label12);
            this.tabPagePosition.Controls.Add(this.label11);
            this.tabPagePosition.Controls.Add(this.comboBoxPosition);
            this.tabPagePosition.Location = new System.Drawing.Point(4, 22);
            this.tabPagePosition.Name = "tabPagePosition";
            this.tabPagePosition.Size = new System.Drawing.Size(302, 217);
            this.tabPagePosition.TabIndex = 6;
            this.tabPagePosition.Text = "Position";
            this.tabPagePosition.UseVisualStyleBackColor = true;
            // 
            // labelBindingControllerPosition
            // 
            this.labelBindingControllerPosition.AutoSize = true;
            this.labelBindingControllerPosition.BindingState = false;
            this.labelBindingControllerPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelBindingControllerPosition.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_POSITION;
            this.labelBindingControllerPosition.Location = new System.Drawing.Point(20, 13);
            this.labelBindingControllerPosition.Name = "labelBindingControllerPosition";
            this.labelBindingControllerPosition.Size = new System.Drawing.Size(47, 13);
            this.labelBindingControllerPosition.TabIndex = 30;
            this.labelBindingControllerPosition.TabStop = true;
            this.labelBindingControllerPosition.TargetControl = this.comboBoxPosition;
            this.labelBindingControllerPosition.Text = "Position:";
            this.labelBindingControllerPosition.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // comboBoxPosition
            // 
            this.comboBoxPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPosition.FormattingEnabled = true;
            this.comboBoxPosition.Location = new System.Drawing.Point(68, 9);
            this.comboBoxPosition.Name = "comboBoxPosition";
            this.comboBoxPosition.Size = new System.Drawing.Size(221, 21);
            this.comboBoxPosition.TabIndex = 9;
            this.comboBoxPosition.SelectedIndexChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // labelBindingControllerPriority
            // 
            this.labelBindingControllerPriority.AutoSize = true;
            this.labelBindingControllerPriority.BindingState = false;
            this.labelBindingControllerPriority.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelBindingControllerPriority.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_PRIORITY;
            this.labelBindingControllerPriority.Location = new System.Drawing.Point(26, 117);
            this.labelBindingControllerPriority.Name = "labelBindingControllerPriority";
            this.labelBindingControllerPriority.Size = new System.Drawing.Size(41, 13);
            this.labelBindingControllerPriority.TabIndex = 29;
            this.labelBindingControllerPriority.TabStop = true;
            this.labelBindingControllerPriority.TargetControl = this.textBoxPriority;
            this.labelBindingControllerPriority.Text = "Priority:";
            this.labelBindingControllerPriority.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // textBoxPriority
            // 
            this.textBoxPriority.Location = new System.Drawing.Point(68, 114);
            this.textBoxPriority.Name = "textBoxPriority";
            this.textBoxPriority.Size = new System.Drawing.Size(221, 20);
            this.textBoxPriority.TabIndex = 28;
            this.textBoxPriority.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxPriority.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxPriority.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // labelBindingControllerAngle
            // 
            this.labelBindingControllerAngle.AutoSize = true;
            this.labelBindingControllerAngle.BindingState = false;
            this.labelBindingControllerAngle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelBindingControllerAngle.LabelBinding = OSGeo.MapServer.MS_LABEL_BINDING_ENUM.MS_LABEL_BINDING_ANGLE;
            this.labelBindingControllerAngle.Location = new System.Drawing.Point(30, 91);
            this.labelBindingControllerAngle.Name = "labelBindingControllerAngle";
            this.labelBindingControllerAngle.Size = new System.Drawing.Size(37, 13);
            this.labelBindingControllerAngle.TabIndex = 18;
            this.labelBindingControllerAngle.TabStop = true;
            this.labelBindingControllerAngle.TargetControl = this.textBoxAngle;
            this.labelBindingControllerAngle.Text = "Angle:";
            this.labelBindingControllerAngle.ValueChanged += new System.EventHandler(this.ValueChanging);
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(68, 88);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(221, 20);
            this.textBoxAngle.TabIndex = 19;
            this.textBoxAngle.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxAngle.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxAngle.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxMaxLength
            // 
            this.textBoxMaxLength.Location = new System.Drawing.Point(68, 167);
            this.textBoxMaxLength.Name = "textBoxMaxLength";
            this.textBoxMaxLength.Size = new System.Drawing.Size(62, 20);
            this.textBoxMaxLength.TabIndex = 26;
            this.textBoxMaxLength.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMaxLength.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxMaxLength.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 170);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(63, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "MaxLength:";
            // 
            // textBoxBuffer
            // 
            this.textBoxBuffer.Location = new System.Drawing.Point(227, 62);
            this.textBoxBuffer.Name = "textBoxBuffer";
            this.textBoxBuffer.Size = new System.Drawing.Size(62, 20);
            this.textBoxBuffer.TabIndex = 24;
            this.textBoxBuffer.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxBuffer.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            this.textBoxBuffer.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(188, 66);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 23;
            this.label19.Text = "Buffer:";
            // 
            // comboBoxAngleMode
            // 
            this.comboBoxAngleMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAngleMode.FormattingEnabled = true;
            this.comboBoxAngleMode.Location = new System.Drawing.Point(68, 62);
            this.comboBoxAngleMode.Name = "comboBoxAngleMode";
            this.comboBoxAngleMode.Size = new System.Drawing.Size(107, 21);
            this.comboBoxAngleMode.TabIndex = 21;
            this.comboBoxAngleMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxAngleMode_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "AngleMode:";
            // 
            // comboBoxAlign
            // 
            this.comboBoxAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlign.FormattingEnabled = true;
            this.comboBoxAlign.Location = new System.Drawing.Point(68, 140);
            this.comboBoxAlign.Name = "comboBoxAlign";
            this.comboBoxAlign.Size = new System.Drawing.Size(221, 21);
            this.comboBoxAlign.TabIndex = 15;
            this.comboBoxAlign.SelectedIndexChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Align:";
            // 
            // textBoxWrap
            // 
            this.textBoxWrap.Location = new System.Drawing.Point(220, 167);
            this.textBoxWrap.MaxLength = 1;
            this.textBoxWrap.Name = "textBoxWrap";
            this.textBoxWrap.Size = new System.Drawing.Size(69, 20);
            this.textBoxWrap.TabIndex = 17;
            this.textBoxWrap.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxWrap.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(184, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Wrap:";
            // 
            // textBoxOffsetY
            // 
            this.textBoxOffsetY.Location = new System.Drawing.Point(227, 36);
            this.textBoxOffsetY.Name = "textBoxOffsetY";
            this.textBoxOffsetY.Size = new System.Drawing.Size(62, 20);
            this.textBoxOffsetY.TabIndex = 13;
            this.textBoxOffsetY.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxOffsetY.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxOffsetY.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxOffsetX
            // 
            this.textBoxOffsetX.Location = new System.Drawing.Point(68, 36);
            this.textBoxOffsetX.Name = "textBoxOffsetX";
            this.textBoxOffsetX.Size = new System.Drawing.Size(65, 20);
            this.textBoxOffsetX.TabIndex = 11;
            this.textBoxOffsetX.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxOffsetX.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxOffsetX.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(178, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Y Offset:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "X Offset:";
            // 
            // tabPageStyles
            // 
            this.tabPageStyles.Controls.Add(this.buttonRemoveStyle);
            this.tabPageStyles.Controls.Add(this.buttonMoveStyleDown);
            this.tabPageStyles.Controls.Add(this.buttonMoveStyleUp);
            this.tabPageStyles.Controls.Add(this.buttonEditStyle);
            this.tabPageStyles.Controls.Add(this.buttonAddStyle);
            this.tabPageStyles.Controls.Add(this.listViewStyles);
            this.tabPageStyles.Location = new System.Drawing.Point(4, 22);
            this.tabPageStyles.Name = "tabPageStyles";
            this.tabPageStyles.Size = new System.Drawing.Size(302, 217);
            this.tabPageStyles.TabIndex = 4;
            this.tabPageStyles.Text = "Styles";
            this.tabPageStyles.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveStyle
            // 
            this.buttonRemoveStyle.Location = new System.Drawing.Point(214, 120);
            this.buttonRemoveStyle.Name = "buttonRemoveStyle";
            this.buttonRemoveStyle.Size = new System.Drawing.Size(85, 23);
            this.buttonRemoveStyle.TabIndex = 5;
            this.buttonRemoveStyle.Text = "Remove Style";
            this.buttonRemoveStyle.UseVisualStyleBackColor = true;
            this.buttonRemoveStyle.Click += new System.EventHandler(this.buttonRemoveStyle_Click);
            // 
            // buttonMoveStyleDown
            // 
            this.buttonMoveStyleDown.Location = new System.Drawing.Point(214, 91);
            this.buttonMoveStyleDown.Name = "buttonMoveStyleDown";
            this.buttonMoveStyleDown.Size = new System.Drawing.Size(85, 23);
            this.buttonMoveStyleDown.TabIndex = 4;
            this.buttonMoveStyleDown.Text = "Move Down";
            this.buttonMoveStyleDown.UseVisualStyleBackColor = true;
            this.buttonMoveStyleDown.Click += new System.EventHandler(this.buttonMoveStyleDown_Click);
            // 
            // buttonMoveStyleUp
            // 
            this.buttonMoveStyleUp.Location = new System.Drawing.Point(214, 62);
            this.buttonMoveStyleUp.Name = "buttonMoveStyleUp";
            this.buttonMoveStyleUp.Size = new System.Drawing.Size(85, 23);
            this.buttonMoveStyleUp.TabIndex = 3;
            this.buttonMoveStyleUp.Text = "Move Up";
            this.buttonMoveStyleUp.UseVisualStyleBackColor = true;
            this.buttonMoveStyleUp.Click += new System.EventHandler(this.buttonMoveStyleUp_Click);
            // 
            // buttonEditStyle
            // 
            this.buttonEditStyle.Location = new System.Drawing.Point(214, 33);
            this.buttonEditStyle.Name = "buttonEditStyle";
            this.buttonEditStyle.Size = new System.Drawing.Size(85, 23);
            this.buttonEditStyle.TabIndex = 2;
            this.buttonEditStyle.Text = "Edit Style";
            this.buttonEditStyle.UseVisualStyleBackColor = true;
            this.buttonEditStyle.Click += new System.EventHandler(this.buttonEditStyle_Click);
            // 
            // buttonAddStyle
            // 
            this.buttonAddStyle.Location = new System.Drawing.Point(214, 4);
            this.buttonAddStyle.Name = "buttonAddStyle";
            this.buttonAddStyle.Size = new System.Drawing.Size(85, 23);
            this.buttonAddStyle.TabIndex = 1;
            this.buttonAddStyle.Text = "Add Style";
            this.buttonAddStyle.UseVisualStyleBackColor = true;
            this.buttonAddStyle.Click += new System.EventHandler(this.buttonAddStyle_Click);
            // 
            // listViewStyles
            // 
            this.listViewStyles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStyles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewStyles.FullRowSelect = true;
            this.listViewStyles.HideSelection = false;
            this.listViewStyles.Location = new System.Drawing.Point(3, 3);
            this.listViewStyles.MultiSelect = false;
            this.listViewStyles.Name = "listViewStyles";
            this.listViewStyles.Size = new System.Drawing.Size(209, 211);
            this.listViewStyles.TabIndex = 0;
            this.listViewStyles.UseCompatibleStateImageBehavior = false;
            this.listViewStyles.View = System.Windows.Forms.View.Details;
            this.listViewStyles.SelectedIndexChanged += new System.EventHandler(this.listViewStyles_SelectedIndexChanged);
            this.listViewStyles.DoubleClick += new System.EventHandler(this.listViewStyles_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 36;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 37;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Width";
            this.columnHeader3.Width = 47;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Symbol";
            this.columnHeader4.Width = 83;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mapControl
            // 
            this.mapControl.Border = true;
            this.mapControl.Gap = 10;
            this.mapControl.InputMode = DMS.MapLibrary.MapControl.InputModes.Pan;
            this.mapControl.Location = new System.Drawing.Point(315, 20);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(146, 222);
            this.mapControl.TabIndex = 6;
            this.mapControl.Target = null;
            this.mapControl.Text = "mapControl1";
            // 
            // LabelPropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapControl);
            this.Controls.Add(this.tabControl1);
            this.Name = "LabelPropertyEditor";
            this.Size = new System.Drawing.Size(466, 247);
            this.tabControl1.ResumeLayout(false);
            this.tabPageRendering.ResumeLayout(false);
            this.tabPageRendering.PerformLayout();
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            this.tabPagePosition.ResumeLayout(false);
            this.tabPagePosition.PerformLayout();
            this.tabPageStyles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MapControl mapControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private ColorPicker colorPickerOutlineColor;
        private ColorPicker colorPickerColor;
        private System.Windows.Forms.CheckBox checkBoxAutoMinFeatureSize;
        private System.Windows.Forms.CheckBox checkBoxForce;
        private System.Windows.Forms.CheckBox checkBoxPartials;
        private System.Windows.Forms.TextBox textBoxShadowSizeY;
        private System.Windows.Forms.TextBox textBoxShadowSizeX;
        private ColorPicker colorPickerShadowColor;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxMaxSize;
        private System.Windows.Forms.TextBox textBoxMinSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAngle;
        private System.Windows.Forms.TextBox textBoxOffsetY;
        private System.Windows.Forms.TextBox textBoxOffsetX;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxPosition;
        private LabelBindingController labelBindingControllerAngle;
        private LabelBindingController labelBindingControllerOutlineColor;
        private LabelBindingController labelBindingControllerColor;
        private System.Windows.Forms.TextBox textBoxMinDistance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAlign;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxWrap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPageStyles;
        private System.Windows.Forms.ListView listViewStyles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button buttonRemoveStyle;
        private System.Windows.Forms.Button buttonMoveStyleDown;
        private System.Windows.Forms.Button buttonMoveStyleUp;
        private System.Windows.Forms.Button buttonEditStyle;
        private System.Windows.Forms.Button buttonAddStyle;
        //private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ComboBox comboBoxAngleMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxExpression;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPageRendering;
        private LabelBindingController labelBindingControllerSize;
        private LabelBindingController labelBindingControllerFont;
        private System.Windows.Forms.ComboBox comboBoxFont;
        private System.Windows.Forms.TabPage tabPagePosition;
        private System.Windows.Forms.TextBox textBoxMinFeatureSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRepeatDistance;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxBuffer;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxMaxLength;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxPriority;
        private System.Windows.Forms.Label labelMaxZoom;
        private System.Windows.Forms.Label labelMinZoom;
        private System.Windows.Forms.TextBox textBoxMaxScale;
        private System.Windows.Forms.TextBox textBoxMinScale;
        private System.Windows.Forms.Button buttonMinScale;
        private System.Windows.Forms.Button buttonMaxScale;
        private System.Windows.Forms.ToolTip toolTip;
        private LabelBindingController labelBindingControllerPriority;
        private LabelBindingController labelBindingControllerPosition;
        private LabelBindingController labelBindingControllerShadowSizeX;
        private LabelBindingController labelBindingControllerShadowSizeY;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxEncoding;
    }
}
