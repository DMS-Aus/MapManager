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
    public partial class QueryMapPropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private queryMapObj queryMap;
        private bool dirtyFlag;
        private mapObj map;
        
        /// <summary>
        /// Constructs a new ScalebarPropertyEditor object.
        /// </summary>
        public QueryMapPropertyEditor()
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
        /// Update the scalebar object according to the current Editor state.
        /// </summary>
        /// <param name="queryMap">The object to be updated.</param>
        private void Update(queryMapObj queryMap)
        {
            if (comboBoxStatus.SelectedItem.ToString() == "MS_ON")
                queryMap.status = mapscript.MS_ON;
            else
                queryMap.status = mapscript.MS_OFF;

            queryMap.style = (int)comboBoxStyle.SelectedItem;

            this.colorPickerColor.ApplyTo(queryMap.color);
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
            if (queryMap == null)
                return;
            if (dirtyFlag)
            {
                Update(this.queryMap);

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
            if (queryMap == null)
                return;

            comboBoxStatus.Items.AddRange(new object[] { "MS_ON", "MS_OFF"});
            if (queryMap.status == mapscript.MS_ON)
                comboBoxStatus.SelectedItem = "MS_ON";
            else
                comboBoxStatus.SelectedItem = "MS_OFF";

            comboBoxStyle.DataSource = Enum.GetValues(typeof(MS_QUERYMAP_STYLES));
            comboBoxStyle.SelectedItem = (MS_QUERYMAP_STYLES)queryMap.style;


            this.colorPickerColor.SetColor(queryMap.color);

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
                    if (value.GetType() != typeof(queryMapObj))
                        throw new ApplicationException("Invalid object type.");
                    queryMap = value;
                    target = value;

                    // tracking down the root object
                    map = target.GetParent();

                    RefreshView();
                    SetDirty(false);
                    return;
                }
                queryMap = null;
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
    }
}
