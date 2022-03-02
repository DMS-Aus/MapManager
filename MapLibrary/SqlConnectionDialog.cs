using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Dialog form for constructing mssql connection strings.
    /// </summary>
    public partial class SqlConnectionDialog : Form
    {
        private static string server;
        private static bool useSQL;
        private static string user;
        private static string password;
        private static string database;

        /// <summary>
        /// Constructs a new SqlConnectionDialog object.
        /// </summary>
        public SqlConnectionDialog()
        {
            InitializeComponent();
            proj4text = null;
            srtext = null;
            //restore last settings
            comboBoxServers.Text = server;
            radioButtonSqlAuth.Checked = useSQL;
            textBoxUser.Text = user;
            textBoxPassword.Text = password;
            if (server != null && server != "" && database!=null && database!="")
            {
                LoadDatabases();
                comboBoxDatabases.Text = database;
            }
            //int i = 0;
            //while (i < comboBoxDatabases.Items.Count)
            //{
            //    if ((string)comboBoxDatabases.Items[i] == database)
            //    {
            //        comboBoxDatabases.SelectedIndex = i;
            //        break;
            //    }
            //    i++;
            //}
        }

        SqlConnection cn;
        string geometryType;
        string proj4text;
        string srtext;
        DataTable dataTable;

        /// <summary>
        /// Returns the geometry type of the geometries in the selected table
        /// </summary>
        public string GeometryType
        {
            get
            {
                return geometryType;
            }
        }

        /// <summary>
        /// Returns the name selected table
        /// </summary>
        public string TableName
        {
            get
            {
                return comboBoxDataTable.Text.Replace("dbo.", "");
            }
        }
        /// <summary>
        /// Returns the SRID if the selected table
        /// </summary>
        public string Srid
        {
            get
            {
                return textBoxSRID.Text;
            }
        }

        /// <summary>
        /// Initialize the connection from the specified parameters
        /// </summary>
        private void InitConnection()
        {
            cn = new SqlConnection(GetConnectionString());
            cn.Open();
        }

        /// <summary>
        /// Get the connection string created from the user selection
        /// </summary>
        /// <returns>The connection string</returns>
        public string GetConnectionString()
        {
            StringBuilder s = new StringBuilder();
            if (comboBoxServers.Text != "")
            {
                s.Append("Server=" + comboBoxServers.Text);
            }
            if (radioButtonWinAuth.Checked)
            {
                s.Append(";Trusted_Connection=True");
            }
            else
            {
                s.Append(";UID=" + textBoxUser.Text);
                s.Append(";PWD=" + textBoxPassword.Text);
            }
            if (comboBoxDatabases.Text != "")
            {
                s.Append(";Database=" + comboBoxDatabases.Text);
            }
            return s.ToString();
        }

        /// <summary>
        /// Initialize the dialog according to the connection string
        /// </summary>
        /// <param name="connString">The connection string</param>
        public void SetConnectionString(string connString)
        {
            string[] param = connString.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            radioButtonWinAuth.Checked = false;
            bool trusted = false;
            string server = "";
            string database = "";
            string uid = "";
            string pwd = "";
            foreach (string item in param)
            {
                string[] namevalue = item.Split(new char[] {'='});
                if (string.Compare(namevalue[0].Trim(), "Server", true) == 0)
                    server = namevalue[1].Trim();
                else if (string.Compare(namevalue[0].Trim(), "Database", true) == 0)
                    database = namevalue[1].Trim();
                else if (string.Compare(namevalue[0].Trim(), "UID", true) == 0)
                    uid = namevalue[1].Trim();
                else if (string.Compare(namevalue[0].Trim(), "PWD", true) == 0)
                    pwd = namevalue[1].Trim();
                else if (string.Compare(namevalue[0].Trim(), "Trusted_Connection", true) == 0)
                    trusted = true;
            }
            comboBoxServers.Text = server;

            if (cn == null || cn.State != ConnectionState.Open)
            {
                cn = new SqlConnection(connString);
                cn.Open();
            }
            LoadDatabases();
            comboBoxDatabases.Text = database;

            if (uid != "")
                textBoxUser.Text = uid;
            if (pwd != "")
                textBoxPassword.Text = pwd;
            radioButtonWinAuth.Checked = trusted;
            radioButtonSqlAuth.Checked = !trusted;
        }

        /// <summary>
        /// Get the projection wkt for the specified layer
        /// </summary>
        /// <returns>The projection wkt</returns>
        public string GetSRText()
        {
            return srtext;
        }

        /// <summary>
        /// Get the proj4 text for the specified layer
        /// </summary>
        /// <returns>The projection name</returns>
        public string GetProj4Text()
        {
            return proj4text;
        }

        /// <summary>
        /// Get the data string created from the user selection
        /// </summary>
        /// <returns>The data string</returns>
        public string GetDataString()
        {
            if (comboBoxGeomCol.Text == "" || comboBoxDataTable.Text == "")
                return null;
            
            StringBuilder s = new StringBuilder();
            s.Append(comboBoxGeomCol.Text + " from ");

            if (comboBoxDataTable.Text.StartsWith("dbo."))
                s.Append(comboBoxDataTable.Text.Substring(4));
            else
                s.Append(comboBoxDataTable.Text);

            if (comboBoxFIDCol.Text != "")
            {
                s.Append(" USING UNIQUE " + comboBoxFIDCol.Text);
            }

            if (textBoxSRID.Text != "")
            {
                s.Append(" USING SRID=" + textBoxSRID.Text);
            }

            return s.ToString();
        }

        /// <summary>
        /// Initialize the dialog according to the data string
        /// </summary>
        /// <param name="connString">The connection string</param>
        public void SetDataString(string dataString)
        {
            // match geometry column
            string geom = Regex.Match(dataString, @"(?<geometry>\s*[A-Za-z0-9-_]+(?=\s+from))",
                RegexOptions.IgnoreCase).ToString().Trim();
            // match table name
            string table = Regex.Match(dataString, @"(?<=from\s+)[A-Za-z0-9-_]+(?=\s*)", 
                RegexOptions.IgnoreCase).ToString().Trim();
            // match fid column
            string fid = Regex.Match(dataString, @"(?<=using\s+unique\s+)[A-Za-z0-9-_]+(?=\s*)",
                RegexOptions.IgnoreCase).ToString().Trim();
            // match srid
            string srid = Regex.Match(dataString, @"(?<=using\s+srid\s*=\s*)[0-9]+",
                RegexOptions.IgnoreCase).ToString().Trim();

            if (table != "")
            {
                LoadDataTables();
                foreach (string item in comboBoxDataTable.Items)
                    if (string.Compare(item, table, true) == 0 || string.Compare(item, "dbo." + table, true) == 0)
                        comboBoxDataTable.Text = item;

                LoadColumns();
            }

            if (geom != "")
            {
                foreach (string item in comboBoxGeomCol.Items)
                    if (string.Compare(item, geom, true) == 0)
                        comboBoxGeomCol.Text = geom;
            }

            if (fid != "")
            {
                foreach (string item in comboBoxFIDCol.Items)
                    if (string.Compare(item, fid, true) == 0)
                        comboBoxFIDCol.Text = fid;
            }

            textBoxSRID.Text = srid;
        }

        /// <summary>
        /// Thread proc to load the servers in background
        /// </summary>
        private void LoadServersProc()
        {
            System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            dataTable = instance.GetDataSources();
        }
        
        /// <summary>
        /// Load the available server instances
        /// </summary>
        private void LoadServers()
        {
            this.Cursor = Cursors.WaitCursor;

            // loading the servers in a background thread and allow
            // the UI message pump to operate in between
            Thread t = new Thread(new ThreadStart(LoadServersProc));
            t.Start();
            t.Join();

            comboBoxServers.Items.Clear();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                //Stephane: just fixed issue with trailing \ when no instance name is specified.
                if (row["InstanceName"].ToString() != null && row["InstanceName"].ToString() != "")
                {
                    comboBoxServers.Items.Add(row["ServerName"] + "\\" + row["InstanceName"]);
                }
                else
                {
                    comboBoxServers.Items.Add(row["ServerName"]);
                }
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Load the database names from the selected instance
        /// </summary>
        private void LoadDatabases()
        {
            this.Cursor = Cursors.WaitCursor;

            if (cn == null || cn.State != ConnectionState.Open)
                InitConnection();
            
            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand();
            SqlCom.Connection = cn;
            SqlCom.CommandType = CommandType.Text;
            SqlCom.CommandText = "select name from sys.databases";

            System.Data.SqlClient.SqlDataReader SqlDR;
            using (SqlDR = SqlCom.ExecuteReader())
            {
                comboBoxDatabases.Items.Clear();
                while (SqlDR.Read())
                {
                    comboBoxDatabases.Items.Add(SqlDR.GetString(0));
                }
            }

            if (comboBoxDatabases.Items.Count == 0)
                comboBoxDatabases.DropDownStyle = ComboBoxStyle.DropDown;
            else
                comboBoxDatabases.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Load the table names of the selected database
        /// </summary>
        private void LoadDataTables()
        {
            this.Cursor = Cursors.WaitCursor;

            if (cn == null || cn.State != ConnectionState.Open)
                InitConnection();

            if (cn.Database != comboBoxDatabases.Text)
                cn.ChangeDatabase(comboBoxDatabases.Text);

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand();
            SqlCom.Connection = cn;
            SqlCom.CommandType = CommandType.Text;
            SqlCom.CommandText = "select TABLE_SCHEMA,TABLE_NAME from INFORMATION_SCHEMA.Tables";

            System.Data.SqlClient.SqlDataReader SqlDR;
            using (SqlDR = SqlCom.ExecuteReader())
            {
                comboBoxDataTable.Items.Clear();
                while (SqlDR.Read())
                {
                    comboBoxDataTable.Items.Add(SqlDR.GetString(0) + "." + SqlDR.GetString(1));
                }
            }

            if (comboBoxDataTable.Items.Count == 0)
                comboBoxDataTable.DropDownStyle = ComboBoxStyle.DropDown;
            else
                comboBoxDataTable.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Load the column names of the selected data table
        /// </summary>
        private void LoadColumns()
        {
            if (cn == null || cn.State != ConnectionState.Open)
                InitConnection();

            if (cn.Database != comboBoxDatabases.Text)
                cn.ChangeDatabase(comboBoxDatabases.Text);

            string[] tableName = comboBoxDataTable.Text.Trim().Split(new char[] {'.'});

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand();
            SqlCom.Connection = cn;
            SqlCom.CommandType = CommandType.Text;
            if (tableName.Length > 1)
                SqlCom.CommandText = "select DATA_TYPE, COLUMN_NAME from INFORMATION_SCHEMA.Columns where TABLE_SCHEMA = '" + tableName[0] + "' and TABLE_NAME = '" + tableName[1] + "'";
            else
                return;

            this.Cursor = Cursors.WaitCursor;
            
            System.Data.SqlClient.SqlDataReader SqlDR;
            using (SqlDR = SqlCom.ExecuteReader())
            {
                comboBoxFIDCol.Items.Clear();
                comboBoxGeomCol.Items.Clear();
                while (SqlDR.Read())
                {
                    string dataType = SqlDR.GetString(0);
                    if (dataType == "geometry")
                        comboBoxGeomCol.Items.Add(SqlDR.GetString(1));
                    else if (dataType == "geography")
                        comboBoxGeomCol.Items.Add(SqlDR.GetString(1) + "(geography)");
                    else if (dataType == "int")
                        comboBoxFIDCol.Items.Add(SqlDR.GetString(1));
                    else if (dataType == "bigint")
                        comboBoxFIDCol.Items.Add(SqlDR.GetString(1));
                }
            }

            if (comboBoxFIDCol.Items.Count == 0)
                comboBoxFIDCol.DropDownStyle = ComboBoxStyle.DropDown;
            else
                comboBoxFIDCol.DropDownStyle = ComboBoxStyle.DropDownList;

            if (comboBoxGeomCol.Items.Count == 0)
                comboBoxGeomCol.DropDownStyle = ComboBoxStyle.DropDown;
            else
                comboBoxGeomCol.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Load the srid of the selected data table
        /// </summary>
        private void LoadSRID()
        {
            if (cn == null || cn.State != ConnectionState.Open)
                InitConnection();

            if (cn.Database != comboBoxDatabases.Text)
                cn.ChangeDatabase(comboBoxDatabases.Text);

            string[] tableName = comboBoxDataTable.Text.Trim().Split(new char[] { '.' });
            string geomCol = comboBoxGeomCol.Text.ToLower().Replace("(geometry)","").Replace("(geography)","").Trim();
            if (geomCol.Length == 0)
                return;

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand();
            SqlCom.Connection = cn;
            SqlCom.CommandType = CommandType.Text;
            if (tableName.Length > 1)
                SqlCom.CommandText = "select top 1 [" + geomCol + "].STSrid, [" + geomCol + "].STGeometryType() from [" + tableName[0] + "].[" + tableName[1] + "] where [" + geomCol + "] is not null";
            else
                return;

            this.Cursor = Cursors.WaitCursor;

            System.Data.SqlClient.SqlDataReader SqlDR;
            using (SqlDR = SqlCom.ExecuteReader())
            {
                while (SqlDR.Read())
                {
                    if (!SqlDR.IsDBNull(0))
                        textBoxSRID.Text = SqlDR.GetInt32(0).ToString();
                    else
                        textBoxSRID.Text = "";

                    geometryType = SqlDR.GetString(1);
                    break;
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadServers();
        }

        /// <summary>
        /// DropDown event handler of the comboBoxServers control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxServers_DropDown(object sender, EventArgs e)
        {
            if (comboBoxServers.Items.Count == 0)
                LoadServers();
        }

        /// <summary>
        /// CheckedChanged event handler of the radioButtonSqlAuth control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void radioButtonSqlAuth_CheckedChanged(object sender, EventArgs e)
        {
            textBoxUser.Enabled = textBoxPassword.Enabled = radioButtonSqlAuth.Checked;
        }

        /// <summary>
        /// Click event handler of the buttonTestConnection control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                InitConnection();
                MessageBox.Show("Test connection succeeded!",
                            "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                            "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// DropDown event handler of the comboBoxDatabases control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxDatabases_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxDatabases.Items.Count == 0)
                    LoadDatabases();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// DropDown event handler of the comboBoxDataTable control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxDataTable_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxDataTable.Items.Count == 0)
                    LoadDataTables();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// DropDown event handler of the comboBoxFIDCol control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxFIDCol_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxFIDCol.Items.Count == 0)
                    LoadColumns();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// DropDown event handler of the comboBoxGeomCol control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxGeomCol_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxGeomCol.Items.Count == 0)
                    LoadColumns();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Common function to validate the integer values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateInteger(object sender, CancelEventArgs e)
        {
            if (((TextBoxBase)sender).Text.Length == 0)
                return;
  
            int result;
            if (!int.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid integer number", "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxDataTable control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxDataTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxFIDCol.Items.Clear();
            comboBoxFIDCol.Text = "";
            comboBoxGeomCol.Items.Clear();
            comboBoxGeomCol.Text = "";
            textBoxSRID.Text = "";
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxGeomCol control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxGeomCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSRID();
        }

        /// <summary>
        /// Click event handler of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxServers.Text==null || comboBoxServers.Text=="")
            {
                MessageBox.Show("SQL Server has not been specified!", "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxDatabases.Text.Trim().Length == 0)
            {
                MessageBox.Show("SQL Server database has not been specified!", "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxDataTable.Text.Trim().Length == 0)
            {
                MessageBox.Show("Data table has not been specified!", "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxGeomCol.Text.Trim().Length == 0)
            {
                MessageBox.Show("Geometry column has not been specified!", "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxSRID.Text.Trim().Length == 0)
            {
                MessageBox.Show("SRID has not been specified!", "SQL Server Connection Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // loading the projection definition
            proj4text = null;
            srtext = null;
            try
            {
                System.Data.SqlClient.SqlCommand SqlCom2 = new System.Data.SqlClient.SqlCommand();
                SqlCom2.Connection = cn;
                SqlCom2.CommandType = CommandType.Text;
                SqlCom2.CommandText = "select srtext, proj4text from spatial_ref_sys where srid = " + textBoxSRID.Text;

                System.Data.SqlClient.SqlDataReader SqlDR2;
                using (SqlDR2 = SqlCom2.ExecuteReader())
                {
                    while (SqlDR2.Read())
                    {
                        if (!SqlDR2.IsDBNull(0))
                            srtext = SqlDR2.GetString(0);

                        if (!SqlDR2.IsDBNull(1))
                            proj4text = SqlDR2.GetString(1);

                        break;
                    }
                }
            }
            catch (Exception) { } // just omit exceptions here

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxDatabases control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
                cn = null;
            }
            comboBoxDataTable.Items.Clear();
            comboBoxDataTable.Text = "";
            comboBoxDataTable_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxServers control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDatabases.Items.Clear();
            comboBoxDatabases.Text = "";
            comboBoxDatabases_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// TextChanged event handler of the comboBoxServers control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxServers_TextChanged(object sender, EventArgs e)
        {
            comboBoxServers_SelectedIndexChanged(sender, e);
        }

        private void SqlConnectionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save current settings
            server = comboBoxServers.Text;
            useSQL=radioButtonSqlAuth.Checked;
            user = textBoxUser.Text;
            password = textBoxPassword.Text;
            database = comboBoxDatabases.Text;
        }
    }
}