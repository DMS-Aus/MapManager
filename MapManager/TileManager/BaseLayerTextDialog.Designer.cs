namespace DMS.MapManager.TileManager
{
    partial class BaseLayerTextDialog
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
            this.txtConfigManager = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtConfigManager
            // 
            this.txtConfigManager.Location = new System.Drawing.Point(10, 13);
            this.txtConfigManager.Name = "txtConfigManager";
            this.txtConfigManager.Size = new System.Drawing.Size(454, 368);
            this.txtConfigManager.TabIndex = 21;
            this.txtConfigManager.Text = "";
            // 
            // BaseLayerTextDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 393);
            this.Controls.Add(this.txtConfigManager);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::MapManager.Properties.Resources.MapManager;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseLayerTextDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Copy to ConfigManager";
            this.Load += new System.EventHandler(this.BaseLayerTextDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtConfigManager;
    }
}