namespace DMS.MapLibrary
{
    partial class AddWMSLayerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWMSLayerForm));
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxSingle = new System.Windows.Forms.CheckBox();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.comboBoxStyle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewLayers = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.treeViewLayers = new DMS.MapLibrary.TreeViewNoImage();
            this.buttonLoadLayers = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelProjDesc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxProj = new System.Windows.Forms.ComboBox();
            this.checkBoxProj = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.colorPickerLayerColor = new DMS.MapLibrary.ColorPicker();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxImageFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.buttonConnection = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxServer
            // 
            this.textBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServer.Location = new System.Drawing.Point(92, 22);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(462, 20);
            this.textBoxServer.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "WMS  Server:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.Location = new System.Drawing.Point(300, 444);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.Location = new System.Drawing.Point(429, 444);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxSingle);
            this.groupBox1.Controls.Add(this.textBoxDesc);
            this.groupBox1.Controls.Add(this.comboBoxStyle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(9, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 262);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WMS Layers:";
            // 
            // checkBoxSingle
            // 
            this.checkBoxSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSingle.AutoSize = true;
            this.checkBoxSingle.Checked = true;
            this.checkBoxSingle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSingle.Location = new System.Drawing.Point(12, 238);
            this.checkBoxSingle.Name = "checkBoxSingle";
            this.checkBoxSingle.Size = new System.Drawing.Size(214, 17);
            this.checkBoxSingle.TabIndex = 4;
            this.checkBoxSingle.Text = "Add the selected layers as a single layer";
            this.checkBoxSingle.UseVisualStyleBackColor = true;
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDesc.Location = new System.Drawing.Point(232, 208);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ReadOnly = true;
            this.textBoxDesc.Size = new System.Drawing.Size(544, 45);
            this.textBoxDesc.TabIndex = 3;
            // 
            // comboBoxStyle
            // 
            this.comboBoxStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStyle.FormattingEnabled = true;
            this.comboBoxStyle.Location = new System.Drawing.Point(47, 208);
            this.comboBoxStyle.Name = "comboBoxStyle";
            this.comboBoxStyle.Size = new System.Drawing.Size(168, 21);
            this.comboBoxStyle.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Style:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.listViewLayers, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeViewLayers, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 183);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listViewLayers
            // 
            this.listViewLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLayers.HideSelection = false;
            this.listViewLayers.Location = new System.Drawing.Point(435, 3);
            this.listViewLayers.MultiSelect = false;
            this.listViewLayers.Name = "listViewLayers";
            this.listViewLayers.Size = new System.Drawing.Size(340, 177);
            this.listViewLayers.SmallImageList = this.imageList;
            this.listViewLayers.TabIndex = 2;
            this.listViewLayers.UseCompatibleStateImageBehavior = false;
            this.listViewLayers.View = System.Windows.Forms.View.List;
            this.listViewLayers.SelectedIndexChanged += new System.EventHandler(this.listViewLayers_SelectedIndexChanged);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "info.png");
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonAdd, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonRemove, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonUp, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.buttonDown, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(348, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(81, 177);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Add >>";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(3, 38);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "<< Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(3, 108);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(75, 23);
            this.buttonUp.TabIndex = 2;
            this.buttonUp.Text = "Move Up";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(3, 143);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(75, 23);
            this.buttonDown.TabIndex = 3;
            this.buttonDown.Text = "Move Down";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // treeViewLayers
            // 
            this.treeViewLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLayers.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeViewLayers.HideSelection = false;
            this.treeViewLayers.ImageIndex = 0;
            this.treeViewLayers.ImageList = this.imageList;
            this.treeViewLayers.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.treeViewLayers.Location = new System.Drawing.Point(3, 3);
            this.treeViewLayers.Name = "treeViewLayers";
            this.treeViewLayers.SelectedImageIndex = 0;
            this.treeViewLayers.Size = new System.Drawing.Size(339, 177);
            this.treeViewLayers.TabIndex = 0;
            this.treeViewLayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLayers_AfterSelect);
            // 
            // buttonLoadLayers
            // 
            this.buttonLoadLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadLayers.Location = new System.Drawing.Point(560, 21);
            this.buttonLoadLayers.Name = "buttonLoadLayers";
            this.buttonLoadLayers.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadLayers.TabIndex = 2;
            this.buttonLoadLayers.Text = "Load Layers";
            this.buttonLoadLayers.UseVisualStyleBackColor = true;
            this.buttonLoadLayers.Click += new System.EventHandler(this.buttonLoadLayers_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(9, 340);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(782, 91);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelProjDesc);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBoxProj);
            this.groupBox2.Controls.Add(this.checkBoxProj);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(394, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Coordinate Reference System:";
            // 
            // labelProjDesc
            // 
            this.labelProjDesc.Location = new System.Drawing.Point(9, 65);
            this.labelProjDesc.Name = "labelProjDesc";
            this.labelProjDesc.Size = new System.Drawing.Size(338, 13);
            this.labelProjDesc.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Projection:";
            // 
            // comboBoxProj
            // 
            this.comboBoxProj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProj.FormattingEnabled = true;
            this.comboBoxProj.Location = new System.Drawing.Point(72, 40);
            this.comboBoxProj.Name = "comboBoxProj";
            this.comboBoxProj.Size = new System.Drawing.Size(310, 21);
            this.comboBoxProj.TabIndex = 2;
            // 
            // checkBoxProj
            // 
            this.checkBoxProj.AutoSize = true;
            this.checkBoxProj.Location = new System.Drawing.Point(6, 19);
            this.checkBoxProj.Name = "checkBoxProj";
            this.checkBoxProj.Size = new System.Drawing.Size(233, 17);
            this.checkBoxProj.TabIndex = 0;
            this.checkBoxProj.Text = "Show Projection Description instead of SRS";
            this.checkBoxProj.UseVisualStyleBackColor = true;
            this.checkBoxProj.CheckedChanged += new System.EventHandler(this.checkBoxProj_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.comboBoxImageFormat);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 85);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxTransparent);
            this.groupBox3.Controls.Add(this.colorPickerLayerColor);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 37);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 48);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image Background:";
            // 
            // checkBoxTransparent
            // 
            this.checkBoxTransparent.AutoSize = true;
            this.checkBoxTransparent.Checked = true;
            this.checkBoxTransparent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTransparent.Location = new System.Drawing.Point(9, 21);
            this.checkBoxTransparent.Name = "checkBoxTransparent";
            this.checkBoxTransparent.Size = new System.Drawing.Size(83, 17);
            this.checkBoxTransparent.TabIndex = 0;
            this.checkBoxTransparent.Text = "Transparent";
            this.checkBoxTransparent.UseVisualStyleBackColor = true;
            // 
            // colorPickerLayerColor
            // 
            this.colorPickerLayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorPickerLayerColor.BackColor = System.Drawing.SystemColors.Window;
            this.colorPickerLayerColor.Context = null;
            this.colorPickerLayerColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPickerLayerColor.Location = new System.Drawing.Point(147, 18);
            this.colorPickerLayerColor.Name = "colorPickerLayerColor";
            this.colorPickerLayerColor.ReadOnly = false;
            this.colorPickerLayerColor.Size = new System.Drawing.Size(232, 20);
            this.colorPickerLayerColor.TabIndex = 2;
            this.colorPickerLayerColor.Value = System.Drawing.Color.Empty;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Colour:";
            // 
            // comboBoxImageFormat
            // 
            this.comboBoxImageFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImageFormat.FormattingEnabled = true;
            this.comboBoxImageFormat.Location = new System.Drawing.Point(86, 7);
            this.comboBoxImageFormat.Name = "comboBoxImageFormat";
            this.comboBoxImageFormat.Size = new System.Drawing.Size(293, 21);
            this.comboBoxImageFormat.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image Format:";
            // 
            // buttonDetails
            // 
            this.buttonDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDetails.Location = new System.Drawing.Point(641, 21);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new System.Drawing.Size(67, 23);
            this.buttonDetails.TabIndex = 3;
            this.buttonDetails.Text = "Details...";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // buttonConnection
            // 
            this.buttonConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnection.Location = new System.Drawing.Point(714, 21);
            this.buttonConnection.Name = "buttonConnection";
            this.buttonConnection.Size = new System.Drawing.Size(84, 23);
            this.buttonConnection.TabIndex = 4;
            this.buttonConnection.Text = "Connection...";
            this.buttonConnection.UseVisualStyleBackColor = true;
            this.buttonConnection.Click += new System.EventHandler(this.buttonConnection_Click);
            // 
            // AddWMSLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 479);
            this.Controls.Add(this.buttonConnection);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.buttonLoadLayers);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.label5);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWMSLayerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add WMS Layer(s)";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddWMSLayerForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listViewLayers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonLoadLayers;
        private System.Windows.Forms.ImageList imageList;
        private DMS.MapLibrary.TreeViewNoImage treeViewLayers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxImageFormat;
        private System.Windows.Forms.Label label1;
        private ColorPicker colorPickerLayerColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxTransparent;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.ComboBox comboBoxStyle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonDetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxProj;
        private System.Windows.Forms.CheckBox checkBoxProj;
        private System.Windows.Forms.Label labelProjDesc;
        private System.Windows.Forms.CheckBox checkBoxSingle;
        private System.Windows.Forms.Button buttonConnection;
    }
}