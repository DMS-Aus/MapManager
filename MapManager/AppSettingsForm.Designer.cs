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
            this.buttonEditor = new System.Windows.Forms.Button();
            this.textBoxEditor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAutoLoadRecent = new System.Windows.Forms.CheckBox();
            this.tabPageLayerPanel = new System.Windows.Forms.TabPage();
            this.checkBoxLabels = new System.Windows.Forms.CheckBox();
            this.colorPickerLayerBackgroundColor = new DMS.MapLibrary.ColorPicker();
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
            this.checkBoxEnableConversion = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageLayerPanel.SuspendLayout();
            this.tabPageColorRamp.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(53, 250);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.buttonCancel.Location = new System.Drawing.Point(216, 250);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(381, 242);
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
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageGeneral.Size = new System.Drawing.Size(373, 213);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // buttonEditor
            // 
            this.buttonEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditor.Location = new System.Drawing.Point(335, 113);
            this.buttonEditor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.textBoxEditor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.label2.Size = new System.Drawing.Size(237, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Preferred text editor to edit map files";
            // 
            // checkBoxAutoLoadRecent
            // 
            this.checkBoxAutoLoadRecent.AutoSize = true;
            this.checkBoxAutoLoadRecent.Location = new System.Drawing.Point(45, 36);
            this.checkBoxAutoLoadRecent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxAutoLoadRecent.Name = "checkBoxAutoLoadRecent";
            this.checkBoxAutoLoadRecent.Size = new System.Drawing.Size(243, 21);
            this.checkBoxAutoLoadRecent.TabIndex = 0;
            this.checkBoxAutoLoadRecent.Text = "Automatically load the recent map";
            this.checkBoxAutoLoadRecent.UseVisualStyleBackColor = true;
            // 
            // tabPageLayerPanel
            // 
            this.tabPageLayerPanel.Controls.Add(this.checkBoxLabels);
            this.tabPageLayerPanel.Controls.Add(this.colorPickerLayerBackgroundColor);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxStyles);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxClasses);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxCheck);
            this.tabPageLayerPanel.Controls.Add(this.checkBoxRoot);
            this.tabPageLayerPanel.Controls.Add(this.label1);
            this.tabPageLayerPanel.Location = new System.Drawing.Point(4, 25);
            this.tabPageLayerPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageLayerPanel.Name = "tabPageLayerPanel";
            this.tabPageLayerPanel.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageLayerPanel.Size = new System.Drawing.Size(373, 213);
            this.tabPageLayerPanel.TabIndex = 1;
            this.tabPageLayerPanel.Text = "Layer Panel";
            this.tabPageLayerPanel.UseVisualStyleBackColor = true;
            // 
            // checkBoxLabels
            // 
            this.checkBoxLabels.AutoSize = true;
            this.checkBoxLabels.Location = new System.Drawing.Point(41, 181);
            this.checkBoxLabels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxLabels.Name = "checkBoxLabels";
            this.checkBoxLabels.Size = new System.Drawing.Size(110, 21);
            this.checkBoxLabels.TabIndex = 6;
            this.checkBoxLabels.Text = "Show Labels";
            this.checkBoxLabels.UseVisualStyleBackColor = true;
            // 
            // colorPickerLayerBackgroundColor
            // 
            this.colorPickerLayerBackgroundColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerLayerBackgroundColor.Context = null;
            this.colorPickerLayerBackgroundColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerLayerBackgroundColor.Location = new System.Drawing.Point(171, 28);
            this.colorPickerLayerBackgroundColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorPickerLayerBackgroundColor.Name = "colorPickerLayerBackgroundColor";
            this.colorPickerLayerBackgroundColor.ReadOnly = false;
            this.colorPickerLayerBackgroundColor.Size = new System.Drawing.Size(187, 22);
            this.colorPickerLayerBackgroundColor.TabIndex = 1;
            this.colorPickerLayerBackgroundColor.Text = "White";
            this.colorPickerLayerBackgroundColor.Value = System.Drawing.Color.White;
            // 
            // checkBoxStyles
            // 
            this.checkBoxStyles.AutoSize = true;
            this.checkBoxStyles.Location = new System.Drawing.Point(41, 153);
            this.checkBoxStyles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxStyles.Name = "checkBoxStyles";
            this.checkBoxStyles.Size = new System.Drawing.Size(106, 21);
            this.checkBoxStyles.TabIndex = 5;
            this.checkBoxStyles.Text = "Show Styles";
            this.checkBoxStyles.UseVisualStyleBackColor = true;
            // 
            // checkBoxClasses
            // 
            this.checkBoxClasses.AutoSize = true;
            this.checkBoxClasses.Location = new System.Drawing.Point(41, 123);
            this.checkBoxClasses.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxClasses.Name = "checkBoxClasses";
            this.checkBoxClasses.Size = new System.Drawing.Size(117, 21);
            this.checkBoxClasses.TabIndex = 4;
            this.checkBoxClasses.Text = "Show Classes";
            this.checkBoxClasses.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheck
            // 
            this.checkBoxCheck.AutoSize = true;
            this.checkBoxCheck.Location = new System.Drawing.Point(41, 94);
            this.checkBoxCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxCheck.Name = "checkBoxCheck";
            this.checkBoxCheck.Size = new System.Drawing.Size(144, 21);
            this.checkBoxCheck.TabIndex = 3;
            this.checkBoxCheck.Text = "Show Checkboxes";
            this.checkBoxCheck.UseVisualStyleBackColor = true;
            // 
            // checkBoxRoot
            // 
            this.checkBoxRoot.AutoSize = true;
            this.checkBoxRoot.Location = new System.Drawing.Point(41, 64);
            this.checkBoxRoot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxRoot.Name = "checkBoxRoot";
            this.checkBoxRoot.Size = new System.Drawing.Size(136, 21);
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
            this.label1.Size = new System.Drawing.Size(133, 17);
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
            this.tabPageColorRamp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageColorRamp.Name = "tabPageColorRamp";
            this.tabPageColorRamp.Size = new System.Drawing.Size(373, 213);
            this.tabPageColorRamp.TabIndex = 2;
            this.tabPageColorRamp.Text = "Color Ramps";
            this.tabPageColorRamp.UseVisualStyleBackColor = true;
            // 
            // panelColorRamp
            // 
            this.panelColorRamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorRamp.Location = new System.Drawing.Point(5, 6);
            this.panelColorRamp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelColorRamp.Name = "panelColorRamp";
            this.panelColorRamp.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelColorRamp.Size = new System.Drawing.Size(270, 200);
            this.panelColorRamp.TabIndex = 4;
            // 
            // buttonDeleteColorRamp
            // 
            this.buttonDeleteColorRamp.Location = new System.Drawing.Point(284, 78);
            this.buttonDeleteColorRamp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.buttonEditColorRamp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.buttonNewColorRamp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonNewColorRamp.Name = "buttonNewColorRamp";
            this.buttonNewColorRamp.Size = new System.Drawing.Size(77, 28);
            this.buttonNewColorRamp.TabIndex = 1;
            this.buttonNewColorRamp.Text = "New";
            this.buttonNewColorRamp.UseVisualStyleBackColor = true;
            this.buttonNewColorRamp.Click += new System.EventHandler(this.buttonNewColorRamp_Click);
            // 
            // checkBoxEnableConversion
            // 
            this.checkBoxEnableConversion.AutoSize = true;
            this.checkBoxEnableConversion.Location = new System.Drawing.Point(45, 62);
            this.checkBoxEnableConversion.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxEnableConversion.Name = "checkBoxEnableConversion";
            this.checkBoxEnableConversion.Size = new System.Drawing.Size(200, 21);
            this.checkBoxEnableConversion.TabIndex = 4;
            this.checkBoxEnableConversion.Text = "Enable map file conversion";
            this.checkBoxEnableConversion.UseVisualStyleBackColor = true;
            // 
            // AppSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 294);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}