namespace DMS.MapLibrary
{
    partial class ColorStopForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.colorPickerStopColor = new DMS.MapLibrary.ColorPicker();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Colour";
            // 
            // colorPickerStopColor
            // 
            this.colorPickerStopColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorPickerStopColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerStopColor.Context = null;
            this.colorPickerStopColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerStopColor.Location = new System.Drawing.Point(55, 12);
            this.colorPickerStopColor.Name = "colorPickerStopColor";
            this.colorPickerStopColor.ReadOnly = false;
            this.colorPickerStopColor.Size = new System.Drawing.Size(157, 20);
            this.colorPickerStopColor.TabIndex = 13;
            this.colorPickerStopColor.Text = "Green";
            this.colorPickerStopColor.Value = System.Drawing.Color.Green;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "%";
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Location = new System.Drawing.Point(55, 41);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.Size = new System.Drawing.Size(33, 20);
            this.textBoxOffset.TabIndex = 17;
            this.textBoxOffset.Text = "50";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Offset";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(41, 78);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(56, 23);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(128, 78);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(60, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ColorStopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 109);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxOffset);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.colorPickerStopColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ColorStopForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Colour Stop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private DMS.MapLibrary.ColorPicker colorPickerStopColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}