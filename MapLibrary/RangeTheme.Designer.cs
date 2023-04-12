namespace DMS.MapLibrary
{
    partial class RangeTheme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RangeTheme));
            this.WizardPage1 = new System.Windows.Forms.Panel();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownClasses = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxColumns = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.WizardPage2 = new System.Windows.Forms.Panel();
            this.colorRampPickerOutlineColor = new DMS.MapLibrary.ColorRampPicker();
            this.colorRampPickerColor = new DMS.MapLibrary.ColorRampPicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxFirstOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepStyles = new System.Windows.Forms.CheckBox();
            this.layerControl = new DMS.MapLibrary.LayerControl();
            this.WizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClasses)).BeginInit();
            this.WizardPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardPage1
            // 
            this.WizardPage1.Controls.Add(this.comboBoxMode);
            this.WizardPage1.Controls.Add(this.label7);
            this.WizardPage1.Controls.Add(this.numericUpDownClasses);
            this.WizardPage1.Controls.Add(this.label6);
            this.WizardPage1.Controls.Add(this.comboBoxColumns);
            this.WizardPage1.Controls.Add(this.label2);
            this.WizardPage1.Controls.Add(this.label1);
            this.WizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPage1.Location = new System.Drawing.Point(0, 0);
            this.WizardPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.WizardPage1.Name = "WizardPage1";
            this.WizardPage1.Size = new System.Drawing.Size(551, 375);
            this.WizardPage1.TabIndex = 4;
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "Equal Interval"});
            this.comboBoxMode.Location = new System.Drawing.Point(136, 208);
            this.comboBoxMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(332, 24);
            this.comboBoxMode.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 212);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Mode:";
            // 
            // numericUpDownClasses
            // 
            this.numericUpDownClasses.Location = new System.Drawing.Point(136, 162);
            this.numericUpDownClasses.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownClasses.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownClasses.Name = "numericUpDownClasses";
            this.numericUpDownClasses.Size = new System.Drawing.Size(123, 22);
            this.numericUpDownClasses.TabIndex = 4;
            this.numericUpDownClasses.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 165);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Classes:";
            // 
            // comboBoxColumns
            // 
            this.comboBoxColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumns.FormattingEnabled = true;
            this.comboBoxColumns.Location = new System.Drawing.Point(136, 116);
            this.comboBoxColumns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxColumns.Name = "comboBoxColumns";
            this.comboBoxColumns.Size = new System.Drawing.Size(332, 24);
            this.comboBoxColumns.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Column:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a Column to Create the Theme:";
            // 
            // WizardPage2
            // 
            this.WizardPage2.Controls.Add(this.colorRampPickerOutlineColor);
            this.WizardPage2.Controls.Add(this.colorRampPickerColor);
            this.WizardPage2.Controls.Add(this.label4);
            this.WizardPage2.Controls.Add(this.label3);
            this.WizardPage2.Controls.Add(this.checkBoxFirstOnly);
            this.WizardPage2.Controls.Add(this.checkBoxKeepStyles);
            this.WizardPage2.Controls.Add(this.layerControl);
            this.WizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPage2.Location = new System.Drawing.Point(0, 0);
            this.WizardPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.WizardPage2.Name = "WizardPage2";
            this.WizardPage2.Size = new System.Drawing.Size(551, 375);
            this.WizardPage2.TabIndex = 4;
            // 
            // colorRampPickerOutlineColor
            // 
            this.colorRampPickerOutlineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorRampPickerOutlineColor.Context = null;
            this.colorRampPickerOutlineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorRampPickerOutlineColor.Location = new System.Drawing.Point(192, 340);
            this.colorRampPickerOutlineColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorRampPickerOutlineColor.Name = "colorRampPickerOutlineColor";
            this.colorRampPickerOutlineColor.ReadOnly = false;
            this.colorRampPickerOutlineColor.Size = new System.Drawing.Size(317, 22);
            this.colorRampPickerOutlineColor.TabIndex = 11;
            this.colorRampPickerOutlineColor.Text = "Empty";
            this.colorRampPickerOutlineColor.Value = ((DMS.MapLibrary.ColorRampValueList)(resources.GetObject("colorRampPickerOutlineColor.Value")));
            this.colorRampPickerOutlineColor.ValueChanged += new System.EventHandler(this.colorRampPickerOutlineColor_ValueChanged);
            // 
            // colorRampPickerColor
            // 
            this.colorRampPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorRampPickerColor.Context = null;
            this.colorRampPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorRampPickerColor.Location = new System.Drawing.Point(192, 307);
            this.colorRampPickerColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorRampPickerColor.Name = "colorRampPickerColor";
            this.colorRampPickerColor.ReadOnly = false;
            this.colorRampPickerColor.Size = new System.Drawing.Size(317, 22);
            this.colorRampPickerColor.TabIndex = 10;
            this.colorRampPickerColor.Text = "Random values";
            this.colorRampPickerColor.Value = ((DMS.MapLibrary.ColorRampValueList)(resources.GetObject("colorRampPickerColor.Value")));
            this.colorRampPickerColor.ValueChanged += new System.EventHandler(this.colorRampPickerColor_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 344);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Outline Colour Ramp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 313);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Colour Ramp";
            // 
            // checkBoxFirstOnly
            // 
            this.checkBoxFirstOnly.AutoSize = true;
            this.checkBoxFirstOnly.Location = new System.Drawing.Point(316, 283);
            this.checkBoxFirstOnly.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxFirstOnly.Name = "checkBoxFirstOnly";
            this.checkBoxFirstOnly.Size = new System.Drawing.Size(193, 20);
            this.checkBoxFirstOnly.TabIndex = 2;
            this.checkBoxFirstOnly.Text = "Override Only the First Style";
            this.checkBoxFirstOnly.UseVisualStyleBackColor = true;
            this.checkBoxFirstOnly.CheckedChanged += new System.EventHandler(this.checkBoxFirstOnly_CheckedChanged);
            // 
            // checkBoxKeepStyles
            // 
            this.checkBoxKeepStyles.AutoSize = true;
            this.checkBoxKeepStyles.Location = new System.Drawing.Point(17, 283);
            this.checkBoxKeepStyles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxKeepStyles.Name = "checkBoxKeepStyles";
            this.checkBoxKeepStyles.Size = new System.Drawing.Size(272, 20);
            this.checkBoxKeepStyles.TabIndex = 1;
            this.checkBoxKeepStyles.Text = "Derive the Styles from The Original Layer";
            this.checkBoxKeepStyles.UseVisualStyleBackColor = true;
            this.checkBoxKeepStyles.CheckedChanged += new System.EventHandler(this.checkBoxKeepStyles_CheckedChanged);
            // 
            // layerControl
            // 
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
            this.layerControl.Size = new System.Drawing.Size(547, 274);
            this.layerControl.TabIndex = 0;
            this.layerControl.Target = null;
            // 
            // RangeTheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WizardPage2);
            this.Controls.Add(this.WizardPage1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RangeTheme";
            this.Size = new System.Drawing.Size(551, 375);
            this.WizardPage1.ResumeLayout(false);
            this.WizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClasses)).EndInit();
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
        private LayerControl layerControl;
        private System.Windows.Forms.CheckBox checkBoxKeepStyles;
        private System.Windows.Forms.CheckBox checkBoxFirstOnly;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownClasses;
        private System.Windows.Forms.Label label6;
        private ColorRampPicker colorRampPickerOutlineColor;
        private ColorRampPicker colorRampPickerColor;
    }
}
