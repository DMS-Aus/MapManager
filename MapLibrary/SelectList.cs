using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;
using System.IO;
using System.Diagnostics;

namespace DMS.MapLibrary
{
    /// <summary>
    /// User control to display the list of the selected features
    /// </summary>
    public partial class SelectList : UserControl, IMapControl
    {
        private MapObjectHolder target;
        private mapObj map;

        /// <summary>
        /// The signature of the GoToLayerText event handler. Raised when the Go To Layer menu is selected.
        /// </summary>
        public delegate void GoToLayerTextEventHandler(object sender, layerObj layer, int classindex);

        /// <summary>
        /// The GoToLayerText event handler. Raised when the Go To Layer menu is selected.
        /// </summary>
        public event GoToLayerTextEventHandler GoToLayerText;

        /// <summary>
        /// Event handler to sign that the position (map center) has been changed.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        /// <param name="x">Current x position in map coordinates</param>
        /// <param name="y">Current y position in map coordinates</param>
        public delegate void PositionChangedEventHandler(object sender, double x, double y);

        /// <summary>
        /// The PositionChanged event object.
        /// </summary>
        public event PositionChangedEventHandler PositionChanged;

        /// <summary>
        /// Constructs a new SelectList object.
        /// </summary>
        public SelectList()
        {
            InitializeComponent();
        }

        #region IMapControl Members

        /// <summary>
        /// Refresh the controls according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            listView.Items.Clear();
                
            if (map != null)
            {
                // setting up the icon background colors createLegendIcon
                // will take over the legend imagecolor setting from the map object 
                int red = map.legend.imagecolor.red;
                int green = map.legend.imagecolor.green;
                int blue = map.legend.imagecolor.blue;
                map.legend.imagecolor.red = this.BackColor.R;
                map.legend.imagecolor.green = this.BackColor.G;
                map.legend.imagecolor.blue = this.BackColor.B;
                listView.BackColor = this.BackColor;
                int layerIndex = -1;
                int classIndex = -1;

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

                    imageList.Images.Clear();
                    imageList.ImageSize = new Size(30, 20);

                    try
                    {
                        for (int i = 0; i < map.numlayers; i++)
                        {
                            layerObj layer = map.getLayer(i);
                            if (layer.status != mapscript.MS_OFF)
                            {
                                resultObj res;
                                shapeObj feature;
                                using (resultCacheObj results = layer.getResults())
                                {
                                    if (results != null && results.numresults > 0)
                                    {                                        
                                        // extracting the features found
                                        for (int j = 0; j < results.numresults; j++)
                                        {
                                            res = results.getResult(j);
                                            feature = layer.getShape(res);
                                            if (feature != null)
                                            {
                                                if (layerIndex != layer.index || classIndex != res.classindex)
                                                {
                                                    layerIndex = layer.index;
                                                    classIndex = res.classindex;
                                                    if (classIndex < 0)
                                                        classIndex = 0;
                                                    // creating the icon for this class
                                                    using (classObj def_class = new classObj(null)) // for drawing legend images
                                                    {
                                                        using (imageObj image = def_class.createLegendIcon(map, layer, 30, 20))
                                                        {
                                                            // drawing the class icons
                                                            if (layer.numclasses > classIndex)
                                                                layer.getClass(classIndex).drawLegendIcon2(map, layer, 20, 10, image, 5, 5);
                                                            byte[] img = image.getBytes();
                                                            using (MemoryStream ms = new MemoryStream(img))
                                                            {
                                                                imageList.Images.Add(Image.FromStream(ms));
                                                            }
                                                        }
                                                    }
                                                }

                                                ListViewItem item = new ListViewItem(layer.name, imageList.Images.Count - 1);
                                                item.SubItems.Add(feature.index.ToString());
                                                item.SubItems.Add(MapUtils.GetShapeTypeName((MS_SHAPE_TYPE)feature.type));
                                                listView.Items.Add(item);

                                                StringBuilder s = new StringBuilder("");
                                                s.AppendLine("Feature Properties:");
                                                for (int k = 0; k < layer.numitems; k++)
                                                {
                                                    s.Append(layer.getItem(k));
                                                    s.Append(" = ");
                                                    s.AppendLine(feature.getValue(k));
                                                }
                                                item.Tag = s.ToString();
                                                item.SubItems[0].Tag = layer;
                                                feature.classindex = res.classindex;
                                                item.SubItems[1].Tag = feature;
                                            }
                                        }
                                    }
                                }
                            }
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

                    listView.SmallImageList = imageList;
                }
                if (listView.Items.Count > 0)
                    listView.Items[0].Selected = true;
                else
                    richTextBox.Text = "";
            }
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
                if (value != null)
                {
                    if (value.GetType() == typeof(mapObj))
                    {
                        map = value;
                        target = value;
                        target.SelectionChanged += new EventHandler(target_SelectionChanged);
                    }
                }
                this.RefreshView();
            }
        }      

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties = delegate { };

        #endregion

        /// <summary>
        /// SelectionChanged event handler of the target control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void target_SelectionChanged(object sender, EventArgs e)
        {
            this.RefreshView();
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the listView control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
                richTextBox.Text = listView.SelectedItems[0].Tag.ToString();
            else
                richTextBox.Text = "";
        }

        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void centerToShapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0 && PositionChanged != null)
            {
                shapeObj shape = (shapeObj)listView.SelectedItems[0].SubItems[1].Tag;
                pointObj pos = shape.getCentroid();
                PositionChanged(this, pos.x, pos.y);
            }
        }

        private void goToClassTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0 && GoToLayerText != null)
            {
                layerObj layer = (layerObj)listView.SelectedItems[0].SubItems[0].Tag;
                shapeObj shape = (shapeObj)listView.SelectedItems[0].SubItems[1].Tag;
                GoToLayerText(this, layer, shape.classindex);
            }               
        }

        private void goToLayerTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0 && GoToLayerText != null)
            {
                layerObj layer = (layerObj)listView.SelectedItems[0].SubItems[0].Tag;
                GoToLayerText(this, layer, -1);
            }
        }
    }
}
