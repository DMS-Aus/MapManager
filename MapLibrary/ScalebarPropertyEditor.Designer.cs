namespace DMS.MapLibrary
{
    partial class ScalebarPropertyEditor
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
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxIntervals = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxPosition = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEditLabel = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxUnits = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorPickerImageColor = new DMS.MapLibrary.ColorPicker();
            this.colorPickerOutlineColor = new DMS.MapLibrary.ColorPicker();
            this.colorPickerBackColor = new DMS.MapLibrary.ColorPicker();
            this.colorPickerColor = new DMS.MapLibrary.ColorPicker();
            this.SuspendLayout();
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(244, 225);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(36, 20);
            this.textBoxHeight.TabIndex = 21;
            this.textBoxHeight.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxHeight.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(115, 225);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(35, 20);
            this.textBoxWidth.TabIndex = 19;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxWidth.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(197, 228);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Height:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(73, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Width:";
            // 
            // textBoxIntervals
            // 
            this.textBoxIntervals.Location = new System.Drawing.Point(115, 5);
            this.textBoxIntervals.Name = "textBoxIntervals";
            this.textBoxIntervals.Size = new System.Drawing.Size(35, 20);
            this.textBoxIntervals.TabIndex = 1;
            this.textBoxIntervals.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxIntervals.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Intervals:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 179);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Image Colour:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(71, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Colour:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Background Colour:";
            // 
            // comboBoxPosition
            // 
            this.comboBoxPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPosition.FormattingEnabled = true;
            this.comboBoxPosition.Location = new System.Drawing.Point(115, 52);
            this.comboBoxPosition.Name = "comboBoxPosition";
            this.comboBoxPosition.Size = new System.Drawing.Size(165, 21);
            this.comboBoxPosition.TabIndex = 5;
            this.comboBoxPosition.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Position:";
            // 
            // buttonEditLabel
            // 
            this.buttonEditLabel.Location = new System.Drawing.Point(114, 250);
            this.buttonEditLabel.Name = "buttonEditLabel";
            this.buttonEditLabel.Size = new System.Drawing.Size(101, 23);
            this.buttonEditLabel.TabIndex = 22;
            this.buttonEditLabel.Text = "Edit Label...";
            this.buttonEditLabel.UseVisualStyleBackColor = true;
            this.buttonEditLabel.Click += new System.EventHandler(this.buttonEditLabel_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(115, 28);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(165, 21);
            this.comboBoxStatus.TabIndex = 3;
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Status:";
            // 
            // comboBoxStyle
            // 
            this.comboBoxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStyle.FormattingEnabled = true;
            this.comboBoxStyle.Location = new System.Drawing.Point(115, 76);
            this.comboBoxStyle.Name = "comboBoxStyle";
            this.comboBoxStyle.Size = new System.Drawing.Size(165, 21);
            this.comboBoxStyle.TabIndex = 7;
            this.comboBoxStyle.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Style:";
            // 
            // comboBoxUnits
            // 
            this.comboBoxUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnits.FormattingEnabled = true;
            this.comboBoxUnits.Location = new System.Drawing.Point(115, 100);
            this.comboBoxUnits.Name = "comboBoxUnits";
            this.comboBoxUnits.Size = new System.Drawing.Size(165, 21);
            this.comboBoxUnits.TabIndex = 9;
            this.comboBoxUnits.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Units:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Outline Colour:";
            // 
            // colorPickerImageColor
            // 
            this.colorPickerImageColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerImageColor.Context = null;
            this.colorPickerImageColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerImageColor.Location = new System.Drawing.Point(115, 176);
            this.colorPickerImageColor.Name = "colorPickerImageColor";
            this.colorPickerImageColor.ReadOnly = false;
            this.colorPickerImageColor.Size = new System.Drawing.Size(167, 20);
            this.colorPickerImageColor.TabIndex = 15;
            this.colorPickerImageColor.Text = "White";
            this.colorPickerImageColor.Value = System.Drawing.Color.White;
            this.colorPickerImageColor.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // colorPickerOutlineColor
            // 
            this.colorPickerOutlineColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerOutlineColor.Context = null;
            this.colorPickerOutlineColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerOutlineColor.Location = new System.Drawing.Point(115, 201);
            this.colorPickerOutlineColor.Name = "colorPickerOutlineColor";
            this.colorPickerOutlineColor.ReadOnly = false;
            this.colorPickerOutlineColor.Size = new System.Drawing.Size(167, 20);
            this.colorPickerOutlineColor.TabIndex = 17;
            this.colorPickerOutlineColor.Text = "White";
            this.colorPickerOutlineColor.Value = System.Drawing.Color.White;
            this.colorPickerOutlineColor.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // colorPickerBackColor
            // 
            this.colorPickerBackColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerBackColor.Context = null;
            this.colorPickerBackColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerBackColor.Location = new System.Drawing.Point(115, 151);
            this.colorPickerBackColor.Name = "colorPickerBackColor";
            this.colorPickerBackColor.ReadOnly = false;
            this.colorPickerBackColor.Size = new System.Drawing.Size(167, 20);
            this.colorPickerBackColor.TabIndex = 13;
            this.colorPickerBackColor.Text = "White";
            this.colorPickerBackColor.Value = System.Drawing.Color.White;
            this.colorPickerBackColor.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // colorPickerColor
            // 
            this.colorPickerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerColor.Context = null;
            this.colorPickerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerColor.Location = new System.Drawing.Point(115, 126);
            this.colorPickerColor.Name = "colorPickerColor";
            this.colorPickerColor.ReadOnly = false;
            this.colorPickerColor.Size = new System.Drawing.Size(167, 20);
            this.colorPickerColor.TabIndex = 11;
            this.colorPickerColor.Text = "White";
            this.colorPickerColor.Value = System.Drawing.Color.White;
            this.colorPickerColor.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // ScalebarPropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.colorPickerImageColor);
            this.Controls.Add(this.comboBoxUnits);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxStyle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonEditLabel);
            this.Controls.Add(this.comboBoxPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorPickerOutlineColor);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.colorPickerBackColor);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.colorPickerColor);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxIntervals);
            this.Controls.Add(this.label2);
            this.Name = "ScalebarPropertyEditor";
            this.Size = new System.Drawing.Size(313, 280);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxIntervals;
        private System.Windows.Forms.Label label2;
        private ColorPicker colorPickerOutlineColor;
        private ColorPicker colorPickerBackColor;
        private ColorPicker colorPickerColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonEditLabel;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxUnits;
        private System.Windows.Forms.Label label6;
        private ColorPicker colorPickerImageColor;
        private System.Windows.Forms.Label label3;
    }
}
