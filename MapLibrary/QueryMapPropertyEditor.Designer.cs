namespace DMS.MapLibrary
{
    partial class QueryMapPropertyEditor
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
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.colorPickerColor = new DMS.MapLibrary.ColorPicker();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Colour:";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(77, 40);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(232, 21);
            this.comboBoxStatus.TabIndex = 3;
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Status:";
            // 
            // comboBoxStyle
            // 
            this.comboBoxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStyle.FormattingEnabled = true;
            this.comboBoxStyle.Location = new System.Drawing.Point(77, 67);
            this.comboBoxStyle.Name = "comboBoxStyle";
            this.comboBoxStyle.Size = new System.Drawing.Size(232, 21);
            this.comboBoxStyle.TabIndex = 5;
            this.comboBoxStyle.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Style:";
            // 
            // colorPickerColor
            // 
            this.colorPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerColor.Context = null;
            this.colorPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerColor.Location = new System.Drawing.Point(75, 14);
            this.colorPickerColor.Name = "colorPickerColor";
            this.colorPickerColor.ReadOnly = false;
            this.colorPickerColor.Size = new System.Drawing.Size(234, 20);
            this.colorPickerColor.TabIndex = 1;
            this.colorPickerColor.Text = "White";
            this.colorPickerColor.Value = System.Drawing.Color.White;
            this.colorPickerColor.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // QueryMapPropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxStyle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.colorPickerColor);
            this.Controls.Add(this.label8);
            this.Name = "QueryMapPropertyEditor";
            this.Size = new System.Drawing.Size(350, 104);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private ColorPicker colorPickerColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStyle;
        private System.Windows.Forms.Label label5;
    }
}
