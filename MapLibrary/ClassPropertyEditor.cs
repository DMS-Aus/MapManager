using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a UserControl for editing the MapScript classObj parameters.
    /// </summary>
    public partial class ClassPropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private classObj classobj;
        private mapObj map;
        private bool dirtyFlag;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClassPropertyEditor()
        {
            InitializeComponent();
            toolTip.SetToolTip(buttonMinScale, "Set Map Scale");
            toolTip.SetToolTip(buttonMaxScale, "Set Map Scale");
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
            if (classobj == null)
                return;
            if (dirtyFlag)
            {
                // general tab
                classobj.name = this.textBoxName.Text;
                classobj.title = this.textBoxTitle.Text;
                classobj.setExpression(this.textBoxExpression.Text);
                classobj.setText(this.textBoxText.Text);
                // display tab
                if (checkBoxVisible.CheckState == CheckState.Checked)
                    classobj.status = mapscript.MS_ON;
                else
                    classobj.status = mapscript.MS_OFF;

                if (checkBoxQueryable.CheckState == CheckState.Checked)
                    classobj.template = "query";
                else
                    classobj.template = null;

                classobj.maxscaledenom = -1;
                classobj.minscaledenom = -1;
                if (textBoxMaxZoom.Text == "")
                    classobj.maxscaledenom = -1;
                else
                    classobj.maxscaledenom = double.Parse(textBoxMaxZoom.Text);

                if (textBoxMinZoom.Text == "")
                    classobj.minscaledenom = -1;
                else
                    classobj.minscaledenom = double.Parse(textBoxMinZoom.Text);

                if (target != null)
                    target.RaisePropertyChanged(this);
                SetDirty(false);
            }
        }

        /// <summary>
        /// Returns the modify flag.
        /// </summary>
        /// <returns>The actual value of the modify flag.</returns>
        public bool IsDirty()
        {
            return dirtyFlag;
        }

        #endregion

        /// <summary>
        /// Update the preview according to the current Editor state.
        /// </summary>
        private void UpdatePreview()
        {
            mapControl.Target = target;
        }

        #region IMapControl Members

        /// <summary>
        /// Refresh the controls according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            if (classobj == null)
                return;
            // general tab
            this.textBoxName.Text = classobj.name;
            this.textBoxTitle.Text = classobj.title;
            this.textBoxExpression.Text = classobj.getExpressionString();
            this.textBoxText.Text = classobj.getTextString();
            // display tab
            if (classobj.status == mapscript.MS_OFF)
                checkBoxVisible.CheckState = CheckState.Unchecked;
            else
                checkBoxVisible.CheckState = CheckState.Checked;

            checkBoxQueryable.Checked = (classobj.template != null && classobj.template.Length > 0);

            if (classobj.minscaledenom >= 0)
                textBoxMinZoom.Text = classobj.minscaledenom.ToString();
            else
                textBoxMinZoom.Text = "";
            if (classobj.maxscaledenom >= 0)
                textBoxMaxZoom.Text = classobj.maxscaledenom.ToString();
            else
                textBoxMaxZoom.Text = "";

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
                    if (value.GetType() != typeof(classObj))
                        throw new ApplicationException("Invalid object type.");
                    classobj = value;
                    target = value;
                    if (classobj.layer != null && classobj.layer.map != null)
                        map = classobj.layer.map;
                    mapControl.Target = value;
                    RefreshView();
                    value.PropertyChanged += new EventHandler(value_PropertyChanged);
                    return;
                }
                classobj = null;
                target = null;
            }
        }

        /// <summary>
        /// PropertyChanging event handler of the target control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void value_PropertyChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties = delegate { };

        #endregion

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
        /// Common function to validate the double values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateDouble2(object sender, CancelEventArgs e)
        {
            double result;
            if (((TextBoxBase)sender).Text != "" && !double.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Click event handler of the buttonMinScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMinScale_Click(object sender, EventArgs e)
        {
            textBoxMinZoom.Text = Convert.ToInt64(map.scaledenom).ToString();
        }

        /// <summary>
        /// Click event handler of the buttonMaxScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMaxScale_Click(object sender, EventArgs e)
        {
            textBoxMaxZoom.Text = Convert.ToInt64(map.scaledenom).ToString();
        }
    }
}
