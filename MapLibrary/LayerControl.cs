using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OSGeo.MapServer;
using OSGeo.OGR;
using System.Runtime.InteropServices;
using OSGeo.GDAL;
using OSGeo.OSR;
using System.Data.SqlClient;

namespace DMS.MapLibrary
{
    /// <summary>
    /// User Control to edit the layerObj, classObj, and styleObj collections.
    /// </summary>
    public partial class LayerControl : UserControl, IMapControl
    {
        private MapObjectHolder target;
        private mapObj map;
        private bool showRootObject;
        private bool showClasses;
        private bool showStyles;
        private bool showLabels;
        // promotes the layer to be selected
        private MapObjectHolder selected;
        private bool enableItemSelect;
        TreeNode topNode;
        private Size legendIconSize;
        private Size legendDrawingSize;
        private Padding legendIconPadding;
        private bool enableLabelEdit;

        /// <summary>
        /// The signature of the ItemSelect event handler. Raised when a new item is selected in the layer tree.
        /// </summary>
        public delegate void ItemSelectEventHandler(object sender, MapObjectHolder target);

        /// <summary>
        /// The ItemSelect event handler. Raised when a new item is selected in the layer tree.
        /// </summary>
        public event ItemSelectEventHandler ItemSelect;

        /// <summary>
        /// The signature of the GoToLayerText event handler. Raised when the Go To Layer menu is selected.
        /// </summary>
        public delegate void GoToLayerTextEventHandler(object sender, layerObj layer, int classindex);

        /// <summary>
        /// The GoToLayerText event handler. Raised when the Go To Layer menu is selected.
        /// </summary>
        public event GoToLayerTextEventHandler GoToLayerText;

        /// <summary>
        /// Signs that the layer control is about to refresh.
        /// </summary>
        public event EventHandler BeforeRefresh;

        /// <summary>
        /// Signs that the layer control has been refreshed.
        /// </summary>
        public event EventHandler AfterRefresh;

        /// <summary>
        /// Constructs the LayerControl object.
        /// </summary>
        public LayerControl()
        {
            InitializeComponent();
            UpdateToolbar();
            showRootObject = true;
            showClasses = true;
            showStyles = false;
            showLabels = false;
            enableItemSelect = true;
            legendIconSize = new Size(30, 20);
            legendDrawingSize = new Size(20, 10);
            legendIconPadding = new Padding(5);
            enableLabelEdit = false;
        }

        /// <summary>
        /// Signals that the initial extent have been set by adding the first layer
        /// </summary>
        public event EventHandler InitialExtentSet;

        /// <summary>
        /// Returns the reference of the active tree
        /// </summary>
        private TreeView CurrentTree
        {
            get
            {
                if (treeView.Visible)
                    return treeView;
                else
                    return treeView2;
            }
        }

        /// <summary>
        /// Adding a new labelObj to the corresponding classObj.
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection of the parent object</param>
        /// <param name="labelHolder">Wrapper class containing the labelObj and the parent object</param>
        /// <param name="imageList">The image list where the label image should be stored.</param>
        /// <param name="index">The current index of the label object.</param>
        private void AddLabelNode(TreeNodeCollection nodes, MapObjectHolder labelHolder, ImageList imageList, int index)
        {
            layerObj layer = labelHolder.GetParent().GetParent();
            classObj layerclass = labelHolder.GetParent();
            labelObj classLabel = labelHolder;

            classObj labelclass = new classObj(null);
            labelclass.name = MapUtils.GetClassName(layer);
            labelclass.addLabel(classLabel);
            // creating the treeicons
            using (classObj def_class = new classObj(null)) // for drawing legend images
            {
                using (imageObj image2 = def_class.createLegendIcon(
                                map, layer, legendIconSize.Width, legendIconSize.Height))
                {
                 
                    MS_LAYER_TYPE layertype = layer.type;
                    layer.type = MS_LAYER_TYPE.MS_LAYER_ANNOTATION;
                    labelclass.drawLegendIcon2(map, layer, 
                        legendDrawingSize.Width, legendDrawingSize.Height, image2, 
                        LegendIconPadding.Left, LegendIconPadding.Top);
                    layer.type = layertype;
                    byte[] img = image2.getBytes();
                    using (MemoryStream ms = new MemoryStream(img))
                    {
                        imageList.Images.Add(Image.FromStream(ms));
                    }

                    TreeNode labelNode = new TreeNode("Label (" + index + ")",
                        imageList.Images.Count - 1, imageList.Images.Count - 1);
                    labelNode.Tag = labelHolder;
                    labelNode.Checked = true;
                    nodes.Add(labelNode);
                }
            }
        }

        /// <summary>
        /// Adding a new styleObj to the corresponding classObj.
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection of the parent object</param>
        /// <param name="styleHolder">Wrapper class containing the styleObj and the parent object</param>
        /// /// <param name="showRoot">A flag indicating whether the root object should be displayed or not.</param>
        private void AddStyleNode(TreeNodeCollection nodes, MapObjectHolder styleHolder, ImageList imageList)
        {
            layerObj layer = styleHolder.GetParent().GetParent();
            classObj layerclass = styleHolder.GetParent();
            styleObj classStyle = styleHolder;

            classObj styleclass = new classObj(null);
            styleclass.name = MapUtils.GetClassName(layer);
            styleclass.insertStyle(classStyle, -1);
            // creating the treeicons
            using (classObj def_class = new classObj(null)) // for drawing legend images
            {
                using (imageObj image2 = def_class.createLegendIcon(
                                 map, layer, legendIconSize.Width, legendIconSize.Height))
                {
                    styleclass.drawLegendIcon2(map, layer, 
                        legendDrawingSize.Width, legendDrawingSize.Height, image2, 
                        LegendIconPadding.Left, LegendIconPadding.Top);
                    byte[] img = image2.getBytes();
                    using (MemoryStream ms = new MemoryStream(img))
                    {
                        imageList.Images.Add(Image.FromStream(ms));
                    }

                    TreeNode styleNode = new TreeNode("Style (" + nodes.Count + ")",
                        imageList.Images.Count - 1, imageList.Images.Count - 1);
                    styleNode.Tag = styleHolder;
                    styleNode.Checked = true;
                    nodes.Add(styleNode);
                }
            }
        }

        /// <summary>
        /// Adding a new classObj to the corresponding layerObj.
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection of the parent object</param>
        /// <param name="classHolder">Wrapper class containing the classObj and the parent object</param>
        /// /// <param name="showRoot">A flag indicating whether the root object should be displayed or not.</param>
        private void AddClassNode(TreeNodeCollection nodes, MapObjectHolder classHolder, ImageList imageList)
        {
            layerObj layer = classHolder.GetParent();
            classObj layerclass = classHolder;
            
            // creating the treeicons
            using (classObj def_class = new classObj(null)) // for drawing legend images
            {
                using (imageObj image = def_class.createLegendIcon(
                                map, layer, legendIconSize.Width, legendIconSize.Height))
                {
                    // drawing the class icons
                    layerclass.drawLegendIcon2(map, layer,
                        legendDrawingSize.Width, legendDrawingSize.Height, image, 
                        LegendIconPadding.Left, LegendIconPadding.Top);
                    byte[] img = image.getBytes();
                    using (MemoryStream ms = new MemoryStream(img))
                    {
                        imageList.Images.Add(Image.FromStream(ms));
                    }

                    if (!showClasses || string.Compare(layer.styleitem, "AUTO", true) == 0)
                        return;

                    TreeNode classNode = null;
                    if (layer.numclasses > 1 || 
                        (layer.numclasses == 1 && target.GetType() == typeof(layerObj) && showRootObject == false)) // classes should always be shown if the layer is not shown (#7118)
                    {
                        classNode = new TreeNode(layerclass.name, imageList.Images.Count - 1,
                                imageList.Images.Count - 1);
                        classNode.Tag = classHolder;
                        classNode.Checked = (layerclass.status != mapscript.MS_OFF);
                        nodes.Add(classNode);
                        nodes = classNode.Nodes;
                    }

                    // drawing the style icons
                    if (showStyles)
                    {
                        for (int k = 0; k < layerclass.numstyles; k++)
                        {
                            styleObj classStyle = layerclass.getStyle(k);
                            
                            if (layerclass.numstyles > 1)
                            {
                                AddStyleNode(nodes, new MapObjectHolder(classStyle, classHolder), imageList);    
                            }
                        }
                    }

                    // drawing the labels icons
                    if (showLabels)
                    {
                        for (int l = 0; l < layerclass.numlabels; l++)
                        {
                            labelObj classLabel = layerclass.getLabel(l);

                            if (layerclass.numlabels > 1)
                            {
                                AddLabelNode(nodes, new MapObjectHolder(classLabel, classHolder), imageList, l);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adding a new layerObj to the mapObj.
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection of the parent object.</param>
        /// <param name="layerHolder">Wrapper class containing the layerObj and the parent object.</param>
        /// <param name="showRoot">A flag indicating whether the root object should be displayed or not.</param>
        private void AddLayerNode(TreeNodeCollection nodes, MapObjectHolder layerHolder, bool showRoot, ImageList imageList)
        {
            layerObj layer = layerHolder;

            // adding the layer based on the icon of the first class
            if (showRoot)
            {
                TreeNode layerNode = new TreeNode(layer.name, imageList.Images.Count,
                    imageList.Images.Count);
                layerNode.Checked = (layer.status != mapscript.MS_OFF);
                layerNode.Tag = layerHolder; // wire up the layer into the node
                if (MapUtils.HasMetadata(layer, "link"))
                {
                    layerNode.NodeFont = new Font(treeView.Font, FontStyle.Underline | FontStyle.Italic);
                    layerNode.ForeColor = Color.Blue;
                }
                nodes.Insert(0, layerNode);
                nodes = layerNode.Nodes;

                if (layer.type == MS_LAYER_TYPE.MS_LAYER_RASTER)
                    imageList.Images.Add(global::MapLibrary.Properties.Resources.raster);
            }

            for (int j = 0; j < layer.numclasses; j++)
            {
                classObj layerclass = layer.getClass(j);
                AddClassNode(nodes, new MapObjectHolder(layerclass, layerHolder), imageList);

                if (!showClasses || string.Compare(layer.styleitem, "AUTO", true) == 0)
                    break;
            }
        }

        /// <summary>
        /// Adding a new mapObj to the treeView.
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection of the treeView object.</param>
        /// <param name="mapHolder">Wrapper class containing the mapObj and the parent object.</param>
        /// <param name="showRoot">A flag indicating whether the root object should be displayed or not.</param> 
        /// <param name="imageList">The imagelist of the treeview.</param>
        private void AddMapNode(TreeNodeCollection nodes, MapObjectHolder mapHolder, bool showRoot, ImageList imageList)
        {
            TreeNode current = null;
            using (intarray ar = map.getLayersDrawingOrder())
            {
                if (showRoot)
                {
                    if (IsStyleLibraryControl)
                    {
                        imageList.Images.Add(global::MapLibrary.Properties.Resources.Map_Legend_Big);
                    }
                    else
                    {
                        imageList.Images.Add(global::MapLibrary.Properties.Resources.Map_Legend);
                    }
                    current = new TreeNode(map.name, imageList.Images.Count - 1, imageList.Images.Count - 1);
                    current.Checked = true;
                    current.Tag = target; // wire up the map into the node
                    nodes.Add(current);                    
                    nodes = current.Nodes;
                }
                for (int i = 0; i < map.numlayers; i++)
                {
                    layerObj layer = map.getLayer(ar.getitem(i));
                    if (layer.name == "__embed__scalebar" || layer.name == "__embed__legend")
                        continue;

                    AddLayerNode(nodes, new MapObjectHolder(layer, mapHolder), true, imageList);
                }
            }
        }

        /// <summary>
        /// Updates the expanded and the selected state of the nodes in the collection according to the node type
        /// </summary>
        /// <param name="source">The source (sample) treeView</param>
        /// <param name="destination">The destination treeView which should be updated</param>
        /// <param name="destnodes">The node collection which should be updated</param>
        private void UpdateNodeState(TreeView source, TreeView destination, TreeNodeCollection destnodes)
        {
            foreach (TreeNode node in destnodes)
            {
                TreeNode n = FindNode(source.Nodes, node);
                if (n == null)
                    n = FindNodeByName(source.Nodes, node);

                if (n != null)
                {
                    if (n.IsExpanded || n.Parent == null)
                        node.Expand();
                    if (n.IsSelected)
                        destination.SelectedNode = node;
                    if (n.Equals(source.TopNode))
                        topNode = node;  // preserve the top node
                }
                else
                {
                    MapObjectHolder h = (MapObjectHolder)node.Tag;
                    if (h.GetType() == typeof(mapObj))
                    {   
                        // expand the map and layer nodes by default
                        node.Expand();
                    }
                }

                // pre select the desired node
                if (selected != null)
                {
                    if (selected.Equals(node.Tag))
                    {
                        destination.SelectedNode = node;
                        source.SelectedNode = null; // remove the previous selection
                        selected = null;
                    }
                }

                UpdateNodeState(source, destination, node.Nodes);
            }
        }

        /// <summary>
        /// Finds a node in the specified collection which equals to the source node
        /// </summary>
        /// <param name="nodes">The collection to be searched</param>
        /// <param name="source">The source node</param>
        /// <returns>The node of the collection which equals to the source node</returns>
        private TreeNode FindNode(TreeNodeCollection nodes, TreeNode source)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null && node.Tag.Equals(source.Tag))
                    return node;
                
                TreeNode node2 = FindNode(node.Nodes, source);
                if (node2 != null)
                    return node2;
            }
            return null;
        }

        /// <summary>
        /// Finds a node in the specified collection with the same name and node type
        /// </summary>
        /// <param name="nodes">The collection to be searched</param>
        /// <param name="source">The source node</param>
        /// <returns>The node of the collection which equals to the source node</returns>
        private TreeNode FindNodeByName(TreeNodeCollection nodes, TreeNode source)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null && ((MapObjectHolder)node.Tag).GetType() == ((MapObjectHolder)source.Tag).GetType())
                {           
                    // trying to match the nodes by name
                    if (((MapObjectHolder)node.Tag).GetType() == typeof(mapObj))
                    {
                        if (node.Text == source.Text)
                            return node;
                    }
                    else if (((MapObjectHolder)node.Tag).GetType() == typeof(layerObj))
                    {
                        if (node.Text == source.Text)
                            return node;
                    }
                    else if (((MapObjectHolder)node.Tag).GetType() == typeof(classObj))
                    {
                        if (node.FullPath == source.FullPath)
                            return node;
                    }
                    else if (((MapObjectHolder)node.Tag).GetType() == typeof(styleObj))
                    {
                        if (node.FullPath == source.FullPath)
                            return node;
                    }
                }

                TreeNode node2 = FindNodeByName(node.Nodes, source);
                if (node2 != null)
                    return node2;
            }
            return null;
        }


        #region IMapControl Members

        /// <summary>
        /// Refresh the controls according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            if (map != null)
            {
                if (BeforeRefresh != null)
                    BeforeRefresh(this, null);

                try
                {
                    // selecting the backup tree to construct the new contents 
                    TreeView tree;
                    ImageList iList;
                    if (treeView.Visible)
                    {
                        tree = treeView2;
                        iList = imageList2;
                    }
                    else
                    {
                        tree = treeView;
                        iList = imageList;
                    }

                    tree.Nodes.Clear();
                    // setting up the icon background colors createLegendIcon
                    // will take over the legend imagecolor setting from the map object 
                    int red = map.legend.imagecolor.red;
                    int green = map.legend.imagecolor.green;
                    int blue = map.legend.imagecolor.blue;
                    map.legend.imagecolor.red = this.BackColor.R;
                    map.legend.imagecolor.green = this.BackColor.G;
                    map.legend.imagecolor.blue = this.BackColor.B;
                    tree.BackColor = this.BackColor;

                    using (outputFormatObj format = map.outputformat)
                    {
                        string imageType = null;
                        if ((format.renderer != mapscript.MS_RENDER_WITH_AGG)
                            || string.Compare(format.mimetype.Trim(), "image/vnd.wap.wbmp", true) == 0
                            || string.Compare(format.mimetype.Trim(), "image/tiff", true) == 0
                            || string.Compare(format.mimetype.Trim(), "image/jpeg", true) == 0)
                        {
                            // falling back to the png type in case of the esoteric or bad looking types
                            imageType = map.imagetype;
                            map.selectOutputFormat("png24");
                        }

                        iList.Images.Clear();
                        iList.ImageSize = new Size(legendIconSize.Width, legendIconSize.Height);

                        try
                        {
                            if (target.GetType() == typeof(mapObj))
                            {
                                AddMapNode(tree.Nodes, target, showRootObject, iList);
                            }
                            else if (target.GetType() == typeof(layerObj))
                            {
                                AddLayerNode(tree.Nodes, target, showRootObject, iList);
                            }
                            else if (target.GetType() == typeof(classObj))
                            {
                                AddClassNode(tree.Nodes, target, iList);
                            }
                        }
                        finally
                        {
                            // switch back to the original type
                            if (imageType != null)
                                map.selectOutputFormat(imageType);
                            // restoring the original legend backgroundcolor
                            map.legend.imagecolor.red = red;
                            map.legend.imagecolor.green = green;
                            map.legend.imagecolor.blue = blue;
                        }

                        tree.ImageList = iList;

                        //treeView.ExpandAll();
                        // removing the checkboxes from the style nodes
                        // display the pre-rendered treeview
                        if (treeView.Visible)
                        {
                            UpdateNodeState(treeView, treeView2, treeView2.Nodes);
                            treeView.Visible = false;
                            treeView2.Visible = true;
                        }
                        else
                        {
                            UpdateNodeState(treeView2, treeView, treeView.Nodes);
                            treeView.Visible = true;
                            treeView2.Visible = false;
                        }

                        // set up the vertical scroll
                        if (topNode != null)
                            CurrentTree.TopNode = topNode;

                        RemoveStyleChecks(tree.Nodes);
                        UpdateToolbar();
                    }
                }
                finally
                {
                    if (AfterRefresh != null)
                        AfterRefresh(this, null);
                }
            }
            else
                CurrentTree.Nodes.Clear();
        }

        /// <summary>
        /// Gets and sets the target object of the editor.
        /// </summary>
        public MapObjectHolder Target
        {
            get
            {
                return target;
            }
            set
            {
                if (value == null)
                {
                    target = null;
                    map = null;
                }
                else if (value.GetType() == typeof(mapObj))
                {
                    map = value;
                    target = value;
                }
                else if (value.GetType() == typeof(layerObj))
                {
                    map = value.GetParent();
                    target = value;
                }
                else if (value.GetType() == typeof(classObj))
                {
                    map = value.GetParent().GetParent();
                    target = value;
                }
                else
                    throw new Exception("Invalid target type: " + value.GetType());
                
                this.RefreshView();
            }
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties;

        #endregion

        /// <summary>
        /// Gets and sets the visibility of the checkboxes.
        /// </summary>
        public bool ShowCheckBoxes
        {
            get { return treeView.CheckBoxes; }
            set
            {
                if (treeView.CheckBoxes != value)
                {
                    treeView.CheckBoxes = value;
                }

                if (treeView2.CheckBoxes != value)
                {
                    treeView2.CheckBoxes = value;
                }
                RefreshView();
            }
        }

        /// <summary>
        /// Gets and sets the visibility of the root node.
        /// </summary>
        public bool ShowRootObject
        {
            get { return showRootObject; }
            set 
            {
                if (showRootObject != value)
                {
                    showRootObject = value;
                    RefreshView();
                }
            }
        }

        /// <summary>
        /// Gets and sets the visibility of the class objects.
        /// </summary>
        public bool ShowClasses
        {
            get { return showClasses; }
            set
            {
                if (showClasses != value)
                {
                    showClasses = value;
                    RefreshView();
                }
            }
        }

        /// <summary>
        /// Gets and sets the visibility of the style objects.
        /// </summary>
        public bool ShowStyles
        {
            get { return showStyles; }
            set
            {
                if (showStyles != value)
                {
                    showStyles = value;
                    RefreshView();
                }
            }
        }

        /// <summary>
        /// Gets and sets the visibility of the style objects.
        /// </summary>
        public bool ShowLabels
        {
            get { return showLabels; }
            set
            {
                if (showLabels != value)
                {
                    showLabels = value;
                    RefreshView();
                }
            }
        }

        /// <summary>
        /// Gets and sets the visibility of the toolbar object.
        /// </summary>
        public bool ShowToolbar
        {
            get { return toolStrip.Visible; }
            set
            {
                if (toolStrip.Visible != value)
                {
                    toolStrip.Visible = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the size of the legend icons.
        /// </summary>
        public Size LegendIconSize
        {
            get { return legendIconSize; }
            set
            {
                if (legendIconSize.Width <= legendIconPadding.Left + legendIconPadding.Right ||
                    legendIconSize.Height <= legendIconPadding.Top + legendIconPadding.Bottom)
                    throw new ArgumentException("Legend icon size should exceed the specified padding.");
                legendIconSize = value;
                legendDrawingSize.Width = legendIconSize.Width -
                    legendIconPadding.Left - legendIconPadding.Right;
                legendDrawingSize.Height = legendIconSize.Height -
                    legendIconPadding.Top - legendIconPadding.Bottom;
            }
        }

        /// <summary>
        /// Gets and sets the padding around the legend icons.
        /// </summary>
        public Padding LegendIconPadding
        {
            get { return legendIconPadding; }
            set
            {
                if (legendIconSize.Width <= legendIconPadding.Left + legendIconPadding.Right ||
                    legendIconSize.Height <= legendIconPadding.Top + legendIconPadding.Bottom)
                    throw new ArgumentException("Legend icon size should exceed the specified padding.");
                legendIconPadding = value;
                legendDrawingSize.Width = legendIconSize.Width -
                    legendIconPadding.Left - legendIconPadding.Right;
                legendDrawingSize.Height = legendIconSize.Height -
                    legendIconPadding.Top - legendIconPadding.Bottom;
            }
        }

        /// <summary>
        /// Update the Visible and Enabled properties of the controls.
        /// </summary>
        private void UpdateToolbar()
        {
            // updating the toolbar and contextmenu state            
            // setting the visibitity to false by default
            moveItemDownToolStripMenuItem.Visible =
            moveItemUpToolStripMenuItem.Visible = 
            deleteItemToolStripMenuItem.Visible = 
            propertiesToolStripMenuItem.Visible =
            addThemeToolStripMenuItem.Visible =
            toolStripMenuItemSplitItems.Visible =
            toolStripMenuItemSplitProp.Visible =
            toolStripMenuItemSplitTheme.Visible = 
            addLayerToolStripMenuItem.Visible =
            addClassToolStripMenuItem.Visible =
            addStyleToolStripMenuItem.Visible =
            addLabelToolStripMenuItem.Visible =
            addNewLayerToolStripMenuItem.Visible =
            addVectorLayerToolStripMenuItem.Visible =
            addMapFileToolStripMenuItem.Visible =
            addMapFileToolStripMenuItem1.Visible =
            addVectorLayerFromFileToolStripMenuItem.Visible =
            addTileIndexLayerToolStripMenuItem.Visible =
            addTileIndexLayerToolStripMenuItem1.Visible =
            addGraticuleLayerToolStripMenuItem.Visible =
            addGraticuleLayerToolStripMenuItem1.Visible =
            addRasterLayerToolStripMenuItem.Visible =
            addRasterLayerFromFileToolStripMenuItem.Visible =
            addWMSLayerToolStripMenuItem.Visible =
            addWMSLayerToolStripMenuItem1.Visible =
            addMSSQLSpatialLayerToolStripMenuItem.Visible =
            addMSSQLSpatialLayerToolStripMenuItem1.Visible =
            addNewClassToolStripMenuItem.Visible =
            addNewStyleToolStripMenuItem.Visible =
            addNewLabelToolStripMenuItem.Visible =
            zoomToLayerExtentToolStripMenuItem.Visible = 
            toolStripSplitButtonNew.Enabled =
            toolStripButtonUp.Enabled = 
            moveItemUpToolStripMenuItem.Enabled =
            toolStripButtonDown.Enabled =
            toolStripButtonDelete.Enabled = 
            autoStyleToolStripMenuItem.Visible =
            goToLayerTextToolStripMenuItem.Visible =
            goToClassTextToolStripMenuItem.Visible =
            renameToolStripMenuItem.Visible = false;
         
            if (target!= null)
            {
                toolStripSplitButtonNew.Enabled = true;
                if (target.GetType() == typeof(mapObj))
                {
                    addVectorLayerToolStripMenuItem.Visible = true;
                    addTileIndexLayerToolStripMenuItem.Visible = true;
                    addTileIndexLayerToolStripMenuItem1.Visible = true;
                    addGraticuleLayerToolStripMenuItem.Visible = true;
                    addGraticuleLayerToolStripMenuItem1.Visible = true;
                    addRasterLayerToolStripMenuItem.Visible = true;
                    addNewLayerToolStripMenuItem.Visible = true;
                    addLayerToolStripMenuItem.Visible = true;
                    addMapFileToolStripMenuItem.Visible = true;
                    addMapFileToolStripMenuItem1.Visible = true;
                    addWMSLayerToolStripMenuItem.Visible = true;
                    addWMSLayerToolStripMenuItem1.Visible = true;
                    if (File.Exists(Application.StartupPath + "\\msplugin_mssql2008.dll"))
                    {
                        addMSSQLSpatialLayerToolStripMenuItem.Visible = true;
                        addMSSQLSpatialLayerToolStripMenuItem1.Visible = true;
                    }
                }
            }

            if (CurrentTree.SelectedNode != null)
            {
                propertiesToolStripMenuItem.Visible = true;
                toolStripMenuItemSplitProp.Visible = true;

                toolStripButtonUp.Enabled = moveItemUpToolStripMenuItem.Enabled =
                toolStripButtonDown.Enabled = moveItemDownToolStripMenuItem.Enabled = true;
                
                TreeNode treeNode = CurrentTree.SelectedNode;
                MapObjectHolder node = (MapObjectHolder)treeNode.Tag;
               
                bool isInGroup = false;
                if (treeNode.Parent != null && treeNode.Parent.Tag != null)
                {
                    MapObjectHolder pnode = (MapObjectHolder)treeNode.Parent.Tag;
                    if (pnode.GetType() == typeof(layerObj) && node.GetType() == typeof(layerObj))
                        isInGroup = true;
                }

                if (treeNode.PrevNode == null && !isInGroup)
                    toolStripButtonUp.Enabled = moveItemUpToolStripMenuItem.Enabled = false;

                if (treeNode.NextNode == null && !isInGroup)
                    toolStripButtonDown.Enabled = moveItemDownToolStripMenuItem.Enabled = false;


                if (node.GetType() == typeof(mapObj))
                {
                    addNewLayerToolStripMenuItem.Visible = true;
                    addMapFileToolStripMenuItem.Visible = true;
                    addMapFileToolStripMenuItem1.Visible = true;
                    addVectorLayerFromFileToolStripMenuItem.Visible = true;
                    addTileIndexLayerToolStripMenuItem.Visible = true;
                    addTileIndexLayerToolStripMenuItem1.Visible = true;
                    addGraticuleLayerToolStripMenuItem.Visible = true;
                    addGraticuleLayerToolStripMenuItem1.Visible = true;
                    addRasterLayerFromFileToolStripMenuItem.Visible = true;
                    addLayerToolStripMenuItem.Visible = true;
                    renameToolStripMenuItem.Visible = true;     
                }
                if (node.GetType() == typeof(layerObj))
                {
                    layerObj layer = node;

                    toolStripMenuItemSplitItems.Visible = true;
                    deleteItemToolStripMenuItem.Visible = true;
                    toolStripButtonDelete.Enabled = true;
                    deleteItemToolStripMenuItem.Text = "Remove Layer";
                    moveItemDownToolStripMenuItem.Visible = true;
                    moveItemDownToolStripMenuItem.Text = "Move Layer Down";
                    moveItemUpToolStripMenuItem.Visible = true;
                    moveItemUpToolStripMenuItem.Text = "Move Layer Up";
                    addClassToolStripMenuItem.Visible = true;
                    addNewClassToolStripMenuItem.Visible = true;
                    zoomToLayerExtentToolStripMenuItem.Visible = true;
                    goToLayerTextToolStripMenuItem.Visible = true;
                    renameToolStripMenuItem.Visible = true;

                    if (layer.numclasses <= 1)
                    {
                        // allow to add style if the class not expanded
                        addStyleToolStripMenuItem.Visible = true;
                        addNewStyleToolStripMenuItem.Visible = true;
                        if (layer.type != MS_LAYER_TYPE.MS_LAYER_ANNOTATION)
                        {
                            addLabelToolStripMenuItem.Visible = true;
                            addNewLabelToolStripMenuItem.Visible = true;
                        }
                    }

                    if (layer.type == MS_LAYER_TYPE.MS_LAYER_RASTER)
                    {
                        addClassToolStripMenuItem.Visible = false;
                        addNewClassToolStripMenuItem.Visible = false;
                        addStyleToolStripMenuItem.Visible = false;
                        addNewStyleToolStripMenuItem.Visible = false;
                        addLabelToolStripMenuItem.Visible = false;
                        addNewLabelToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        addClassToolStripMenuItem.Visible = true;
                        addNewClassToolStripMenuItem.Visible = true;
                        addStyleToolStripMenuItem.Visible = true;
                        addNewStyleToolStripMenuItem.Visible = true;
                        if (layer.type != MS_LAYER_TYPE.MS_LAYER_ANNOTATION)
                        {
                            addLabelToolStripMenuItem.Visible = true;
                            addNewLabelToolStripMenuItem.Visible = true;
                        }
                        addThemeToolStripMenuItem.Visible = true;
                        toolStripMenuItemSplitTheme.Visible = true;
                        autoStyleToolStripMenuItem.Visible = true;

                        if (layer.styleitem != null && layer.styleitem.ToUpper() == "AUTO")
                        {
                            addClassToolStripMenuItem.Visible = false;
                            addNewClassToolStripMenuItem.Visible = false;
                            addStyleToolStripMenuItem.Visible = false;
                            addNewStyleToolStripMenuItem.Visible = false;
                            addLabelToolStripMenuItem.Visible = false;
                            addNewLabelToolStripMenuItem.Visible = false;
                            autoStyleToolStripMenuItem.Checked = true;
                        }
                        else
                            autoStyleToolStripMenuItem.Checked = false;
                    }
                }
                else if (node.GetType() == typeof(classObj))
                {
                    classObj classobj = node;
                    layerObj layer = classobj.layer;
                    toolStripMenuItemSplitItems.Visible = true;
                    deleteItemToolStripMenuItem.Visible = true;
                    toolStripButtonDelete.Enabled = true;
                    deleteItemToolStripMenuItem.Text = "Remove Class";
                    moveItemDownToolStripMenuItem.Visible = true;
                    moveItemDownToolStripMenuItem.Text = "Move Class Down";
                    moveItemUpToolStripMenuItem.Visible = true;
                    moveItemUpToolStripMenuItem.Text = "Move Class Up";
                    addStyleToolStripMenuItem.Visible = true;
                    addNewStyleToolStripMenuItem.Visible = true;
                    if (layer != null && layer.type != MS_LAYER_TYPE.MS_LAYER_ANNOTATION)
                    {
                        addLabelToolStripMenuItem.Visible = true;
                        addNewLabelToolStripMenuItem.Visible = true;
                    }
                    
                    renameToolStripMenuItem.Visible = true;
                    goToClassTextToolStripMenuItem.Visible = true;
                }
                else if (node.GetType() == typeof(styleObj))
                {
                    toolStripMenuItemSplitItems.Visible = true;
                    deleteItemToolStripMenuItem.Visible = true;
                    toolStripButtonDelete.Enabled = true;
                    deleteItemToolStripMenuItem.Text = "Remove Style";
                    moveItemDownToolStripMenuItem.Visible = true;
                    moveItemDownToolStripMenuItem.Text = "Move Style Down";
                    moveItemUpToolStripMenuItem.Visible = true;
                    moveItemUpToolStripMenuItem.Text = "Move Style Up";
                    if (node.GetParent() != null && node.GetParent().GetType() == typeof(classObj))
                    {
                        classObj parentclass = node.GetParent();
                        if (treeNode.Index == parentclass.numstyles - 1)
                            toolStripButtonDown.Enabled = moveItemDownToolStripMenuItem.Enabled = false;
                    }
                }
                else if (node.GetType() == typeof(labelObj))
                {
                    toolStripMenuItemSplitItems.Visible = true;
                    deleteItemToolStripMenuItem.Visible = true;
                    toolStripButtonDelete.Enabled = true;
                    deleteItemToolStripMenuItem.Text = "Remove Label";
                    moveItemDownToolStripMenuItem.Visible = true;
                    moveItemDownToolStripMenuItem.Text = "Move Label Down";
                    moveItemUpToolStripMenuItem.Visible = true;
                    moveItemUpToolStripMenuItem.Text = "Move Label Up";
                    if (node.GetParent() != null && node.GetParent().GetType() == typeof(classObj))
                    {
                        classObj parentclass = node.GetParent();
                        if (treeNode.Index == 0 || ((MapObjectHolder)treeNode.PrevNode.Tag).GetType() != typeof(labelObj))
                            toolStripButtonUp.Enabled = moveItemUpToolStripMenuItem.Enabled = false;
                    }
                }
                if (IsStyleLibraryControl)
                {
                    propertiesToolStripMenuItem.Visible =
                    toolStripMenuItem1.Visible = node.GetType() != typeof(mapObj);
                }
            }
            if (IsStyleLibraryControl)
            {
                addVectorLayerFromFileToolStripMenuItem.Visible = false;
                addVectorLayerToolStripMenuItem.Visible = false;
                addTileIndexLayerToolStripMenuItem.Visible = false;
                addTileIndexLayerToolStripMenuItem1.Visible = false;
                addGraticuleLayerToolStripMenuItem.Visible = false;
                addGraticuleLayerToolStripMenuItem1.Visible = false;
                addRasterLayerFromFileToolStripMenuItem.Visible = false;
                addRasterLayerToolStripMenuItem.Visible = false;
                addWMSLayerToolStripMenuItem1.Visible = false;
                addWMSLayerToolStripMenuItem.Visible = false;
                addMapFileToolStripMenuItem.Visible = false;
                addMapFileToolStripMenuItem1.Visible = false;
                addMSSQLSpatialLayerToolStripMenuItem1.Visible = false;
                addMSSQLSpatialLayerToolStripMenuItem.Visible = false;
                zoomToLayerExtentToolStripMenuItem.Visible = false;
                autoStyleToolStripMenuItem.Visible = false;
                addThemeToolStripMenuItem.Visible = false;
                toolStripMenuItemSplitTheme.Visible = false;
                addStyleToolStripMenuItem.Visible = false;
                addNewStyleToolStripMenuItem.Visible = false;
                addLabelToolStripMenuItem.Visible = false;
                addNewLabelToolStripMenuItem.Visible = false;
                addNewLayerToolStripMenuItem.Text = addLayerToolStripMenuItem.Text = "Add New Category";
                addNewClassToolStripMenuItem.Text = addClassToolStripMenuItem.Text = "Add New Style";
            }
        }

        /// <summary>
        /// AfterCheck event handler of the treeView object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            MapObjectHolder selected = (MapObjectHolder)e.Node.Tag;
            if (selected.GetType() == typeof(layerObj))
            {
                layerObj layer = (layerObj)selected;
                
                if (e.Node.Checked)
                    layer.status = mapscript.MS_ON;
                else
                    layer.status = mapscript.MS_OFF;
                if (target != null)
                    target.RaisePropertyChanged(this);

                RaiseItemSelect(selected);
            }
            else if (selected.GetType() == typeof(classObj))
            {
                classObj class_object = (classObj)selected;
                if (e.Node.Checked)
                    class_object.status = mapscript.MS_ON;
                else
                    class_object.status = mapscript.MS_OFF;
                if (target != null)
                    target.RaisePropertyChanged(this);

                RaiseItemSelect(selected);
            }
            else if (selected.GetType() == typeof(styleObj))
            {
                // styles are always visible
                if (!e.Node.Checked)
                    e.Node.Checked = true;
            }
            else if (selected.GetType() == typeof(labelObj))
            {
                // labels are always visible
                if (!e.Node.Checked)
                    e.Node.Checked = true;
            }
        }

        /// <summary>
        /// Click event handler of the toolStripButtonUp object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            TreeNode source = CurrentTree.SelectedNode;
            if (source != null && source.Tag != null)
            {
                bool indent = false;
                
                TreeNode destNode = source.PrevNode;
                if (destNode == null)
                    destNode = source.Parent;
                    
                RaiseItemSelect((MapObjectHolder)source.Tag);
                if (MoveTreeNode(source, destNode, true, indent))
                {
                    if (target != null)
                        target.RaisePropertyChanged(this);
                }
            }
        }

        /// <summary>
        /// Click event handler of the toolStripButtonDown object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            TreeNode source = CurrentTree.SelectedNode;
            if (source != null && source.Tag != null)
            {
                bool indent = false;
                
                TreeNode destNode = source.NextNode;
                if (destNode == null)
                    destNode = source.Parent;
                else if (destNode.Tag != null)
                {
                    MapObjectHolder h = (MapObjectHolder)destNode.Tag;
                    if (h.GetType() == typeof(layerObj))
                    {
                        layerObj layer = h;
                        if (MapUtils.HasMetadata(layer, "begin_group"))
                            indent = true;
                    }
                }

                RaiseItemSelect((MapObjectHolder)source.Tag);
                if (MoveTreeNode(source, destNode, false, indent))
                {
                    if (target != null)
                        target.RaisePropertyChanged(this);
                }
            }
        }

        private void RemoveLayerNode(TreeNode node)
        {
            layerObj layer = (MapObjectHolder)node.Tag;
            map.removeLayer(layer.index);
            node.Parent.Nodes.Remove(node);
        }

        /// <summary>
        /// Click event handler of the toolStripButtonDelete object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            bool needRefresh = false;
            MapObjectHolder selected = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
            if (selected.GetType() == typeof(layerObj))
            {
                layerObj layer = selected;
                if (MessageBox.Show("Are you sure you want to delete layer " + layer.name + "?",
                    "MapManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                RemoveLayerNode(CurrentTree.SelectedNode);
               
                UpdateLayerOrder(CurrentTree.Nodes, -1, 0);
                needRefresh = true;
            }
            else if (selected.GetType() == typeof(classObj))
            {
                if (MessageBox.Show("Are you sure you want to delete the selected class?",
                    "MapManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                layerObj layer = selected.GetParent();
                layer.removeClass(CurrentTree.SelectedNode.Index);
                if (layer.numclasses <= 1)
                    needRefresh = true;
            }
            else if (selected.GetType() == typeof(styleObj))
            {
                if (MessageBox.Show("Are you sure you want to delete the selected style?",
                    "MapManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                classObj classobj = selected.GetParent();
                classobj.removeStyle(CurrentTree.SelectedNode.Index);
                if (classobj.numstyles <= 1)
                    needRefresh = true;
            }
            else if (selected.GetType() == typeof(labelObj))
            {
                if (MessageBox.Show("Are you sure you want to delete the selected label?",
                    "MapManager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                classObj classobj = selected.GetParent();
                for (int l = 0; l < classobj.numlabels; l++)
                {
                    if (classobj.getLabel(l).Equals((labelObj)selected))
                    {
                        classobj.removeLabel(l);
                        needRefresh = true;
                        break;
                    }
                }
                if (classobj.numlabels <= 1)
                    needRefresh = true;
            }
            if (needRefresh)
                RefreshView();
            else
            {
                CurrentTree.SelectedNode.Tag = null;
                CurrentTree.SelectedNode.Remove();
            }
            if (target != null)
                target.RaisePropertyChanged(this);
        }

        /// <summary>
        /// ButtonClick event handler of the toolStripSplitButtonNew object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void toolStripSplitButtonNew_ButtonClick(object sender, EventArgs e)
        {
            // selecting the default item
            if (toolStripSplitButtonNew.DropDownItems.Count > 0)
                toolStripSplitButtonNew.DropDownItems[0].PerformClick();
        }

        /// <summary>
        /// Raises an ItemSelect event
        /// </summary>
        private void RaiseItemSelect(MapObjectHolder selected)
        {
            if (this.ItemSelect != null && enableItemSelect)
                ItemSelect(this, selected);
        }

        /// <summary>
        /// AfterSelect event handler of the treeView object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateToolbar();
            MapObjectHolder selected = (MapObjectHolder)e.Node.Tag;
            RaiseItemSelect(selected);
        }

        /// <summary>
        /// KeyDown event handler of the treeView object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && CurrentTree.SelectedNode != null)
            {
                toolStripButtonDelete_Click(this, null);
            }
            if (e.KeyCode == Keys.Space && CurrentTree.SelectedNode != null)
            {
                MapObjectHolder current = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                if (current.GetType() == typeof(styleObj) || current.GetType() == typeof(labelObj))
                {
                    e.SuppressKeyPress = true;
                }
            }
            if (e.KeyCode == Keys.F2 && CurrentTree.SelectedNode != null)
            {
                enableLabelEdit = true;
                CurrentTree.SelectedNode.BeginEdit();
            }
        }

        /// <summary>
        /// Click event handler of the refreshListToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        /// <summary>
        /// Click event handler of the moveLayerUpToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void moveLayerUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonUp_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the moveLayerDownToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void moveLayerDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDown_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the propertiesToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditProperties != null)
                EditProperties(this, (MapObjectHolder)CurrentTree.SelectedNode.Tag);
        }

        /// <summary>
        /// Click event handler of the deleteLayerToolStripMenuItem object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void deleteLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDelete_Click(sender, e);
        }

        /// <summary>
        /// ItemDrag event handler of the treeView object. Occurs when the selected item is dragged.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Move the dragged node when the left mouse button is used.
            // The drag is only enabled for layerObj, classObj, styleObj and labelObj nodes.
            if (e.Button == MouseButtons.Left)
            {
                if (e.Item.GetType() == typeof(TreeNode))
                {
                    TreeNode treeNode = (TreeNode)e.Item;
                    MapObjectHolder node = (MapObjectHolder)treeNode.Tag;
                    if (node.GetType() == typeof(layerObj) || 
                        node.GetType() == typeof(classObj) || 
                        node.GetType() == typeof(styleObj) ||
                        node.GetType() == typeof(labelObj))
                    {
                        RaiseItemSelect(node);
                        DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                }
            }
        }

        /// <summary>
        /// DragEnter event handler of the treeView object. Occurs when the item is dragged into the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Update the style names after reordering the styles
        /// </summary>
        /// <param name="nodes">The TreeNode collection of the style set</param>
        private void UpdateStyleNames(TreeNodeCollection nodes)
        {
            int styleCount = 0, labelCount = 0, nodeCount = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                MapObjectHolder node = (MapObjectHolder)nodes[i].Tag;
                if (node.GetType() == typeof(styleObj))
                    nodes[i].Text = "Style (" + Convert.ToString(styleCount++) + ")";
                else if (node.GetType() == typeof(labelObj))
                    nodes[i].Text = "Label (" + Convert.ToString(labelCount++) + ")";
                else
                    nodes[i].Text = "Node (" + Convert.ToString(nodeCount++) + ")";
            }
        }

        /// <summary>
        /// Clear the result cache of the layers in the current map
        /// </summary>
        private void ClearResults()
        {
            if (map != null)
            {
                map.freeQuery(-1);
                // close all layers to free expression tokens
                for (int i = 0; i < map.numlayers; i++)
                    map.getLayer(i).close();

                this.target.RaiseSelectionChanged(this);
            }
        }

        /// <summary>
        /// Update the map object to reflect the layer order and groups in the treeview
        /// </summary>
        /// <returns>The last layer index used in the in the map object</returns>
        private int UpdateLayerOrder(TreeNodeCollection nodes, int index, int group_count)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                MapObjectHolder h = (MapObjectHolder)nodes[i].Tag;

                if (h.GetType() == typeof(mapObj))
                    return UpdateLayerOrder(nodes[i].Nodes, index, group_count);
                else if (h.GetType() == typeof(layerObj))
                {
                    layerObj layer = (MapObjectHolder)nodes[i].Tag;
                    ++index;

                    if (!layer.Equals(map.getLayer(index)))
                    {
                        // invalid order, should be updated
                        map.insertLayer(map.removeLayer(layer.index), index);
                    }
                }
            }

            return index;
        }


        /// <summary>
        /// A generic function to reposition the nodes within the same parent in the current tree.
        /// </summary>
        /// <param name="SourceNode">The source node which should be repositioned</param>
        /// <param name="DestinationNode">The destination node which should be the adjacent node of the source</param>
        /// <param name="above">The desired location of the source node related to the destination</param>
        /// <param name="indent">Source node should be added as a child of the destination</param>
        /// <returns>true if the operation was successful, otherwise false</returns>
        private bool MoveTreeNode(TreeNode SourceNode, TreeNode DestinationNode, 
            bool above, bool indent)
        {
            bool layerorderChanged = false;
            bool classorderChanged = false;
            // allows to reposition only within the same parent
            bool isSameParent = false;

            if (SourceNode.Parent != null)
            {
                if (SourceNode.Parent.Equals(DestinationNode.Parent))
                    isSameParent = true;
            }
            else if (DestinationNode.Parent == null)
                isSameParent = true;

            // Testing the existence of the attached MapScript objects
            if (SourceNode.Tag != null && DestinationNode.Tag != null)
            {
                MapObjectHolder source = (MapObjectHolder)SourceNode.Tag;
                MapObjectHolder destination = (MapObjectHolder)DestinationNode.Tag;
                // getting the parent collection
                TreeNodeCollection nodes;
                if (DestinationNode.Parent == null)
                    nodes = CurrentTree.Nodes;
                else
                    nodes = DestinationNode.Parent.Nodes;
                // testing wether the node types are the same
                if (source.GetType() == destination.GetType())
                {
                    if (source.GetType() == typeof(layerObj))
                    {
                        layerObj layer = source;
                        if (isSameParent)
                            layerorderChanged = true;
                    }
                    else if (source.GetType() == typeof(classObj) && isSameParent)
                    {
                        // repositioning the classes
                        // there's no index property of the MapScript classes
                        // we use the node index to define the relation
                        layerObj layer = source.GetParent();
                        classObj srcClass = source;
                        classObj destClass = destination;
                        if (DestinationNode.Index > SourceNode.Index)
                        {
                            classorderChanged = true;
                            if (above && DestinationNode.Index > 0)
                                layer.insertClass(layer.removeClass(SourceNode.Index), DestinationNode.Index - 1);
                            else if (!above && DestinationNode.Index < nodes.Count - 1)
                                layer.insertClass(layer.removeClass(SourceNode.Index), DestinationNode.Index);
                            else if (!above)
                                layer.insertClass(layer.removeClass(SourceNode.Index), -1);
                            else
                                return false;
                        }
                        else if (DestinationNode.Index < SourceNode.Index)
                        {
                            classorderChanged = true;
                            if (above)
                                layer.insertClass(layer.removeClass(SourceNode.Index), DestinationNode.Index);
                            else if (!above && DestinationNode.Index < nodes.Count - 1)
                                layer.insertClass(layer.removeClass(SourceNode.Index), DestinationNode.Index + 1);
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else if (source.GetType() == typeof(styleObj) && isSameParent)
                    {
                        // repositioning the styles
                        // there's no index property of the MapScript styles
                        // we use the node index to define the relation
                        classObj classobj = source.GetParent();
                        styleObj srcStyle = source;
                        styleObj destStyle = destination;

                        if (DestinationNode.Index > SourceNode.Index)
                        {
                            if (above && DestinationNode.Index > 0)
                                classobj.insertStyle(classobj.removeStyle(SourceNode.Index), DestinationNode.Index - 1);
                            else if (!above && DestinationNode.Index < classobj.numstyles - 1)
                                classobj.insertStyle(classobj.removeStyle(SourceNode.Index), DestinationNode.Index);
                            else if (!above)
                                classobj.insertStyle(classobj.removeStyle(SourceNode.Index), -1);
                            else
                                return false;
                        }
                        else if (DestinationNode.Index < SourceNode.Index)
                        {
                            if (above)
                                classobj.insertStyle(classobj.removeStyle(SourceNode.Index), DestinationNode.Index);
                            else if (!above && DestinationNode.Index < nodes.Count - 1)
                                classobj.insertStyle(classobj.removeStyle(SourceNode.Index), DestinationNode.Index + 1);
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else if (source.GetType() == typeof(labelObj) && isSameParent)
                    {
                        // repositioning the labels
                        // mapscript doesn't support movedown and moveup operations for labels within a class,
                        // we need to reorder the label array
                        classObj classobj = source.GetParent();
                        // move the selected label to the bottom
                        classobj.addLabel(classobj.removeLabel(SourceNode.Index - classobj.numstyles));
                        // move items below the inserted node
                        int srcIndex = DestinationNode.Index - classobj.numstyles;
                        if (above && DestinationNode.Index > SourceNode.Index)
                            --srcIndex;
                        int nodeCount = classobj.numlabels - srcIndex;
                        while (--nodeCount > 0)
                            classobj.addLabel(classobj.removeLabel(srcIndex));
                    }
                    // repositioning the nodes to reflect the underlying changes
                    try
                    {
                        enableItemSelect = false;
                        SourceNode.Remove();

                        if (above)
                        {
                            if (indent)
                                DestinationNode.Nodes.Add(SourceNode);
                            else
                                nodes.Insert(DestinationNode.Index, SourceNode);
                        }
                        else
                        {
                            // insert the source node below the destination in the tree
                            if (indent)
                                DestinationNode.Nodes.Insert(0, SourceNode);
                            else
                            {
                                if (DestinationNode.Index >= nodes.Count - 1)
                                    nodes.Add(SourceNode);
                                else
                                    nodes.Insert(DestinationNode.Index + 1, SourceNode);
                            }
                        }
                    }
                    finally
                    {
                        enableItemSelect = true;
                    }

                    if (layerorderChanged)
                    {
                        UpdateLayerOrder(CurrentTree.Nodes, -1, 0);
                    }

                    CurrentTree.SelectedNode = SourceNode;

                    if (source.GetType() == typeof(styleObj) || source.GetType() == typeof(labelObj))
                    {
                        RemoveNodeCheckBox(SourceNode);
                        UpdateStyleNames(nodes);
                    }

                    UpdateToolbar();

                    if (layerorderChanged || classorderChanged)
                    {
                        // this change invalidated the layerindex and classindex in the result set
                        ClearResults();
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// DragDrop event handler of the treeView object. Occurs when the item is dropped into the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
                {
                    Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                    TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);
                    if (DestinationNode != null)
                    {
                        TreeNode SourceNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                        if (MoveTreeNode(SourceNode, DestinationNode, true, false))
                        {
                            if (target != null)
                                target.RaisePropertyChanged(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting item, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// DragOver event handler of the treeView object. Occurs when the item is dragged over the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            // allow to select the destination of the same node type
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);
                if (DestinationNode != null)
                {
                    TreeNode SourceNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (IsStyleLibraryControl != IsStyleLibrary(SourceNode.TreeView))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }

                    if (SourceNode.Tag != null && DestinationNode.Tag != null)
                    {
                        bool isSameParent = false;

                        if (SourceNode.Parent != null)
                        {
                            if (SourceNode.Parent.Equals(DestinationNode.Parent))
                                isSameParent = true;
                        }
                        else if (DestinationNode.Parent == null)
                            isSameParent = true;                            
                        
                        MapObjectHolder source = (MapObjectHolder)SourceNode.Tag;
                        MapObjectHolder destination = (MapObjectHolder)DestinationNode.Tag;
                        if (source.GetType() == destination.GetType())
                        {
                            if (source.GetType() == typeof(layerObj))
                            {
                                layerObj layer = source;
                                if (isSameParent)
                                     CurrentTree.SelectedNode = DestinationNode;
                                else
                                    CurrentTree.SelectedNode = null;
                            }
                            else if (isSameParent)
                                CurrentTree.SelectedNode = DestinationNode;
                            else
                                CurrentTree.SelectedNode = null; // parent doesn't match
                        }
                        else
                            CurrentTree.SelectedNode = null; // type doesn't match
                    }
                }
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Create a new layer above the selected layer
        /// </summary>
        /// <returns>The new layer object</returns>
        private layerObj CreateNewLayer()
        {
            layerObj currentLayer = null;
            layerObj layer = null;

            if (CurrentTree.SelectedNode != null)
            {
                MapObjectHolder selected = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                if (selected.GetType() == typeof(layerObj))
                    currentLayer = selected;
                else if (selected.GetType() == typeof(classObj))
                    currentLayer = selected.GetParent();
                else if (selected.GetType() == typeof(styleObj))
                    currentLayer = selected.GetParent().GetParent();
                else if (selected.GetType() == typeof(labelObj))
                    currentLayer = selected.GetParent().GetParent();
            }
            
            if (currentLayer != null)
            { 
                using (intarray ar = map.getLayersDrawingOrder())
                {
                    for (int i = 0; i < map.numlayers; i++)
                    {
                        if (ar.getitem(i) == currentLayer.index)
                        {
                            if (i < map.numlayers - 1)
                            {
                                layer = new layerObj(null);
                                map.insertLayer(layer, ar.getitem(i + 1));
                            }
                            else
                                layer = new layerObj(map);

                            break;
                        }
                    }
                }
            }
            else
                layer = new layerObj(map);

            return layer;
        }

        /// <summary>
        /// Opens a vector layer.
        /// </summary>
        /// <param name="fileName">The name of the file to be opened.</param>
        private void OpenVectorLayer(string fileName)
        {
            layerObj layer = null;
            
            try
            {
                // trying to open the layer
                layer = CreateNewLayer();

                if (Path.GetExtension(fileName) == ".shp" || Path.GetExtension(fileName) == ".shx")
                {
                    // open as shapefile
                    layer.data = fileName;
                }
                else
                {
                    // open as OGR layer
                    layer.connection = fileName;
                    layer.connectiontype = MS_CONNECTION_TYPE.MS_OGR;
                    layer.data = null;
                }

                layer.name = MapUtils.GetUniqueLayerName(map, Path.GetFileNameWithoutExtension(fileName), 0);
                layer.setProjection("+AUTO");
                layer.status = mapscript.MS_ON;
                layer.template = "query.html";

                MapUtils.CreateRandomClass(layer);
                //MapUtils.InitializeDefaultLabel(layer);

                layer.open();
                layer.close();
                // try to find out the shape type using OGR
                Ogr.RegisterAll();
                DataSource ds = Ogr.Open(fileName, 0);
                if (ds != null)
                {
                    if (ds.GetLayerCount() > 0)
                    {
                        Layer ogrLayer = ds.GetLayerByIndex(0);
                        wkbGeometryType gType = ogrLayer.GetLayerDefn().GetGeomType();
                        // reading the first feature to detect the type, if necessary
                        Feature feat;
                        while (gType == wkbGeometryType.wkbUnknown  && (feat = ogrLayer.GetNextFeature()) != null)
                        {
                            Geometry geom = feat.GetGeometryRef();
                            if (geom != null)
                                gType = geom.GetGeometryType();
                            feat.Dispose();
                        }
                        layer.type = MapUtils.GetLayerType(gType);

                                                // identify projection
                        if (layer.getProjection() == "+AUTO")
                        {
                            SpatialReference sr = ogrLayer.GetSpatialRef();

                            if (sr != null)
                            {
                                string proj4;
                                if (sr.ExportToProj4(out proj4) == Ogr.OGRERR_NONE)
                                    layer.setProjection(proj4);
                            }
                        }

                        //ds.ReleaseResultSet(ogrLayer);
                    }
                    ds.Dispose();
                }
                if (layer.styleitem != "AUTO")
                {
                    if (layer.type == MS_LAYER_TYPE.MS_LAYER_POINT)
                    {
                        // initialize with the default marker if specified in the symbol file for point symbols
                        symbolObj symbol;
                        for (int i = 0; i < map.symbolset.numsymbols; i++)
                        {
                            symbol = map.symbolset.getSymbol(i);

                            if (symbol.name == "default-marker")
                            {
                                layer.getClass(0).getStyle(0).symbol = i;
                                break;
                            }
                        }
                    }

                    // setting the default color
                    styleObj style = layer.getClass(0).getStyle(0);
                    MapUtils.SetDefaultColor(layer.type, style);
                }

                // setting up the selected layer
                selected = new MapObjectHolder(layer, target.GetParent());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to open layer, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (layer != null)
                {
                    if (layer.map != null)
                        layer.map.removeLayer(layer.index);
                    layer = null;
                }
            }

            if (layer != null)
            {
                // trying to find out the projname
                string proj = layer.getProjection().Trim();
                string proj4;
                int epsg;
                string proj_name = MapUtils.FindProjection(proj, out proj4, out epsg);
                if (proj4.Trim().StartsWith("+"))
                {
                    if (proj != proj4.Trim())
                        layer.setProjection(proj4);

                    layer.metadata.set("coordsys_name", proj_name);
                }

                // if this is the first layer or the map projection hasn't been set zoom to this extent
                if (map.numlayers == 1 || map.getProjection().Trim().Length == 0)
                {
                    ZoomToLayerExtent(layer);
                    if (InitialExtentSet != null)
                        InitialExtentSet(this, null);

                    // setting the projection of the map according to the layer projection
                    if (map.getProjection().Trim().Length == 0 && proj.Length > 0 && proj != "+AUTO")
                    {
                        map.setProjection(layer.getProjection());
                        map.web.metadata.set("coordsys_name", proj_name);
                        // setting up the default unit
                        if (proj.Contains("+proj=longlat"))
                            map.units = MS_UNITS.MS_DD;
                        else
                            map.units = MS_UNITS.MS_METERS;
                    }
                }
            }
        }

        /// <summary>
        /// Opens a tile index layer.
        /// </summary>
        /// <param name="fileName">The name of the file to be opened.</param>
        private void OpenTileIndexLayer(string fileName)
        {
            layerObj layer = null;

            try
            {
                // trying to open the layer
                layer = CreateNewLayer();

                layer.connection = fileName;

                layer.connectiontype = MS_CONNECTION_TYPE.MS_OGR;
                layer.name = MapUtils.GetUniqueLayerName(map, Path.GetFileNameWithoutExtension(fileName), 0);
                layer.setProjection("+AUTO");
                layer.data = null;
                layer.status = mapscript.MS_OFF;
                layer.template = "query.html";

                MapUtils.CreateRandomClass(layer);
                //MapUtils.InitializeDefaultLabel(layer);

                // setting auto style for the mapinfo layers
                string ext = Path.GetExtension(fileName).Trim().ToLower();
                if (ext == ".tab" || ext == ".mif")
                    layer.styleitem = "AUTO";

                layer.open();
                layer.close();
                // try to find out the shape type using OGR
                Ogr.RegisterAll();
                DataSource ds = Ogr.Open(layer.connection, 0);
                if (ds != null)
                {
                    if (ds.GetLayerCount() > 0)
                    {
                        Layer ogrLayer;
                        if (layer.data != null)
                            ogrLayer = ds.GetLayerByName(layer.data);
                        else
                            ogrLayer = ds.GetLayerByIndex(0);
                        wkbGeometryType gType = ogrLayer.GetLayerDefn().GetGeomType();
                        if (gType != wkbGeometryType.wkbUnknown)
                            layer.type = MapUtils.GetLayerType(gType);

                        int tileFieldIndex = ogrLayer.GetLayerDefn().GetFieldIndex("location");
                        if (tileFieldIndex < 0)
                        {
                            // Prompt for the index
                            FieldSelectionForm form = new FieldSelectionForm(layer, "Select the field for the file locations");
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                tileFieldIndex = ogrLayer.GetLayerDefn().GetFieldIndex(form.SelectedItem);
                                layer.tileitem = form.SelectedItem;
                            }
                        }
                        Feature feat;
                        while ((feat = ogrLayer.GetNextFeature()) != null)
                        {
                            string basePath = "";
                            string tileFile = feat.GetFieldAsString(tileFieldIndex);
                            // separate source and layer
                            string[] src = tileFile.Split(new char[] { ',' });

                            if (!File.Exists(src[0]))
                            {
                                if (map.shapepath != "" && File.Exists(map.shapepath + "\\" + src[0]))
                                    basePath = map.shapepath + "\\";
                                else if (map.mappath != "" && File.Exists(map.mappath + "\\" + src[0]))
                                    basePath = map.mappath + "\\"; 
                                else
                                    throw new Exception("File fot found: " + src[0] + ". If your tile file contains relative pathes try to set SHAPEPATH to the base directory.");
                            }
                            // trying to open as vector data source
                            DataSource ds2 = Ogr.Open(basePath + src[0], 0);
                            if (ds2 == null)
                            {
                                // trying to open as raster data source
                                Dataset ds3 = Gdal.Open(basePath + src[0], Access.GA_ReadOnly);
                                if (ds3 == null)                               
                                    throw new Exception("Can't open data source: " + src[0] + ".");

                                // successfully opened
                                layer.type = MS_LAYER_TYPE.MS_LAYER_RASTER;
                                layer.styleitem = null;
                                // TODO: check for SRS entry (if upgrading to Mapserver 6.4)
                                ds3.Dispose();
                            }
                            else
                            {
                                try
                                {
                                    // find out geometry type
                                    if (gType == wkbGeometryType.wkbUnknown)
                                    {
                                        Geometry geom = feat.GetGeometryRef();
                                        if (geom != null)
                                            gType = geom.GetGeometryType();
                                        layer.type = MapUtils.GetLayerType(gType);
                                    }
                                    feat.Dispose();
                                    
                                    Layer l;
                                    if (src.Length == 2)
                                    {
                                        int layerIndex;
                                        if (int.TryParse(src[1], out layerIndex))
                                            l = ds2.GetLayerByIndex(layerIndex);
                                        else
                                            l = ds2.GetLayerByName(src[1]);
                                    }
                                    else
                                        l = ds2.GetLayerByIndex(0);
                                    if (l == null)
                                    {
                                        throw new Exception("Can't open layer: " + tileFile + ".");
                                    }
                                    else
                                        l.Dispose();
                                }
                                finally
                                {
                                    ds2.Dispose();
                                }
                            }
                        }
                    }
                    ds.Dispose();
                }
                if (layer.styleitem != "AUTO")
                {
                    if (layer.type == MS_LAYER_TYPE.MS_LAYER_POINT)
                    {
                        // initialize with the default marker if specified in the symbol file for point symbols
                        symbolObj symbol;
                        for (int i = 0; i < map.symbolset.numsymbols; i++)
                        {
                            symbol = map.symbolset.getSymbol(i);

                            if (symbol.name == "default-marker")
                            {
                                layer.getClass(0).getStyle(0).symbol = i;
                                break;
                            }
                        }
                    }

                    // setting the default color
                    styleObj style = layer.getClass(0).getStyle(0);
                    MapUtils.SetDefaultColor(layer.type, style);
                }

                // setting up the selected layer
                selected = new MapObjectHolder(layer, target.GetParent());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open layer, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (layer != null)
                {
                    if (layer.map != null)
                        layer.map.removeLayer(layer.index);
                    layer = null;
                }
            }

            if (layer != null)
            {
                // set up layer extent, we still need to open the layer as a vector layer to get extent
                
                // trying to find out the projname
                string proj = layer.getProjection().Trim();
                string proj4;
                int epsg;
                string proj_name = MapUtils.FindProjection(proj, out proj4, out epsg);
                if (proj4.Trim().StartsWith("+"))
                {
                    if (proj != proj4.Trim())
                        layer.setProjection(proj4);

                    layer.metadata.set("coordsys_name", proj_name);
                }

                // if this is the first layer or the map projection hasn't been set zoom to this extent
                if (map.numlayers == 1 || map.getProjection().Trim().Length == 0)
                {
                    ZoomToLayerExtent(layer);
                    if (InitialExtentSet != null)
                        InitialExtentSet(this, null);

                    // setting the projection of the map according to the layer projection
                    if (map.getProjection().Trim().Length == 0 && proj.Length > 0 && proj != "+AUTO")
                    {
                        map.setProjection(layer.getProjection());
                        map.web.metadata.set("coordsys_name", proj_name);
                        // setting up the default unit
                        if (proj.Contains("+proj=longlat"))
                            map.units = MS_UNITS.MS_DD;
                        else
                            map.units = MS_UNITS.MS_METERS;
                    }
                }
                // set up tile index
                layer.tileindex = layer.connection;
                if (layer.type == MS_LAYER_TYPE.MS_LAYER_RASTER)
                {
                    // switch to raster layer, modify vtable
                    layer.setConnectionType((int)MS_CONNECTION_TYPE.MS_RASTER, null);
                }
                // tile index layer doesn't use connection parameter
                layer.connection = null;
            }
        }

        /// <summary>
        /// Opens a raster layer
        /// </summary>
        /// <param name="fileName">The name of the file to be opened.</param>
        private void OpenRasterLayer(string fileName)
        {
            layerObj layer = null;
            try
            {
                // trying to open the layer
                layer = CreateNewLayer();
                layer.connectiontype = MS_CONNECTION_TYPE.MS_RASTER;
                layer.type = MS_LAYER_TYPE.MS_LAYER_RASTER;
                layer.name = MapUtils.GetUniqueLayerName(map, Path.GetFileNameWithoutExtension(fileName), 0);
                layer.setProjection("AUTO");
                layer.data = fileName;
                layer.status = mapscript.MS_ON;

                // setting up the selected layer
                selected = new MapObjectHolder(layer, target.GetParent());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open layer, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (layer != null)
                {
                    if (layer.map != null)
                        layer.map.removeLayer(layer.index);
                    layer = null;
                }
            }

            // calculating the extent of this layer
            Gdal.AllRegister();
            Dataset ds = Gdal.Open(fileName, Access.GA_ReadOnly);
            if (ds != null)
            {
                double[] adfGeoTransform = new double[6];
                double minx, miny, maxx, maxy;
                ds.GetGeoTransform(adfGeoTransform);
                minx = adfGeoTransform[0];
                miny = adfGeoTransform[3];
                maxx = adfGeoTransform[0] + adfGeoTransform[1] * ds.RasterXSize + adfGeoTransform[2] * ds.RasterYSize;
                maxy = adfGeoTransform[3] + adfGeoTransform[4] * ds.RasterXSize + adfGeoTransform[5] * ds.RasterYSize;
                if (minx != maxx || miny != maxy)
                {
                    layer.setExtent(Math.Min(minx, maxx), Math.Min(miny, maxy), Math.Max(minx, maxx), Math.Max(miny, maxy));
                }

                // try to find out the projection and use that explicitly
                string wkt = ds.GetProjection();
                if (wkt != "")
                {
                    SpatialReference srs = new SpatialReference(wkt);
                    string proj4; 
                    srs.ExportToProj4(out proj4);
                    if (proj4 != null || proj4 != "")
                    {
                        string proj4_out;
                        int epsg;
                        string projName = MapUtils.FindProjection(proj4, out proj4_out, out epsg);
                        if (proj4_out.StartsWith("+"))
                        {
                            layer.setProjection(proj4_out);
                            layer.metadata.set("coordsys_name", projName);
                        }
                    }
                }
            }

            // if this is the first layer zoom to this extent
            if (layer != null && map.numlayers == 1)
            {
                ZoomToLayerExtent(layer);
                if (InitialExtentSet != null)
                    InitialExtentSet(this, null);

                // setting the projection of the map according to the layer projection
                string proj = layer.getProjection().Trim();
                if (map.getProjection().Trim().Length == 0 && proj.Length > 0 && proj != "+AUTO")
                {
                    map.setProjection(proj);
                    // setting up the default unit
                    if (proj.Contains("+proj=longlat"))
                        map.units = MS_UNITS.MS_DD;
                    else
                        map.units = MS_UNITS.MS_METERS;
                }
            }
        }

        /// <summary>
        /// Click event handler of the addVectorLayerToolStripMenuItem control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addVectorLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select one or more vector layer files";            
            openFileDialog.Filter = global::MapLibrary.Properties.Resources.OGR_FILE_TYPES;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog.FileNames)
                    OpenVectorLayer(fileName);
                if (target != null)
                    target.RaisePropertyChanged(this);
                RefreshView();

                TreeNode source = CurrentTree.SelectedNode;
                if (source != null && source.Tag != null)
                    RaiseItemSelect((MapObjectHolder)source.Tag);
                UpdateToolbar();
            }
        }

        /// <summary>
        /// Click event handler of the addRasterLayerToolStripMenuItem control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addRasterLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select one or more raster layer files";
            openFileDialog.Filter = null;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog.FileNames)
                    OpenRasterLayer(fileName);

                if (target != null)
                    target.RaisePropertyChanged(this);
                RefreshView();

                TreeNode source = CurrentTree.SelectedNode;
                if (source != null && source.Tag != null)
                    RaiseItemSelect((MapObjectHolder)source.Tag);
                UpdateToolbar();
            }
        }

        /// <summary>
        /// Click event handler of the addThemeToolStripMenuItem control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeWizardForm wizardForm = new ThemeWizardForm((MapObjectHolder)CurrentTree.SelectedNode.Tag);
            wizardForm.ShowDialog(this);
        }

        /// <summary>
        /// PropertyChanged event handler of the propertyEditor control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void propertyEditor_PropertyChanged(object sender, EventArgs e)
        {
            if (target != null)
                target.RaisePropertyChanged(this);
        }

        /// <summary>
        /// EditProperties event handler of the propertyEditor control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="target">The object to be edited.</param>
        void propertyEditor_EditProperties(object sender, MapObjectHolder target)
        {
            if (EditProperties != null)
                EditProperties(this, target);
        }

        /// <summary>
        /// Click event handler of the addLayerToolStripMenuItem control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (target == null)
                return;
            if (target.GetType() != typeof(mapObj))
                return;

            TreeNode source = CurrentTree.SelectedNode;
            if (source != null && source.Tag != null)
                RaiseItemSelect((MapObjectHolder)source.Tag);

            layerObj layer;
            classObj classobj;
            styleObj style;
            if (IsStyleLibraryControl)
            {
                AddStyleCategoryForm form = new AddStyleCategoryForm(MapUtils.GetUniqueLayerName(map, "New Category", 0));
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (form.CategoryType != "(Empty Category)")
                    {
                        layer = CreateNewLayer();
                        layer.connectiontype = MS_CONNECTION_TYPE.MS_INLINE;
                        layer.template = "query.html";
                        layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;
                        layer.name = MapUtils.GetUniqueLayerName(map, form.CategoryName, 0);

                        classobj = new classObj(layer);
                        classobj.name = form.CategoryName;
                        style = new styleObj(classobj);
                        layer.metadata.set("character-count", form.CharCount.ToString());
                        
                        MapUtils.SetDefaultColor(layer.type, style);
                        style.width = 1;
                        style.size = 24;

                        // create all symbols
                        StyleLibrary.ExpandFontStyles();

                        this.selected = new MapObjectHolder(layer, target);

                        RefreshView();

                        if (target != null)
                            target.RaisePropertyChanged(this);

                        return;
                    }
                }
                else
                    return;
            }
            
            layer = CreateNewLayer();
            layer.connectiontype = MS_CONNECTION_TYPE.MS_INLINE;
            layer.template = "query.html";
            layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;
            if (!IsStyleLibraryControl)
                layer.name = MapUtils.GetUniqueLayerName(map, "New Layer", 0);

            classobj = new classObj(layer);
            classobj.name = MapUtils.GetClassName(layer);
            style = new styleObj(classobj);

            // initialize with the default marker if specified in the symbol file for point symbols
            symbolObj symbol;
            for (int i = 0; i < map.symbolset.numsymbols; i++)
            {
                symbol = map.symbolset.getSymbol(i);

                if (symbol.name == "default-marker")
                {
                    layer.getClass(0).getStyle(0).symbol = i;
                    break;
                }
            }

            MapUtils.SetDefaultColor(layer.type, style);
            style.width = 1;
            style.size = 8; // set default size (#4339)
             
            this.selected = new MapObjectHolder(layer, target);

            RefreshView();

            if (target != null)
                target.RaisePropertyChanged(this);
        }

        /// <summary>
        /// Click event handler of the addClassToolStripMenuItem control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapObjectHolder selected = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
            RaiseItemSelect(selected);
            layerObj layer = selected;
            classObj classobj = new classObj(layer);
            classobj.name = MapUtils.GetClassName(layer);
            styleObj style = new styleObj(classobj);
            //STEPH: set default colour to be consistent with adding new style
            MapUtils.SetDefaultColor(layer.type, style);
            if (layer.type == MS_LAYER_TYPE.MS_LAYER_POINT)
            {
                if (layer.map != null)
                    style.setSymbolByName(layer.map, "default-marker");
                style.size = 8;
            }

            this.selected = new MapObjectHolder(classobj, selected);

            RefreshView();

            if (target != null)
                target.RaisePropertyChanged(this);
        }

        /// <summary>
        /// Click event handler of the addLabelToolStripMenuItem control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapObjectHolder selected = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
            RaiseItemSelect(selected);
            classObj classobj;
            if (selected.GetType() == typeof(layerObj))
            {
                layerObj layer = selected;
                classobj = layer.getClass(0);
                MapObjectHolder classHolder = new MapObjectHolder(classobj, selected);

                // adding an empty label to this class
                classobj.addLabel(new labelObj());
                labelObj label = classobj.getLabel(classobj.numlabels - 1);
                MapUtils.SetDefaultLabel(label, layer.map);

                this.selected = new MapObjectHolder(label, classHolder);

                RefreshView();
                if (target != null)
                    target.RaisePropertyChanged(this);
            }
            else if (selected.GetType() == typeof(classObj))
            {
                layerObj layer = selected.GetParent();
                classobj = selected;

                // adding an empty label to this class
                classobj.addLabel(new labelObj());
                labelObj label = classobj.getLabel(classobj.numlabels - 1);
                MapUtils.SetDefaultLabel(label, layer.map);

                this.selected = new MapObjectHolder(label, selected);

                RefreshView();
                if (target != null)
                    target.RaisePropertyChanged(this);
            }
            else
                return; 
        }

        /// <summary>
        /// Click event handler of the addStyleToolStripMenuItem control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapObjectHolder selected = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
            RaiseItemSelect(selected);
            classObj classobj;
            if (selected.GetType() == typeof(layerObj))
            {
                layerObj layer = selected;
                classobj = layer.getClass(0);
                MapObjectHolder classHolder = new MapObjectHolder(classobj, selected);
                styleObj style = new styleObj(classobj);
                MapUtils.SetDefaultColor(layer.type, style);
                style.width = 1;

                this.selected = new MapObjectHolder(style, classHolder);

                RefreshView();
                //if (EditProperties != null)
                //    EditProperties(this, new MapObjectHolder(style, classHolder));
                if (target != null)
                    target.RaisePropertyChanged(this);
            }
            else if (selected.GetType() == typeof(classObj))
            {
                layerObj layer = selected.GetParent();
                classobj = selected;
                styleObj style = new styleObj(classobj);
                MapUtils.SetDefaultColor(layer.type, style);
                style.width = 1;

                this.selected = new MapObjectHolder(style, selected);

                RefreshView();
                //if (EditProperties != null)
                //    EditProperties(this, new MapObjectHolder(style, selected));
                if (target != null)
                    target.RaisePropertyChanged(this);
            }
            else
                return;   
        }

        /// <summary>
        /// Calculate mssql spatial layer extent
        /// </summary>
        /// <param name="layer">mssql spatial layer</param>
        private void GetMssqlSpatialLayerExtent(layerObj layer)
        {
            int fromPos = layer.data.IndexOf(" from ", StringComparison.InvariantCultureIgnoreCase);

            int usingPos = layer.data.IndexOf(" using ", StringComparison.InvariantCultureIgnoreCase);


            string geomCol = layer.data.Substring(0, fromPos).ToLower();
            string tableName;

            if (usingPos > 0)
                tableName = layer.data.Substring(fromPos + 6, usingPos - fromPos - 6);
            else
                tableName = layer.data.Substring(fromPos + 6);

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand();
            SqlCom.Connection = new SqlConnection(layer.connection);
            SqlCom.Connection.Open();
            SqlCom.CommandType = CommandType.Text;
            if (geomCol.Contains("(geography)"))
            {
                geomCol = geomCol.Replace("(geography)", "").Trim();
                SqlCom.CommandText = "select MIN(" + geomCol + ".STStartPoint().Long), MIN(" + geomCol + ".STStartPoint().Lat), MAX(" + geomCol + ".STStartPoint().Long), MAX(" + geomCol + ".STStartPoint().Lat) from " + tableName;
                
            }
            else
            {
                geomCol = geomCol.Replace("(geometry)", "").Trim();
                SqlCom.CommandText = "select MIN(" + geomCol + ".STStartPoint().STX), MIN(" + geomCol + ".STStartPoint().STY), MAX(" + geomCol + ".STStartPoint().STX), MAX(" + geomCol + ".STStartPoint().STY) from " + tableName;
            }

            System.Data.SqlClient.SqlDataReader SqlDR;
            using (SqlDR = SqlCom.ExecuteReader())
            {
                while (SqlDR.Read())
                {
                    if (!SqlDR.IsDBNull(0) && !SqlDR.IsDBNull(1) && !SqlDR.IsDBNull(2) && !SqlDR.IsDBNull(3))
                    layer.extent.minx = SqlDR.GetDouble(0);
                    layer.extent.miny = SqlDR.GetDouble(1);
                    layer.extent.maxx = SqlDR.GetDouble(2);
                    layer.extent.maxy = SqlDR.GetDouble(3);
                }
            }
        }

        /// <summary>
        /// Triggers a Zoom Chnaged event
        /// </summary>
        private void RaiseZoomChanged()
        {
            MS_UNITS mapunits = map.units;

            int unitPrecision = MapUtils.GetUnitPrecision(mapunits);

            double zoom = (map.extent.maxx - map.extent.minx);
            if (mapunits != map.units)
                zoom = zoom * MapUtils.InchesPerUnit(map.units) / MapUtils.InchesPerUnit(mapunits);
            target.RaiseZoomChanged(this, Math.Round(zoom, unitPrecision), map.scaledenom);
        }

        /// <summary>
        /// Change the extent of the map according to the extent of the layer. 
        /// Reproject the extent if the layer and the map projection differs.
        /// </summary>
        /// <param name="layer">The layer to be used to calculate the extent</param>
        private void ZoomToLayerExtent(layerObj layer)
        {
            try
            {
                if (layer.connectiontype == MS_CONNECTION_TYPE.MS_PLUGIN &&
                    layer.plugin_library == "msplugin_mssql2008.dll")
                    GetMssqlSpatialLayerExtent(layer);
                
                using (rectObj extent = layer.getExtent())
                {
                    if (extent.minx < extent.maxx && extent.miny < extent.maxy)
                    {
                        if (map.getProjection() != "" && layer.getProjection() != "")
                        {
                            // need to reproject the extent
                            using (projectionObj oldProj = new projectionObj(layer.getProjection()))
                            {
                                using (projectionObj newProj = new projectionObj(map.getProjection()))
                                {
                                    using (rectObj rect = new rectObj(extent.minx, extent.miny, extent.maxx, extent.maxy, 0))
                                    {
                                        rect.project(oldProj, newProj);
                                        if (rect.minx < rect.maxx && rect.miny < rect.maxy)
                                            map.setExtent(rect.minx, rect.miny, rect.maxx, rect.maxy);
                                        if (target != null)
                                        {
                                            target.RaisePropertyChanged(this);
                                            RaiseZoomChanged();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // don't reproject
                            map.setExtent(extent.minx, extent.miny, extent.maxx, extent.maxy);
                            if (target != null)
                            {
                                target.RaisePropertyChanged(this);
                                RaiseZoomChanged();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Click event handler of the zoomToLayerExtentToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void zoomToLayerExtentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapObjectHolder selected = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
            if (selected.GetType() == typeof(layerObj))
            {
                ZoomToLayerExtent(selected);
            }
        }

        /// <summary>
        /// NodeMouseClick event handler of the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // select the item regardless of the mouse button
            CurrentTree.SelectedNode = e.Node;
        }

        /// <summary>
        /// NodeMouseDoubleClick event handler of the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MapObjectHolder node = (MapObjectHolder)e.Node.Tag;

            // should allow the double click only for the class and style obj (#4336)
            if (node.GetType() == typeof(classObj) || node.GetType() == typeof(styleObj) 
                || node.GetType() == typeof(labelObj))
            {
                if (EditProperties != null)
                    EditProperties(this, node);
            }
        }

        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;
        public const int TV_FIRST = 0x1100;
        public const int TVM_SETITEM = TV_FIRST + 63;

        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Remove the checkbox for the specified node
        /// </summary>
        /// <param name="node">TreeNode reference</param>
        private void RemoveNodeCheckBox(TreeNode node)
        {
            TVITEM tvItem = new TVITEM();
            tvItem.hItem = node.Handle;
            tvItem.mask = TVIF_STATE;
            tvItem.stateMask = TVIS_STATEIMAGEMASK;
            tvItem.state = 0;
            IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvItem));
            Marshal.StructureToPtr(tvItem, lparam, false);
            SendMessage(this.CurrentTree.Handle, TVM_SETITEM, IntPtr.Zero, lparam);

        }

        /// <summary>
        /// Remove the checkbox for the style and label nodes
        /// </summary>
        /// <param name="nodes">Collection of the TreeNodes</param>
        private void RemoveStyleChecks(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                MapObjectHolder current = (MapObjectHolder)node.Tag;
                if (current.GetType() == typeof(styleObj) || current.GetType() == typeof(labelObj))
                {
                    RemoveNodeCheckBox(node);
                }
                else
                    RemoveStyleChecks(node.Nodes);
            }
        }

        /// <summary>
        /// Click event handler of the addRasterLayerFromFileToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addRasterLayerFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addRasterLayerToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the addVectorLayerFromFileToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addVectorLayerFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addVectorLayerToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the addWMSLayerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addWMSLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode source = CurrentTree.SelectedNode;
            if (source != null && source.Tag != null)
                RaiseItemSelect((MapObjectHolder)source.Tag);

            int numlayers = map.numlayers;
            
            AddWMSLayerForm form = new AddWMSLayerForm(target);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.selected = form.GetSelectedLayer();
                layerObj layer = selected;

                // if this is the first layer zoom to this extent
                if (layer != null && numlayers == 0)
                {
                    ZoomToLayerExtent(layer);
                    if (InitialExtentSet != null)
                        InitialExtentSet(this, null);

                    // setting the projection of the map according to the layer projection
                    string proj = layer.getProjection().Trim();
                    if (map.getProjection().Trim().Length == 0 && proj.Length > 0 && proj != "+AUTO")
                    {
                        map.setProjection(proj);
                        // setting up the default unit
                        if (proj.Contains("+proj=longlat"))
                            map.units = MS_UNITS.MS_DD;
                        else
                            map.units = MS_UNITS.MS_METERS;
                    }
                }

                if (target != null)
                    target.RaisePropertyChanged(this);

                RefreshView();
            }
        }

        /// <summary>
        /// Click event handler of the goToLayerTextToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void goToLayerTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GoToLayerText != null)
            {
                layerObj layer = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                GoToLayerText(this, layer, -1);
            }
        }

        /// <summary>
        /// Click event handler of the goToClassTextToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void goToClassTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GoToLayerText != null)
            {
                classObj classobj = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                GoToLayerText(this, classobj.layer, CurrentTree.SelectedNode.Index);
            }
        }

        /// <summary>
        /// Click event handler of the addTileIndexLayerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addTileIndexLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select the tile index file";
            openFileDialog.Filter = global::MapLibrary.Properties.Resources.OGR_FILE_TYPES;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                OpenTileIndexLayer(openFileDialog.FileName);
                if (target != null)
                    target.RaisePropertyChanged(this);
                RefreshView();

                TreeNode source = CurrentTree.SelectedNode;
                if (source != null && source.Tag != null)
                    RaiseItemSelect((MapObjectHolder)source.Tag);
                UpdateToolbar();
            }
        }

        /// <summary>
        /// Click event handler of the addMSSQLSpatialLayerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addMSSQLSpatialLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode source = CurrentTree.SelectedNode;
            if (source != null && source.Tag != null)
                RaiseItemSelect((MapObjectHolder)source.Tag);

            int numlayers = map.numlayers;

            SqlConnectionDialog form = new SqlConnectionDialog();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                layerObj layer = null;
                try
                {
                    // trying to open the layer
                    MapUtils.TestMSSQLConnection(form.GetConnectionString(), form.GetDataString());

                    layer = CreateNewLayer();
                    layer.connectiontype = MS_CONNECTION_TYPE.MS_PLUGIN;
                    layer.plugin_library = layer.plugin_library_original = "msplugin_mssql2008.dll";

                    layer.connection = form.GetConnectionString();
                    layer.data = form.GetDataString();
                    
                    switch (form.GeometryType)
                    {
                        case "Point":
                        case "MultiPoint":
                            layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;
                            break;
                        case "LineString":
                        case "MultiLineString":
                            layer.type = MS_LAYER_TYPE.MS_LAYER_LINE;
                            break;
                        case "Polygon":
                        case "MultiPolygon":
                            layer.type = MS_LAYER_TYPE.MS_LAYER_POLYGON;
                            break;
                        default:
                            layer.type = MS_LAYER_TYPE.MS_LAYER_LINE;
                            break;
                    }

                    MapUtils.CreateRandomClass(layer);
                    MapUtils.InitializeDefaultLabel(layer);

                    layer.name = MapUtils.GetUniqueLayerName(map, form.TableName, 0);
                    string projName;
                    string proj4text;
                    int epsg;

                    if (form.GetProj4Text() != null)
                    {
                        projName = MapUtils.FindProjection(form.GetProj4Text(), out proj4text, out epsg);
                        layer.setProjection(form.GetProj4Text());
                        layer.metadata.set("coordsys_name", projName);
                    }
                    else if (form.GetSRText() != null && form.GetSRText() != "")
                    {
                        SpatialReference srs = new SpatialReference(form.GetSRText());
                        string proj4;
                        srs.ExportToProj4(out proj4);
                        if (proj4 != null || proj4 != "")
                        {
                            projName = MapUtils.FindProjection(proj4, out proj4text, out epsg);
                            //Steph: looks like there was a copy and paste error, I have replaced form.GetProj4Text() with proj4text
                            layer.setProjection(proj4text);
                            layer.metadata.set("coordsys_name", projName);
                        }  
                    }
                    else
                        layer.setProjection("AUTO");

                    layer.status = mapscript.MS_ON;

                    // setting up the selected layer
                    selected = new MapObjectHolder(layer, target.GetParent());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to open layer, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (layer != null)
                    {
                        if (layer.map != null)
                            layer.map.removeLayer(layer.index);
                        layer = null;
                    }
                    return;
                }

                // if this is the first layer or the map projection hasn't been set zoom to this extent
                if (map.numlayers == 1 || map.getProjection().Trim().Length == 0)
                {
                    ZoomToLayerExtent(layer);
                    if (InitialExtentSet != null)
                        InitialExtentSet(this, null);

                    // setting the projection of the map according to the layer projection
                    string proj = layer.getProjection().Trim();
                    if (proj.Length > 0 && proj != "+AUTO")
                    {
                        map.setProjection(proj);
                        // setting up the default unit
                        if (proj.Contains("+proj=longlat"))
                            map.units = MS_UNITS.MS_DD;
                        else
                            map.units = MS_UNITS.MS_METERS;
                    }
                }

                if (target != null)
                    target.RaisePropertyChanged(this);
                RefreshView();

                TreeNode src = CurrentTree.SelectedNode;
                if (src != null && src.Tag != null)
                    RaiseItemSelect((MapObjectHolder)src.Tag);
                UpdateToolbar();
            }
        }

        /// <summary>
        /// Click event handler of the autoStyleToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void autoStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoStyleToolStripMenuItem.Checked = !autoStyleToolStripMenuItem.Checked;
            layerObj layer = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                
            if (autoStyleToolStripMenuItem.Checked == false)
            {
                // removing the text property from the classes to allow LABELITEM to work
                for (int i = 0; i < layer.numclasses; i++)
                {
                    layer.getClass(i).setText("");
                }
                // initialize the default label
                MapUtils.InitializeDefaultLabel(layer);
                // allow the labelcache to be the default
                layer.labelcache = mapscript.MS_ON;
                layer.styleitem = null;
            }
            else
                layer.styleitem = "AUTO";

            RefreshView();

            if (target != null)
                target.RaisePropertyChanged(this);
        }

        /// <summary>
        /// BeforeLabelEdit event handler of the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!enableLabelEdit)
            {
                e.CancelEdit = true;
                return;
            }

            enableLabelEdit = false;
            
            if (CurrentTree.SelectedNode != null)
            {
                MapObjectHolder current = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                if (current.GetType() != typeof(mapObj) && current.GetType() != typeof(layerObj)
                     && current.GetType() != typeof(classObj))
                    e.CancelEdit = true;
            }         
        }

        /// <summary>
        /// AfterLabelEdit event handler of the treeView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                return;
            }
            
            if (CurrentTree.SelectedNode != null)
            {
                string label = e.Label;
                
                MapObjectHolder current = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                if (current.GetType() == typeof(mapObj))
                {
                    mapObj map = current;
                    if (map.name != label)
                    {
                        map.name = label;
                        current.RaisePropertyChanged(current);
                    }
                }
                else if (current.GetType() == typeof(layerObj))
                {
                    label = MapUtils.GetUniqueLayerName(map, label, 0);
                    layerObj layer = current;
                    if (layer.name != label)
                    {
                        layer.name = label;
                        current.RaisePropertyChanged(current);
                    }
                }
                else if (current.GetType() == typeof(classObj))
                {
                    classObj classobj = current;
                    if (classobj.name != label)
                    {
                        classobj.name = label;
                        current.RaisePropertyChanged(current);
                    }
                }
            }
        }

        /// <summary>
        /// Click event handler of the renameToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTree.SelectedNode != null)
            {
                enableLabelEdit = true;
                CurrentTree.SelectedNode.BeginEdit();
            }
        }

        /// <summary>
        /// Checks whether the specified treeview displays a style library
        /// </summary>
        /// <param name="tree">The treeview</param>
        /// <returns></returns>
        private bool IsStyleLibrary(TreeView tree)
        {
            if (tree.Tag != null)
                return Convert.ToBoolean(tree.Tag);
            else
                return false;
        }

        /// <summary>
        /// Get and sets wheteher to operate in an SL form or not
        /// </summary>
        public bool IsStyleLibraryControl
        {
            get
            {
                return IsStyleLibrary(CurrentTree);
            }
            set
            {
                treeView.Tag = value;
                treeView2.Tag = value;
            }
        }

        /// <summary>
        /// Add the layers of an entire mapfile
        /// </summary>
        /// <param name="mapfile">Path to the mapfile should be opened</param>
        private void AddMapFileLayers(string mapfile)
        {
            try
            {
                layerObj currentLayer = null;
                if (CurrentTree.SelectedNode != null)
                {
                    MapObjectHolder selected = (MapObjectHolder)CurrentTree.SelectedNode.Tag;
                    if (selected.GetType() == typeof(layerObj))
                        currentLayer = selected;
                    else if (selected.GetType() == typeof(classObj))
                        currentLayer = selected.GetParent();
                    else if (selected.GetType() == typeof(styleObj))
                        currentLayer = selected.GetParent().GetParent();
                    else if (selected.GetType() == typeof(labelObj))
                        currentLayer = selected.GetParent().GetParent();
                }

                
                mapObj map2 = new mapObj(mapfile);
                for (int i = 0; i < map2.numlayers; i++)
                {

                    layerObj layer = map2.getLayer(i).clone();
                    layer.status = mapscript.MS_OFF;
                    if (currentLayer != null)
                    {
                        if (currentLayer.index < map.numlayers -1)
                            map.insertLayer(layer, currentLayer.index + 1);
                        else
                            map.insertLayer(layer, -1);
 
                        currentLayer = layer;
                    }
                    else
                    {
                        map.insertLayer(layer, -1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Click event handler of the addMapFileToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addMapFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select the mapfile which contains the layer(s) to be added";
            openFileDialog.Filter = "MapServer mapfiles|*.map";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog.FileNames)
                    AddMapFileLayers(fileName);
                if (target != null)
                    target.RaisePropertyChanged(this);
                RefreshView();

                TreeNode source = CurrentTree.SelectedNode;
                if (source != null && source.Tag != null)
                    RaiseItemSelect((MapObjectHolder)source.Tag);
                UpdateToolbar();
            }
        }

        /// <summary>
        /// Click event handler of the addGraticuleLayerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void addGraticuleLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGraticuleLayerForm form = new AddGraticuleLayerForm(map);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (target != null)
                    target.RaisePropertyChanged(this);
                RefreshView();
            }
        }
    }
}
