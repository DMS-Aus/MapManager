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
            PopulateList();
        }

        /// <summary>
        /// Get the proj4 definition of the selected projection
        /// </summary>
        /// <returns></returns>
        private string GetProj4()
        {
            if (treeView.SelectedNode != null)
                return treeView.SelectedNode.Tag.ToString();
            else return "";
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
                    name = treeView.SelectedNode.Name;
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
        /// Gets the selected projection (proj4).
        /// </summary>
        public string ProjectionNative
        {
            get
            {
                if (treeView.SelectedNode != null)
                    return treeView.SelectedNode.Tag.ToString();
                return "+AUTO";
            }
        }

        /// <summary>
        /// Adding a new item to the projection tree
        /// </summary>
        /// <param name="datum_name">The datum name.</param>
        /// <param name="coord_ref_name">The name of the coordinate reference.</param>
        /// <param name="epsg">The EPSG code of the projection.</param>
        /// <param name="proj4">The corresponding proj4 definition.</param>
        private void AddListItem(string datum_name, string coord_ref_name, string epsg, string proj4)
        {
            TreeNode[] nodes = treeView.Nodes.Find(datum_name, false);
            TreeNode parent = null;
            if (nodes.Length > 0)
                parent = nodes[0];
            else
            {
                parent = treeView.Nodes.Add(datum_name, datum_name);
            }

            TreeNode node = parent.Nodes.Add(datum_name + " / " + coord_ref_name, coord_ref_name + " - EPSG:" + epsg);
            node.Tag = proj4;
        }

 /// <summary>
        /// Polulate the projection tree based on the EPSG file.
        /// </summary>
        private void PopulateList()
        {
            treeView.Nodes.Clear();
            using (Stream s = File.OpenRead(MapUtils.GetPROJ_LIB() + "\\epsg"))
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    string line;
                    string name = "";
                    string proj4 = "";
                    int row = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        ++row;
                        if (line.StartsWith("#"))
                        {
                            if (proj4 != "")
                            {
                                ProcessLine(name, proj4); 
                            }
                            proj4 = "";
                            name = line.Substring(1).Trim();
                        }
                        else
                            proj4 += line;
                    }

                    //process last line
                    if (proj4 != "" && name!="")
                    {
                        ProcessLine(name, proj4);
                    }
                }
            }
            treeView.Sort();
        }
        /// <summary>
        /// use to create the entry in the treview after after reading name for line1 and proj4 from line2
        /// </summary>
        /// <param name="name"></param>
        /// <param name="proj4"></param>
        private void ProcessLine(string name, string proj4)
        {
            // adding the previous section
            string[] names = name.Split(new string[] { " / " }, StringSplitOptions.None);
            string[] proj_defs = proj4.Split(new char[] { '<', '>' });
            if (proj_defs.Length > 3)
            {
                if (names.Length > 1)
                {
                    AddListItem(names[0].Trim(), names[1].Trim(), proj_defs[1].Trim(), proj_defs[2].Trim());
                }
                else
                {
                    if (proj_defs[2].Contains("longlat"))
                        AddListItem("Longitude-Latitude", names[0].Trim(), proj_defs[1].Trim(), proj_defs[2].Trim());
                    else
                        AddListItem("Other Non Geographic", names[0].Trim(), proj_defs[1].Trim(), proj_defs[2].Trim());
                }
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
    }
}
