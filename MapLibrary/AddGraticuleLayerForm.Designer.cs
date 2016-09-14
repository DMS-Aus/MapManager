namespace DMS.MapLibrary
{
    partial class AddGraticuleLayerForm
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
            this.textBoxLabelFormat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMinArcs = new System.Windows.Forms.TextBox();
            this.textBoxMaxArcs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMaxInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMinInterval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMaxSubdivide = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMinSubdivide = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonProjection = new System.Windows.Forms.Button();
            this.textBoxProjection = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxLineWidth = new System.Windows.Forms.TextBox();
            this.colorPickerLineColor = new DMS.MapLibrary.ColorPicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.colorPickerLabelColor = new DMS.MapLibrary.ColorPicker();
            this.textBoxLabelSize = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxLabelFormat
            // 
            this.textBoxLabelFormat.Location = new System.Drawing.Point(79, 14);
            this.textBoxLabelFormat.Name = "textBoxLabelFormat";
            this.textBoxLabelFormat.Size = new System.Drawing.Size(233, 20);
            this.textBoxLabelFormat.TabIndex = 17;
            this.textBoxLabelFormat.Text = "%g°";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "LabelFormat";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(190, 250);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(56, 23);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(252, 250);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(60, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Min. Arcs";
            // 
            // textBoxMinArcs
            // 
            this.textBoxMinArcs.Location = new System.Drawing.Point(79, 47);
            this.textBoxMinArcs.Name = "textBoxMinArcs";
            this.textBoxMinArcs.Size = new System.Drawing.Size(66, 20);
            this.textBoxMinArcs.TabIndex = 22;
            this.textBoxMinArcs.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // textBoxMaxArcs
            // 
            this.textBoxMaxArcs.Location = new System.Drawing.Point(246, 47);
            this.textBoxMaxArcs.Name = "textBoxMaxArcs";
            this.textBoxMaxArcs.Size = new System.Drawing.Size(66, 20);
            this.textBoxMaxArcs.TabIndex = 24;
            this.textBoxMaxArcs.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Max. Arcs";
            // 
            // textBoxMaxInterval
            // 
            this.textBoxMaxInterval.Location = new System.Drawing.Point(246, 80);
            this.textBoxMaxInterval.Name = "textBoxMaxInterval";
            this.textBoxMaxInterval.Size = new System.Drawing.Size(66, 20);
            this.textBoxMaxInterval.TabIndex = 28;
            this.textBoxMaxInterval.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Max. Interval";
            // 
            // textBoxMinInterval
            // 
            this.textBoxMinInterval.Location = new System.Drawing.Point(79, 80);
            this.textBoxMinInterval.Name = "textBoxMinInterval";
            this.textBoxMinInterval.Size = new System.Drawing.Size(66, 20);
            this.textBoxMinInterval.TabIndex = 26;
            this.textBoxMinInterval.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Min. Interval";
            // 
            // textBoxMaxSubdivide
            // 
            this.textBoxMaxSubdivide.Location = new System.Drawing.Point(246, 112);
            this.textBoxMaxSubdivide.Name = "textBoxMaxSubdivide";
            this.textBoxMaxSubdivide.Size = new System.Drawing.Size(66, 20);
            this.textBoxMaxSubdivide.TabIndex = 32;
            this.textBoxMaxSubdivide.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(166, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Max. Subdivide";
            // 
            // textBoxMinSubdivide
            // 
            this.textBoxMinSubdivide.Location = new System.Drawing.Point(79, 112);
            this.textBoxMinSubdivide.Name = "textBoxMinSubdivide";
            this.textBoxMinSubdivide.Size = new System.Drawing.Size(66, 20);
            this.textBoxMinSubdivide.TabIndex = 30;
            this.textBoxMinSubdivide.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Min. Subdivide";
            // 
            // buttonProjection
            // 
            this.buttonProjection.Location = new System.Drawing.Point(279, 142);
            this.buttonProjection.Name = "buttonProjection";
            this.buttonProjection.Size = new System.Drawing.Size(30, 23);
            this.buttonProjection.TabIndex = 35;
            this.buttonProjection.Text = "...";
            this.buttonProjection.UseVisualStyleBackColor = true;
            this.buttonProjection.Click += new System.EventHandler(this.buttonProjection_Click);
            // 
            // textBoxProjection
            // 
            this.textBoxProjection.Location = new System.Drawing.Point(79, 144);
            this.textBoxProjection.Name = "textBoxProjection";
            this.textBoxProjection.ReadOnly = true;
            this.textBoxProjection.Size = new System.Drawing.Size(194, 20);
            this.textBoxProjection.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Projection";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Line Width";
            // 
            // textBoxLineWidth
            // 
            this.textBoxLineWidth.Location = new System.Drawing.Point(79, 176);
            this.textBoxLineWidth.Name = "textBoxLineWidth";
            this.textBoxLineWidth.Size = new System.Drawing.Size(45, 20);
            this.textBoxLineWidth.TabIndex = 37;
            this.textBoxLineWidth.Text = "1";
            this.textBoxLineWidth.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // colorPickerLineColor
            // 
            this.colorPickerLineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerLineColor.Context = null;
            this.colorPickerLineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerLineColor.Location = new System.Drawing.Point(195, 176);
            this.colorPickerLineColor.Name = "colorPickerLineColor";
            this.colorPickerLineColor.ReadOnly = false;
            this.colorPickerLineColor.Size = new System.Drawing.Size(117, 20);
            this.colorPickerLineColor.TabIndex = 38;
            this.colorPickerLineColor.Text = "Black";
            this.colorPickerLineColor.Value = System.Drawing.Color.Black;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(132, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Line Colour";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(126, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Label Colour";
            // 
            // colorPickerLabelColor
            // 
            this.colorPickerLabelColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerLabelColor.Context = null;
            this.colorPickerLabelColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerLabelColor.Location = new System.Drawing.Point(195, 208);
            this.colorPickerLabelColor.Name = "colorPickerLabelColor";
            this.colorPickerLabelColor.ReadOnly = false;
            this.colorPickerLabelColor.Size = new System.Drawing.Size(117, 20);
            this.colorPickerLabelColor.TabIndex = 42;
            this.colorPickerLabelColor.Text = "Red";
            this.colorPickerLabelColor.Value = System.Drawing.Color.Red;
            // 
            // textBoxLabelSize
            // 
            this.textBoxLabelSize.Location = new System.Drawing.Point(79, 208);
            this.textBoxLabelSize.Name = "textBoxLabelSize";
            this.textBoxLabelSize.Size = new System.Drawing.Size(45, 20);
            this.textBoxLabelSize.TabIndex = 41;
            this.textBoxLabelSize.Text = "8";
            this.textBoxLabelSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 212);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "Label Size";
            // 
            // AddGraticuleLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 280);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.colorPickerLabelColor);
            this.Controls.Add(this.textBoxLabelSize);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.colorPickerLineColor);
            this.Controls.Add(this.textBoxLineWidth);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonProjection);
            this.Controls.Add(this.textBoxProjection);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxMaxSubdivide);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxMinSubdivide);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxMaxInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMinInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMaxArcs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxMinArcs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxLabelFormat);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddGraticuleLayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Graticule Layer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLabelFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMinArcs;
        private System.Windows.Forms.TextBox textBoxMaxArcs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMaxInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMinInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMaxSubdivide;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMinSubdivide;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonProjection;
        private System.Windows.Forms.TextBox textBoxProjection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxLineWidth;
        private ColorPicker colorPickerLineColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private ColorPicker colorPickerLabelColor;
        private System.Windows.Forms.TextBox textBoxLabelSize;
        private System.Windows.Forms.Label label12;
    }
}