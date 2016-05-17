using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;
using System.IO;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a UserControl for editing the MapScript labelObj parameters.
    /// </summary>
    public partial class LabelPropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private labelObj label;
        private bool dirtyFlag;
        private mapObj map;
        private bool isStyleChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LabelPropertyEditor()
        {
            InitializeComponent();
            // setting up the backcolor of the tab pages
            tabPageDisplay.BackColor =
            tabPageRendering.BackColor =
            tabPagePosition.BackColor =
            tabPageStyles.BackColor = this.BackColor;
            toolTip.SetToolTip(buttonMinScale, "Set Map Scale");
            toolTip.SetToolTip(buttonMaxScale, "Set Map Scale");
            isStyleChanged = false;
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
        /// Updating the enabled state of the controls
        /// </summary>
        private void UpdateControlState()
        {
            buttonEditStyle.Enabled = buttonRemoveStyle.Enabled = listViewStyles.SelectedIndices.Count > 0;
            buttonMoveStyleUp.Enabled = buttonMoveStyleDown.Enabled = listViewStyles.SelectedIndices.Count > 0;
            labelBindingControllerAngle.Enabled = textBoxAngle.Enabled = 
                (MS_POSITIONS_ENUM)comboBoxAngleMode.SelectedItem == MS_POSITIONS_ENUM.MS_NONE;

            layerObj layer = null;
            if (target.GetParent().GetParent().GetType() == typeof(layerObj))
                layer = target.GetParent().GetParent();
            if (layer != null)
            {
                checkBoxForce.Enabled = false;
                checkBoxAutoMinFeatureSize.Enabled = false;
                textBoxMinFeatureSize.Enabled = false;
                textBoxPriority.Enabled = false;
                labelBindingControllerPriority.Enabled = false;
                textBoxBuffer.Enabled = false;
                textBoxMinSize.Enabled = false;
                textBoxMaxSize.Enabled = false;

                if (layer.labelcache == mapscript.MS_ON)
                {
                    checkBoxForce.Enabled = true;
                    checkBoxAutoMinFeatureSize.Enabled = true;
                    textBoxMinFeatureSize.Enabled = true;
                    textBoxPriority.Enabled = true;
                    labelBindingControllerPriority.Enabled = true;
                    textBoxBuffer.Enabled = true;
                }
                if (layer.symbolscaledenom > 0)
                {
                    textBoxMinSize.Enabled = true;
                    textBoxMaxSize.Enabled = true;
                }

                buttonMoveStyleUp.Enabled = false;
                buttonMoveStyleDown.Enabled = false;
                buttonEditStyle.Enabled = false;
                if (listViewStyles.SelectedIndices.Count > 0)
                {
                    buttonEditStyle.Enabled = true;
                    if (listViewStyles.SelectedIndices[0] > 0)
                        buttonMoveStyleUp.Enabled = true;
                    if (listViewStyles.SelectedIndices[0] < listViewStyles.Items.Count - 1)
                        buttonMoveStyleDown.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Adding a new style to the list
        /// </summary>
        /// <param name="style">the style to add</param>
        private void AddStyleToList(styleObj style)
        {
            layerObj layer;
            if (target.GetParent().GetType() == typeof(scalebarObj))
                layer = new layerObj(null);
            else
                layer = target.GetParent().GetParent();
            
            classObj styleclass = new classObj(null);
            styleclass.insertStyle(style, -1);
            // creating the list icon
            using (classObj def_class = new classObj(null)) // for drawing legend images
            {
                using (imageObj image2 = def_class.createLegendIcon(map, layer, 30, 20))
                {
                    MS_LAYER_TYPE type = layer.type;
                    try
                    {
                        // modify the layer type in certain cases for drawing correct images
                        string geomtransform = style.getGeomTransform().ToLower();
                        if (geomtransform != null)
                        {
                            if (geomtransform.Contains("labelpoly"))
                                layer.type = MS_LAYER_TYPE.MS_LAYER_POLYGON;
                            else if (geomtransform.Contains("labelpnt"))
                                layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;
                        }
                        styleclass.drawLegendIcon(map, layer, 20, 10, image2, 5, 5);
                    }
                    finally
                    {
                        layer.type = type;
                    }
                    
                    byte[] img = image2.getBytes();
                    using (MemoryStream ms = new MemoryStream(img))
                    {
                        imageList.Images.Add(Image.FromStream(ms));
                    }

                    ListViewItem item = new ListViewItem(
                        new string[] { "", style.size.ToString(), style.width.ToString(), style.symbolname });

                    item.ImageIndex = imageList.Images.Count - 1;
                    item.Tag = style;

                    listViewStyles.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Update the list item according to a modified style
        /// </summary>
        /// <param name="index">the index of the lit item</param>
        /// <param name="style">the style object</param>
        private void UpdateStyleInList(int index)
        {
            layerObj layer;
            if (target.GetParent().GetType() == typeof(scalebarObj))
                layer = new layerObj(null);
            else
                layer = target.GetParent().GetParent();

            ListViewItem item = listViewStyles.Items[index];
            styleObj style = (styleObj)item.Tag;

            classObj styleclass = new classObj(null);
            styleclass.insertStyle(style, -1);
            // creating the list icon
            using (classObj def_class = new classObj(null)) // for drawing legend images
            {
                using (imageObj image2 = def_class.createLegendIcon(map, layer, 30, 20))
                {
                    MS_LAYER_TYPE type = layer.type;
                    try
                    {
                        // modify the layer type in certain cases for drawing correct images
                        string geomtransform = style.getGeomTransform().ToLower();
                        if (geomtransform != null)
                        {
                            if (geomtransform.Contains("labelpoly"))
                                layer.type = MS_LAYER_TYPE.MS_LAYER_POLYGON;
                            else if (geomtransform.Contains("labelpnt"))
                                layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;
                        }
                        styleclass.drawLegendIcon(map, layer, 20, 10, image2, 5, 5);
                    }
                    finally
                    {
                        layer.type = type;
                    }
                    
                    byte[] img = image2.getBytes();
                    using (MemoryStream ms = new MemoryStream(img))
                    {
                        imageList.Images[item.ImageIndex] = Image.FromStream(ms);
                    }

                    
                    item.SubItems[1].Text = style.size.ToString();
                    item.SubItems[2].Text = style.width.ToString();
                    item.SubItems[3].Text = style.symbolname;
                }
            }
        }

        /// <summary>
        /// Refresh the style list of this label object.
        /// </summary>
        private void RefreshStyles()
        {
            listViewStyles.Items.Clear();
            imageList.ImageSize = new Size(30, 20);

            // populate list in reverse order
            for (int i = label.numstyles-1; i >= 0; --i)
            {
                styleObj style = label.getStyle(i).clone();
                AddStyleToList(style);
            }

            listViewStyles.SmallImageList = imageList;

            UpdateControlState();
        }

        /// <summary>
        /// Update the label object according to the current Editor state.
        /// </summary>
        /// <param name="label">The object to be updated.</param>
        private void Update(labelObj label)
        {
            label.anglemode = (MS_POSITIONS_ENUM)comboBoxAngleMode.SelectedItem;
            label.autominfeaturesize = checkBoxAutoMinFeatureSize.Checked ? mapscript.MS_TRUE : mapscript.MS_FALSE;
            label.partials = checkBoxPartials.Checked ? mapscript.MS_TRUE : mapscript.MS_FALSE;
            label.force = checkBoxForce.Checked ? mapscript.MS_TRUE : mapscript.MS_FALSE;

            label.setExpression(this.textBoxExpression.Text);
            label.setText(this.textBoxText.Text);

            colorPickerColor.ApplyTo(label.color);
            colorPickerOutlineColor.ApplyTo(label.outlinecolor);
            colorPickerShadowColor.ApplyTo(label.shadowcolor);

            label.size = int.Parse(textBoxSize.Text);
            label.minsize = int.Parse(textBoxMinSize.Text);
            label.maxsize = int.Parse(textBoxMaxSize.Text);
            label.mindistance = int.Parse(textBoxMinDistance.Text);
            label.offsetx = int.Parse(textBoxOffsetX.Text);
            label.offsety = int.Parse(textBoxOffsetY.Text);
            if (textBoxWrap.Text == "")
                label.wrap = '\0';
            else
                label.wrap = Convert.ToChar(textBoxWrap.Text);

            label.shadowsizex = int.Parse(textBoxShadowSizeX.Text);
            label.shadowsizey = int.Parse(textBoxShadowSizeY.Text);
            
            if (textBoxEncoding.Text == "")
                label.encoding = null;
            else
                label.encoding = textBoxEncoding.Text;
            label.angle = double.Parse(textBoxAngle.Text);
            
            label.position = (int)comboBoxPosition.SelectedItem;

            label.align = (int)comboBoxAlign.SelectedItem;

            if ((string)(comboBoxFont.SelectedItem) == "(none)")
                label.font = null;
            else
                label.font = (string)(comboBoxFont.SelectedItem);

            label.priority = int.Parse(textBoxPriority.Text);
            if (textBoxMaxScale.Text == "")
                label.maxscaledenom = -1;
            else
                label.maxscaledenom = double.Parse(textBoxMaxScale.Text);

            if (textBoxMinScale.Text == "")
                label.minscaledenom = -1;
            else
                label.minscaledenom = double.Parse(textBoxMinScale.Text);
            label.maxlength = int.Parse(textBoxMaxLength.Text);
            label.minfeaturesize = int.Parse(textBoxMinFeatureSize.Text);
            label.buffer = int.Parse(textBoxBuffer.Text);
            label.repeatdistance = int.Parse(textBoxRepeatDistance.Text);

            if (isStyleChanged)
            {
                // reconstruct styles
                while (label.numstyles > 0)
                    label.removeStyle(0);
                // insert styles in reverse order
                for (int i = listViewStyles.Items.Count - 1; i >= 0; --i)
                {
                    ListViewItem item = listViewStyles.Items[i];
                    styleObj style = (styleObj)item.Tag;
                    label.insertStyle(style.clone(), -1);
                }
            }
        }

        /// <summary>
        /// Update the preview according to the current Editor state.
        /// In case of the preview a temporary object will only be updated.
        /// </summary>
        private void UpdatePreview()
        {
            labelObj style = mapControl.Target;
            if (style != null)
            {
                Update(style);
                if (style.getTextString() == null || style.getTextString() == "")
                    style.setText("Sample Text");
                style.setExpression("");
                mapControl.RefreshView();
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
            if (label == null)
                return;
            if (dirtyFlag)
            {
                Update(this.label);

                labelBindingControllerFont.ApplyBinding();
                labelBindingControllerAngle.ApplyBinding();
                labelBindingControllerSize.ApplyBinding();
                labelBindingControllerColor.ApplyBinding();
                labelBindingControllerOutlineColor.ApplyBinding();
                labelBindingControllerPosition.ApplyBinding();
                labelBindingControllerPriority.ApplyBinding();
                labelBindingControllerShadowSizeX.ApplyBinding();
                labelBindingControllerShadowSizeY.ApplyBinding();
            
                if (target != null)
                    target.RaisePropertyChanged(this);
                SetDirty(false);
                isStyleChanged = false;
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
            if (label == null)
                return;

            layerObj layer = null;
            if (target.GetParent().GetParent().GetType() == typeof(layerObj))
                layer = target.GetParent().GetParent();
            
            labelBindingControllerSize.InitializeBinding(target);

            textBoxExpression.Text = label.getExpressionString();
            textBoxText.Text = label.getTextString();
            
            comboBoxAngleMode.Items.AddRange(new object[] { MS_POSITIONS_ENUM.MS_AUTO, MS_POSITIONS_ENUM.MS_AUTO2, MS_POSITIONS_ENUM.MS_FOLLOW, MS_POSITIONS_ENUM.MS_NONE });
            comboBoxAngleMode.SelectedItem = (MS_POSITIONS_ENUM)label.anglemode;
            checkBoxAutoMinFeatureSize.Checked = (label.autominfeaturesize == mapscript.MS_TRUE);
            checkBoxAutoMinFeatureSize.Enabled = (layer != null && layer.labelcache == mapscript.MS_ON);
            checkBoxPartials.Checked = (label.partials == mapscript.MS_TRUE);
            checkBoxPartials.Enabled = (layer != null && layer.labelcache == mapscript.MS_ON);
            checkBoxForce.Checked = (label.force == mapscript.MS_TRUE);
            checkBoxForce.Enabled = (layer != null && layer.labelcache == mapscript.MS_ON);

            colorPickerColor.SetColor(label.color);
            labelBindingControllerColor.InitializeBinding(target);
            colorPickerOutlineColor.SetColor(label.outlinecolor);
            labelBindingControllerOutlineColor.InitializeBinding(target);
            colorPickerShadowColor.SetColor(label.shadowcolor);
            //STEPH: remove forcing size to 8 when it is 0
            textBoxSize.Text = label.size.ToString();
            textBoxMinSize.Text = label.minsize.ToString();

            textBoxMaxSize.Text = label.maxsize.ToString();
            textBoxMinDistance.Text = label.mindistance.ToString();
            textBoxOffsetX.Text = label.offsetx.ToString();
            textBoxOffsetY.Text = label.offsety.ToString();
            textBoxWrap.Text = label.wrap.ToString();
            textBoxShadowSizeX.Text = label.shadowsizex.ToString();
            labelBindingControllerShadowSizeX.InitializeBinding(target);
            textBoxShadowSizeY.Text = label.shadowsizey.ToString();
            labelBindingControllerShadowSizeY.InitializeBinding(target);
            
            textBoxEncoding.Text = label.encoding;
            textBoxAngle.Text = label.angle.ToString();
            labelBindingControllerAngle.InitializeBinding(target);

            comboBoxPosition.Items.AddRange(new object[] { MS_POSITIONS_ENUM.MS_UL, MS_POSITIONS_ENUM.MS_LR, MS_POSITIONS_ENUM.MS_UR, MS_POSITIONS_ENUM.MS_LL, MS_POSITIONS_ENUM.MS_CR, MS_POSITIONS_ENUM.MS_CL, MS_POSITIONS_ENUM.MS_UC, MS_POSITIONS_ENUM.MS_LC, MS_POSITIONS_ENUM.MS_CC, MS_POSITIONS_ENUM.MS_AUTO, MS_POSITIONS_ENUM.MS_XY });
            comboBoxPosition.SelectedItem = (MS_POSITIONS_ENUM)label.position;
            labelBindingControllerPosition.InitializeBinding(target);

            comboBoxAlign.DataSource = Enum.GetValues(typeof(MS_ALIGN_VALUE));
            comboBoxAlign.SelectedItem = (MS_ALIGN_VALUE)label.align;

            comboBoxFont.Items.Add("(none)");
            if (map != null)
            {
                string key = null;
                while ((key = map.fontset.fonts.nextKey(key)) != null)
                {
                    if (File.Exists(map.fontset.fonts.get(key, "")))
                    {
                        comboBoxFont.Items.Add(key);
                        if (string.Compare(key, label.font, true) == 0)
                        {
                            comboBoxFont.SelectedItem = key;
                        }
                        if (string.Compare(key, "arial", true) == 0 && comboBoxFont.SelectedIndex < 0)
                        {
                            // set the default font to Arial if exists
                            comboBoxFont.SelectedItem = key;
                        }
                    }
                }
            }

            if (comboBoxFont.SelectedIndex < 0)
                comboBoxFont.SelectedIndex = 0;

            // preserve the current item when sorting 
            object selectedItem = comboBoxFont.SelectedItem;
            comboBoxFont.Sorted = true;
            comboBoxFont.SelectedItem = selectedItem;
            
            labelBindingControllerFont.InitializeBinding(target);

            textBoxPriority.Text = label.priority.ToString();
            labelBindingControllerPriority.InitializeBinding(target);

            if (label.minscaledenom >= 0)
                textBoxMinScale.Text = label.minscaledenom.ToString();
            else
                textBoxMinScale.Text = "";
            if (label.maxscaledenom >= 0)
                textBoxMaxScale.Text = label.maxscaledenom.ToString();
            else
                textBoxMaxScale.Text = "";

            textBoxMaxLength.Text = label.maxlength.ToString();
            textBoxMinFeatureSize.Text = label.minfeaturesize.ToString();
            textBoxBuffer.Text = label.buffer.ToString();
            textBoxRepeatDistance.Text = label.repeatdistance.ToString();
            
            RefreshStyles();

            UpdateControlState();

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
                    if (value.GetType() != typeof(labelObj))
                        throw new ApplicationException("Invalid object type.");
                    label = value;
                    target = value;

                    // tracking down the root object
                    MapObjectHolder classHolder = target.GetParent();
                    if (classHolder != null)
                    {
                        if (classHolder.GetType() == typeof(classObj))
                        {
                            MapObjectHolder layerHolder = classHolder.GetParent();
                            if (layerHolder != null)
                            {
                                map = layerHolder.GetParent();
                            }
                        }
                        else if (classHolder.GetType() == typeof(scalebarObj))
                        {
                            map = classHolder.GetParent();
                        }
                    }

                    RefreshView();
                    mapControl.Target = value;
                    UpdatePreview();
                    return;
                }
                label = null;
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
        /// Common function to sign that a value have been changed.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
            //SetDirty(true);
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
        /// Common function to sign that a value is changing and the preview is to be updated.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValueChangingWithPreview(object sender, EventArgs e)
        {
            UpdatePreview();
            SetDirty(true);
        }

        
        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxFont control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
            SetDirty(true);
        }

        /// <summary>
        /// Click event handler of the buttonAddStyle control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonAddStyle_Click(object sender, EventArgs e)
        {
            styleObj style = new styleObj(null);
            style.setGeomTransform("labelpnt");
            style.color = new colorObj(0, 0, 0, 0);
            style.size = 8;
            AddStyleToList(style);
            isStyleChanged = true;
            UpdatePreview();
        }

        /// <summary>
        /// Click event handler of the buttonEditStyle control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonEditStyle_Click(object sender, EventArgs e)
        {
            if (listViewStyles.SelectedItems.Count == 0)
                return;

            try
            {

                styleObj style = (styleObj)listViewStyles.SelectedItems[0].Tag;

                MapObjectHolder styletarget = new MapObjectHolder(style, target);
                styletarget.PropertyChanged += new EventHandler(styletarget_PropertyChanged);

                //MapPropertyEditorForm stylePropertyEditor = new MapPropertyEditorForm(styletarget);
                //stylePropertyEditor.ShowDialog(this);
                if (this.EditProperties != null)
                    this.EditProperties(this, styletarget);

                isStyleChanged = true;
                UpdatePreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// PropertyChanged event handler of the styletarget control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void styletarget_PropertyChanged(object sender, EventArgs e)
        {
            UpdateStyleInList(listViewStyles.SelectedIndices[0]);
            SetDirty(true);
            isStyleChanged = true;
            mapControl.Target = target; // reload symbolset
            UpdatePreview();
        }

        /// <summary>
        /// Click event handler of the buttonMoveStyleUp control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMoveStyleUp_Click(object sender, EventArgs e)
        {
            if ( listViewStyles.SelectedItems.Count == 0)
                return;

            ListViewItem item = listViewStyles.SelectedItems[0];
            int index = item.Index;

            listViewStyles.Items.Remove(item);
            listViewStyles.Items.Insert(index - 1, item);

            isStyleChanged = true;
            SetDirty(true);
            UpdatePreview();
        }

        /// <summary>
        /// Click event handler of the buttonMoveStyleDown control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMoveStyleDown_Click(object sender, EventArgs e)
        {
            if (listViewStyles.SelectedItems.Count == 0)
                return;

            ListViewItem item = listViewStyles.SelectedItems[0];
            int index = item.Index;

            listViewStyles.Items.Remove(item);
            listViewStyles.Items.Insert(index + 1, item);

            isStyleChanged = true;
            SetDirty(true);
            UpdatePreview();
        }

        /// <summary>
        /// Click event handler of the buttonRemoveStyle control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonRemoveStyle_Click(object sender, EventArgs e)
        {
            if (listViewStyles.SelectedItems.Count == 0)
                return;

            ListViewItem item = listViewStyles.SelectedItems[0];
            listViewStyles.Items.Remove(item);

            isStyleChanged = true;
            SetDirty(true);
            UpdatePreview();
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the listViewStyles control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void listViewStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControlState();
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxAngleMode control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxAngleMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
            UpdateControlState();
            SetDirty(true);
        }

        /// <summary>
        /// DoubleClick event handler of the listViewStyles control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void listViewStyles_DoubleClick(object sender, EventArgs e)
        {
            buttonEditStyle_Click(sender, e);
        }

        /// <summary>
        /// Click event handler of the buttonMinScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMinScale_Click(object sender, EventArgs e)
        {
            textBoxMinScale.Text = Convert.ToInt64(map.scaledenom).ToString();
        }

        /// <summary>
        /// Click event handler of the buttonMaxScale control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonMaxScale_Click(object sender, EventArgs e)
        {
            textBoxMaxScale.Text = Convert.ToInt64(map.scaledenom).ToString();
        }

        /// <summary>
        /// Validated event handler of the textBoxText control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxText_Validated(object sender, EventArgs e)
        {
            UpdatePreview();
        }
    }
}
