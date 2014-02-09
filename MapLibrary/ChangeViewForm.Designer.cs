namespace DMS.MapLibrary
{
    partial class ChangeViewForm
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
            this.labelUnit1 = new System.Windows.Forms.Label();
            this.textBoxZoomWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.labelUnit2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelUnit1
            // 
            this.labelUnit1.Location = new System.Drawing.Point(286, 25);
            this.labelUnit1.Name = "labelUnit1";
            this.labelUnit1.Size = new System.Drawing.Size(36, 13);
            this.labelUnit1.TabIndex = 2;
            this.labelUnit1.Text = "m";
            // 
            // textBoxZoomWidth
            // 
            this.textBoxZoomWidth.Location = new System.Drawing.Point(138, 22);
            this.textBoxZoomWidth.Name = "textBoxZoomWidth";
            this.textBoxZoomWidth.Size = new System.Drawing.Size(139, 20);
            this.textBoxZoomWidth.TabIndex = 1;
            this.textBoxZoomWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxZoomWidth_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Zoom (Window width):";
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(138, 57);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(139, 20);
            this.textBoxScale.TabIndex = 5;
            this.textBoxScale.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScale_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Map scale:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "X:";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(138, 91);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(139, 20);
            this.textBoxX.TabIndex = 8;
            this.textBoxX.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Center of window:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Y:";
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(138, 115);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(139, 20);
            this.textBoxY.TabIndex = 10;
            this.textBoxY.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // labelUnit2
            // 
            this.labelUnit2.Location = new System.Drawing.Point(286, 106);
            this.labelUnit2.Name = "labelUnit2";
            this.labelUnit2.Size = new System.Drawing.Size(36, 13);
            this.labelUnit2.TabIndex = 11;
            this.labelUnit2.Text = "m";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(128, 160);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 13;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(229, 160);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(26, 160);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 12;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // ChangeViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 195);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelUnit2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxScale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelUnit1);
            this.Controls.Add(this.textBoxZoomWidth);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change View";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangeViewForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUnit1;
        private System.Windows.Forms.TextBox textBoxZoomWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label labelUnit2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
    }
}