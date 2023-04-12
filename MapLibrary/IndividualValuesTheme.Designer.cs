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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndividualValuesTheme));
            this.WizardPage1 = new System.Windows.Forms.Panel();
            this.checkBoxZero = new System.Windows.Forms.CheckBox();
            this.comboBoxColumns = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.WizardPage2 = new System.Windows.Forms.Panel();
            this.checkBoxClassItem = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.WizardPage1.Margin = new System.Windows.Forms.Padding(4);
            this.WizardPage1.Name = "WizardPage1";
            this.WizardPage1.Size = new System.Drawing.Size(551, 398);
            this.WizardPage1.TabIndex = 4;
            // 
            // checkBoxZero
            // 
            this.checkBoxZero.AutoSize = true;
            this.checkBoxZero.Location = new System.Drawing.Point(53, 174);
            this.checkBoxZero.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxZero.Name = "checkBoxZero";
            this.checkBoxZero.Size = new System.Drawing.Size(143, 20);
            this.checkBoxZero.TabIndex = 3;
            this.checkBoxZero.Text = "Ignore Zero Values";
            this.checkBoxZero.UseVisualStyleBackColor = true;
            // 
            // comboBoxColumns
            // 
            this.comboBoxColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumns.FormattingEnabled = true;
            this.comboBoxColumns.Location = new System.Drawing.Point(136, 116);
            this.comboBoxColumns.Margin = new System.Windows.Forms.Padding(4);
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
            this.WizardPage2.Controls.Add(this.checkBoxClassItem);
            this.WizardPage2.Controls.Add(this.label4);
            this.WizardPage2.Controls.Add(this.colorRampPickerOutlineColor);
            this.WizardPage2.Controls.Add(this.label3);
            this.WizardPage2.Controls.Add(this.colorRampPickerColor);
            this.WizardPage2.Controls.Add(this.checkBoxFirstOnly);
            this.WizardPage2.Controls.Add(this.checkBoxKeepStyles);
            this.WizardPage2.Controls.Add(this.layerControl);
            this.WizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardPage2.Location = new System.Drawing.Point(0, 0);
            this.WizardPage2.Margin = new System.Windows.Forms.Padding(4);
            this.WizardPage2.Name = "WizardPage2";
            this.WizardPage2.Size = new System.Drawing.Size(551, 398);
            this.WizardPage2.TabIndex = 4;
            // 
            // checkBoxClassItem
            // 
            this.checkBoxClassItem.AutoSize = true;
            this.checkBoxClassItem.Checked = true;
            this.checkBoxClassItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClassItem.Location = new System.Drawing.Point(68, 306);
            this.checkBoxClassItem.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxClassItem.Name = "checkBoxClassItem";
            this.checkBoxClassItem.Size = new System.Drawing.Size(379, 20);
            this.checkBoxClassItem.TabIndex = 10;
            this.checkBoxClassItem.Text = "Use CLASSITEM for class selection instead of expressions";
            this.checkBoxClassItem.UseVisualStyleBackColor = true;
            this.checkBoxClassItem.CheckedChanged += new System.EventHandler(this.checkBoxClassItem_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 367);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Outline Colour Ramp";
            // 
            // colorRampPickerOutlineColor
            // 
            this.colorRampPickerOutlineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorRampPickerOutlineColor.Context = null;
            this.colorRampPickerOutlineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorRampPickerOutlineColor.Location = new System.Drawing.Point(192, 362);
            this.colorRampPickerOutlineColor.Margin = new System.Windows.Forms.Padding(4);
            this.colorRampPickerOutlineColor.Name = "colorRampPickerOutlineColor";
            this.colorRampPickerOutlineColor.ReadOnly = false;
            this.colorRampPickerOutlineColor.Size = new System.Drawing.Size(317, 22);
            this.colorRampPickerOutlineColor.TabIndex = 7;
            this.colorRampPickerOutlineColor.Text = "Empty";
            this.colorRampPickerOutlineColor.Value = ((DMS.MapLibrary.ColorRampValueList)(resources.GetObject("colorRampPickerOutlineColor.Value")));
            this.colorRampPickerOutlineColor.ValueChanged += new System.EventHandler(this.colorRampPickerOutlineColor_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 336);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Colour Ramp";
            // 
            // colorRampPickerColor
            // 
            this.colorRampPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorRampPickerColor.Context = null;
            this.colorRampPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorRampPickerColor.Location = new System.Drawing.Point(192, 331);
            this.colorRampPickerColor.Margin = new System.Windows.Forms.Padding(4);
            this.colorRampPickerColor.Name = "colorRampPickerColor";
            this.colorRampPickerColor.ReadOnly = false;
            this.colorRampPickerColor.Size = new System.Drawing.Size(317, 22);
            this.colorRampPickerColor.TabIndex = 6;
            this.colorRampPickerColor.Text = "Random values";
            this.colorRampPickerColor.Value = ((DMS.MapLibrary.ColorRampValueList)(resources.GetObject("colorRampPickerColor.Value")));
            this.colorRampPickerColor.ValueChanged += new System.EventHandler(this.colorRampPickerColor_ValueChanged);
            // 
            // checkBoxFirstOnly
            // 
            this.checkBoxFirstOnly.AutoSize = true;
            this.checkBoxFirstOnly.Location = new System.Drawing.Point(316, 282);
            this.checkBoxFirstOnly.Margin = new System.Windows.Forms.Padding(4);
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
            this.checkBoxKeepStyles.Location = new System.Drawing.Point(17, 282);
            this.checkBoxKeepStyles.Margin = new System.Windows.Forms.Padding(4);
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
            this.layerControl.Margin = new System.Windows.Forms.Padding(5);
            this.layerControl.Name = "layerControl";
            this.layerControl.ShowCheckBoxes = true;
            this.layerControl.ShowClasses = true;
            this.layerControl.ShowLabels = false;
            this.layerControl.ShowRootObject = true;
            this.layerControl.ShowStyles = false;
            this.layerControl.ShowToolbar = true;
            this.layerControl.Size = new System.Drawing.Size(547, 273);
            this.layerControl.TabIndex = 0;
            this.layerControl.Target = null;
            // 
            // IndividualValuesTheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WizardPage2);
            this.Controls.Add(this.WizardPage1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "IndividualValuesTheme";
            this.Size = new System.Drawing.Size(551, 398);
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
        private ColorRampPicker colorRampPickerOutlineColor;
        private System.Windows.Forms.Label label3;
        private ColorRampPicker colorRampPickerColor;
        private System.Windows.Forms.CheckBox checkBoxClassItem;
    }
}
