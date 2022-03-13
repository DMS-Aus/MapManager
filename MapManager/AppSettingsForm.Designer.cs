namespace DMS.MapManager
{
    partial class AppSettingsForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.checkBoxEnableConversion = new System.Windows.Forms.CheckBox();
            this.buttonEditor = new System.Windows.Forms.Button();
            this.textBoxEditor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAutoLoadRecent = new System.Windows.Forms.CheckBox();
            this.tabPageLayerPanel = new System.Windows.Forms.TabPage();
            this.checkBoxLabels = new System.Windows.Forms.CheckBox();
            this.checkBoxStyles = new System.Windows.Forms.CheckBox();
            this.checkBoxClasses = new System.Windows.Forms.CheckBox();
            this.checkBoxCheck = new System.Windows.Forms.CheckBox();
            this.checkBoxRoot = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageColorRamp = new System.Windows.Forms.TabPage();
            this.panelColorRamp = new System.Windows.Forms.Panel();
            this.buttonDeleteColorRamp = new System.Windows.Forms.Button();
            this.buttonEditColorRamp = new System.Windows.Forms.Button();
            this.buttonNewColorRamp = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tabPageTextEditor = new System.Windows.Forms.TabPage();
            this.checkBoxTextWrapping = new System.Windows.Forms.CheckBox();
            this.checkBoxShowGlyphs = new System.Windows.Forms.CheckBox();
            this.colorPickerLayerBackgroundColor = new DMS.MapLibrary.ColorPicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.colorPickerTextEditorString = new DMS.MapLibrary.ColorPicker();
            this.label5 = new System.Windows.Forms.Label();
            this.colorPickerTextEditorNumber = new DMS.MapLibrary.ColorPicker();
            this.label4 = new System.Windows.Forms.Label();
            this.colorPickerTextEditorComment = new DMS.MapLibrary.ColorPicker();
            this.colorPickerTextEditorPropertyName = new DMS.MapLibrary.ColorPicker();
            this.label3 = new System.Windows.Forms.Label();
            this.colorPickerTextEditorObjectName = new DMS.MapLibrary.ColorPicker();
            this.label8 = new System.Windows.Forms.Label();
            this.colorPickerTextEditorKeyword = new DMS.MapLibrary.ColorPicker();
            this.label9 = new System.Windows.Forms.Label();
            this.colorPickerTextEditorBinding = new DMS.MapLibrary.ColorPicker();
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageLayerPanel.SuspendLayout();
            this.tabPageColorRamp.SuspendLayout();
            this.tabPageTextEditor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(20, 319);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 28);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(249, 319);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageLayerPanel);
            this.tabControl.Controls.Add(this.tabPageColorRamp);
            this.tabControl.Controls.Add(this.tabPageTextEditor);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(381, 311);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.checkBoxEnableConversion);
            this.tabPageGeneral.Controls.Add(this.buttonEditor);
            this.tabPageGeneral.Controls.Add(this.textBoxEditor);
            this.tabPageGeneral.Controls.Add(this.label2);
            this.tabPageGeneral.Controls.Add(this.checkBoxAutoLoadRecent);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageGeneral.Size = new System.Drawing.Size(373, 282);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableConversion
            // 
            this.checkBoxEnableConversion.AutoSize = true;
            this.checkBoxEnableConversion.Location = new System.Drawing.Point(45, 62);
            this.checkBoxEnableConversion.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxEnableConversion.Name = "checkBoxEnableConversion";
            this.checkBoxEnableConversion.Size = new System.Drawing.Size(191, 20);
            this.checkBoxEnableConversion.TabIndex = 4;
            this.checkBoxEnableConversion.Text = "Enable map file conversion";
            this.checkBoxEnableConversion.UseVisualStyleBackColor = true;
            // 
            // buttonEditor
            // 
            this.buttonEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditor.Location = new System.Drawing.Point(335, 113);
            this.buttonEditor.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditor.Name = "buttonEditor";
            this.buttonEditor.Size = new System.Drawing.Size(32, 28);
            this.buttonEditor.TabIndex = 3;
            this.buttonEditor.Text = "...";
            this.buttonEditor.UseVisualStyleBackColor = true;
            this.buttonEditor.Click += new System.EventHandler(this.buttonEditor_Click);
            // 
            // textBoxEditor
            // 
            this.textBoxEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEditor.Location = new System.Drawing.Point(15, 116);
            this.textBoxEditor.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEditor.Name = "textBoxEditor";
            this.textBoxEditor.Size = new System.Drawing.Size(309, 22);
            this.textBoxEditor.TabIndex = 2;
            this.textBoxEditor.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEditor_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Preferred text editor to edit map files";
            // 
            // checkBoxAutoLoadRecent
            // 
            this.checkBoxAutoLoadRecent.AutoSize = true;
            this.checkBoxAutoLoadRecent.Location = new System.Drawing.Point(45, 36);
            this.checkBoxAutoLoadRecent.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAutoLoadRecent.Name = "checkBoxAutoLoadRecent";
            this.checkBoxAutoLoadRecent.Size = new System.Drawing.Size(230, 20);
            this.checkBoxAutoLoadRecent.TabIndex = 0;
            this.checkBoxAutoLoadRecent.Text = "Automatically load the recent map";
            this.checkBoxAutoLoadRecent.UseVisualStyleBackColor = true;
            // 
            // tabPageLayerPanel
            // 
            this.tabPageLayerPanel.Controls.Add(this.checkBoxLabels);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxStyles);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxClasses);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxCheck);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxRoot);
            this.tabPageLayerPanel.Controls.Add(this.label1);
            this.tabPageLayerPanel.Controls.Add(this.colorPickerLayerBackgroundColor);
            this.tabPageLayerPanel.Location = new System.Drawing.Point(4, 25);
            this.tabPageLayerPanel.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageLayerPanel.Name = "tabPageLayerPanel";
            this.tabPageLayerPanel.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageLayerPanel.Size = new System.Drawing.Size(373, 282);
            this.tabPageLayerPanel.TabIndex = 1;
            this.tabPageLayerPanel.Text = "Layer Panel";
            this.tabPageLayerPanel.UseVisualStyleBackColor = true;
            // 
            // checkBoxLabels
            // 
            this.checkBoxLabels.AutoSize = true;
            this.checkBoxLabels.Location = new System.Drawing.Point(41, 181);
            this.checkBoxLabels.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxLabels.Name = "checkBoxLabels";
            this.checkBoxLabels.Size = new System.Drawing.Size(106, 20);
            this.checkBoxLabels.TabIndex = 6;
            this.checkBoxLabels.Text = "Show Labels";
            this.checkBoxLabels.UseVisualStyleBackColor = true;
            // 
            // checkBoxStyles
            // 
            this.checkBoxStyles.AutoSize = true;
            this.checkBoxStyles.Location = new System.Drawing.Point(41, 153);
            this.checkBoxStyles.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxStyles.Name = "checkBoxStyles";
            this.checkBoxStyles.Size = new System.Drawing.Size(102, 20);
            this.checkBoxStyles.TabIndex = 5;
            this.checkBoxStyles.Text = "Show Styles";
            this.checkBoxStyles.UseVisualStyleBackColor = true;
            // 
            // checkBoxClasses
            // 
            this.checkBoxClasses.AutoSize = true;
            this.checkBoxClasses.Location = new System.Drawing.Point(41, 123);
            this.checkBoxClasses.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxClasses.Name = "checkBoxClasses";
            this.checkBoxClasses.Size = new System.Drawing.Size(114, 20);
            this.checkBoxClasses.TabIndex = 4;
            this.checkBoxClasses.Text = "Show Classes";
            this.checkBoxClasses.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheck
            // 
            this.checkBoxCheck.AutoSize = true;
            this.checkBoxCheck.Location = new System.Drawing.Point(41, 94);
            this.checkBoxCheck.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxCheck.Name = "checkBoxCheck";
            this.checkBoxCheck.Size = new System.Drawing.Size(140, 20);
            this.checkBoxCheck.TabIndex = 3;
            this.checkBoxCheck.Text = "Show Checkboxes";
            this.checkBoxCheck.UseVisualStyleBackColor = true;
            // 
            // checkBoxRoot
            // 
            this.checkBoxRoot.AutoSize = true;
            this.checkBoxRoot.Location = new System.Drawing.Point(41, 64);
            this.checkBoxRoot.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRoot.Name = "checkBoxRoot";
            this.checkBoxRoot.Size = new System.Drawing.Size(131, 20);
            this.checkBoxRoot.TabIndex = 2;
            this.checkBoxRoot.Text = "Show Root Node";
            this.checkBoxRoot.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Background Colour:";
            // 
            // tabPageColorRamp
            // 
            this.tabPageColorRamp.Controls.Add(this.panelColorRamp);
            this.tabPageColorRamp.Controls.Add(this.buttonDeleteColorRamp);
            this.tabPageColorRamp.Controls.Add(this.buttonEditColorRamp);
            this.tabPageColorRamp.Controls.Add(this.buttonNewColorRamp);
            this.tabPageColorRamp.Location = new System.Drawing.Point(4, 25);
            this.tabPageColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageColorRamp.Name = "tabPageColorRamp";
            this.tabPageColorRamp.Size = new System.Drawing.Size(373, 282);
            this.tabPageColorRamp.TabIndex = 2;
            this.tabPageColorRamp.Text = "Color Ramps";
            this.tabPageColorRamp.UseVisualStyleBackColor = true;
            // 
            // panelColorRamp
            // 
            this.panelColorRamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorRamp.Location = new System.Drawing.Point(5, 6);
            this.panelColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.panelColorRamp.Name = "panelColorRamp";
            this.panelColorRamp.Padding = new System.Windows.Forms.Padding(4);
            this.panelColorRamp.Size = new System.Drawing.Size(270, 272);
            this.panelColorRamp.TabIndex = 4;
            // 
            // buttonDeleteColorRamp
            // 
            this.buttonDeleteColorRamp.Location = new System.Drawing.Point(284, 78);
            this.buttonDeleteColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDeleteColorRamp.Name = "buttonDeleteColorRamp";
            this.buttonDeleteColorRamp.Size = new System.Drawing.Size(76, 28);
            this.buttonDeleteColorRamp.TabIndex = 3;
            this.buttonDeleteColorRamp.Text = "Delete";
            this.buttonDeleteColorRamp.UseVisualStyleBackColor = true;
            this.buttonDeleteColorRamp.Click += new System.EventHandler(this.buttonDeleteColorRamp_Click);
            // 
            // buttonEditColorRamp
            // 
            this.buttonEditColorRamp.Location = new System.Drawing.Point(284, 42);
            this.buttonEditColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditColorRamp.Name = "buttonEditColorRamp";
            this.buttonEditColorRamp.Size = new System.Drawing.Size(76, 28);
            this.buttonEditColorRamp.TabIndex = 2;
            this.buttonEditColorRamp.Text = "Edit";
            this.buttonEditColorRamp.UseVisualStyleBackColor = true;
            this.buttonEditColorRamp.Click += new System.EventHandler(this.buttonEditColorRamp_Click);
            // 
            // buttonNewColorRamp
            // 
            this.buttonNewColorRamp.Location = new System.Drawing.Point(284, 6);
            this.buttonNewColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNewColorRamp.Name = "buttonNewColorRamp";
            this.buttonNewColorRamp.Size = new System.Drawing.Size(77, 28);
            this.buttonNewColorRamp.TabIndex = 1;
            this.buttonNewColorRamp.Text = "New";
            this.buttonNewColorRamp.UseVisualStyleBackColor = true;
            this.buttonNewColorRamp.Click += new System.EventHandler(this.buttonNewColorRamp_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(135, 319);
            this.buttonApply.Margin = new System.Windows.Forms.Padding(4);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(100, 28);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // tabPageTextEditor
            // 
            this.tabPageTextEditor.Controls.Add(this.groupBox1);
            this.tabPageTextEditor.Controls.Add(this.checkBoxShowGlyphs);
            this.tabPageTextEditor.Controls.Add(this.checkBoxTextWrapping);
            this.tabPageTextEditor.Location = new System.Drawing.Point(4, 25);
            this.tabPageTextEditor.Name = "tabPageTextEditor";
            this.tabPageTextEditor.Size = new System.Drawing.Size(373, 282);
            this.tabPageTextEditor.TabIndex = 3;
            this.tabPageTextEditor.Text = "Text Editor";
            this.tabPageTextEditor.UseVisualStyleBackColor = true;
            // 
            // checkBoxTextWrapping
            // 
            this.checkBoxTextWrapping.AutoSize = true;
            this.checkBoxTextWrapping.Location = new System.Drawing.Point(28, 14);
            this.checkBoxTextWrapping.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxTextWrapping.Name = "checkBoxTextWrapping";
            this.checkBoxTextWrapping.Size = new System.Drawing.Size(122, 20);
            this.checkBoxTextWrapping.TabIndex = 1;
            this.checkBoxTextWrapping.Text = "Wrap long lines";
            this.checkBoxTextWrapping.UseVisualStyleBackColor = true;
            this.checkBoxTextWrapping.CheckedChanged += new System.EventHandler(this.checkBoxTextWrapping_CheckedChanged);
            // 
            // checkBoxShowGlyphs
            // 
            this.checkBoxShowGlyphs.AutoSize = true;
            this.checkBoxShowGlyphs.Enabled = false;
            this.checkBoxShowGlyphs.Location = new System.Drawing.Point(180, 14);
            this.checkBoxShowGlyphs.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxShowGlyphs.Name = "checkBoxShowGlyphs";
            this.checkBoxShowGlyphs.Size = new System.Drawing.Size(143, 20);
            this.checkBoxShowGlyphs.TabIndex = 2;
            this.checkBoxShowGlyphs.Text = "Show visual glyphs";
            this.checkBoxShowGlyphs.UseVisualStyleBackColor = true;
            // 
            // colorPickerLayerBackgroundColor
            // 
            this.colorPickerLayerBackgroundColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerLayerBackgroundColor.Context = null;
            this.colorPickerLayerBackgroundColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerLayerBackgroundColor.Location = new System.Drawing.Point(171, 28);
            this.colorPickerLayerBackgroundColor.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerLayerBackgroundColor.Name = "colorPickerLayerBackgroundColor";
            this.colorPickerLayerBackgroundColor.ReadOnly = false;
            this.colorPickerLayerBackgroundColor.Size = new System.Drawing.Size(187, 22);
            this.colorPickerLayerBackgroundColor.TabIndex = 1;
            this.colorPickerLayerBackgroundColor.Text = "White";
            this.colorPickerLayerBackgroundColor.Value = System.Drawing.Color.White;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.colorPickerTextEditorBinding);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.colorPickerTextEditorKeyword);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.colorPickerTextEditorString);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.colorPickerTextEditorNumber);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.colorPickerTextEditorComment);
            this.groupBox1.Controls.Add(this.colorPickerTextEditorPropertyName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.colorPickerTextEditorObjectName);
            this.groupBox1.Location = new System.Drawing.Point(9, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 237);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colours";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 146);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "String Value:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 116);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Number Value:";
            // 
            // colorPickerTextEditorString
            // 
            this.colorPickerTextEditorString.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerTextEditorString.Context = null;
            this.colorPickerTextEditorString.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerTextEditorString.Location = new System.Drawing.Point(162, 142);
            this.colorPickerTextEditorString.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerTextEditorString.Name = "colorPickerTextEditorString";
            this.colorPickerTextEditorString.ReadOnly = false;
            this.colorPickerTextEditorString.Size = new System.Drawing.Size(187, 22);
            this.colorPickerTextEditorString.TabIndex = 20;
            this.colorPickerTextEditorString.Text = "White";
            this.colorPickerTextEditorString.Value = System.Drawing.Color.White;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 86);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Comment:";
            // 
            // colorPickerTextEditorNumber
            // 
            this.colorPickerTextEditorNumber.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerTextEditorNumber.Context = null;
            this.colorPickerTextEditorNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerTextEditorNumber.Location = new System.Drawing.Point(162, 112);
            this.colorPickerTextEditorNumber.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerTextEditorNumber.Name = "colorPickerTextEditorNumber";
            this.colorPickerTextEditorNumber.ReadOnly = false;
            this.colorPickerTextEditorNumber.Size = new System.Drawing.Size(187, 22);
            this.colorPickerTextEditorNumber.TabIndex = 18;
            this.colorPickerTextEditorNumber.Text = "White";
            this.colorPickerTextEditorNumber.Value = System.Drawing.Color.White;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Property Name:";
            // 
            // colorPickerTextEditorComment
            // 
            this.colorPickerTextEditorComment.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerTextEditorComment.Context = null;
            this.colorPickerTextEditorComment.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerTextEditorComment.Location = new System.Drawing.Point(162, 82);
            this.colorPickerTextEditorComment.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerTextEditorComment.Name = "colorPickerTextEditorComment";
            this.colorPickerTextEditorComment.ReadOnly = false;
            this.colorPickerTextEditorComment.Size = new System.Drawing.Size(187, 22);
            this.colorPickerTextEditorComment.TabIndex = 15;
            this.colorPickerTextEditorComment.Text = "White";
            this.colorPickerTextEditorComment.Value = System.Drawing.Color.White;
            // 
            // colorPickerTextEditorPropertyName
            // 
            this.colorPickerTextEditorPropertyName.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerTextEditorPropertyName.Context = null;
            this.colorPickerTextEditorPropertyName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerTextEditorPropertyName.Location = new System.Drawing.Point(162, 52);
            this.colorPickerTextEditorPropertyName.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerTextEditorPropertyName.Name = "colorPickerTextEditorPropertyName";
            this.colorPickerTextEditorPropertyName.ReadOnly = false;
            this.colorPickerTextEditorPropertyName.Size = new System.Drawing.Size(187, 22);
            this.colorPickerTextEditorPropertyName.TabIndex = 16;
            this.colorPickerTextEditorPropertyName.Text = "White";
            this.colorPickerTextEditorPropertyName.Value = System.Drawing.Color.White;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Object Name:";
            // 
            // colorPickerTextEditorObjectName
            // 
            this.colorPickerTextEditorObjectName.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerTextEditorObjectName.Context = null;
            this.colorPickerTextEditorObjectName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerTextEditorObjectName.Location = new System.Drawing.Point(162, 22);
            this.colorPickerTextEditorObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerTextEditorObjectName.Name = "colorPickerTextEditorObjectName";
            this.colorPickerTextEditorObjectName.ReadOnly = false;
            this.colorPickerTextEditorObjectName.Size = new System.Drawing.Size(187, 22);
            this.colorPickerTextEditorObjectName.TabIndex = 12;
            this.colorPickerTextEditorObjectName.Text = "White";
            this.colorPickerTextEditorObjectName.Value = System.Drawing.Color.White;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 176);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 16);
            this.label8.TabIndex = 23;
            this.label8.Text = "Keyword Value:";
            // 
            // colorPickerTextEditorKeyword
            // 
            this.colorPickerTextEditorKeyword.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerTextEditorKeyword.Context = null;
            this.colorPickerTextEditorKeyword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerTextEditorKeyword.Location = new System.Drawing.Point(162, 172);
            this.colorPickerTextEditorKeyword.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerTextEditorKeyword.Name = "colorPickerTextEditorKeyword";
            this.colorPickerTextEditorKeyword.ReadOnly = false;
            this.colorPickerTextEditorKeyword.Size = new System.Drawing.Size(187, 22);
            this.colorPickerTextEditorKeyword.TabIndex = 24;
            this.colorPickerTextEditorKeyword.Text = "White";
            this.colorPickerTextEditorKeyword.Value = System.Drawing.Color.White;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 206);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 16);
            this.label9.TabIndex = 25;
            this.label9.Text = "Attribute Binding:";
            // 
            // colorPickerTextEditorBinding
            // 
            this.colorPickerTextEditorBinding.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerTextEditorBinding.Context = null;
            this.colorPickerTextEditorBinding.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerTextEditorBinding.Location = new System.Drawing.Point(162, 202);
            this.colorPickerTextEditorBinding.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerTextEditorBinding.Name = "colorPickerTextEditorBinding";
            this.colorPickerTextEditorBinding.ReadOnly = false;
            this.colorPickerTextEditorBinding.Size = new System.Drawing.Size(187, 22);
            this.colorPickerTextEditorBinding.TabIndex = 26;
            this.colorPickerTextEditorBinding.Text = "White";
            this.colorPickerTextEditorBinding.Value = System.Drawing.Color.White;
            // 
            // AppSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 360);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppSettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AppSettingsForm_KeyDown);
            this.tabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageLayerPanel.ResumeLayout(false);
            this.tabPageLayerPanel.PerformLayout();
            this.tabPageColorRamp.ResumeLayout(false);
            this.tabPageTextEditor.ResumeLayout(false);
            this.tabPageTextEditor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageLayerPanel;
        private System.Windows.Forms.CheckBox checkBoxAutoLoadRecent;
        private System.Windows.Forms.CheckBox checkBoxStyles;
        private System.Windows.Forms.CheckBox checkBoxClasses;
        private System.Windows.Forms.CheckBox checkBoxCheck;
        private System.Windows.Forms.CheckBox checkBoxRoot;
        private DMS.MapLibrary.ColorPicker colorPickerLayerBackgroundColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonEditor;
        private System.Windows.Forms.TextBox textBoxEditor;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabPage tabPageColorRamp;
        private System.Windows.Forms.Button buttonNewColorRamp;
        private System.Windows.Forms.Button buttonDeleteColorRamp;
        private System.Windows.Forms.Button buttonEditColorRamp;
        private System.Windows.Forms.Panel panelColorRamp;
        private System.Windows.Forms.CheckBox checkBoxLabels;
        private System.Windows.Forms.CheckBox checkBoxEnableConversion;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabPage tabPageTextEditor;
        private System.Windows.Forms.CheckBox checkBoxShowGlyphs;
        private System.Windows.Forms.CheckBox checkBoxTextWrapping;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private MapLibrary.ColorPicker colorPickerTextEditorKeyword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private MapLibrary.ColorPicker colorPickerTextEditorString;
        private System.Windows.Forms.Label label5;
        private MapLibrary.ColorPicker colorPickerTextEditorNumber;
        private System.Windows.Forms.Label label4;
        private MapLibrary.ColorPicker colorPickerTextEditorComment;
        private MapLibrary.ColorPicker colorPickerTextEditorPropertyName;
        private System.Windows.Forms.Label label3;
        private MapLibrary.ColorPicker colorPickerTextEditorObjectName;
        private System.Windows.Forms.Label label9;
        private MapLibrary.ColorPicker colorPickerTextEditorBinding;
    }
}