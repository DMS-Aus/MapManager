namespace DMS.MapManager
{
    partial class SelectListForm
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
            this.selectList = new DMS.MapLibrary.SelectList();
            this.SuspendLayout();
            // 
            // selectList
            // 
            this.selectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectList.Location = new System.Drawing.Point(0, 0);
            this.selectList.Name = "selectList";
            this.selectList.Size = new System.Drawing.Size(372, 367);
            this.selectList.TabIndex = 0;
            this.selectList.Target = null;
            // 
            // SelectListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 367);
            this.Controls.Add(this.selectList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(500, 40);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Selected Features";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectListForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectListForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        internal DMS.MapLibrary.SelectList selectList;

    }
}