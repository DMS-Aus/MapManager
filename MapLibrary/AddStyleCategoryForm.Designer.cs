namespace DMS.MapLibrary
{
    partial class AddStyleCategoryForm
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
            this.textBoxFontCategoryName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxFonts = new System.Windows.Forms.ComboBox();
            this.textBoxCharCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCharMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Category Type";
            // 
            // textBoxFontCategoryName
            // 
            this.textBoxFontCategoryName.Location = new System.Drawing.Point(92, 43);
            this.textBoxFontCategoryName.Name = "textBoxFontCategoryName";
            this.textBoxFontCategoryName.Size = new System.Drawing.Size(187, 20);
            this.textBoxFontCategoryName.TabIndex = 17;
            this.textBoxFontCategoryName.Text = "New Category";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Category Name";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(79, 109);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(56, 23);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(166, 109);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(60, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxFonts
            // 
            this.comboBoxFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFonts.FormattingEnabled = true;
            this.comboBoxFonts.Location = new System.Drawing.Point(92, 12);
            this.comboBoxFonts.Name = "comboBoxFonts";
            this.comboBoxFonts.Size = new System.Drawing.Size(187, 21);
            this.comboBoxFonts.TabIndex = 21;
            this.comboBoxFonts.SelectedIndexChanged += new System.EventHandler(this.comboBoxFonts_SelectedIndexChanged);
            // 
            // textBoxCharCount
            // 
            this.textBoxCharCount.Location = new System.Drawing.Point(92, 73);
            this.textBoxCharCount.Name = "textBoxCharCount";
            this.textBoxCharCount.Size = new System.Drawing.Size(76, 20);
            this.textBoxCharCount.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Character Count";
            // 
            // buttonCharMap
            // 
            this.buttonCharMap.Location = new System.Drawing.Point(188, 71);
            this.buttonCharMap.Name = "buttonCharMap";
            this.buttonCharMap.Size = new System.Drawing.Size(91, 23);
            this.buttonCharMap.TabIndex = 26;
            this.buttonCharMap.TabStop = false;
            this.buttonCharMap.Text = "Character Map";
            this.buttonCharMap.UseVisualStyleBackColor = true;
            this.buttonCharMap.Click += new System.EventHandler(this.buttonCharMap_Click);
            // 
            // AddStyleCategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 142);
            this.Controls.Add(this.buttonCharMap);
            this.Controls.Add(this.textBoxCharCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxFonts);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxFontCategoryName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddStyleCategoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Category";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxFontCategoryName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxFonts;
        private System.Windows.Forms.TextBox textBoxCharCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCharMap;
    }
}