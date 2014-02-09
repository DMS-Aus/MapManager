using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;
using System.Xml;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Net;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a Form for adding WMS layers.
    /// </summary>
    public partial class AddWMSLayerForm : Form
    {
        private MapObjectHolder target;
        private MapObjectHolder selected;
        private mapObj map;
        XmlDocument doc;
        string wms_server_version;
        string serverURL;
        BindingSource bs;
        List<KeyValuePair<string, string>> sortedProj;
        rectObj wms_bbox;
        string wms_name;
        string wms_title;
        string wms_queryable;
        double wms_minscaledenom;
        double wms_maxscaledenom;

        static XmlProxyUrlResolver resolver = new XmlProxyUrlResolver();

        /// <summary>
        /// Constructs a new AddWMSLayerForm class.
        /// </summary>
        public AddWMSLayerForm(MapObjectHolder target)
        {
            InitializeComponent();
            if (target.GetType() != typeof(mapObj))
                throw new ApplicationException("Invalid object type.");
            this.map = target;
            this.target = target;
            UpdateControls();
        }

        /// <summary>
        /// Returns the recently added layer
        /// </summary>
        /// <returns>Reference of the layer added</returns>
        public MapObjectHolder GetSelectedLayer()
        {
            return selected;
        }

        /// <summary>
        /// Add a node to the layer tree
        /// </summary>
        /// <param name="nodes">The collection of the tree nodes to which the node should be added</param>
        /// <param name="layerDesc">The XML data for the layer node</param>
        private void AddLayerNode(TreeNodeCollection nodes, XmlNode layerDesc)
        {
            string title = layerDesc.SelectSingleNode("Title").InnerText;
            TreeNode layerNode = new TreeNode(title);
            XmlAttribute att = layerDesc.Attributes["queryable"];
            if (att != null && att.Value.Trim() == "1")
            {
                layerNode.ImageIndex = 0;
                layerNode.SelectedImageIndex = 0;
            }
            else
            {
                layerNode.ImageIndex = TreeViewNoImage.NOIMAGE;
                layerNode.SelectedImageIndex = TreeViewNoImage.NOIMAGE;
            }
            
            layerNode.Tag = layerDesc; // wire up the layer XML in the node
            nodes.Add(layerNode);
            // add sublayers (if any)
            foreach (XmlNode node in layerDesc.SelectNodes("Layer"))
            {
                AddLayerNode(layerNode.Nodes, node);
            }
        }

        /// <summary>
        /// Update the state of the controls
        /// </summary>
        private void UpdateControls()
        {
            buttonAdd.Enabled = (treeViewLayers.SelectedNode != null);
            buttonRemove.Enabled = false;
            buttonUp.Enabled = false;
            buttonDown.Enabled = false;
            buttonDetails.Enabled = (doc != null);

            if (listViewLayers.SelectedItems.Count > 0)
            {
                buttonRemove.Enabled = true;
                if (listViewLayers.SelectedItems[0].Index > 0)
                    buttonUp.Enabled = true;
                if (listViewLayers.SelectedItems[0].Index < listViewLayers.Items.Count -1)
                    buttonDown.Enabled = true;
            }
        }

        /// <summary>
        /// Refresh the layer list by submitting a GetCapabilities request.
        /// </summary>
        public void LoadLayers()
        {
            //Create the XmlDocument.
            doc = new XmlDocument();
            doc.XmlResolver = null;

            //serverURL = textBoxServer.Text.Trim().Split(new char[] { '?' })[0];
            serverURL = textBoxServer.Text.Trim();


            //steph: remove the check as it force user to encode \ in their URL (map=c:\data\mymap.map)
            //if url is not valid, let the app report it 
            //if (!Uri.IsWellFormedUriString(serverURL, UriKind.Absolute))
            //{
            //    MessageBox.Show("The specified server URL is invalid",
            //        "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            
            while (true)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    XmlTextReader reader;
                    if (serverURL.Contains("?"))
                        reader = new XmlTextReader(serverURL + "&SERVICE=WMS&REQUEST=GetCapabilities&VERSION=1.3.0");
                    else
                        reader = new XmlTextReader(serverURL + "?SERVICE=WMS&REQUEST=GetCapabilities&VERSION=1.3.0");

                    reader.Namespaces = false;
                    reader.XmlResolver = resolver;
                    doc.Load(reader);
                    break;
                }
                catch (WebException wex)
                {
                    if (wex.Status == WebExceptionStatus.ProtocolError)
                    {
                        // Get HttpWebResponse to check the HTTP status code.
                        HttpWebResponse httpResponse = (HttpWebResponse)wex.Response;
                        if (httpResponse.StatusCode == HttpStatusCode.Unauthorized || 
                            httpResponse.StatusCode == HttpStatusCode.ProxyAuthenticationRequired)
                        {
                            CredentialsForm form = new CredentialsForm("Authentication required for " + serverURL, 
                                    resolver);
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                continue;
                            }
                            return;
                        }
                    }
                    MessageBox.Show("Unable to load WMS capabilities of the layer, " + wex.Message,
                        "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load WMS capabilities of the layer, " + ex.Message,
                        "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }

            // server version
            XmlAttribute att = doc.DocumentElement.Attributes["version"];
            wms_server_version = "1.1.1";
            if (att != null)
                wms_server_version = att.Value.Trim();
            if (!wms_server_version.StartsWith("1.0") && !wms_server_version.StartsWith("1.1"))
                wms_server_version = "1.1.1";

            // load supported image types
            comboBoxImageFormat.Items.Clear();
            foreach (XmlNode node in doc.SelectNodes("//Request/GetMap/Format"))
            {
                int index = comboBoxImageFormat.Items.Add(node.InnerText.Trim());
                if (comboBoxImageFormat.Items[index].ToString() == map.outputformat.mimetype || 
                    (comboBoxImageFormat.SelectedIndex < 0 && 
                         map.outputformat.mimetype.StartsWith(comboBoxImageFormat.Items[index].ToString()))||
                    comboBoxImageFormat.Items[index].ToString().ToLower().StartsWith("image/png"))
                    comboBoxImageFormat.SelectedIndex = index;
            }

            // load epsg values
            Hashtable epsg = new Hashtable();
            using (Stream s = File.OpenRead(MapUtils.GetPROJ_LIB() + "\\epsg"))
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    string line;
                    string projName = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("#") && line.Length > 2)
                            projName = line.Substring(2);
                        else if (line.StartsWith("<"))
                        {
                            string[] items = line.Split(new char[] { '<', '>' }, 
                                               StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length > 0)
                                epsg.Add("EPSG:" + items[0], projName);
                        }
                    }
                }
            }

            // load projections
            Dictionary<string, string> projections = new Dictionary<string, string>();
            string selectedProj = null;
            foreach (XmlNode srs in doc.SelectNodes("//CRS | //SRS"))
            {
                string[] srs2 = srs.InnerText.Split();
                foreach (string s in srs2)
                {
                    if (!projections.ContainsKey(s))
                    {
                        if (epsg.ContainsKey(s))
                            projections.Add(s, epsg[s].ToString());
                        else
                            projections.Add(s, s);

                        if (s.Contains("EPSG:4326"))
                            selectedProj = epsg[s].ToString();
                    }
                }
            }

            sortedProj = new List<KeyValuePair<string, string>>(projections);
            
            bs = new BindingSource();
            bs.DataSource = sortedProj;
            comboBoxProj.DataSource = bs;
            
            UpdateProjBinding();

            if (selectedProj != null)
                comboBoxProj.SelectedValue = selectedProj;

            if (comboBoxImageFormat.SelectedIndex < 0 && comboBoxImageFormat.Items.Count > 0)
                comboBoxImageFormat.SelectedIndex = 0;

            // ensure default imageindices are invalid 
            if (treeViewLayers.ImageList != null)
            {
                treeViewLayers.ImageIndex = treeViewLayers.ImageList.Images.Count;
                treeViewLayers.SelectedImageIndex = treeViewLayers.ImageList.Images.Count;
            }

            // set up the treeView
            treeViewLayers.BeginUpdate();
            treeViewLayers.Nodes.Clear();
            listViewLayers.Items.Clear();
            foreach (XmlNode node in doc.SelectNodes("//Capability/Layer"))
            {
                AddLayerNode(treeViewLayers.Nodes, node);
            }
            treeViewLayers.EndUpdate();
            treeViewLayers.ExpandAll();

            if (treeViewLayers.Nodes.Count > 0)
                treeViewLayers.Nodes[0].EnsureVisible();
        }

        /// <summary>
        /// Update the binding of the projection controls
        /// </summary>
        private void UpdateProjBinding()
        {
            KeyValuePair<string, string> current = new KeyValuePair<string, string>();
            if (bs != null && bs.Current != null)
                current = (KeyValuePair<string, string>)bs.Current;
            
            if (checkBoxProj.Checked)
            {    
                if (sortedProj != null)
                {
                    sortedProj.Sort(delegate(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
                    {
                        return a.Value.CompareTo(b.Value);
                    });
                }

                comboBoxProj.ValueMember = "Key";
                comboBoxProj.DisplayMember = "Value";
                if (bs != null)
                {
                    labelProjDesc.DataBindings.Clear();
                    labelProjDesc.DataBindings.Add("Text", bs, "Key");
                    if (current.Key != null)
                        comboBoxProj.SelectedValue = current.Key;
                }
            }
            else
            {
                if (sortedProj != null)
                {
                    sortedProj.Sort(delegate(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
                    {
                        return a.Key.CompareTo(b.Key);
                    });
                }

                comboBoxProj.ValueMember = "Value";
                comboBoxProj.DisplayMember = "Key";
                if (bs != null)
                {
                    labelProjDesc.DataBindings.Clear();
                    labelProjDesc.DataBindings.Add("Text", bs, "Value");
                    if (current.Value != null)
                        comboBoxProj.SelectedValue = current.Value;
                }
            }
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxProj control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxProj_CheckedChanged(object sender, EventArgs e)
        {
            UpdateProjBinding();   
        }

        /// <summary>
        /// Enum for the layer inheritance (see WMS specification)
        /// </summary>
        private enum LayerInheritConstants { No, Add, Replace};

        /// <summary>
        /// Get the property of the layer in the hierarchy
        /// </summary>
        /// <param name="node">The tree node that represents the layer</param>
        /// <param name="prop">The property name</param>
        /// <param name="inherit">The type of the inheritance (see WMS specification)</param>
        /// <returns></returns>
        private List<XmlNode> GetLayerProp(TreeNode node, string xpath, LayerInheritConstants inherit)
        {
            XmlNode layerDesc = (XmlNode)node.Tag;
            XmlNodeList propNodes = layerDesc.SelectNodes(xpath);
            List<XmlNode> retNodes = new List<XmlNode>();
            foreach (XmlNode n in propNodes)
                retNodes.Add(n);

            if (inherit != LayerInheritConstants.No && node.Parent != null)
            {
                // trying to find the property from the parent layer
                if (propNodes.Count == 0 || inherit == LayerInheritConstants.Add)
                    retNodes.AddRange(GetLayerProp(node.Parent, xpath, inherit));
            }
            
            return retNodes;
        }

        private void GetLayerAttributes(TreeNode node)
        {
            XmlNode minx = null;
            XmlNode miny = null;
            XmlNode maxx = null;
            XmlNode maxy = null;
            NumberFormatInfo ni = new NumberFormatInfo();
            ni.NumberDecimalSeparator = ".";

            // minscale - maxscale
            List<XmlNode> props = GetLayerProp(node, "MaxScaleDenominator", LayerInheritConstants.Replace);
            if (props.Count > 0)
                wms_maxscaledenom = Convert.ToDouble(props[0].InnerText, ni);

            props = GetLayerProp(node, "MinScaleDenominator", LayerInheritConstants.Replace);
            if (props.Count > 0)
                wms_minscaledenom = Convert.ToDouble(props[0].InnerText, ni);

            // Title
            props = GetLayerProp(node, "Title", LayerInheritConstants.No);
            if (props.Count > 0)
                wms_title = props[0].InnerText.Trim();

            // Name
            props = GetLayerProp(node, "Name", LayerInheritConstants.No);
            if (props.Count > 0)
            {
                if (wms_name == "")
                    wms_name = props[0].InnerText.Trim();
                else
                    wms_name += "," + props[0].InnerText.Trim();
            }

            // queryable
            XmlAttribute att = ((XmlNode)node.Tag).Attributes["queryable"];
            if (att != null && att.Value.Trim() == "1")
                wms_queryable = "1";

            // bbox
            props = GetLayerProp(node, "BoundingBox|EX_GeographicBoundingBox|LatLonBoundingBox", LayerInheritConstants.Replace);
            if (props.Count > 0)
            {
                if (props[0].Name == "BoundingBox" || props[0].Name == "LatLonBoundingBox")
                {
                    minx = props[0].Attributes["minx"];
                    miny = props[0].Attributes["miny"];
                    maxx = props[0].Attributes["maxx"];
                    maxy = props[0].Attributes["maxy"];
                }
                else
                {
                    minx = props[0].SelectSingleNode("westBoundLongitude");
                    miny = props[0].SelectSingleNode("southBoundLatitude");
                    maxx = props[0].SelectSingleNode("eastBoundLongitude");
                    maxy = props[0].SelectSingleNode("northBoundLatitude");
                }
            }

            if (minx != null && miny != null && maxx != null && maxy != null)
            {
                double fminx = Convert.ToDouble(minx.InnerText, ni);
                double fminy = Convert.ToDouble(miny.InnerText, ni);
                double fmaxx = Convert.ToDouble(maxx.InnerText, ni);
                double fmaxy = Convert.ToDouble(maxy.InnerText, ni);
                
                if (wms_bbox == null)
                    wms_bbox = new rectObj(fminx, fminy, fmaxx, fmaxy, 0);
                else
                {
                    if (wms_bbox.minx > fminx)
                        wms_bbox.minx = fminx;
                    if (wms_bbox.miny > fminy)
                        wms_bbox.miny = fminy;
                    if (wms_bbox.maxx < fmaxx)
                        wms_bbox.maxx = fmaxx;
                    if (wms_bbox.maxy < fmaxy)
                        wms_bbox.maxy = fmaxy;
                }
            }
        }

        /// <summary>
        /// Adding the selected layer to map
        /// </summary>
        void AddLayerToMap()
        {
            // trying to open the layer
            layerObj layer = new layerObj(map);
            layer.connection = textBoxServer.Text.Trim();
            layer.connectiontype = MS_CONNECTION_TYPE.MS_WMS;
            layer.type = MS_LAYER_TYPE.MS_LAYER_RASTER;
            layer.status = mapscript.MS_ON;

            // set up authentication
            NetworkCredential cred = (NetworkCredential)resolver.GetCredentials();
            if (cred != null)
            {
                layer.metadata.set("wms_auth_username", cred.UserName);
                layer.metadata.set("wms_auth_password", cred.Password);
                layer.metadata.set("wms_auth_type", "any");
            }
            WebProxy proxy = (WebProxy)resolver.Proxy;
            if (proxy != null)
            {
                layer.metadata.set("wms_proxy_host", proxy.Address.Host);
                layer.metadata.set("wms_proxy_port", proxy.Address.Port.ToString());
                layer.metadata.set("wms_proxy_auth_type", "any");
                layer.metadata.set("wms_proxy_type", resolver.ProxyType);
                cred = (NetworkCredential)resolver.GetProxyCredentials();
                if (cred != null)
                {
                    layer.metadata.set("wms_proxy_username", cred.UserName);
                    layer.metadata.set("wms_proxy_password", cred.Password);    
                }
            }

            // setting up the selected layer
            selected = new MapObjectHolder(layer, target);

            // set queryable flag
            if (wms_queryable == "1")
                layer.template = "query.html";

            // setting up the layer metadata section
            layer.metadata.set("wms_name", wms_name);
            layer.metadata.set("wms_format", comboBoxImageFormat.Text.Trim());
 
            layer.metadata.set("wms_server_version", wms_server_version);

            if (!colorPickerLayerColor.Value.IsEmpty)
            {
                colorObj color = new colorObj(colorPickerLayerColor.Value.R, 
                    colorPickerLayerColor.Value.G, colorPickerLayerColor.Value.B, 0);
                layer.metadata.set("wms_bgcolor", "0x" + color.toHex().Substring(1));
            }

            if (!checkBoxTransparent.Checked)
                layer.metadata.set("wms_transparent", "FALSE");

            // wms_style
            if (comboBoxStyle.Text != "")
                layer.metadata.set("wms_style", comboBoxStyle.Text);

            // title
            if (wms_title != null)
                layer.name = MapUtils.GetUniqueLayerName(map, wms_title, 0);
            else
                layer.name = MapUtils.GetUniqueLayerName(map, "Layer", 0);

            // scale denom
            if (wms_maxscaledenom > 0)
                layer.maxscaledenom = wms_maxscaledenom;
            if (wms_minscaledenom > 0)
                layer.maxscaledenom = wms_minscaledenom;

            // get bbox parameters
            if (wms_bbox != null && map.numlayers == 1)
            {
                // this is the first layer, set the extent of the map
                map.setExtent(wms_bbox.minx, wms_bbox.miny, wms_bbox.maxx, wms_bbox.maxy);
            }

            // setting up the selected projection
            KeyValuePair<string, string> current = (KeyValuePair<string, string>)bs.Current;
            string wms_srs = current.Key;

            // mapping not EPSG systems to the EPSG variants
            if (string.Compare(wms_srs, "CRS:84", true) == 0)
                wms_srs = "EPSG:4326";
            else if (string.Compare(wms_srs, "CRS:83", true) == 0)
                wms_srs = "EPSG:4369";
            else if (string.Compare(wms_srs, "CRS:27", true) == 0)
                wms_srs = "EPSG:4267";

            if (wms_srs.StartsWith("EPSG", StringComparison.InvariantCultureIgnoreCase))
                layer.metadata.set("wms_srs", wms_srs);
            layer.setProjection(wms_srs);
        }

        /// <summary>
        /// Validate the WMS settings before adding to the map
        /// </summary>
        /// <returns>The result of the validation</returns>
        private bool ValidateSettings()
        {
            if (bs == null || bs.Current == null)
            {
                MessageBox.Show("The projection of the layer has not been specified!",
                   "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (listViewLayers.Items.Count == 0)
            {
                MessageBox.Show("No WMS layers have been selected!",
                   "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            if (map.web.imagepath == null || !Directory.Exists(map.web.imagepath))
            {
                // must query for the image path
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Please select a folder to store the temporary image files!";
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    map.web.imagepath = dialog.SelectedPath;
                    return true;
                }
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Click event handler of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!ValidateSettings())
                return;
            
            try
            {
                // adding the selected layers
                if (checkBoxSingle.Checked)
                {
                    // add multiple layers as a single layer (merge attributes)
                    wms_bbox = null;
                    wms_name = "";
                    wms_title = null;
                    wms_minscaledenom = -1;
                    wms_maxscaledenom = -1;
                    wms_queryable = null;
                    foreach (ListViewItem item in listViewLayers.Items)
                        GetLayerAttributes((TreeNode)item.Tag);

                    AddLayerToMap();
                }
                else
                {
                    foreach (ListViewItem item in listViewLayers.Items)
                    {
                        // add as a separate layer
                        wms_bbox = null;
                        wms_name = "";
                        wms_title = null;
                        wms_minscaledenom = -1;
                        wms_maxscaledenom = -1;
                        wms_queryable = null;

                        GetLayerAttributes((TreeNode)item.Tag);
                        AddLayerToMap();
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable add layer, " + ex.Message,
                   "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        /// <summary>
        /// Click event handler of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click event handler of the buttonLoadLayers control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonLoadLayers_Click(object sender, EventArgs e)
        {
            LoadLayers();
            UpdateControls();
        }

        /// <summary>
        /// Click event handler of the buttonAdd control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewLayers.SelectedNode;
            ListViewItem item = new ListViewItem(node.Text);
            item.Tag = node; // must preserve the hierarchy (inheritance)
            XmlAttribute att = ((XmlNode)node.Tag).Attributes["queryable"];
            if (att != null && att.Value.Trim() == "1")
            {
                item.ImageIndex = 0;
            }
            listViewLayers.Items.Add(item);
            UpdateControls();
        }

        /// <summary>
        /// Click event handler of the buttonRemove control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            listViewLayers.Items.Remove(listViewLayers.SelectedItems[0]);
            UpdateControls();
        }

        /// <summary>
        /// Click event handler of the buttonUp control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonUp_Click(object sender, EventArgs e)
        {
            ListViewItem selected = listViewLayers.SelectedItems[0];
            int selectedIndex = selected.Index;
            if (selectedIndex > 0)
            {
                listViewLayers.Items.Remove(selected);
                listViewLayers.Items.Insert(selectedIndex - 1, selected);
                listViewLayers.Focus();
            }
            UpdateControls();
        }

        /// <summary>
        /// Click event handler of the buttonDown control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonDown_Click(object sender, EventArgs e)
        {
            ListViewItem selected = listViewLayers.SelectedItems[0];
            int selectedIndex = selected.Index;
            if (selectedIndex < listViewLayers.Items.Count - 1)
            {
                listViewLayers.Items.Remove(selected);
                listViewLayers.Items.Insert(selectedIndex + 1, selected);
                listViewLayers.Focus();
            }
            UpdateControls();
        }

        /// <summary>
        /// Updating the layer specific parameters according to the selected item
        /// </summary>
        /// <param name="style">The XML description of the selected item</param>
        private void UpdateLayerParams(XmlNode layer)
        {
            XmlNode n;
            // update the style
            comboBoxStyle.Items.Clear();
            foreach (XmlNode node in layer.SelectNodes("Style"))
            {
                n = node.SelectSingleNode("Name");
                if (n != null)
                    comboBoxStyle.Items.Add(n.InnerText);
            }
            // update the layer description
            n = layer.SelectSingleNode("Abstract");
            if (n != null)
                textBoxDesc.Text = n.InnerText;
            else
                textBoxDesc.Text = "";
        }

        /// <summary>
        /// AfterSelect event handler of the treeViewLayers control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeViewLayers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewLayers.SelectedNode != null)
                UpdateLayerParams((XmlNode)treeViewLayers.SelectedNode.Tag);
            UpdateControls();
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the listViewLayers control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void listViewLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewLayers.SelectedItems.Count > 0)
                UpdateLayerParams((XmlNode)((TreeNode)listViewLayers.SelectedItems[0].Tag).Tag);
            UpdateControls();
        }

        /// <summary>
        /// Click event handler of the buttonDetails control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonDetails_Click(object sender, EventArgs e)
        {
            if (doc != null)
            {
                AddWMSLayerDetailsForm form = new AddWMSLayerDetailsForm(doc, serverURL, wms_server_version);
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// KeyDown event handler of the AddWMSLayerForm control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void AddWMSLayerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Click event handler of the buttonProxy control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonConnection_Click(object sender, EventArgs e)
        {
            CredentialsForm form = new CredentialsForm("Specify Connection Settings", resolver);
            form.ShowDialog(this);
        }
    }
}