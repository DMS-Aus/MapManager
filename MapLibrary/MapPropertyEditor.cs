using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OSGeo.MapServer;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a UserControl for editing the MapScript mapObj parameters.
    /// </summary>
    public partial class MapPropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private mapObj map;
        private bool dirtyFlag;
        private bool dirtyFlagExtent;
        ProjectionBrowserDialog projDialog;
        public new event HelpEventHandler HelpRequested;

        private double zoomFactor = 1.0;
        int unitPrecision;
        MS_UNITS mapunits;
        
        /// <summary>
        /// Constructs a new MapPropertyEditor class.
        /// </summary>
        public MapPropertyEditor()
        {
            InitializeComponent();
            // setting up the backcolor of the tab pages
            tabPageGeneral.BackColor =
            tabPageExtent.BackColor =
            tabPageImage.BackColor =
            tabPageProjection.BackColor = this.BackColor;
            dirtyFlag = false;
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
        /// HelpRequested event handler of the MapPropertyEditor control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="hlpevent">The parameters of the help event.</param>
        void MapPropertyEditor_HelpRequested(object sender, HelpEventArgs hlpevent)
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
            MapPropertyEditor_HelpRequested(this, hevent);
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
            if (map == null)
                return;

            if (dirtyFlag)
            {
                dirtyFlag = false;
                // general tab
                if (map.name != this.textBoxName.Text)
                    map.name = this.textBoxName.Text;
                if (map.shapepath != this.textBoxShapePath.Text)
                    map.shapepath = this.textBoxShapePath.Text;
                if (map.web.imagepath != this.textBoxImagepath.Text)
                    map.web.imagepath = this.textBoxImagepath.Text;

                try
                {
                    map.setFontSet2(this.textBoxFontset.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    map.setSymbolSet2(this.textBoxSymbolset.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // image details tab
                this.colorPickerBackColor.ApplyTo(map.imagecolor);
                if (map.imagetype != comboBoxImageType.Text)
                    map.selectOutputFormat(comboBoxImageType.Text);

                map.resolution = Convert.ToDouble(this.textBoxResolution.Text);
                // coordinate space
                // need to recalculate the extent to point to the same visible area
                try
                {
                    // setting up the projection if it have been changed 
                    if (map.getProjection() != this.textBoxProjection.Tag.ToString())
                    {
                        if (map.getProjection() != "" && this.textBoxProjection.Tag.ToString() != "" &&
                            map.extent.minx < map.extent.maxx && map.extent.miny < map.extent.maxy)
                        {
                            using (projectionObj oldProj = new projectionObj(map.getProjection()))
                            {
                                using (projectionObj newProj = new projectionObj(this.textBoxProjection.Tag.ToString()))
                                {
                                    using (rectObj rect = new rectObj(map.extent.minx, map.extent.miny, map.extent.maxx, map.extent.maxy, 0))
                                    {
                                        rect.project(oldProj, newProj);
                                        map.units = (MS_UNITS)this.comboBoxUnits.SelectedItem;
                                        if (rect.minx < rect.maxx && rect.miny < rect.maxy)
                                        {
                                            map.setExtent(rect.minx, rect.miny, rect.maxx, rect.maxy);
                                            dirtyFlagExtent = true;
                                            UpdateExtentValues();
                                        }
                                    }
                                }
                            }
                        }

                        if (this.textBoxProjection.Tag.ToString().Trim().StartsWith("+"))
                        {
                            map.setProjection(this.textBoxProjection.Tag.ToString());
                            map.setMetaData("coordsys_name", this.textBoxProjection.Text);
                        }
                        else
                            map.setProjection("+AUTO");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to set projection value, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (checkBoxTransparent.Checked)
                {
                    map.outputformat.transparent = mapscript.MS_TRUE;
                    if (map.outputformat.imagemode == (int)MS_IMAGEMODE.MS_IMAGEMODE_RGB)
                        map.outputformat.imagemode = (int)MS_IMAGEMODE.MS_IMAGEMODE_RGBA;
                }
                else
                {
                    map.outputformat.transparent = mapscript.MS_FALSE;
                    if (map.outputformat.imagemode == (int)MS_IMAGEMODE.MS_IMAGEMODE_RGBA)
                        map.outputformat.imagemode = (int)MS_IMAGEMODE.MS_IMAGEMODE_RGB;
                }

                if (target != null && !dirtyFlagExtent)
                    target.RaisePropertyChanged(this);
                SetDirty(false);
            }
            if (dirtyFlagExtent)
            {
                ApplyExtent();
                dirtyFlagExtent = false;
            }
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
            if (map == null)
                return;
            // general tab
            this.textBoxName.Text = map.name;
            this.textBoxShapePath.Text = map.shapepath;
            this.textBoxImagepath.Text = map.web.imagepath;
            this.textBoxSymbolset.Text = map.symbolset.filename;
            this.textBoxFontset.Text = map.fontset.filename;
            // image details tab
            this.colorPickerBackColor.SetColor(map.imagecolor);
            comboBoxImageType.Items.Clear();
            comboBoxImageType.Items.AddRange(new object[] { "png", "jpeg", "gif", "png8", "png24", "pdf", "svg", "cairopng"
                , "GTiff", "kml", "kmz" });

            for (int i = 0; i < map.numoutputformats; i++)
            {
                outputFormatObj format = map.getOutputFormat(i);
                if (!comboBoxImageType.Items.Contains(format.name))
                    comboBoxImageType.Items.Add(format.name);
            }

            //outputFormatObj[] formats = map.outputformatlist;
            //for (int i = 0; i < formats.Length; i++)
            //{
            //    if (!comboBoxImageType.Items.Contains(formats[i].name))
            //        comboBoxImageType.Items.Add(formats[i].name);
            //}
            comboBoxImageType.SelectedItem = map.imagetype;
            this.textBoxResolution.Text = map.resolution.ToString();

            // setting up the projection information
            this.textBoxProjection.Tag = map.getProjection();
            this.textBoxProjection.Text = "";
            string key = map.getFirstMetaDataKey();
            while (key != null)
            {
                if (key == "coordsys_name")
                {
                    this.textBoxProjection.Text = map.getMetaData("coordsys_name");
                    break;
                }
                key = map.getNextMetaDataKey(key);
            }
            if (this.textBoxProjection.Text == "")
            {
                string proj4;
                int epsg;
                this.textBoxProjection.Text = MapUtils.FindProjection(this.textBoxProjection.Tag.ToString(), out proj4, out epsg);
            }

            comboBoxUnits.DataSource = Enum.GetValues(typeof(MS_UNITS));
            comboBoxUnits.SelectedItem = (MS_UNITS)map.units;

            checkBoxTransparent.Checked = (map.outputformat.transparent == mapscript.MS_TRUE && map.outputformat.imagemode == (int)MS_IMAGEMODE.MS_IMAGEMODE_RGBA);
            checkBoxTransparent.Enabled = (map.outputformat.imagemode == (int)MS_IMAGEMODE.MS_IMAGEMODE_RGB || map.outputformat.imagemode == (int)MS_IMAGEMODE.MS_IMAGEMODE_RGBA);

            // extent tab
            UpdateExtentValues();

            SetDirty(false);
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
                    if (value.GetType() != typeof(mapObj))
                        throw new ApplicationException("Invalid object type.");

                    map = value;
                    target = value;
                    RefreshView();
                    target.ZoomChanged += new MapObjectHolder.ZoomChangedEventHandler(target_ZoomChanged);
                    target.PositionChanged += new MapObjectHolder.PositionChangedEventHandler(target_PositionChanged);
                    return;
                }
                map = null;
                target = null;
            }
        }

        /// <summary>
        /// Event handler to sign that the position (map center) has been changed.
        /// </summary>
        /// <param name="sender">The source object of the event.</param>
        /// <param name="x">Current x position in map coordinates</param>
        /// <param name="y">Current y position in map coordinates</param>
        void target_PositionChanged(object sender, double x, double y)
        {
            bool dirtyFlagSave = dirtyFlag;
            UpdateExtentValues();
            dirtyFlagExtent = false;
            dirtyFlag = dirtyFlagSave;
            if (target != null)
                target.RaisePropertyChanging(this);
        }

        /// <summary>
        /// Zoom change event handler
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="zoom">Current zoom</param>
        /// <param name="scale">Current scale</param>
        void target_ZoomChanged(object sender, double zoom, double scale)
        {
            bool dirtyFlagSave = dirtyFlag;
            UpdateExtentValues();
            dirtyFlagExtent = false;
            dirtyFlag = dirtyFlagSave;
            if (target != null)
                target.RaisePropertyChanging(this);
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties;

        #endregion

        /// <summary>
        /// Common function to sign that a value have been changed.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValueChanged(object sender, EventArgs e)
        {
            SetDirty(true);
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
        /// Common function to sign that the extent is about to change
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ExtentChanging(object sender, EventArgs e)
        {
            dirtyFlagExtent = true;
            SetDirty(true);
        }

        /// <summary>
        /// Update the extent parameters according to the map.extent parameters.
        /// </summary>
        private void UpdateExtentValues()
        {
            if (comboBoxUnits.SelectedItem != null)
                mapunits = (MS_UNITS)comboBoxUnits.SelectedItem;
            else
                mapunits = map.units;

            unitPrecision = MapUtils.GetUnitPrecision(mapunits);
            textBoxX.Text = Convert.ToString(Math.Round((map.extent.maxx + map.extent.minx) / 2, unitPrecision));
            textBoxY.Text = Convert.ToString(Math.Round((map.extent.maxy + map.extent.miny) / 2, unitPrecision));
            labelUnit2.Text = MapUtils.GetUnitName(mapunits);
            textBoxScale.Text = Convert.ToString(Convert.ToInt64(map.scaledenom));

            unitPrecision = MapUtils.GetUnitPrecision(mapunits);
            labelUnit1.Text = MapUtils.GetUnitName(mapunits);

            double zoom = (map.extent.maxx - map.extent.minx);
            if (mapunits != map.units)
                zoom = zoom * MapUtils.InchesPerUnit(map.units) / MapUtils.InchesPerUnit(mapunits);

            textBoxZoomWidth.Text = Convert.ToString(Math.Round(zoom, unitPrecision));
        }

        private void ApplyExtent()
        {
            double deltaX = (map.extent.maxx - map.extent.minx) * zoomFactor / 2;
            double deltaY = (map.extent.maxy - map.extent.miny) * zoomFactor / 2;
            double centerX = double.Parse(textBoxX.Text);
            double centerY = double.Parse(textBoxY.Text);

            map.setExtent(centerX - deltaX, centerY - deltaY, centerX + deltaX, centerY + deltaY);
            if (textBoxRotation.Text != "")
                map.setRotation(double.Parse(textBoxRotation.Text));

            if (target != null)
                target.RaisePropertyChanged(this);

            double zoom = (map.extent.maxx - map.extent.minx);
            if (mapunits != map.units)
                zoom = zoom * MapUtils.InchesPerUnit(map.units) / MapUtils.InchesPerUnit(mapunits);

            if (target != null)
                target.RaiseZoomChanged(this, Math.Round(zoom, unitPrecision), map.scaledenom);

            zoomFactor = 1.0;
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
        /// SelectedIndexChanged event handler of the comboBoxUnits control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDirty(true);
            dirtyFlagExtent = true;
            UpdateExtentValues();
            //RaiseZoomChanged();
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxDistanceUnits control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxDistanceUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDirty(true);
            dirtyFlagExtent = true;
            UpdateExtentValues();
        }

        /// <summary>
        /// Click event handler of the buttonShapePath control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonShapePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = this.textBoxShapePath.Text;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxShapePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Click event handler of the buttonSymbolset control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonSymbolset_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select The Symbolset";
            openFileDialog.InitialDirectory = map.mappath;
            if (textBoxSymbolset.Text != "")
            {
                string dirName = Path.GetDirectoryName(textBoxSymbolset.Text);
                if (Directory.Exists(dirName))
                    openFileDialog.InitialDirectory = dirName;
            }
            openFileDialog.Filter = "Symbolset files|*.sym|All files|*.*";
            openFileDialog.FileName = Path.GetFileName(this.textBoxSymbolset.Text);
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {  
                this.textBoxSymbolset.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Click event handler of the buttonFontset control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonFontset_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select The Fontset";
            openFileDialog.InitialDirectory = map.mappath;
            if (textBoxFontset.Text != "")
            {
                string dirName = Path.GetDirectoryName(textBoxFontset.Text);
                if (Directory.Exists(dirName))
                    openFileDialog.InitialDirectory = dirName;
            }
            openFileDialog.Filter = "Fontset files|*.list;*.txt|All files|*.*";
            openFileDialog.FileName = Path.GetFileName(textBoxFontset.Text);
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxFontset.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Click event handler of the buttonImagepath control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonImagepath_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select the folder to store the temp images in";
            folderBrowserDialog.SelectedPath = this.textBoxImagepath.Text;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxImagepath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Click event handler of the buttonProjection control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonProjection_Click(object sender, EventArgs e)
        {
            if (projDialog == null)
                projDialog = new ProjectionBrowserDialog();

            projDialog.HelpRequested += new HelpEventHandler(MapPropertyEditor_HelpRequested);
            projDialog.Projection = textBoxProjection.Text;
            if (projDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxProjection.Text = projDialog.Projection;
                textBoxProjection.Tag = projDialog.ProjectionNative;
                comboBoxUnits.SelectedItem = MapUtils.GetMapUnitFromProj4(textBoxProjection.Tag.ToString());
            }
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxImageType control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxImageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDirty(true);
        }

        /// <summary>
        /// ValueChanged event handler of the colorPickerBackColor control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void colorPickerBackColor_ValueChanged(object sender, EventArgs e)
        {
            SetDirty(true);
        }

        /// <summary>
        /// Common function to validate the double values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateDouble(object sender, CancelEventArgs e)
        {
            double result;
            if (!double.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Click event handler of the buttonScalebar control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonScalebar_Click(object sender, EventArgs e)
        {
            if (this.EditProperties != null)
            {
                // need to update the current changes
                if (IsDirty())
                {
                    if (MessageBox.Show("Changing scalebar settings will automatically apply the pending changes of the map. Would you like to continue?", "MapManager",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        UpdateValues();
                    }
                    else
                        return;
                }

                EditProperties(this, new MapObjectHolder(map.scalebar, target));
            }
        }

        /// <summary>
        /// Click event handler of the buttonQueryMap control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonQueryMap_Click(object sender, EventArgs e)
        {
            if (this.EditProperties != null)
            {
                EditProperties(this, new MapObjectHolder(map.querymap, target));
            }
        }

        /// <summary>
        /// Validating event handler of the textBoxZoomWidth control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxZoomWidth_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (!double.TryParse(((TextBoxBase)sender).Text, out result) || result <= 0)
            {
                MessageBox.Show("Invalid zoom width or wrong number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (mapunits != map.units)
                result = result * MapUtils.InchesPerUnit(mapunits) / MapUtils.InchesPerUnit(map.units);

            zoomFactor = result / (map.extent.maxx - map.extent.minx);
            textBoxScale.Text = Convert.ToString(Convert.ToInt32(map.scaledenom * zoomFactor));
        }

        /// <summary>
        /// Validating event handler of the textBoxScale control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxScale_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (!double.TryParse(((TextBoxBase)sender).Text, out result) || result <= 0)
            {
                MessageBox.Show("Invalid scale or wrong number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            zoomFactor = result / map.scaledenom;

            double zoom = (map.extent.maxx - map.extent.minx);
            if (mapunits != map.units)
                zoom = zoom * MapUtils.InchesPerUnit(map.units) / MapUtils.InchesPerUnit(mapunits);

            textBoxZoomWidth.Text = Convert.ToString(Math.Round((zoom) * zoomFactor, unitPrecision));
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxTransparent control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxTransparent_CheckedChanged(object sender, EventArgs e)
        {
            SetDirty(true);
        }
    }
}
