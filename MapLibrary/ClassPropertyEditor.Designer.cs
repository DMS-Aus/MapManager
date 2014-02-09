namespace DMS.MapLibrary
{
    partial class ClassPropertyEditor
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.textBoxExpression = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.buttonMaxScale = new System.Windows.Forms.Button();
            this.buttonMinScale = new System.Windows.Forms.Button();
            this.checkBoxQueryable = new System.Windows.Forms.CheckBox();
            this.labelMaxZoom = new System.Windows.Forms.Label();
            this.labelMinZoom = new System.Windows.Forms.Label();
            this.textBoxMaxZoom = new System.Windows.Forms.TextBox();
            this.textBoxMinZoom = new System.Windows.Forms.TextBox();
            this.checkBoxVisible = new System.Windows.Forms.CheckBox();
            this.mapControl = new DMS.MapLibrary.MapControl();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sample:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageDisplay);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(298, 177);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.textBoxExpression);
            this.tabPageGeneral.Controls.Add(this.label5);
            this.tabPageGeneral.Controls.Add(this.textBoxText);
            this.tabPageGeneral.Controls.Add(this.label4);
            this.tabPageGeneral.Controls.Add(this.textBoxTitle);
            this.tabPageGeneral.Controls.Add(this.label3);
            this.tabPageGeneral.Controls.Add(this.textBoxName);
            this.tabPageGeneral.Controls.Add(this.label2);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(290, 151);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // textBoxExpression
            // 
            this.textBoxExpression.Location = new System.Drawing.Point(75, 108);
            this.textBoxExpression.Name = "textBoxExpression";
            this.textBoxExpression.Size = new System.Drawing.Size(209, 20);
            this.textBoxExpression.TabIndex = 7;
            this.textBoxExpression.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Expression:";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(75, 78);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(209, 20);
            this.textBoxText.TabIndex = 5;
            this.textBoxText.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Text:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(75, 48);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(209, 20);
            this.textBoxTitle.TabIndex = 3;
            this.textBoxTitle.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Title:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(75, 18);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(209, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.ValueChanging);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Class Name:";
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.buttonMaxScale);
            this.tabPageDisplay.Controls.Add(this.buttonMinScale);
            this.tabPageDisplay.Controls.Add(this.checkBoxQueryable);
            this.tabPageDisplay.Controls.Add(this.labelMaxZoom);
            this.tabPageDisplay.Controls.Add(this.labelMinZoom);
            this.tabPageDisplay.Controls.Add(this.textBoxMaxZoom);
            this.tabPageDisplay.Controls.Add(this.textBoxMinZoom);
            this.tabPageDisplay.Controls.Add(this.checkBoxVisible);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplay.Size = new System.Drawing.Size(290, 151);
            this.tabPageDisplay.TabIndex = 1;
            this.tabPageDisplay.Text = "Display";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // buttonMaxScale
            // 
            this.buttonMaxScale.Image = global::MapLibrary.Properties.Resources.Map;
            this.buttonMaxScale.Location = new System.Drawing.Point(260, 85);
            this.buttonMaxScale.Name = "buttonMaxScale";
            this.buttonMaxScale.Size = new System.Drawing.Size(24, 24);
            this.buttonMaxScale.TabIndex = 34;
            this.buttonMaxScale.UseVisualStyleBackColor = true;
            this.buttonMaxScale.Click += new System.EventHandler(this.buttonMaxScale_Click);
            // 
            // buttonMinScale
            // 
            this.buttonMinScale.Image = global::MapLibrary.Properties.Resources.Map;
            this.buttonMinScale.Location = new System.Drawing.Point(260, 51);
            this.buttonMinScale.Name = "buttonMinScale";
            this.buttonMinScale.Size = new System.Drawing.Size(24, 24);
            this.buttonMinScale.TabIndex = 33;
            this.buttonMinScale.UseVisualStyleBackColor = true;
            this.buttonMinScale.Click += new System.EventHandler(this.buttonMinScale_Click);
            // 
            // checkBoxQueryable
            // 
            this.checkBoxQueryable.AutoSize = true;
            this.checkBoxQueryable.Location = new System.Drawing.Point(104, 21);
            this.checkBoxQueryable.Name = "checkBoxQueryable";
            this.checkBoxQueryable.Size = new System.Drawing.Size(74, 17);
            this.checkBoxQueryable.TabIndex = 1;
            this.checkBoxQueryable.Text = "Queryable";
            this.checkBoxQueryable.UseVisualStyleBackColor = true;
            this.checkBoxQueryable.CheckedChanged += new System.EventHandler(this.ValueChanging);
            // 
            // labelMaxZoom
            // 
            this.labelMaxZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMaxZoom.AutoSize = true;
            this.labelMaxZoom.Location = new System.Drawing.Point(12, 90);
            this.labelMaxZoom.Name = "labelMaxZoom";
            this.labelMaxZoom.Size = new System.Drawing.Size(88, 13);
            this.labelMaxZoom.TabIndex = 4;
            this.labelMaxZoom.Text = "Farthest scale: 1:";
            this.labelMaxZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMinZoom
            // 
            this.labelMinZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMinZoom.AutoSize = true;
            this.labelMinZoom.Location = new System.Drawing.Point(15, 56);
            this.labelMinZoom.Name = "labelMinZoom";
            this.labelMinZoom.Size = new System.Drawing.Size(84, 13);
            this.labelMinZoom.TabIndex = 2;
            this.labelMinZoom.Text = "Closest scale: 1:";
            this.labelMinZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxMaxZoom
            // 
            this.textBoxMaxZoom.Location = new System.Drawing.Point(104, 87);
            this.textBoxMaxZoom.Name = "textBoxMaxZoom";
            this.textBoxMaxZoom.Size = new System.Drawing.Size(150, 20);
            this.textBoxMaxZoom.TabIndex = 5;
            this.textBoxMaxZoom.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMaxZoom.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // textBoxMinZoom
            // 
            this.textBoxMinZoom.Location = new System.Drawing.Point(104, 53);
            this.textBoxMinZoom.Name = "textBoxMinZoom";
            this.textBoxMinZoom.Size = new System.Drawing.Size(150, 20);
            this.textBoxMinZoom.TabIndex = 3;
            this.textBoxMinZoom.TextChanged += new System.EventHandler(this.ValueChanging);
            this.textBoxMinZoom.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble2);
            // 
            // checkBoxVisible
            // 
            this.checkBoxVisible.AutoSize = true;
            this.checkBoxVisible.Location = new System.Drawing.Point(15, 21);
            this.checkBoxVisible.Name = "checkBoxVisible";
            this.checkBoxVisible.Size = new System.Drawing.Size(56, 17);
            this.checkBoxVisible.TabIndex = 0;
            this.checkBoxVisible.Text = "Visible";
            this.checkBoxVisible.UseVisualStyleBackColor = true;
            this.checkBoxVisible.CheckedChanged += new System.EventHandler(this.ValueChanging);
            // 
            // mapControl
            // 
            this.mapControl.Border = true;
            this.mapControl.EnableRendering = true;
            this.mapControl.Gap = 10;
            this.mapControl.InputMode = DMS.MapLibrary.MapControl.InputModes.Pan;
            this.mapControl.Location = new System.Drawing.Point(304, 21);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(146, 155);
            this.mapControl.TabIndex = 2;
            this.mapControl.Target = null;
            this.mapControl.Text = "mapControl1";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(199, 34);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(87, 23);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Edit Label";
            this.buttonEdit.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::MapLibrary.Properties.Resources.Map;
            this.button1.Location = new System.Drawing.Point(260, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 34;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ClassPropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapControl);
            this.Name = "ClassPropertyEditor";
            this.Size = new System.Drawing.Size(453, 181);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MapControl mapControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxVisible;
        private System.Windows.Forms.Label labelMaxZoom;
        private System.Windows.Forms.Label labelMinZoom;
        private System.Windows.Forms.TextBox textBoxMaxZoom;
        private System.Windows.Forms.TextBox textBoxMinZoom;
        private System.Windows.Forms.TextBox textBoxExpression;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxQueryable;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonMaxScale;
        private System.Windows.Forms.Button buttonMinScale;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button1;

    }
}
