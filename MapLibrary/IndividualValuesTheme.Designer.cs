namespace DMS.MapLibrary
{
    partial class IndividualValuesTheme
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
            this.WizardPage1 = new System.Windows.Forms.Panel();
            this.checkBoxZero = new System.Windows.Forms.CheckBox();
            this.comboBoxColumns = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.WizardPage2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.colorRampPickerBackgroundColor = new DMS.MapLibrary.ColorRampPicker();
            this.colorRampPickerOutlineColor = new DMS.MapLibrary.ColorRampPicker();
            this.label3 = new System.Windows.Forms.Label();
            this.colorRampPickerColor = new DMS.MapLibrary.ColorRampPicker();
            this.checkBoxFirstOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepStyles = new System.Windows.Forms.CheckBox();
            this.layerControl = new DMS.MapLibrary.LayerControl();
            this.WizardPage1.SuspendLayout();
            this.WizardPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardPage1
            // 
            this.WizardPage1.Controls.Add(this.checkBoxZero);
            this.WizardPage1.Controls.Add(this.comboBoxColumns);
            this.WizardPage1.Controls.Add(this.label2);
            this.WizardPage1.Controls.Add(this.label1);
            this.WizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPage1.Location = new System.Drawing.Point(0, 0);
            this.WizardPage1.Name = "WizardPage1";
            this.WizardPage1.Size = new System.Drawing.Size(413, 305);
            this.WizardPage1.TabIndex = 4;
            // 
            // checkBoxZero
            // 
            this.checkBoxZero.AutoSize = true;
            this.checkBoxZero.Location = new System.Drawing.Point(40, 141);
            this.checkBoxZero.Name = "checkBoxZero";
            this.checkBoxZero.Size = new System.Drawing.Size(116, 17);
            this.checkBoxZero.TabIndex = 3;
            this.checkBoxZero.Text = "Ignore Zero Values";
            this.checkBoxZero.UseVisualStyleBackColor = true;
            // 
            // comboBoxColumns
            // 
            this.comboBoxColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumns.FormattingEnabled = true;
            this.comboBoxColumns.Location = new System.Drawing.Point(102, 94);
            this.comboBoxColumns.Name = "comboBoxColumns";
            this.comboBoxColumns.Size = new System.Drawing.Size(250, 21);
            this.comboBoxColumns.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Column:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a Column to Create the Theme:";
            // 
            // WizardPage2
            // 
            this.WizardPage2.Controls.Add(this.label5);
            this.WizardPage2.Controls.Add(this.label4);
            this.WizardPage2.Controls.Add(this.colorRampPickerBackgroundColor);
            this.WizardPage2.Controls.Add(this.colorRampPickerOutlineColor);
            this.WizardPage2.Controls.Add(this.label3);
            this.WizardPage2.Controls.Add(this.colorRampPickerColor);
            this.WizardPage2.Controls.Add(this.checkBoxFirstOnly);
            this.WizardPage2.Controls.Add(this.checkBoxKeepStyles);
            this.WizardPage2.Controls.Add(this.layerControl);
            this.WizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPage2.Location = new System.Drawing.Point(0, 0);
            this.WizardPage2.Name = "WizardPage2";
            this.WizardPage2.Size = new System.Drawing.Size(413, 305);
            this.WizardPage2.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Background Colour Ramp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Outline Colour Ramp";
            // 
            // colorRampPickerBackgroundColor
            // 
            this.colorRampPickerBackgroundColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorRampPickerBackgroundColor.Context = null;
            this.colorRampPickerBackgroundColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorRampPickerBackgroundColor.Location = new System.Drawing.Point(144, 277);
            this.colorRampPickerBackgroundColor.Name = "colorRampPickerBackgroundColor";
            this.colorRampPickerBackgroundColor.ReadOnly = false;
            this.colorRampPickerBackgroundColor.Size = new System.Drawing.Size(238, 20);
            this.colorRampPickerBackgroundColor.TabIndex = 7;
            this.colorRampPickerBackgroundColor.Value = ColorRampValueList.Empty;
            this.colorRampPickerBackgroundColor.ValueChanged += new System.EventHandler(this.colorRampPickerBackgroundColor_ValueChanged);
            // 
            // colorRampPickerOutlineColor
            // 
            this.colorRampPickerOutlineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorRampPickerOutlineColor.Context = null;
            this.colorRampPickerOutlineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorRampPickerOutlineColor.Location = new System.Drawing.Point(144, 252);
            this.colorRampPickerOutlineColor.Name = "colorRampPickerOutlineColor";
            this.colorRampPickerOutlineColor.ReadOnly = false;
            this.colorRampPickerOutlineColor.Size = new System.Drawing.Size(238, 20);
            this.colorRampPickerOutlineColor.TabIndex = 7;
            this.colorRampPickerOutlineColor.Value = ColorRampValueList.Empty;
            this.colorRampPickerOutlineColor.ValueChanged += new System.EventHandler(this.colorRampPickerOutlineColor_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Colour Ramp";
            // 
            // colorRampPickerColor
            // 
            this.colorRampPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorRampPickerColor.Context = null;
            this.colorRampPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorRampPickerColor.Location = new System.Drawing.Point(144, 227);
            this.colorRampPickerColor.Name = "colorRampPickerColor";
            this.colorRampPickerColor.ReadOnly = false;
            this.colorRampPickerColor.Size = new System.Drawing.Size(238, 20);
            this.colorRampPickerColor.TabIndex = 6;
            this.colorRampPickerColor.Value = ColorRampValueList.Random;
            this.colorRampPickerColor.ValueChanged += new System.EventHandler(this.colorRampPickerColor_ValueChanged);
            // 
            // checkBoxFirstOnly
            // 
            this.checkBoxFirstOnly.AutoSize = true;
            this.checkBoxFirstOnly.Location = new System.Drawing.Point(237, 206);
            this.checkBoxFirstOnly.Name = "checkBoxFirstOnly";
            this.checkBoxFirstOnly.Size = new System.Drawing.Size(156, 17);
            this.checkBoxFirstOnly.TabIndex = 2;
            this.checkBoxFirstOnly.Text = "Override Only the First Style";
            this.checkBoxFirstOnly.UseVisualStyleBackColor = true;
            this.checkBoxFirstOnly.CheckedChanged += new System.EventHandler(this.checkBoxFirstOnly_CheckedChanged);
            // 
            // checkBoxKeepStyles
            // 
            this.checkBoxKeepStyles.AutoSize = true;
            this.checkBoxKeepStyles.Location = new System.Drawing.Point(13, 206);
            this.checkBoxKeepStyles.Name = "checkBoxKeepStyles";
            this.checkBoxKeepStyles.Size = new System.Drawing.Size(218, 17);
            this.checkBoxKeepStyles.TabIndex = 1;
            this.checkBoxKeepStyles.Text = "Derive the Styles from The Original Layer";
            this.checkBoxKeepStyles.UseVisualStyleBackColor = true;
            this.checkBoxKeepStyles.CheckedChanged += new System.EventHandler(this.checkBoxKeepStyles_CheckedChanged);
            // 
            // layerControl
            // 
            this.layerControl.Location = new System.Drawing.Point(0, 0);
            this.layerControl.Name = "layerControl";
            this.layerControl.ShowCheckBoxes = true;
            this.layerControl.ShowClasses = true;
            this.layerControl.ShowRootObject = true;
            this.layerControl.ShowStyles = false;
            this.layerControl.ShowLabels = false;
            this.layerControl.Size = new System.Drawing.Size(410, 200);
            this.layerControl.TabIndex = 0;
            this.layerControl.Target = null;
            // 
            // IndividualValuesTheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WizardPage2);
            this.Controls.Add(this.WizardPage1);
            this.Name = "IndividualValuesTheme";
            this.Size = new System.Drawing.Size(413, 305);
            this.WizardPage1.ResumeLayout(false);
            this.WizardPage1.PerformLayout();
            this.WizardPage2.ResumeLayout(false);
            this.WizardPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel WizardPage1;
        private System.Windows.Forms.ComboBox comboBoxColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel WizardPage2;
        private System.Windows.Forms.CheckBox checkBoxZero;
        private LayerControl layerControl;
        private System.Windows.Forms.CheckBox checkBoxKeepStyles;
        private System.Windows.Forms.CheckBox checkBoxFirstOnly;
        private System.Windows.Forms.Label label4;
        private ColorRampPicker colorRampPickerBackgroundColor;
        private ColorRampPicker colorRampPickerOutlineColor;
        private System.Windows.Forms.Label label3;
        private ColorRampPicker colorRampPickerColor;
        private System.Windows.Forms.Label label5;
    }
}
