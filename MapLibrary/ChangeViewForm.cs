using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a Form for changing the map view.
    /// </summary>
    public partial class ChangeViewForm : Form, IMapControl
    {
        private MapObjectHolder target;
        private mapObj map;
        private bool dirtyFlag;
        private double zoomFactor = 1.0;
        int unitPrecision;
        MS_UNITS mapunits;

        /// <summary>
        /// Constructs a new ChangeViewForm class.
        /// </summary>
        public ChangeViewForm()
        {
            InitializeComponent();
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

        #region IMapControl Members

        /// <summary>
        /// Refresh the controls according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            mapunits = map.units;

            unitPrecision = MapUtils.GetUnitPrecision(mapunits);
            textBoxX.Text = Convert.ToString(Math.Round((map.extent.maxx + map.extent.minx) / 2, unitPrecision));
            textBoxY.Text = Convert.ToString(Math.Round((map.extent.maxy + map.extent.miny) / 2, unitPrecision));
            labelUnit2.Text = MapUtils.GetUnitName(mapunits);
            textBoxScale.Text = Convert.ToString(Convert.ToInt32(map.scaledenom));

            labelUnit1.Text = MapUtils.GetUnitName(mapunits);

            double zoom = (map.extent.maxx - map.extent.minx);
            if (mapunits != map.units)
                zoom = zoom * MapUtils.InchesPerUnit(map.units) / MapUtils.InchesPerUnit(mapunits);

            textBoxZoomWidth.Text = Convert.ToString(Math.Round(zoom, unitPrecision));
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
                    return;
                }
                map = null;
                target = null;
            }
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties = delegate { };

        #endregion

        /// <summary>
        /// Click event handler of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(sender, e);
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
        /// Click event handler of the buttonApply control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonApply_Click(object sender, EventArgs e)
        {
            double deltaX = (map.extent.maxx - map.extent.minx) * zoomFactor / 2;
            double deltaY = (map.extent.maxy - map.extent.miny) * zoomFactor / 2;
            double centerX = double.Parse(textBoxX.Text);
            double centerY = double.Parse(textBoxY.Text);

            map.setExtent(centerX - deltaX, centerY - deltaY, centerX + deltaX, centerY + deltaY);

            target.RaisePropertyChanged(this);

            double zoom = (map.extent.maxx - map.extent.minx);
            if (mapunits != map.units)
                zoom = zoom * MapUtils.InchesPerUnit(map.units) / MapUtils.InchesPerUnit(mapunits);

            target.RaiseZoomChanged(this, Math.Round(zoom, unitPrecision), map.scaledenom);

            zoomFactor = 1.0;
        }

        private void ChangeViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}