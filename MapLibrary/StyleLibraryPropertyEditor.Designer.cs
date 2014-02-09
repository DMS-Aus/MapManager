namespace DMS.MapLibrary
{
    partial class StyleLibraryPropertyEditor
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
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxAngle = new System.Windows.Forms.TextBox();
            this.textBoxGap = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.colorPickerOutlineColor = new DMS.MapLibrary.ColorPicker();
            this.colorPickerBackColor = new DMS.MapLibrary.ColorPicker();
            this.colorPickerColor = new DMS.MapLibrary.ColorPicker();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxSymbol = new System.Windows.Forms.ComboBox();
            this.textBoxPattern = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(98, 85);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(34, 20);
            this.textBoxWidth.TabIndex = 7;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxWidth.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxWidth.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(98, 59);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(34, 20);
            this.textBoxSize.TabIndex = 1;
            this.textBoxSize.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxSize.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxSize.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(-2, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Background Colour:";
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(224, 59);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(37, 20);
            this.textBoxAngle.TabIndex = 13;
            this.textBoxAngle.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxAngle.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxAngle.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // textBoxGap
            // 
            this.textBoxGap.Location = new System.Drawing.Point(224, 85);
            this.textBoxGap.Name = "textBoxGap";
            this.textBoxGap.Size = new System.Drawing.Size(37, 20);
            this.textBoxGap.TabIndex = 35;
            this.textBoxGap.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxGap.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            this.textBoxGap.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(156, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Symbol Gap:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(22, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Outline Colour:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(57, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Colour:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Symbol Size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(186, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Angle:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Line Width:";
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.White;
            this.pictureBoxPreview.Location = new System.Drawing.Point(273, 27);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(113, 96);
            this.pictureBoxPreview.TabIndex = 41;
            this.pictureBoxPreview.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(270, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Preview:";
            // 
            // colorPickerOutlineColor
            // 
            this.colorPickerOutlineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerOutlineColor.Context = null;
            this.colorPickerOutlineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerOutlineColor.Location = new System.Drawing.Point(98, 161);
            this.colorPickerOutlineColor.Name = "colorPickerOutlineColor";
            this.colorPickerOutlineColor.ReadOnly = false;
            this.colorPickerOutlineColor.Size = new System.Drawing.Size(163, 20);
            this.colorPickerOutlineColor.TabIndex = 19;
            this.colorPickerOutlineColor.Text = "White";
            this.colorPickerOutlineColor.Value = System.Drawing.Color.White;
            this.colorPickerOutlineColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // colorPickerBackColor
            // 
            this.colorPickerBackColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerBackColor.Context = null;
            this.colorPickerBackColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerBackColor.Location = new System.Drawing.Point(98, 136);
            this.colorPickerBackColor.Name = "colorPickerBackColor";
            this.colorPickerBackColor.ReadOnly = false;
            this.colorPickerBackColor.Size = new System.Drawing.Size(163, 20);
            this.colorPickerBackColor.TabIndex = 17;
            this.colorPickerBackColor.Text = "White";
            this.colorPickerBackColor.Value = System.Drawing.Color.White;
            this.colorPickerBackColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // colorPickerColor
            // 
            this.colorPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerColor.Context = null;
            this.colorPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerColor.Location = new System.Drawing.Point(98, 111);
            this.colorPickerColor.Name = "colorPickerColor";
            this.colorPickerColor.ReadOnly = false;
            this.colorPickerColor.Size = new System.Drawing.Size(163, 20);
            this.colorPickerColor.TabIndex = 15;
            this.colorPickerColor.Text = "White";
            this.colorPickerColor.Value = System.Drawing.Color.White;
            this.colorPickerColor.ValueChanged += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(54, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 50;
            this.label13.Text = "Symbol:";
            // 
            // comboBoxSymbol
            // 
            this.comboBoxSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSymbol.FormattingEnabled = true;
            this.comboBoxSymbol.Location = new System.Drawing.Point(98, 8);
            this.comboBoxSymbol.Name = "comboBoxSymbol";
            this.comboBoxSymbol.Size = new System.Drawing.Size(164, 21);
            this.comboBoxSymbol.Sorted = true;
            this.comboBoxSymbol.TabIndex = 49;
            this.comboBoxSymbol.SelectionChangeCommitted += new System.EventHandler(this.ValueChangingWithPreview);
            // 
            // textBoxPattern
            // 
            this.textBoxPattern.Location = new System.Drawing.Point(98, 35);
            this.textBoxPattern.Name = "textBoxPattern";
            this.textBoxPattern.Size = new System.Drawing.Size(163, 20);
            this.textBoxPattern.TabIndex = 48;
            this.textBoxPattern.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxPattern.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPattern_Validating);
            this.textBoxPattern.Validated += new System.EventHandler(this.ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Line Pattern:";
            // 
            // StyleLibraryPropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBoxSymbol);
            this.Controls.Add(this.textBoxPattern);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxGap);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxAngle);
            this.Controls.Add(this.colorPickerOutlineColor);
            this.Controls.Add(this.colorPickerBackColor);
            this.Controls.Add(this.colorPickerColor);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.textBoxSize);
            this.Name = "StyleLibraryPropertyEditor";
            this.Size = new System.Drawing.Size(397, 188);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxSize;
        private ColorPicker colorPickerOutlineColor;
        private ColorPicker colorPickerBackColor;
        private ColorPicker colorPickerColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxAngle;
        private System.Windows.Forms.TextBox textBoxGap;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxSymbol;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.Label label10;
    }
}
