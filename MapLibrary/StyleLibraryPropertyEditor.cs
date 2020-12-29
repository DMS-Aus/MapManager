using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OSGeo.MapServer;
using System.Globalization;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a UserControl for editing the MapScript styleObj parameters.
    /// </summary>
    public partial class StyleLibraryPropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private styleObj style;
        private classObj classobj;
        private bool dirtyFlag;
        private bool enablePreview;
        private mapObj map;
        private layerObj layer;
        private bool isLabelStyle;

        private NumberFormatInfo ni;
       
        /// <summary>
        /// Constructs a new StylePropertyEditor object.
        /// </summary>
        public StyleLibraryPropertyEditor()
        {
            InitializeComponent();
            ni = new NumberFormatInfo();
            ni.NumberDecimalSeparator = ".";
            isLabelStyle = false;
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
        /// Update the style object according to the current Editor state.
        /// </summary>
        /// <param name="style">The object to be updated.</param>
        private void Update(styleObj style)
        {
            // general tab
            style.size = double.Parse(textBoxSize.Text);
            style.width = double.Parse(textBoxWidth.Text);
            style.angle = double.Parse(textBoxAngle.Text);
            // display tab
            this.colorPickerColor.ApplyTo(style.color);
            this.colorPickerBackColor.ApplyTo(style.backgroundcolor);
            this.colorPickerOutlineColor.ApplyTo(style.outlinecolor);
            
            style.gap = double.Parse(textBoxGap.Text);

            string[] values = textBoxPattern.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 0)
            {
                double[] result = new double[values.Length];
                for (int i = 0; i < values.Length; i++)
                    result[i] = double.Parse(values[i], ni);
                style.pattern = result;
            }
            else
                style.patternlength = 0;

            if (comboBoxSymbol.SelectedIndex > 0)
                style.setSymbolByName(map, comboBoxSymbol.SelectedItem.ToString());
            else
            {
                // no symbol selected
                style.symbol = 0;
            }
        }

        /// <summary>
        /// Update the enabled state of the controls according to the layer type
        /// </summary>
        private void UpdateEnabledState()
        {
            textBoxAngle.Enabled = true;
            textBoxWidth.Enabled = true;
            textBoxGap.Enabled = true;
            textBoxPattern.Enabled = true;
            
            if (layer.type == MS_LAYER_TYPE.MS_LAYER_POINT)
            {
                textBoxAngle.Enabled = false;
                textBoxWidth.Enabled = false;
                textBoxGap.Enabled = false;
                textBoxPattern.Enabled = false;
            }
            else if (layer.type == MS_LAYER_TYPE.MS_LAYER_POLYGON)
            {
                textBoxPattern.Enabled = false;
            }
        }

        /// <summary>
        /// Update the preview according to the changes
        /// </summary>
        private void UpdatePreview()
        {
            if (style != null && enablePreview)
            {
                styleObj pstyle = style.clone();
                Update(pstyle);

                classObj styleclass = new classObj(null);
                styleclass.insertStyle(pstyle, -1);

                using (classObj def_class = new classObj(null)) // for drawing legend images
                {
                    using (imageObj image2 = def_class.createLegendIcon(
                                     map, layer, pictureBoxPreview.Width, pictureBoxPreview.Height))
                    {
                        styleclass.drawLegendIcon(map, layer,
                            pictureBoxPreview.Width - 10, pictureBoxPreview.Height - 10, image2, 4, 4);
                        byte[] img = image2.getBytes();
                        using (MemoryStream ms = new MemoryStream(img))
                        {
                            pictureBoxPreview.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            else
                pictureBoxPreview.Image = null;
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
            if (style == null)
                return;
            if (dirtyFlag)
            {
                Update(this.style);

                if (classobj != null && style.symbolname != null)
                    classobj.name = style.symbolname;
                
                SetDirty(false);
                if (target != null)
                    target.RaisePropertyChanged(this);
               
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
            if (style == null)
                return;

            if (style.size < 0) // set default size (#4339)
                textBoxSize.Text = "8";
            else
                textBoxSize.Text = style.size.ToString();
            
            textBoxWidth.Text = style.width.ToString();
            textBoxAngle.Text = style.angle.ToString();
            
            this.colorPickerColor.SetColor(style.color);
            this.colorPickerBackColor.SetColor(style.backgroundcolor);
            this.colorPickerOutlineColor.SetColor(style.outlinecolor);
            
            textBoxGap.Text = style.gap.ToString();

            StringBuilder s = new StringBuilder();
            double[] pattern = style.pattern;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (i > 0)
                    s.Append(" ");
                s.Append(pattern[i].ToString(ni));
            }
            textBoxPattern.Text = s.ToString();

            comboBoxSymbol.Items.Add("");
            comboBoxSymbol.SelectedIndex = 0;
            for (int i = 0; i < map.symbolset.numsymbols; i++)
            {
                string symbolname = map.symbolset.getSymbol(i).name;
                if (symbolname != null)
                {
                    comboBoxSymbol.Items.Add(symbolname);
                }
            }

            if (style.symbolname != null)
                comboBoxSymbol.SelectedItem = style.symbolname;

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
                    if (value.GetType() != typeof(styleObj))
                        throw new ApplicationException("Invalid object type.");
                    style = value;
                    target = value;

                    // tracking down the root object
                    MapObjectHolder classHolder;
                    if (target.GetParent().GetType() == typeof(labelObj))
                    {
                        classHolder = target.GetParent().GetParent();
                        isLabelStyle = true;
                    }
                    else
                    {
                        classHolder = target.GetParent();
                        isLabelStyle = false;
                    }

                    if (classHolder != null)
                    {
                        classobj = classHolder;
                        
                        MapObjectHolder layerHolder = classHolder.GetParent();
                        if (layerHolder != null)
                        {
                            map = layerHolder.GetParent();
                            layer = layerHolder;
                        }
                    }

                    enablePreview = false;
                    RefreshView();
                    enablePreview = true;

                    UpdatePreview();

                    UpdateEnabledState();
                    
                    SetDirty(false);
                    return;
                }
                style = null;
                classobj = null;
                target = null;
                map = null;
                layer = null;
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
            UpdatePreview();
        }

        /// <summary>
        /// Common function to sign that a value is changing.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValueChanging(object sender, EventArgs e)
        {
           SetDirty(true);
        }

        /// <summary>
        /// Common function to sign that a value is changing and the preview is to be updated.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValueChangingWithPreview(object sender, EventArgs e)
        {
            SetDirty(true);
            UpdatePreview();
        }

        private void textBoxPattern_Validating(object sender, CancelEventArgs e)
        {
            string[] values = textBoxPattern.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double result;
            for (int i = 0; i < values.Length; i++)
            {
                if (!double.TryParse(values[i], NumberStyles.AllowDecimalPoint, ni, out result))
                {
                    MessageBox.Show("Invalid number: " + values[i], "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }
    }
}
