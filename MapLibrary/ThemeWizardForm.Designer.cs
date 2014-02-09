namespace DMS.MapLibrary
{
    partial class ThemeWizardForm
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
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelDefault = new System.Windows.Forms.Panel();
            this.listViewThemes = new System.Windows.Forms.ListView();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonBack.Location = new System.Drawing.Point(34, 209);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "<< Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonNext.Location = new System.Drawing.Point(125, 209);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Next >>";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.Location = new System.Drawing.Point(255, 209);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelDefault
            // 
            this.panelDefault.Controls.Add(this.listViewThemes);
            this.panelDefault.Controls.Add(this.labelTitle);
            this.panelDefault.Location = new System.Drawing.Point(-1, 1);
            this.panelDefault.Name = "panelDefault";
            this.panelDefault.Size = new System.Drawing.Size(379, 195);
            this.panelDefault.TabIndex = 0;
            // 
            // listViewThemes
            // 
            this.listViewThemes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewThemes.FullRowSelect = true;
            this.listViewThemes.HideSelection = false;
            this.listViewThemes.Location = new System.Drawing.Point(35, 38);
            this.listViewThemes.MultiSelect = false;
            this.listViewThemes.Name = "listViewThemes";
            this.listViewThemes.Size = new System.Drawing.Size(312, 144);
            this.listViewThemes.TabIndex = 1;
            this.listViewThemes.UseCompatibleStateImageBehavior = false;
            this.listViewThemes.View = System.Windows.Forms.View.List;
            this.listViewThemes.SelectedIndexChanged += new System.EventHandler(this.listViewThemes_SelectedIndexChanged);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(32, 11);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(121, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Select the Wizard Type:";
            // 
            // ThemeWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(376, 236);
            this.Controls.Add(this.panelDefault);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemeWizardForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Theme";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ThemeWizardForm_KeyDown);
            this.panelDefault.ResumeLayout(false);
            this.panelDefault.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelDefault;
        private System.Windows.Forms.ListView listViewThemes;
        private System.Windows.Forms.Label labelTitle;
    }
}