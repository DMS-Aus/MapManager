using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using OSGeo.MapServer;
using System.IO;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a UserControl for editing the MapScript layerObj parameters.
    /// </summary>
    public partial class LayerPropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private MapObjectHolder OriginalTarget;
        private layerObj layer;
        private bool dirtyFlag;
        ProjectionBrowserDialog projDialog;
        
        public new event HelpEventHandler HelpRequested;
        
        /// <summary>
        /// Constructs a new LayerPropertyEditor object.
        /// </summary>
        public LayerPropertyEditor()
        {
            InitializeComponent();
            // setting up the backcolor of the tab pages
            tabPageGeneral.BackColor =
            tabPageData.BackColor =
            tabPageStyle.BackColor = this.BackColor;
            dirtyFlag = false;
        }

        /// <summary>
        /// Update the Enabled state of the controls.
        /// </summary>
        private void UpdateControlState()
        {
            this.buttonEditDefaultLabel.Enabled = false;
            this.buttonEditDefaultStyle.Enabled = false;
            this.buttonEditDefaultClass.Enabled = false;
            this.comboBoxLabelItem.Enabled = false;
            this.textBoxLabelMinScale.Enabled = false;
            this.textBoxLabelMaxScale.Enabled = false;
            this.checkBoxLabelCache.Enabled = false;
            this.checkBoxAutoStyle.Enabled = false;
            this.textBoxFilter.Enabled = false;
            this.textBoxSymbolScale.Enabled = false;
            this.textBoxData.Enabled = false;
            this.textBoxConnection.Enabled = false;
            this.buttonConnection.Enabled = false;
            this.buttonData.Enabled = false;
            this.buttonProjection.Enabled = false;
            this.checkBoxDisplayRange.Enabled = false;
            this.checkBoxVisible.Enabled = false;
            this.checkBoxQueryable.Enabled = false;
            this.trackBarOpacity.Enabled = false;
            this.comboBoxPlugin.Enabled = false;
            this.comboBoxConnectionType.Enabled = false;
            this.comboBoxType.Enabled = false;
            this.textBoxProcessing.Enabled = false;
            this.labelOpacityPercent.Enabled = false;

            // checkBoxDisplayRange dependent controls
            comboBoxDisplayRange.Enabled = textBoxMinZoom.Enabled = textBoxMaxZoom.Enabled =
                labelMaxZoom.Enabled = labelMinZoom.Enabled = labelUnitMaxZoom.Enabled =
                labelUnitMinZoom.Enabled = textBoxMinScale.Enabled = textBoxMaxScale.Enabled = 
                buttonMinScale.Enabled = buttonMaxScale.Enabled = false;

            if (layer != null)
            {
                this.textBoxData.Enabled = true;
                this.textBoxConnection.Enabled = true;
                this.buttonConnection.Enabled = true;
                this.buttonData.Enabled = true;
                this.buttonProjection.Enabled = true;
                this.checkBoxDisplayRange.Enabled = true;
                this.checkBoxVisible.Enabled = true;
                this.checkBoxQueryable.Enabled = true;
                this.comboBoxConnectionType.Enabled = true;
                this.comboBoxType.Enabled = true;
                this.textBoxProcessing.Enabled = true;
                
                if (comboBoxType.SelectedValue != null && 
                    (MS_LAYER_TYPE)comboBoxType.SelectedValue != MS_LAYER_TYPE.MS_LAYER_RASTER)
                {
                    this.checkBoxAutoStyle.Enabled = true;
                    this.textBoxFilter.Enabled = true;
                    this.textBoxSymbolScale.Enabled = true;

                    if (layer.numclasses >= 1)
                    {
                        if (!checkBoxAutoStyle.Checked)
                        {
                            this.buttonEditDefaultClass.Enabled = true;
                            this.buttonEditDefaultLabel.Enabled = true;
                            this.buttonEditDefaultStyle.Enabled = true;
                            this.comboBoxLabelItem.Enabled = true;
                            this.textBoxLabelMinScale.Enabled = true;
                            this.textBoxLabelMaxScale.Enabled = true;
                            this.checkBoxLabelCache.Enabled = true;
                            if (layer.map != null)
                            {
                                this.buttonLabelMaxScale.Enabled = true;
                                this.buttonLabelMinScale.Enabled = true;
                            }
                        }
                    }
                }

                if (comboBoxConnectionType.SelectedValue != null && 
                    (MS_CONNECTION_TYPE)comboBoxConnectionType.SelectedValue == MS_CONNECTION_TYPE.MS_WMS)
                {
                    this.textBoxData.Enabled = false;
                    this.comboBoxPlugin.Enabled = ((MS_CONNECTION_TYPE)comboBoxConnectionType.SelectedValue == MS_CONNECTION_TYPE.MS_PLUGIN);
                }

                comboBoxDisplayRange.Enabled = textBoxMinZoom.Enabled = textBoxMaxZoom.Enabled =
                labelMaxZoom.Enabled = labelMinZoom.Enabled = labelUnitMaxZoom.Enabled =
                labelUnitMinZoom.Enabled = textBoxMinScale.Enabled = 
                textBoxMaxScale.Enabled = checkBoxDisplayRange.Checked;

                if (checkBoxDisplayRange.Checked && layer.map != null)
                {
                    this.buttonMaxScale.Enabled = true;
                    this.buttonMinScale.Enabled = true;
                }

                trackBarOpacity.Enabled = labelOpacityPercent.Enabled = (layer.opacity != mapscript.MS_GD_ALPHA);
            }
        }

        /// <summary>
        /// Sets the modify flag of the editor.
        /// </summary>
        /// <param name="dirty">The modify flag to be set.</param>
        private void SetDirty(bool dirty)
        {
            dirtyFlag = dirty;
            if (dirty)
            {
                if (target != null)
                    target.RaisePropertyChanging(this);
            }
        }

        /// <summary>
        /// HelpRequested event handler of the LayerPropertyEditor control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="hlpevent">The parameters of the help event.</param>
        void LayerPropertyEditor_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (this.HelpRequested != null)
                this.HelpRequested(sender, hlpevent);
        }

        /// <summary>
        /// Override the HelpRequested Event
        /// </summary>
        /// <param name="hevent">The parameters of the help event.</param>
        protected override void OnHelpRequested(HelpEventArgs hevent)
        {
            LayerPropertyEditor_HelpRequested(this, hevent);
        }

        #region IPropertyEditor Members

        /// <summary>
        /// Cancel the pending changes in the underlying object.
        /// </summary>
        public void CancelEditing()
        {
        }

        /// <summary>
        /// Let the editor to update the modified values to the underlying object.
        /// </summary>
        public void UpdateValues()
        {
            if (layer == null)
                return;
            if (dirtyFlag)
            {
                MS_CONNECTION_TYPE connectiontype = layer.connectiontype;
                if (layer.connectiontype != (MS_CONNECTION_TYPE)comboBoxConnectionType.SelectedItem)
                {
                    // try to change the connection type
                    try
                    {
                        if ((MS_CONNECTION_TYPE)comboBoxConnectionType.SelectedItem == MS_CONNECTION_TYPE.MS_PLUGIN
                            && comboBoxPlugin.SelectedIndex >= 0)
                            layer.setConnectionType((int)comboBoxConnectionType.SelectedItem, comboBoxPlugin.Text);
                        else
                            layer.setConnectionType((int)comboBoxConnectionType.SelectedItem, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error setting the connection type, " + ex.Message,
                                "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // reverting to the old values
                        layer.connectiontype = connectiontype;
                        return;
                    }
                }

                if ((MS_LAYER_TYPE)comboBoxType.SelectedItem == MS_LAYER_TYPE.MS_LAYER_ANNOTATION &&
                        layer.type != MS_LAYER_TYPE.MS_LAYER_ANNOTATION)
                {
                    // remove the styles to prevent from drawing the shapes under the text
                    for (int i = 0; i < layer.numclasses; i++)
                    {
                        classObj c = layer.getClass(i);
                        while (c.numstyles > 0)
                            c.removeStyle(0);
                        // add a new empty style (#6616)
                        styleObj style = new styleObj(c);
                    }
                }

                MS_LAYER_TYPE type = layer.type;
                layer.type = (MS_LAYER_TYPE)comboBoxType.SelectedItem;

                string connection = layer.connection;
                if (textBoxConnection.Text == "" 
                    || layer.connectiontype == MS_CONNECTION_TYPE.MS_INLINE)
                    layer.connection = null;
                else
                    layer.connection = textBoxConnection.Text;

                string data = layer.data;
                if (textBoxData.Text == "" 
                    || layer.connectiontype == MS_CONNECTION_TYPE.MS_INLINE)
                    layer.data = null;
                else
                    layer.data = textBoxData.Text;

                // inline layers must have at least one shape
                if (layer.connectiontype == MS_CONNECTION_TYPE.MS_INLINE)
                    layer.addFeature(new shapeObj((int)MS_SHAPE_TYPE.MS_SHAPE_NULL));

                string filter = layer.getFilterString();
                if (textBoxFilter.Text == "")
                    layer.setFilter(null);
                else
                    layer.setFilter(textBoxFilter.Text);

                // at this point we try to open the layer
                try
                {
                    layer.open();
                    if (layer.getResults() == null)
                        layer.close(); // close only is no query results
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening layer, " + ex.Message,
                            "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // reverting to the old values
                    layer.connection = connection;
                    layer.data = data;
                    layer.type = type;
                    layer.setFilter(filter);
                    layer.connectiontype = connectiontype;
                    return;
                }

                // setting up the projection if it have been changed 
                //steph - it wasn't updating the coordsys-name if changing projection with same proj4 text but different display name
                string key = layer.getFirstMetaDataKey();
                string coordsys = "";
                while (key != null)
                {
                    if (key == "coordsys_name")
                    {
                        coordsys = layer.getMetaData("coordsys_name");
                        break;
                    }
                    key = layer.getNextMetaDataKey(key);
                }
                if (layer.getProjection() != this.textBoxProjection.Tag.ToString() || this.textBoxProjection.Text != coordsys)
                {
                    if (this.textBoxProjection.Tag.ToString().Trim().StartsWith("+"))
                    {
                        layer.setProjection(this.textBoxProjection.Tag.ToString());
                        layer.setMetaData("coordsys_name", this.textBoxProjection.Text);
                    }
                    else
                        layer.setProjection("+AUTO");
                }

                if (checkBoxVisible.CheckState == CheckState.Checked)
                    layer.status = mapscript.MS_ON;
                else
                    layer.status = mapscript.MS_OFF;

                if (checkBoxQueryable.CheckState == CheckState.Checked)
                    layer.template = "query";
                else
                {
                    layer.template = null;
                    for (int c = 0; c < layer.numclasses; c++)
                        layer.getClass(c).template = null;
                }

                layer.maxscaledenom = -1;
                layer.minscaledenom = -1;
                layer.maxgeowidth = -1;
                layer.mingeowidth = -1;
                if (checkBoxDisplayRange.Checked)
                {
                    if (comboBoxDisplayRange.SelectedIndex == 0)
                    {
                        if (textBoxMaxScale.Text == "")
                            layer.maxscaledenom = -1;
                        else
                            layer.maxscaledenom = double.Parse(textBoxMaxScale.Text);

                        if (textBoxMinScale.Text == "")
                            layer.minscaledenom = -1;
                        else
                            layer.minscaledenom = double.Parse(textBoxMinScale.Text);
                    }
                    else
                    {
                        if (layer.map != null)
                        {
                            MS_UNITS mapunits = layer.map.units;
                            
                            if (textBoxMaxZoom.Text == "")
                                layer.maxgeowidth = -1;
                            else
                                layer.maxgeowidth = double.Parse(textBoxMaxZoom.Text);
                            if (textBoxMinZoom.Text == "")
                                layer.mingeowidth = -1;
                            else
                                layer.mingeowidth = double.Parse(textBoxMinZoom.Text);
                        }
                        else
                        {
                            if (textBoxMaxZoom.Text == "")
                                layer.maxgeowidth = -1;
                            else
                                layer.maxgeowidth = double.Parse(textBoxMaxZoom.Text);
                            if (textBoxMinZoom.Text == "")
                                layer.mingeowidth = -1;
                            else
                                layer.mingeowidth = double.Parse(textBoxMinZoom.Text);
                        }
                    }
                }

                if (layer.opacity != mapscript.MS_GD_ALPHA)
                    layer.opacity = trackBarOpacity.Value;

                // labelling  & style tab
                if (comboBoxLabelItem.SelectedItem.ToString() == "(no label)")
                    layer.labelitem = null;
                else
                {
                    layer.labelitem = comboBoxLabelItem.SelectedItem.ToString();
                    // creating default labels for each class
                    for (int c = 0; c < layer.numclasses; c++)
                    {
                        classObj classobj = layer.getClass(c);
                        if (classobj.numlabels <= 0)
                        {
                            // adding an empty label to this class
                            classobj.addLabel(new labelObj());
                            labelObj label = classobj.getLabel(0);
                            MapUtils.SetDefaultLabel(label, layer.map);
                        }
                    }
                }

                if (textBoxLabelMaxScale.Text == "")
                    layer.labelmaxscaledenom = -1;
                else
                    layer.labelmaxscaledenom = double.Parse(textBoxLabelMaxScale.Text);

                if (textBoxLabelMinScale.Text == "")
                    layer.labelminscaledenom = -1;
                else
                    layer.labelminscaledenom = double.Parse(textBoxLabelMinScale.Text);

                if (checkBoxAutoStyle.Checked && (string.Compare(layer.styleitem, "AUTO", true) != 0))
                    layer.styleitem = "AUTO";
                else if (!checkBoxAutoStyle.Checked && (string.Compare(layer.styleitem, "AUTO", true) == 0))
                {
                    layer.styleitem = null;
                    // set default color (#4337)
                    //styleObj style = layer.getClass(0).getStyle(0);
                    //MapUtils.SetDefaultColor(layer.type, style);
                }

                if (checkBoxLabelCache.Checked)
                    layer.labelcache = mapscript.MS_ON;
                else
                    layer.labelcache = mapscript.MS_OFF;

                if (layer.type == MS_LAYER_TYPE.MS_LAYER_POLYGON &&
                    type != MS_LAYER_TYPE.MS_LAYER_POLYGON &&
                    layer.styleitem == null)
                {
                    // the type have been changed to polygon
                    styleObj style = layer.getClass(0).getStyle(0);
                    MapUtils.SetDefaultColor(layer.type, style);
                }

                if (textBoxSymbolScale.Text == "")
                    layer.symbolscaledenom = -1;
                else
                    layer.symbolscaledenom = double.Parse(textBoxSymbolScale.Text);

                // processing tab
                layer.clearProcessing();
                string[] processing = textBoxProcessing.Text.Split(new char[] { '\n', '\r' });
                for (int i = 0; i < processing.Length; i++)
                {
                    string[] directive = processing[i].Trim().Split(new char[] { '=' });
                    if (directive.Length > 1)
                        layer.setProcessingKey(directive[0], directive[1]); // don't add duplicates
                }

                if (layer.name != this.textBoxName.Text)
                    layer.name = this.textBoxName.Text;

                if (textBoxLink.Text.Length > 0)
                    layer.setMetaData("link", textBoxLink.Text);
                else
                {
                    // remove previous setting
                    string key2;
                    while ((key2 = MapUtils.FindMetadata(layer, "link")) != null)
                        layer.removeMetaData(key2);
                }
           
                if (target != null)
                    target.RaisePropertyChanged(this);
                SetDirty(false);
            }
            else
                CancelEditing();
        }

        /// <summary>
        /// Returns the modified state of the Editor.
        /// </summary>
        /// <returns>The current modified state.</returns>
        public bool IsDirty()
        {
            return dirtyFlag;
        }

        #endregion

        #region IMapControl Members

        /// <summary>
        /// Refresh the controls according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            if (layer == null)
                return;

            CancelEditing();

            // general tab
            this.textBoxName.Text = layer.name;

            // setting up the projection information
            this.textBoxProjection.Tag = layer.getProjection();
            this.textBoxProjection.Text = "";
            string key = layer.getFirstMetaDataKey();
            while (key != null)
            {
                if (key == "coordsys_name")
                {
                    this.textBoxProjection.Text = layer.getMetaData("coordsys_name");
                    break;
                }
                key = layer.getNextMetaDataKey(key);
            }
            if (this.textBoxProjection.Text == "")
            {
                string proj4;
                int epsg;
                this.textBoxProjection.Text = MapUtils.FindProjection(this.textBoxProjection.Tag.ToString(), out proj4, out epsg);
            }
            
            // data tab
            comboBoxPlugin.Items.Clear();
            if (File.Exists(Application.StartupPath + "\\msplugin_mssql2008.dll"))
                comboBoxPlugin.Items.Add("msplugin_mssql2008.dll");
            if (File.Exists(Application.StartupPath + "\\msplugin_oracle.dll"))
                comboBoxPlugin.Items.Add("msplugin_oracle.dll");
            if (File.Exists(Application.StartupPath + "\\msplugin_sde_91.dll"))
                comboBoxPlugin.Items.Add("msplugin_sde_91.dll");
           
            comboBoxConnectionType.Items.Clear();
            comboBoxConnectionType.Items.AddRange(new object[] { MS_CONNECTION_TYPE.MS_GRATICULE, MS_CONNECTION_TYPE.MS_INLINE, MS_CONNECTION_TYPE.MS_OGR, MS_CONNECTION_TYPE.MS_PLUGIN, MS_CONNECTION_TYPE.MS_POSTGIS,
            MS_CONNECTION_TYPE.MS_RASTER, MS_CONNECTION_TYPE.MS_TILED_SHAPEFILE, MS_CONNECTION_TYPE.MS_SHAPEFILE, MS_CONNECTION_TYPE.MS_WFS,
            MS_CONNECTION_TYPE.MS_WMS});

            if (layer.connectiontype != MS_CONNECTION_TYPE.MS_ORACLESPATIAL || layer.connectiontype != MS_CONNECTION_TYPE.MS_SDE)
                comboBoxConnectionType.SelectedItem = (MS_CONNECTION_TYPE)layer.connectiontype;

            // setting the raster connection type if it's not initialized yet
            if (layer.type == MS_LAYER_TYPE.MS_LAYER_RASTER && layer.connectiontype != MS_CONNECTION_TYPE.MS_WMS &&
                layer.connectiontype != MS_CONNECTION_TYPE.MS_RASTER)
                comboBoxConnectionType.SelectedItem = MS_CONNECTION_TYPE.MS_RASTER;

            comboBoxType.DataSource = Enum.GetValues(typeof(MS_LAYER_TYPE));
            comboBoxType.SelectedItem = (MS_LAYER_TYPE)layer.type;
            textBoxConnection.Text = layer.connection;
            textBoxData.Text = layer.data;
            textBoxFilter.Text = layer.getFilterString();
            // display tab
            if (layer.status == mapscript.MS_OFF)
                checkBoxVisible.CheckState = CheckState.Unchecked;
            else
                checkBoxVisible.CheckState = CheckState.Checked;

            checkBoxQueryable.Checked = (layer.template != null && layer.template.Length > 0);
            if (!checkBoxQueryable.Checked)
            {
                // searching for a subclass which is queryable
                for (int c = 0; c < layer.numclasses; c++)
                {
                    classObj classobj = layer.getClass(c);
                    if ((classobj.template != null && classobj.template.Length > 0))
                    {
                        checkBoxQueryable.Checked = true;
                        break;
                    }
                }
            }

            comboBoxDisplayRange.SelectedIndex = -1;
            if (layer.minscaledenom >= 0 || layer.maxscaledenom >= 0)
            {
                checkBoxDisplayRange.Checked = true;
                comboBoxDisplayRange.SelectedIndex = 0;
            }
            else if (layer.mingeowidth >= 0 || layer.maxgeowidth >= 0)
            {
                checkBoxDisplayRange.Checked = true;
                comboBoxDisplayRange.SelectedIndex = 1;
            }
            else
            {
                checkBoxDisplayRange.Checked = false;
                comboBoxDisplayRange.SelectedIndex = 0;
            }

            if (layer.minscaledenom >= 0)
                textBoxMinScale.Text = layer.minscaledenom.ToString();
            else
                textBoxMinScale.Text = "";
            if (layer.maxscaledenom >= 0)
                textBoxMaxScale.Text = layer.maxscaledenom.ToString();
            else
                textBoxMaxScale.Text = "";

            textBoxMinZoom.Text = "";
            textBoxMaxZoom.Text = "";
            if (layer.map != null)
            {
                MS_UNITS mapunits = layer.map.units;
                
                if (layer.mingeowidth >= 0)
                    textBoxMinZoom.Text = Math.Round(layer.mingeowidth, MapUtils.GetUnitPrecision(mapunits)).ToString();
                if (layer.maxgeowidth >= 0)
                    textBoxMaxZoom.Text = Math.Round(layer.maxgeowidth, MapUtils.GetUnitPrecision(mapunits)).ToString();
            }
            else
            {
                if (layer.mingeowidth >= 0)
                    textBoxMinZoom.Text = layer.mingeowidth.ToString();
                if (layer.maxgeowidth >= 0)
                    textBoxMaxZoom.Text = layer.maxgeowidth.ToString();
            }

            if (layer.opacity == mapscript.MS_GD_ALPHA)
                trackBarOpacity.Value = 100;
            else
                trackBarOpacity.Value = layer.opacity;

            labelOpacityPercent.Text = trackBarOpacity.Value + "%";

            // labelling & style tab
            comboBoxLabelItem.Items.Clear();
            comboBoxLabelItem.Items.Add("(no label)");
            
            try
            {
                layer.open();
                for (int i = 0; i < layer.numitems; i++)
                    comboBoxLabelItem.Items.Add(layer.getItem(i));
                if (layer.getResults() == null)
                    layer.close(); // close only is no query results
            }
            catch (Exception ex)
            {
                EventProvider.RaiseEventMessage(this, ex.Message, EventProvider.EventTypes.Error);
            }
            
            if (layer.connectiontype == MS_CONNECTION_TYPE.MS_OGR)
            {
                comboBoxLabelItem.Items.Add("OGR:LabelFont");
                comboBoxLabelItem.Items.Add("OGR:LabelSize");
                comboBoxLabelItem.Items.Add("OGR:LabelText");
                comboBoxLabelItem.Items.Add("OGR:LabelAngle");
                comboBoxLabelItem.Items.Add("OGR:LabelFColor");
                comboBoxLabelItem.Items.Add("OGR:LabelBColor");
                comboBoxLabelItem.Items.Add("OGR:LabelPlacement");
                comboBoxLabelItem.Items.Add("OGR:LabelAnchor");
                comboBoxLabelItem.Items.Add("OGR:LabelDx");
                comboBoxLabelItem.Items.Add("OGR:LabelDy");
                comboBoxLabelItem.Items.Add("OGR:LabelPerp");
                comboBoxLabelItem.Items.Add("OGR:LabelBold");
                comboBoxLabelItem.Items.Add("OGR:LabelItalic");
                comboBoxLabelItem.Items.Add("OGR:LabelUnderline");
                comboBoxLabelItem.Items.Add("OGR:LabelPriority");
                comboBoxLabelItem.Items.Add("OGR:LabelStrikeout");
                comboBoxLabelItem.Items.Add("OGR:LabelStretch");
                comboBoxLabelItem.Items.Add("OGR:LabelAdjHor");
                comboBoxLabelItem.Items.Add("OGR:LabelAdjVert");
                comboBoxLabelItem.Items.Add("OGR:LabelHColor");
                comboBoxLabelItem.Items.Add("OGR:LabelOColor");
            }
            if (layer.labelitem == null)
                comboBoxLabelItem.SelectedItem = "(no label)";
            else
                comboBoxLabelItem.SelectedItem = layer.labelitem;

            if (layer.labelminscaledenom >= 0)
                textBoxLabelMinScale.Text = layer.labelminscaledenom.ToString();
            else
                textBoxLabelMinScale.Text = "";
            if (layer.labelmaxscaledenom >= 0)
                textBoxLabelMaxScale.Text = layer.labelmaxscaledenom.ToString();
            else
                textBoxLabelMaxScale.Text = "";

            checkBoxAutoStyle.Checked = (string.Compare(layer.styleitem, "AUTO", true) == 0);
            checkBoxLabelCache.Checked = (layer.labelcache == mapscript.MS_ON);

            if (layer.symbolscaledenom >= 0)
                textBoxSymbolScale.Text = layer.symbolscaledenom.ToString();
            else
                textBoxSymbolScale.Text = "";

            // processing tab
            StringBuilder processing = new StringBuilder();
            for (int i = 0; i < layer.numprocessing; i++)
                processing.AppendLine(layer.getProcessing(i));

            textBoxProcessing.Text = processing.ToString();

            if (MapUtils.HasMetadata(layer, "link"))
                textBoxLink.Text = layer.getMetaData("link");
            else
                textBoxLink.Text = "";
            
            SetDirty(false);
        }

        /// <summary>
        /// Update the related controls according to the DisplayRange selection.
        /// </summary>
        /// <param name="setting">The current DisplayRange setting.</param>
        private void UpdateDisplayRange(int setting)
        {
            comboBoxDisplayRange.SelectedIndex = setting;
            if (setting == 0)
            {
                labelMinZoom.Text = "Closest scale: 1:";
                labelMaxZoom.Text = "Farthest scale: 1:";
                labelUnitMaxZoom.Text = labelUnitMinZoom.Text = "";
                textBoxMinZoom.Visible = textBoxMaxZoom.Visible = false;
                textBoxMinScale.Visible = textBoxMaxScale.Visible = true;
                toolTip.SetToolTip(buttonMinScale, "Set Map Scale");
                toolTip.SetToolTip(buttonMaxScale, "Set Map Scale");
            }
            else
            {
                labelMinZoom.Text = "Zoom is at least:";
                labelMaxZoom.Text = "Zoom is less than:";
                if (layer.map != null)
                    labelUnitMaxZoom.Text = labelUnitMinZoom.Text = MapUtils.GetUnitName(layer.map.units);
                textBoxMinZoom.Visible = textBoxMaxZoom.Visible = true;
                textBoxMinScale.Visible = textBoxMaxScale.Visible = false;
                toolTip.SetToolTip(buttonMinScale, "Set Map Zoom");
                toolTip.SetToolTip(buttonMaxScale, "Set Map Zoom");
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
                CancelEditing();

                if (value != null)
                {
                    OriginalTarget = value;
                    if (value.GetType() == typeof(layerObj))
                    {
                        layer = value;
                        target = value;
                    }
                    else if (value.GetType() == typeof(classObj))
                    {
                        target = value.GetParent();
                        layer = target;
                    }
                    else if (value.GetType() == typeof(styleObj))
                    {
                        if (value.GetParent().GetType() == typeof(labelObj))
                            target = value.GetParent().GetParent().GetParent();
                        else
                            target = value.GetParent().GetParent();

                        layer = target;
                    }
                    else if (value.GetType() == typeof(labelObj))
                    {
                        target = value.GetParent().GetParent();
                        layer = target;
                    }
                    else
                    {
                        throw new ApplicationException("Invalid object type.");
                    }

                    RefreshView();
                    UpdateControlState();
                    return;
                }
                layer = null;
                target = null;
                UpdateControlState();
            }
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties;

        #endregion

        /// <summary>
        /// Click event handler of the buttonProjection control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonProjection_Click(object sender, EventArgs e)
        {
            if (projDialog == null)
                projDialog = new ProjectionBrowserDialog();
            
            projDialog.HelpRequested +=new HelpEventHandler(LayerPropertyEditor_HelpRequested);
            projDialog.Projection = textBoxProjection.Text;
            if (projDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxProjection.Text = projDialog.Projection;
                textBoxProjection.Tag = projDialog.ProjectionNative;
            }
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxDisplayRange control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxDisplayRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplayRange(comboBoxDisplayRange.SelectedIndex);
            SetDirty(true);
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxDisplayRange control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxDisplayRange_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplayRange(comboBoxDisplayRange.SelectedIndex);
            UpdateControlState();
            SetDirty(true);
        }

        /// <summary>
        /// Validating event handler of the textBoxMinZoom control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxMinZoom_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (textBoxMinZoom.Text != "" && !double.TryParse(textBoxMinZoom.Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Validating event handler of the textBoxMaxZoom control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxMaxZoom_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (textBoxMaxZoom.Text != "" && !double.TryParse(textBoxMaxZoom.Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Click event handler of the buttonConnection control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonConnection_Click(object sender, EventArgs e)
        {
            if (comboBoxPlugin.SelectedItem != null &&
                comboBoxPlugin.SelectedItem.ToString() == "msplugin_mssql2008.dll")
            {
                SqlConnectionDialog cd = new SqlConnectionDialog();
                if (textBoxConnection.Text.Trim() != "")
                    cd.SetConnectionString(textBoxConnection.Text.Trim());
                if (textBoxData.Text.Trim() != "")
                    cd.SetDataString(textBoxData.Text.Trim());
                if (cd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        MapUtils.TestMSSQLConnection(cd.GetConnectionString(), cd.GetDataString());

                        textBoxConnection.Text = cd.GetConnectionString();
                        string data = cd.GetDataString();
                        if (data != null)
                            textBoxData.Text = data;

                        SetDirty(true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to open layer, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return;
            }
            
            if (layer.map != null)
                openFileDialog.InitialDirectory = layer.map.shapepath;
            if ((MS_CONNECTION_TYPE)comboBoxConnectionType.SelectedItem == MS_CONNECTION_TYPE.MS_OGR)
                openFileDialog.Filter = global::MapLibrary.Properties.Resources.OGR_FILE_TYPES;
            else
                openFileDialog.Filter = null;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxConnection.Text = openFileDialog.FileName;
                SetDirty(true);
            }
        }

        /// <summary>
        /// Click event handler of the buttonData control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonData_Click(object sender, EventArgs e)
        {
            if (comboBoxPlugin.SelectedItem != null &&
                comboBoxPlugin.SelectedItem.ToString() == "msplugin_mssql2008.dll")
            {
                SqlConnectionDialog cd = new SqlConnectionDialog();
                if (textBoxConnection.Text.Trim() != "")
                    cd.SetConnectionString(textBoxConnection.Text.Trim());
                if (textBoxData.Text.Trim() != "")
                    cd.SetDataString(textBoxData.Text.Trim());
                if (cd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        MapUtils.TestMSSQLConnection(cd.GetConnectionString(), cd.GetDataString());

                        textBoxConnection.Text = cd.GetConnectionString();
                        string data = cd.GetDataString();
                        if (data != null)
                            textBoxData.Text = data;

                        SetDirty(true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to open layer, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return;
            }
            
            if (layer.map != null)
                openFileDialog.InitialDirectory = layer.map.shapepath;
            openFileDialog.Filter = null;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxData.Text = openFileDialog.FileName;
                SetDirty(true);
            }
        }

        /// <summary>
        /// Common function to validate the double values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateDouble(object sender, CancelEventArgs e)
        {
            double result;
            if (((TextBoxBase)sender).Text != "" && !double.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Common function to sign that a value have been changed.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValueChanged(object sender, EventArgs e)
        {
            SetDirty(true);
            UpdateControlState();
        }

        /// <summary>
        /// Common function to sign that a value is about to change
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValueChanging(object sender, EventArgs e)
        {
            SetDirty(true);
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxPlugin control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDirty(true);
            // pre-populate the connection and data parameters
            if (comboBoxPlugin.SelectedItem.ToString() == "msplugin_mssql2008.dll")
            {
                textBoxConnection.Text = "Server=<Your server>;Database=<Your Database>;UID=<Your User>;PWD=<Your Password>";
                textBoxData.Text = "<Geometry Column> from <Table> USING UNIQUE <ID Column> USING SRID=<EPSG Code>";
            }
            else
            {
                textBoxConnection.Text = "";
                textBoxData.Text = "";
            }

        }

        /// <summary>
        /// Click event handler of the checkBoxAutoStyle control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxAutoStyle_Click(object sender, EventArgs e)
        {
            checkBoxAutoStyle.Checked = !checkBoxAutoStyle.Checked;
            // there's no need to report the change in this case
            dirtyFlag = true;

            if (checkBoxAutoStyle.Checked == false)
            {
                // removing the text property from the classes to allow LABELITEM to work
                for (int i = 0; i < layer.numclasses; i++)
                {
                    layer.getClass(i).setText("");
                }
                // initialize the default label
                MapUtils.InitializeDefaultLabel(layer);
                // allow the labelcache to be the default
                checkBoxLabelCache.Checked = true;
            }

            UpdateValues();
            UpdateControlState();
        }

        /// <summary>
        /// Scroll event handler of the trackBarOpacity control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void trackBarOpacity_Scroll(object sender, EventArgs e)
        {
            SetDirty(true);
            labelOpacityPercent.Text = trackBarOpacity.Value + "%";
        }

        /// <summary>
        /// Click event handler of the buttonEditDefaultLabel control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonEditDefaultLabel_Click(object sender, EventArgs e)
        {
            if (this.EditProperties != null)
            {
                // need to update the current changes
                if (IsDirty())
                {
                    UpdateValues();
                    UpdateControlState();
                }

                if (OriginalTarget.GetType() == typeof(labelObj))
                {
                    EditProperties(this, OriginalTarget);
                }
                else if (OriginalTarget.GetType() == typeof(styleObj))
                {
                    classObj classobj = OriginalTarget.GetParent();

                    if (classobj.numlabels <= 0)
                    {
                        // adding an empty label to this class
                        classobj.addLabel(new labelObj());
                        labelObj label = classobj.getLabel(0);
                        MapUtils.SetDefaultLabel(label, layer.map);
                    }
                    EditProperties(this, new MapObjectHolder(classobj.getLabel(0),
                        OriginalTarget.GetParent()));
                }
                else if (OriginalTarget.GetType() == typeof(classObj))
                {
                    classObj classobj = OriginalTarget;
                    if (classobj.numlabels <= 0)
                    {
                        // adding an empty label to this class
                        classobj.addLabel(new labelObj());
                        labelObj label = classobj.getLabel(0);
                        MapUtils.SetDefaultLabel(label, layer.map);     
                    }
                    EditProperties(this, new MapObjectHolder(classobj.getLabel(0), OriginalTarget));
                }
                else
                {
                    classObj classobj = layer.getClass(0);
                    if (classobj.numlabels <= 0)
                    {
                        // adding an empty label to this class
                        classobj.addLabel(new labelObj());
                        labelObj label = classobj.getLabel(0);
                        MapUtils.SetDefaultLabel(label, layer.map);
                    }
                    EditProperties(this, new MapObjectHolder(classobj.getLabel(0),
                                new MapObjectHolder(classobj, target)));
                }               
            }
        }

        /// <summary>
        /// Click event handler of the buttonEditDefaultStyle control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonEditDefaultStyle_Click(object sender, EventArgs e)
        {
            if (this.EditProperties != null)
            {
                // need to update the current changes
                if (IsDirty())
                {
                    UpdateValues();
                    UpdateControlState();
                }

                if (OriginalTarget.GetType() == typeof(styleObj))
                {
                    EditProperties(this, OriginalTarget);
                }
                else if (OriginalTarget.GetType() == typeof(classObj))
                {
                    classObj classobj = OriginalTarget;
                    if (classobj.numstyles <= 0)
                    {
                        // adding an empty style to this class
                        styleObj style = new styleObj(classobj);
                    }

                    EditProperties(this, new MapObjectHolder(classobj.getStyle(0), OriginalTarget));
                }
                else
                {
                    classObj classobj = layer.getClass(0);
                    if (classobj.numstyles <= 0)
                    {
                        // adding an empty style to this class
                        styleObj style = new styleObj(classobj);
                    }

                    EditProperties(this, new MapObjectHolder(classobj.getStyle(0),
                    new MapObjectHolder(classobj, target)));
                }
            }
        }

        /// <summary>
        /// Click event handler of the buttonEditDefaultClass control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonEditDefaultClass_Click(object sender, EventArgs e)
        {
            if (this.EditProperties != null)
            {
                // need to update the current changes
                if (IsDirty())
                {
                    UpdateValues();
                    UpdateControlState();
                }

                if (OriginalTarget.GetType() == typeof(classObj))
                    EditProperties(this, OriginalTarget);
                else
                    EditProperties(this, new MapObjectHolder(layer.getClass(0), target));
            }
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxVisible control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxVisible_CheckedChanged(object sender, EventArgs e)
        {
            SetDirty(true);
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxConnectionType control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDirty(true);
            comboBoxPlugin.Enabled = ((MS_CONNECTION_TYPE)comboBoxConnectionType.SelectedItem == MS_CONNECTION_TYPE.MS_PLUGIN);
            if (comboBoxPlugin.Enabled)
            {
                if (comboBoxPlugin.Items.Count > 0)
                {
                    comboBoxPlugin.SelectedIndex = 0;

                    foreach (object item in comboBoxPlugin.Items)
                    {
                        if (string.Compare(layer.plugin_library, item.ToString(), true) == 0)
                            comboBoxPlugin.SelectedItem = layer.plugin_library;
                    }
                }
            }
            else
                comboBoxPlugin.SelectedIndex = -1;
        }

        /// <summary>
        /// TextChanged event handler of the textBoxProcessing control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxProcessing_TextChanged(object sender, EventArgs e)
        {
            SetDirty(true);
        }

        /// <summary>
        /// Validating event handler of the textBoxName control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxName_Validating(object sender, CancelEventArgs e)
        {
            if (layer.name!= null && textBoxName.Text.Trim() == layer.name.Trim())
                return;  // nothing has changed
            
            // checking whether this name is unique or not
            if (layer.map != null)
            {
                string originalName = layer.name;
                layer.name = null;
                string versionedName = MapUtils.GetUniqueLayerName(layer.map, textBoxName.Text, 0);
                layer.name = originalName;

                if (textBoxName.Text.Trim() != versionedName)
                {
                    MessageBox.Show("Duplicated layer names are not supported by IntraMaps, the layer will be renamed to an unique name.", "MapManager",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                textBoxName.Text = versionedName;
            }
        }

        /// <summary>
        /// Click event handler of the buttonLabelMinScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonLabelMinScale_Click(object sender, EventArgs e)
        {
            if (layer.map != null)
                textBoxLabelMinScale.Text = Convert.ToInt64(layer.map.scaledenom).ToString();
        }

        /// <summary>
        /// Click event handler of the buttonLabelMaxScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonLabelMaxScale_Click(object sender, EventArgs e)
        {
            if (layer.map != null)
                textBoxLabelMaxScale.Text = Convert.ToInt64(layer.map.scaledenom).ToString();
        }

        /// <summary>
        /// Click event handler of the buttonMinScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMinScale_Click(object sender, EventArgs e)
        {
            if (layer.map != null)
            {
                mapObj map = layer.map;
                if (comboBoxDisplayRange.SelectedIndex == 0)
                {
                    textBoxMinScale.Text = Convert.ToInt64(map.scaledenom).ToString();
                }
                else
                {
                    MS_UNITS mapunits = layer.map.units;
                    textBoxMinZoom.Text = Math.Round(map.extent.maxx - map.extent.minx, MapUtils.GetUnitPrecision(mapunits)).ToString();
                }
            }
        }

        /// <summary>
        /// Click event handler of the buttonMaxScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMaxScale_Click(object sender, EventArgs e)
        {
            if (layer.map != null)
            {
                mapObj map = layer.map;
                if (comboBoxDisplayRange.SelectedIndex == 0)
                {
                    textBoxMaxScale.Text = Convert.ToInt64(map.scaledenom).ToString();
                }
                else
                {
                    MS_UNITS mapunits = layer.map.units;
                    textBoxMaxZoom.Text = Math.Round(map.extent.maxx - map.extent.minx, MapUtils.GetUnitPrecision(mapunits)).ToString();
                }
            }
        }

        
    }
}
