using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using OSGeo.OGR;
using OSGeo.OSR;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using OSGeo.MapServer;
using System.Net.Security;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Dialog form for browsing the proj4 projections based on the EPSG file.
    /// </summary>
    public partial class ProjectionBrowserDialog : Form
    {
        TreeNode firstNode;
        
        /// <summary>
        /// Constructs a new ProjectionBrowserDialog object.
        /// </summary>
        public ProjectionBrowserDialog()
        {
            InitializeComponent();
            comboBoxAuthority.Items.AddRange(Osr.GetAuthorityListFromDatabase());
            comboBoxAuthority.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets and sets the selected projection name.
        /// </summary>
        public string Projection
        {
            get
            {
                string name = "";
                if (treeView.SelectedNode != null)
                {
                    CRSInfo crs = treeView.SelectedNode.Tag as CRSInfo;

                    if (crs != null)
                    {
                        return crs.name;
                    }
                }

                return name;
            }
            set
            {
                TreeNode[] nodes = treeView.Nodes.Find(value, true);
                if (nodes.Length > 0)
                {
                    treeView.SelectedNode = nodes[0];
                    treeView.SelectedNode.EnsureVisible();
                }
            }
        }

        /// <summary>
        /// Gets the selected projection.
        /// </summary>
        public string ProjectionNative
        {
            get
            {
                if (treeView.SelectedNode != null)
                {
                    CRSInfo crs = treeView.SelectedNode.Tag as CRSInfo;

                    if (crs != null)
                    {
                        return $"+{crs.auth_name}:{crs.code}";
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Adding a new item to the projection tree
        /// </summary>
        /// <param name="datum_name">The datum name.</param>
        /// <param name="coord_ref_name">The name of the coordinate reference.</param>
        /// <param name="epsg">The EPSG code of the projection.</param>
        /// <param name="proj4">The corresponding proj4 definition.</param>
        private void AddListItem(string datum_name, CRSInfo crs)
        {
            TreeNode[] nodes = treeView.Nodes.Find(datum_name, false);
            TreeNode parent = null;
            if (nodes.Length > 0)
                parent = nodes[0];
            else
            {
                parent = treeView.Nodes.Add(datum_name, datum_name);
            }

            TreeNode node = parent.Nodes.Add($"{datum_name} / {crs.name}", $"{crs.name} - {crs.auth_name}:{crs.code}");
            node.Tag = crs;
        }

        /// <summary>
        /// Polulate the projection tree based on the EPSG file.
        /// </summary>
        private void PopulateList()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                treeView.Nodes.Clear();
                int count;
                string auth_name = comboBoxAuthority.SelectedItem.ToString();
                var crsInfoList = Osr.GetCRSInfoListFromDatabase(auth_name, out count);
                using (SpatialReference srs = new SpatialReference(null))
                {
                    for (int i = 0; i < count; i++)
                    {
                        var crs = crsInfoList[i];

                        var authCode = $"{crs.auth_name}:{crs.code}";

                        MapUtils.UpdateSpatialReferenceFromAuthCode(srs, authCode);

                        string datum;
                        if (srs.IsVertical() == 1)
                        {
                            datum = srs.GetAttrValue("VERT_DATUM", 0);
                        }
                        else if (srs.IsLocal() == 1)
                        {
                            datum = srs.GetAttrValue("LOCAL_DATUM", 0);
                        }
                        else if (srs.IsGeographic() == 1)
                        {
                            datum = "Longitude-Latitude";
                        }
                        else
                        {
                            datum = srs.GetAttrValue("DATUM", 0);
                        }
                        string wkt;
                        srs.ExportToWkt(out wkt, null);
                        if (string.IsNullOrEmpty(datum))
                        {
                            datum = "Other Non Geographic";
                        }
                        var unit = srs.GetAttrValue("UNIT", 0);
                        AddListItem(datum, crs);
                    }
                }
                treeView.Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                panelRefresh.Visible = false;
            }
        }
        
        /// <summary>
        /// AfterSelect event handler of the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            buttonOK.Enabled = (treeView.SelectedNode.Tag != null);
        }

        /// <summary>
        /// KeyDown event handler of the ProjectionBrowserDialog object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ProjectionBrowserDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Click event handler of the buttonSearch object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (treeView.Nodes.Count > 0)
            {
                TreeNode current = treeView.SelectedNode;
                string searchTerm = textBoxSearch.Text.ToLower();

                if (searchTerm.Length == 0)
                {
                    MessageBox.Show("No search term specified!", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                while (true)
                {
                    // set the pointer to the next node
                    if (current == null)
                        current = treeView.Nodes[0];
                    else if (current.Nodes.Count > 0)
                        current = current.Nodes[0];
                    else if (current.NextNode != null)
                        current = current.NextNode;
                    else if (current.Parent != null && current.Parent.NextNode != null)
                        current = current.Parent.NextNode;
                    else
                        current = treeView.Nodes[0];

                    // check if found the node
                    if (current.Text.ToLower().Contains(searchTerm))
                    {
                        treeView.SelectedNode = current;
                        current.EnsureVisible();
                        return;
                    }
                    if (firstNode == null)
                        firstNode = current;
                    else if (firstNode.Equals(current))
                    {
                        MessageBox.Show("No more records found!", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        firstNode = null;
                        return;
                    }
                }
            }
            MessageBox.Show("No matching node found!", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// TextChanged event handler of the textBoxSearch object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            firstNode = null;
        }

        /// <summary>
        /// KeyDown event handler of the textBoxSearch object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch.PerformClick();
                e.Handled = true;
            }
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxAuthority object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxAuthority_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelRefresh.Visible = true;
            timerRefresh.Enabled = true;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            timerRefresh.Enabled = false;
            PopulateList();
        }
    }
}
