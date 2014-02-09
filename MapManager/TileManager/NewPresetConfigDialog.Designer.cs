namespace DMS.MapManager.TileManager
{
    partial class NewPresetConfigDialog
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
            this.btnNewPreset = new System.Windows.Forms.Button();
            this.txtNewPreset = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnNewPreset
            // 
            this.btnNewPreset.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNewPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPreset.Location = new System.Drawing.Point(369, 10);
            this.btnNewPreset.Name = "btnNewPreset";
            this.btnNewPreset.Size = new System.Drawing.Size(100, 30);
            this.btnNewPreset.TabIndex = 31;
            this.btnNewPreset.Text = "Accept";
            this.btnNewPreset.UseVisualStyleBackColor = false;
            this.btnNewPreset.Click += new System.EventHandler(this.btnNewPreset_Click);
            // 
            // txtNewPreset
            // 
            this.txtNewPreset.Location = new System.Drawing.Point(12, 12);
            this.txtNewPreset.Name = "txtNewPreset";
            this.txtNewPreset.Size = new System.Drawing.Size(351, 27);
            this.txtNewPreset.TabIndex = 32;
            // 
            // NewPresetConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 49);
            this.Controls.Add(this.txtNewPreset);
            this.Controls.Add(this.btnNewPreset);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPresetConfigDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter name for new preset";
            this.Load += new System.EventHandler(this.NewPresetConfigDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewPreset;
        public System.Windows.Forms.TextBox txtNewPreset;
    }
}