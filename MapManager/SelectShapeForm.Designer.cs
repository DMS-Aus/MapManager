namespace DMS.MapManager
{
    partial class SelectShapeForm
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
            this.textBoxShape = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.checkBoxCenter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxShape
            // 
            this.textBoxShape.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShape.Location = new System.Drawing.Point(2, 26);
            this.textBoxShape.Multiline = true;
            this.textBoxShape.Name = "textBoxShape";
            this.textBoxShape.Size = new System.Drawing.Size(491, 278);
            this.textBoxShape.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selection Shape (WKT)";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSelect.Location = new System.Drawing.Point(260, 310);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(127, 32);
            this.buttonSelect.TabIndex = 2;
            this.buttonSelect.Text = "Execute Query";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // checkBoxCenter
            // 
            this.checkBoxCenter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxCenter.AutoSize = true;
            this.checkBoxCenter.Checked = true;
            this.checkBoxCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCenter.Location = new System.Drawing.Point(38, 317);
            this.checkBoxCenter.Name = "checkBoxCenter";
            this.checkBoxCenter.Size = new System.Drawing.Size(169, 21);
            this.checkBoxCenter.TabIndex = 3;
            this.checkBoxCenter.Text = "Center Map To Shape";
            this.checkBoxCenter.UseVisualStyleBackColor = true;
            // 
            // SelectShapeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 345);
            this.Controls.Add(this.checkBoxCenter);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxShape);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectShapeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select By Shape";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectShapeForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectShapeForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxShape;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.CheckBox checkBoxCenter;
    }
}