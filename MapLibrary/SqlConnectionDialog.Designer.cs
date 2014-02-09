namespace DMS.MapLibrary
{
    partial class SqlConnectionDialog
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxServers = new System.Windows.Forms.ComboBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonSqlAuth = new System.Windows.Forms.RadioButton();
            this.radioButtonWinAuth = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxDatabases = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxSRID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxGeomCol = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxFIDCol = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxDataTable = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.Location = new System.Drawing.Point(89, 425);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(69, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(208, 425);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(69, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server name:";
            // 
            // comboBoxServers
            // 
            this.comboBoxServers.FormattingEnabled = true;
            this.comboBoxServers.Location = new System.Drawing.Point(12, 31);
            this.comboBoxServers.Name = "comboBoxServers";
            this.comboBoxServers.Size = new System.Drawing.Size(255, 21);
            this.comboBoxServers.Sorted = true;
            this.comboBoxServers.TabIndex = 1;
            this.comboBoxServers.SelectedIndexChanged += new System.EventHandler(this.comboBoxServers_SelectedIndexChanged);
            this.comboBoxServers.DropDown += new System.EventHandler(this.comboBoxServers_DropDown);
            this.comboBoxServers.TextChanged += new System.EventHandler(this.comboBoxServers_TextChanged);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(287, 29);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(70, 23);
            this.buttonRefresh.TabIndex = 2;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonTestConnection);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxUser);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButtonSqlAuth);
            this.groupBox1.Controls.Add(this.radioButtonWinAuth);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 160);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log on to the server";
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new System.Drawing.Point(218, 128);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(121, 23);
            this.buttonTestConnection.TabIndex = 6;
            this.buttonTestConnection.Text = "Test Connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(90, 102);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(249, 20);
            this.textBoxPassword.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Enabled = false;
            this.textBoxUser.Location = new System.Drawing.Point(90, 76);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(249, 20);
            this.textBoxUser.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User name:";
            // 
            // radioButtonSqlAuth
            // 
            this.radioButtonSqlAuth.AutoSize = true;
            this.radioButtonSqlAuth.Location = new System.Drawing.Point(22, 52);
            this.radioButtonSqlAuth.Name = "radioButtonSqlAuth";
            this.radioButtonSqlAuth.Size = new System.Drawing.Size(173, 17);
            this.radioButtonSqlAuth.TabIndex = 1;
            this.radioButtonSqlAuth.TabStop = true;
            this.radioButtonSqlAuth.Text = "Use SQL Server Authentication";
            this.radioButtonSqlAuth.UseVisualStyleBackColor = true;
            this.radioButtonSqlAuth.CheckedChanged += new System.EventHandler(this.radioButtonSqlAuth_CheckedChanged);
            // 
            // radioButtonWinAuth
            // 
            this.radioButtonWinAuth.AutoSize = true;
            this.radioButtonWinAuth.Checked = true;
            this.radioButtonWinAuth.Location = new System.Drawing.Point(22, 29);
            this.radioButtonWinAuth.Name = "radioButtonWinAuth";
            this.radioButtonWinAuth.Size = new System.Drawing.Size(162, 17);
            this.radioButtonWinAuth.TabIndex = 0;
            this.radioButtonWinAuth.TabStop = true;
            this.radioButtonWinAuth.Text = "Use Windows Authentication";
            this.radioButtonWinAuth.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxDatabases);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 54);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connect to a database";
            // 
            // comboBoxDatabases
            // 
            this.comboBoxDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabases.FormattingEnabled = true;
            this.comboBoxDatabases.Location = new System.Drawing.Point(90, 24);
            this.comboBoxDatabases.Name = "comboBoxDatabases";
            this.comboBoxDatabases.Size = new System.Drawing.Size(249, 21);
            this.comboBoxDatabases.Sorted = true;
            this.comboBoxDatabases.TabIndex = 1;
            this.comboBoxDatabases.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabases_SelectedIndexChanged);
            this.comboBoxDatabases.DropDown += new System.EventHandler(this.comboBoxDatabases_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Database:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxSRID);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comboBoxGeomCol);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBoxFIDCol);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxDataTable);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(12, 284);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(345, 135);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Specify layer";
            // 
            // textBoxSRID
            // 
            this.textBoxSRID.Location = new System.Drawing.Point(90, 104);
            this.textBoxSRID.Name = "textBoxSRID";
            this.textBoxSRID.Size = new System.Drawing.Size(249, 20);
            this.textBoxSRID.TabIndex = 7;
            this.textBoxSRID.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInteger);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(48, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = "SRID:";
            // 
            // comboBoxGeomCol
            // 
            this.comboBoxGeomCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGeomCol.FormattingEnabled = true;
            this.comboBoxGeomCol.Location = new System.Drawing.Point(90, 77);
            this.comboBoxGeomCol.Name = "comboBoxGeomCol";
            this.comboBoxGeomCol.Size = new System.Drawing.Size(249, 21);
            this.comboBoxGeomCol.Sorted = true;
            this.comboBoxGeomCol.TabIndex = 5;
            this.comboBoxGeomCol.SelectedIndexChanged += new System.EventHandler(this.comboBoxGeomCol_SelectedIndexChanged);
            this.comboBoxGeomCol.DropDown += new System.EventHandler(this.comboBoxGeomCol_DropDown);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "Geom. Column:";
            // 
            // comboBoxFIDCol
            // 
            this.comboBoxFIDCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFIDCol.FormattingEnabled = true;
            this.comboBoxFIDCol.Location = new System.Drawing.Point(90, 50);
            this.comboBoxFIDCol.Name = "comboBoxFIDCol";
            this.comboBoxFIDCol.Size = new System.Drawing.Size(249, 21);
            this.comboBoxFIDCol.Sorted = true;
            this.comboBoxFIDCol.TabIndex = 3;
            this.comboBoxFIDCol.DropDown += new System.EventHandler(this.comboBoxFIDCol_DropDown);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(22, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "FID Column:";
            // 
            // comboBoxDataTable
            // 
            this.comboBoxDataTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataTable.FormattingEnabled = true;
            this.comboBoxDataTable.Location = new System.Drawing.Point(90, 23);
            this.comboBoxDataTable.Name = "comboBoxDataTable";
            this.comboBoxDataTable.Size = new System.Drawing.Size(249, 21);
            this.comboBoxDataTable.Sorted = true;
            this.comboBoxDataTable.TabIndex = 1;
            this.comboBoxDataTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataTable_SelectedIndexChanged);
            this.comboBoxDataTable.DropDown += new System.EventHandler(this.comboBoxDataTable_DropDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Data Table:";
            // 
            // SqlConnectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 458);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.comboBoxServers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SqlConnectionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Specify SQL Data Source";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlConnectionDialog_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxServers;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonSqlAuth;
        private System.Windows.Forms.RadioButton radioButtonWinAuth;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.ComboBox comboBoxDatabases;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxDataTable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSRID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxGeomCol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxFIDCol;

    }
}