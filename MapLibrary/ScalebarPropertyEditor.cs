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
    /// Represents a UserControl for editing the MapScript scalebarObj parameters.
    /// </summary>
    public partial class ScalebarPropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private scalebarObj scalebar;
        private bool dirtyFlag;
        private mapObj map;
        
        /// <summary>
        /// Constructs a new ScalebarPropertyEditor object.
        /// </summary>
        public ScalebarPropertyEditor()
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

        /// <summary>
        /// Common function to validate the integer values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateInteger(object sender, CancelEventArgs e)
        {
            int result;
            if (!int.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid integer number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
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
            if (!double.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Update the scalebar object according to the current Editor state.
        /// </summary>
        /// <param name="scalebar">The object to be updated.</param>
        private void Update(scalebarObj scalebar)
        {
            scalebar.intervals = int.Parse(textBoxIntervals.Text);
            scalebar.position = (int)comboBoxPosition.SelectedItem;
            scalebar.units = (int)comboBoxUnits.SelectedItem;

            if (comboBoxStatus.SelectedItem.ToString() == "MS_EMBED")
                scalebar.status = mapscript.MS_EMBED;
            else if (comboBoxStatus.SelectedItem.ToString() == "MS_ON")
                scalebar.status = mapscript.MS_ON;
            else if (comboBoxStatus.SelectedItem.ToString() == "MS_OFF")
                scalebar.status = mapscript.MS_OFF;

            scalebar.style = (int)comboBoxStyle.SelectedItem;

            this.colorPickerColor.ApplyTo(scalebar.color);
            this.colorPickerBackColor.ApplyTo(scalebar.backgroundcolor);
            this.colorPickerOutlineColor.ApplyTo(scalebar.outlinecolor);
            this.colorPickerImageColor.ApplyTo(scalebar.imagecolor);

            scalebar.width = int.Parse(textBoxWidth.Text);
            scalebar.height = int.Parse(textBoxHeight.Text);
        }

        #region IPropertyEditor Members

        /// <summary>
        /// Cancel the pending changes in the underlying object.
        /// </summary>
        public void CancelEditing()
        {
        }

        /// <summary>
        /// Update the preview according to the current Editor state.
        /// In case of the preview a temporary object will only be updated.
        /// </summary>
        public void UpdateValues()
        {
            if (scalebar == null)
                return;
            if (dirtyFlag)
            {
                Update(this.scalebar);

                if (target != null)
                    target.RaisePropertyChanged(this);
                SetDirty(false);
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
            if (scalebar == null)
                return;

            textBoxIntervals.Text = scalebar.intervals.ToString();

            comboBoxPosition.Items.Clear();
            comboBoxPosition.Items.AddRange(new object[] { MS_POSITIONS_ENUM.MS_UL, MS_POSITIONS_ENUM.MS_UC,
                MS_POSITIONS_ENUM.MS_UR, MS_POSITIONS_ENUM.MS_LL, MS_POSITIONS_ENUM.MS_LC, MS_POSITIONS_ENUM.MS_LR});
            comboBoxPosition.SelectedItem = (MS_POSITIONS_ENUM)scalebar.position;

            comboBoxUnits.Items.Clear();
            comboBoxUnits.Items.AddRange(new object[] { MS_UNITS.MS_FEET, MS_UNITS.MS_INCHES,
                MS_UNITS.MS_KILOMETERS, MS_UNITS.MS_METERS, MS_UNITS.MS_MILES});
            comboBoxUnits.SelectedItem = (MS_UNITS)scalebar.units;

            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.AddRange(new object[] { "MS_EMBED", "MS_ON", "MS_OFF"});
            if (scalebar.status == mapscript.MS_EMBED)
                comboBoxStatus.SelectedItem = "MS_EMBED";
            else if (scalebar.status == mapscript.MS_ON)
                comboBoxStatus.SelectedItem = "MS_ON";
            else if (scalebar.status == mapscript.MS_OFF)
                comboBoxStatus.SelectedItem = "MS_OFF";

            comboBoxStyle.Items.Clear();
            comboBoxStyle.Items.AddRange(new object[] { 0, 1 });
            comboBoxStyle.SelectedItem = scalebar.style;

            this.colorPickerColor.SetColor(scalebar.color);
            this.colorPickerBackColor.SetColor(scalebar.backgroundcolor);
            this.colorPickerOutlineColor.SetColor(scalebar.outlinecolor);
            this.colorPickerImageColor.SetColor(scalebar.imagecolor);

            this.textBoxWidth.Text = scalebar.width.ToString();
            this.textBoxHeight.Text = scalebar.height.ToString();

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
                    if (value.GetType() != typeof(scalebarObj))
                        throw new ApplicationException("Invalid object type.");
                    scalebar = value;
                    target = value;

                    // tracking down the root object
                    map = target.GetParent();

                    RefreshView();
                    SetDirty(false);
                    return;
                }
                scalebar = null;
                target = null;
                map = null;
            }
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
        /// Click event handler of the buttonEditLabel control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonEditLabel_Click(object sender, EventArgs e)
        {
            if (this.EditProperties != null)
            {
                // need to update the current changes
                if (IsDirty())
                {
                    if (MessageBox.Show("Changing label settings will automatically apply the pending changes of the scalebar. Would you like to continue?", "MapManager",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        UpdateValues();
                    }
                    else
                        return;
                }

                EditProperties(this, new MapObjectHolder(scalebar.label, target));
            }
        }
    }
}
