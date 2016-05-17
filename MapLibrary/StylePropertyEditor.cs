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
using System.Runtime.InteropServices;
using System.Collections;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a UserControl for editing the MapScript styleObj parameters.
    /// </summary>
    public partial class StylePropertyEditor : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private styleObj style;
        private bool dirtyFlag;
        private bool enablePreview;
        private mapObj map;
        private layerObj layer;
        private bool isLabelStyle;
        
        private NumberFormatInfo ni;
        //STEPH: flag to determine if the dialog is first loaded to make sure all properties of the style object
        //passed to the dialog are applied and not overriden by listView_SelectedIndexChanged event
        private bool firstLoad=false;
        
        /// <summary>
        /// Constructs a new StylePropertyEditor object.
        /// </summary>
        public StylePropertyEditor()
        {
            InitializeComponent();
            ni = new NumberFormatInfo();
            ni.NumberDecimalSeparator = ".";
            isLabelStyle = false;
            SetSpacing((short)(imageList.ImageSize.Width + 10), 
                       (short)(imageList.ImageSize.Height + 30));
            toolTip.SetToolTip(buttonMinScale, "Set Map Scale");
            toolTip.SetToolTip(buttonMaxScale, "Set Map Scale");
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern Int32 SendMessage(IntPtr hwnd, Int32 wMsg, Int32 wParam, Int32 lParam);
        const int LVM_FIRST = 0x1000;
        const int LVM_SETICONSPACING = LVM_FIRST + 53;

        /// <summary>
        /// Sets the space between the list images.
        /// </summary>
        /// <param name="x">The x offset of the image.</param>
        /// <param name="y">The y offset of the image.</param>
        private void SetSpacing(short x, short y)
        {
            SendMessage(this.listView.Handle, LVM_SETICONSPACING, 0, y * 65536 + x);
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
        /// Update the style object according to the current Editor state.
        /// </summary>
        /// <param name="style">The object to be updated.</param>
        private void Update(styleObj style)
        {
       
            // general tab
            style.size = double.Parse(textBoxSize.Text);
            styleBindingControllerSize.ApplyBinding();
            style.minsize = double.Parse(textBoxMinSize.Text);
            style.maxsize = double.Parse(textBoxMaxSize.Text);
            style.width = double.Parse(textBoxWidth.Text);
            styleBindingControllerWidth.ApplyBinding();
            style.angle = double.Parse(textBoxAngle.Text);
            styleBindingControllerAngle.ApplyBinding();
            style.minwidth = double.Parse(textBoxMinWidth.Text);
            style.maxwidth = double.Parse(textBoxMaxWidth.Text);
            // display tab
            this.colorPickerColor.ApplyTo(style.color);
            styleBindingControllerColor.ApplyBinding();
            this.colorPickerBackColor.ApplyTo(style.backgroundcolor);
            this.colorPickerOutlineColor.ApplyTo(style.outlinecolor);
            styleBindingControllerOutlineColor.ApplyBinding();
            style.offsetx = int.Parse(textBoxOffsetX.Text);
            style.offsety = int.Parse(textBoxOffsetY.Text);
            style.opacity = trackBarOpacity.Value;
            // set up alpha values according to opacity
            int alpha = Convert.ToInt32(style.opacity * 2.55);
            style.color.alpha = alpha;
            style.outlinecolor.alpha = alpha;
            style.backgroundcolor.alpha = alpha;
            style.mincolor.alpha = alpha;
            style.maxcolor.alpha = alpha;

            if (checkBoxAutoAngle.Checked)
                style.autoangle = mapscript.MS_TRUE;
            else
                style.autoangle = mapscript.MS_FALSE;

            if (comboBoxGeomTransform.Text != "")
                style.setGeomTransform(comboBoxGeomTransform.Text);
            else
                style.setGeomTransform(null);

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

            if (textBoxMaxZoom.Text == "")
                style.maxscaledenom = -1;
            else
                style.maxscaledenom = double.Parse(textBoxMaxZoom.Text);

            if (textBoxMinZoom.Text == "")
                style.minscaledenom = -1;
            else
                style.minscaledenom = double.Parse(textBoxMinZoom.Text);
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
            textBoxSize.Enabled = true;

            if (layer != null && layer.symbolscaledenom >= 0)
            {
                textBoxMinSize.Enabled = true;
                textBoxMaxSize.Enabled = true;
                textBoxMinWidth.Enabled = true;
                textBoxMaxWidth.Enabled = true;
            }
            else
            {
                textBoxMinSize.Enabled = false;
                textBoxMaxSize.Enabled = false;
                textBoxMinWidth.Enabled = false;
                textBoxMaxWidth.Enabled = false;
            }

            MS_LAYER_TYPE categoryType;

            //check the style category selected
            if (comboBoxCategory.Text != "")
            {
                if (comboBoxCategory.Text == "Inline Symbols")
                {
                    categoryType = MS_LAYER_TYPE.MS_LAYER_POINT;
                }
                else
                {
                    mapObj styles = StyleLibrary.Styles;
                    categoryType = styles.getLayerByName(comboBoxCategory.Text).type;
                }
                //disable rellevant control according to the category ticket #4982
                if (categoryType == MS_LAYER_TYPE.MS_LAYER_POINT)
                {
                    textBoxPattern.Enabled = false;
                }
                else if (categoryType == MS_LAYER_TYPE.MS_LAYER_LINE)
                {
                    if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0].Text != "Default")
                    {
                        textBoxPattern.Enabled = false;
                        textBoxGap.Enabled = false;
                    }
                }
                else if (categoryType == MS_LAYER_TYPE.MS_LAYER_POLYGON)
                {
                    if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0].Text != "Default")
                    {
                        textBoxPattern.Enabled = false;
                        textBoxGap.Enabled = false;
                        textBoxSize.Enabled = false;
                        textBoxAngle.Enabled = false;
                        textBoxWidth.Enabled = false;
                    }
                }
            }

            if (isLabelStyle)
            {
                textBoxMinZoom.Enabled = false;
                textBoxMaxZoom.Enabled = false;
                checkBoxAutoAngle.Enabled = false;
                buttonMinScale.Enabled = false;
                buttonMaxScale.Enabled = false;
            }
        }

        private void UpdatePreview()
        {
            if (style != null && enablePreview)
            {
                styleObj pstyle = style.clone();
                Update(pstyle);

                // apply current settings (opacity) to colors
                colorPickerColor.SetColor(pstyle.color);
                colorPickerBackColor.SetColor(pstyle.backgroundcolor);
                colorPickerOutlineColor.SetColor(pstyle.outlinecolor);               
                
                // select the proper map containing symbols
                mapObj stylemap = map;
                if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0].Text != "Default")
                {
                    if (comboBoxCategory.Text != "Inline Symbols")
                        stylemap = StyleLibrary.Styles;
                    styleObj classStyle = (styleObj)listView.SelectedItems[0].Tag;
                    pstyle.setSymbolByName(stylemap, classStyle.symbolname);
                }
                else
                {
                    pstyle.symbol = 0;
                    pstyle.symbolname = null;
                }

                classObj styleclass = new classObj(null);
                styleclass.insertStyle(pstyle, -1);

                using (classObj def_class = new classObj(null)) // for drawing legend images
                {
                    using (imageObj image2 = def_class.createLegendIcon(
                                     stylemap, layer, pictureBoxSample.Width, pictureBoxSample.Height))
                    {
                        MS_LAYER_TYPE type = layer.type;
                        try
                        {
                            // modify the layer type in certain cases for drawing correct images
                            if (comboBoxGeomTransform.Text.ToLower().Contains("labelpoly"))
                                layer.type = MS_LAYER_TYPE.MS_LAYER_POLYGON;
                            else if (comboBoxGeomTransform.Text.ToLower().Contains("labelpnt") || comboBoxGeomTransform.Text.ToLower().Contains("centroid"))
                                layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;

                            styleclass.drawLegendIcon(stylemap, layer,
                            pictureBoxSample.Width - 10, pictureBoxSample.Height - 10, image2, 4, 4);
                        }
                        finally
                        {
                            layer.type = type;
                        }
                                              
                        byte[] img = image2.getBytes();
                        using (MemoryStream ms = new MemoryStream(img))
                        {
                            pictureBoxSample.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
        }

        private ListViewItem AddListItem(styleObj classStyle, layerObj layer, string name)
        {
            ListViewItem item = null;
            classObj styleclass = new classObj(null);
            styleclass.insertStyle(classStyle, -1);
            mapObj map2 = map;
            if (layer.map != null)
                map2 = layer.map;
            // creating the listicons
            using (classObj def_class = new classObj(null)) // for drawing legend images
            {
                using (imageObj image2 = def_class.createLegendIcon(
                                 map2, layer, imageList.ImageSize.Width, imageList.ImageSize.Height))
                {
                    MS_LAYER_TYPE type = layer.type;
                    try
                    {
                        //SETPH: actually we should not modify the type of the style(point line polygon) for the style category list, only for the preview 
                        //// modify the layer type in certain cases for drawing correct images
                        //if (comboBoxGeomTransform.Text.ToLower().Contains("labelpoly"))
                            //layer.type = MS_LAYER_TYPE.MS_LAYER_POLYGON;
                        //else if (comboBoxGeomTransform.Text.ToLower().Contains("labelpnt"))
                            //layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;

                        styleclass.drawLegendIcon(map2, layer, 44, 44, image2, 2, 2);
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

                    // add new item
                    item = new ListViewItem(name, imageList.Images.Count - 1);
                    item.ToolTipText = name;
                    item.Tag = classStyle;
                    listView.Items.Add(item);
                }
            }
            return item;
        }

        private void UpdateStyleList()
        {
            string selectedName = style.symbolname;
            if (listView.SelectedItems.Count > 0)
                selectedName = listView.SelectedItems[0].Text;
            
            // populate the style listview
            listView.Items.Clear();
            imageList.Images.Clear();

            ListViewItem selected = null;

            if (comboBoxCategory.Text != "")
            {
                // Create "no symbol" entry
                styleObj nosymbolstyle = new styleObj(null);
                MapUtils.SetDefaultColor(layer.type, nosymbolstyle);
                ListViewItem nosymbolitem = AddListItem(nosymbolstyle, layer, "Default");
                if (selectedName == null)
                    selected = nosymbolitem;
                
                if (comboBoxCategory.Text == "Inline Symbols")
                {
                    for (int i = 0; i < map.symbolset.numsymbols; i++)
                    {
                        symbolObj symbol = map.symbolset.getSymbol(i);
                        if (symbol.inmapfile == mapscript.MS_TRUE && 
                            !StyleLibrary.HasSymbol(symbol.name))
                        {
                            styleObj libstyle = new styleObj(null);
                            //if (symbol.type == (int)MS_SYMBOL_TYPE.MS_SYMBOL_PATTERNMAP)
                            //    MapUtils.SetDefaultColor(MS_LAYER_TYPE.MS_LAYER_LINE, libstyle);
                            //else
                                MapUtils.SetDefaultColor(layer.type, libstyle);
                            libstyle.setSymbolByName(map, symbol.name);
                            libstyle.size = 8;
                            MS_LAYER_TYPE type = layer.type;
                            try
                            {
                                //STEPH: change layer passed to the list view to be consistent with the other symbol categories
                                //so that it uses a point layer to display the style in the list
                                layer.type = MS_LAYER_TYPE.MS_LAYER_POINT;
                                ListViewItem item = AddListItem(libstyle, layer, symbol.name);
                                if (selectedName == item.Text)
                                    selected = item;
                            }
                            finally
                            {
                                layer.type = type;
                            }
                        }
                    }
                }
                else
                {
                    // collect all fonts specified in the fontset file
                    Hashtable fonts = new Hashtable();
                    string key = null;
                    while ((key = map.fontset.fonts.nextKey(key)) != null)
                        fonts.Add(key, key);
                    
                    mapObj styles = StyleLibrary.Styles;
                    layerObj stylelayer = styles.getLayerByName(comboBoxCategory.Text);
                    for (int i = 0; i < stylelayer.numclasses; i++)
                    {
                        classObj classobj = stylelayer.getClass(i);
                        int symbolIndex = classobj.getStyle(0).symbol;
                        if (symbolIndex >= 0)
                        {
                            string font = styles.symbolset.getSymbol(symbolIndex).font;
                            if (font != null && !fonts.ContainsKey(font))
                                continue; // this font cannot be found in fontset
                        }
 
                        ListViewItem item = AddListItem(classobj.getStyle(0), stylelayer, classobj.name);
                        if (selectedName == item.Text)
                            selected = item;
                    }
                }
            }

            if (selected != null)
            {
                selected.Selected = true;
                selected.EnsureVisible();
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
                
                // copy symbol to the map if needed
                if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0].Text != "Default")
                {
                    // try to find symbol
                    int symbolIndex;
                    string symbolName = listView.SelectedItems[0].Text;
                    for (symbolIndex = 0; symbolIndex < map.symbolset.numsymbols; symbolIndex++)
                        if (map.symbolset.getSymbol(symbolIndex).name == symbolName)
                            break;
                    if (symbolIndex == map.symbolset.numsymbols &&
                        comboBoxCategory.Text != "Inline Symbols")
                    {
                        // copy symbol from style library
                        mapObj sourceMap = StyleLibrary.Styles;
                        // removing the embedded scalebar symbol from top (should be fixed in mapserver)
                        string lastsymbolName = map.symbolset.getSymbol(map.symbolset.numsymbols - 1).name;
                        if (lastsymbolName == "scalebar" || lastsymbolName == "legend")
                            map.symbolset.removeSymbol(map.symbolset.numsymbols - 1);
                        lastsymbolName = map.symbolset.getSymbol(map.symbolset.numsymbols - 1).name;
                        if (lastsymbolName == "scalebar" || lastsymbolName == "legend")
                            map.symbolset.removeSymbol(map.symbolset.numsymbols - 1);
                        symbolIndex = map.symbolset.appendSymbol(MapUtils.CloneSymbol(
                            sourceMap.symbolset.getSymbolByName(symbolName)));
                    }
                    style.symbolname = symbolName;
                    style.symbol = symbolIndex;
                }
                else
                {
                    // remove symbol
                    style.symbol = 0;
                    style.symbolname = null;
                }

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

        /// <summary>
        /// Get the string representation of the pattern array
        /// </summary>
        /// <param name="pattern">pattern array</param>
        /// <returns>pattern string</returns>
        private string GetPattenString(double[] pattern)
        {
            if (pattern == null || pattern.Length == 0)
                return "";
            
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < pattern.Length; i++)
            {
                if (i > 0)
                    s.Append(" ");
                s.Append(pattern[i].ToString(ni));
            }
            return s.ToString();
        }

        #region IMapControl Members

        /// <summary>
        /// Refresh the controls according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            if (style == null)
                return;
            //STEPH: set first load flag to make sure values are not updated by listView_SelectedIndexChanged event
            firstLoad = true;

            if (style.size < 0) // set default size (#4339)
                textBoxSize.Text = "8";
            else
                textBoxSize.Text = style.size.ToString();
            styleBindingControllerSize.InitializeBinding(target);

            textBoxMinSize.Text = style.minsize.ToString();
            textBoxMaxSize.Text = style.maxsize.ToString();
            textBoxWidth.Text = style.width.ToString();
            styleBindingControllerWidth.InitializeBinding(target);
            textBoxAngle.Text = style.angle.ToString();
            styleBindingControllerAngle.InitializeBinding(target);
            textBoxMinWidth.Text = style.minwidth.ToString();
            textBoxMaxWidth.Text = style.maxwidth.ToString();
            textBoxOffsetX.Text = style.offsetx.ToString();
            textBoxOffsetY.Text = style.offsety.ToString();

            this.colorPickerColor.SetColor(style.color);
            styleBindingControllerColor.InitializeBinding(target);
            this.colorPickerBackColor.SetColor(style.backgroundcolor);
            this.colorPickerOutlineColor.SetColor(style.outlinecolor);
            styleBindingControllerOutlineColor.InitializeBinding(target);
            trackBarOpacity.Value = style.opacity;
            labelOpacityPercent.Text = trackBarOpacity.Value + "%";

            checkBoxAutoAngle.Checked = (style.autoangle == mapscript.MS_TRUE);

            comboBoxGeomTransform.Items.Clear();
            if (isLabelStyle)
                comboBoxGeomTransform.Items.AddRange(new string[] {"labelpnt", "labelpoly"});
            else
                comboBoxGeomTransform.Items.AddRange(new string[] { "start", "end", "vertices", "bbox", "centroid" });

            comboBoxGeomTransform.Text = style.getGeomTransform();
            textBoxGap.Text = style.gap.ToString();

            textBoxPattern.Text = GetPattenString(style.pattern);

            if (style.minscaledenom >= 0)
                textBoxMinZoom.Text = style.minscaledenom.ToString();
            else
                textBoxMinZoom.Text = "";
            if (style.maxscaledenom >= 0)
                textBoxMaxZoom.Text = style.maxscaledenom.ToString();
            else
                textBoxMaxZoom.Text = "";

            // populate the category combo
            mapObj styles = StyleLibrary.Styles;
            string selectedCategory = null;
            bool isStyleSelected = false;
            for (int i = styles.numlayers - 1; i > -1; i--)
            {
                layerObj stylelayer = styles.getLayer(i);
                if (isLabelStyle)
                {
                    // for label styles add polygon and point categories
                    if (stylelayer.type == MS_LAYER_TYPE.MS_LAYER_POLYGON || 
                        stylelayer.type ==MS_LAYER_TYPE.MS_LAYER_POINT)
                        comboBoxCategory.Items.Add(stylelayer.name);
                    if (selectedCategory == null && ((stylelayer.type == MS_LAYER_TYPE.MS_LAYER_POLYGON && style.getGeomTransform().Contains("labelpoly")) || 
                        (stylelayer.type == MS_LAYER_TYPE.MS_LAYER_POINT && style.getGeomTransform().Contains("labelpnt"))))
                        selectedCategory = stylelayer.name; // for label style select default
                }
                else if ((layer.type == MS_LAYER_TYPE.MS_LAYER_POLYGON ||
                    layer.type == MS_LAYER_TYPE.MS_LAYER_CIRCLE) &&
                    (stylelayer.type == MS_LAYER_TYPE.MS_LAYER_POLYGON ||
                     stylelayer.type == MS_LAYER_TYPE.MS_LAYER_LINE))
                {
                    comboBoxCategory.Items.Add(stylelayer.name);
                    if (selectedCategory == null)
                        selectedCategory = stylelayer.name; // for polygon layers select default
                }
                else if (layer.type == MS_LAYER_TYPE.MS_LAYER_LINE &&
                    stylelayer.type == MS_LAYER_TYPE.MS_LAYER_LINE)
                {
                    comboBoxCategory.Items.Add(stylelayer.name);
                    if (selectedCategory == null)
                        selectedCategory = stylelayer.name; // for line layers select default
                }
                else if (stylelayer.type == MS_LAYER_TYPE.MS_LAYER_POINT)
                {
                    comboBoxCategory.Items.Add(stylelayer.name);
                    if (selectedCategory == null)
                        selectedCategory = stylelayer.name; // for point layers select default
                }

                // select the style
                if (style.symbolname != null)
                {
                    for (int c = 0; c < stylelayer.numclasses; c++)
                    {
                        classObj styleclass = stylelayer.getClass(c);
                        styleObj libstyle = styleclass.getStyle(0);

                        if (style.symbolname == libstyle.symbolname)
                        {
                            selectedCategory = stylelayer.name;
                            isStyleSelected = true;
                            break;
                        }
                    }
                }
            }
            // check if we have inline symbols added to the map file
            bool inlineAdded = false;
            for (int i = 0; i < map.symbolset.numsymbols; i++)
            {
                symbolObj symbol = map.symbolset.getSymbol(i);
                if (symbol.inmapfile == mapscript.MS_TRUE && 
                    !StyleLibrary.HasSymbol(symbol.name))
                {
                    if (!inlineAdded)
                    {
                        comboBoxCategory.Items.Add("Inline Symbols");
                        inlineAdded = true;
                    }
                    if (!isStyleSelected && style.symbolname == symbol.name)
                        selectedCategory = "Inline Symbols";
                }
            }

            if (selectedCategory != null)
                comboBoxCategory.SelectedItem = selectedCategory;
            else if (comboBoxCategory.Items.Count > 0)
                comboBoxCategory.SelectedIndex = 0;

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
                //symbolSelectorControl.Target = null;
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
                        if (classHolder.GetType() == typeof(scalebarObj))
                        {
                            map = classHolder.GetParent();
                            layer = new layerObj(null);
                        }
                        else
                        {
                            MapObjectHolder layerHolder = classHolder.GetParent();
                            if (layerHolder != null)
                            {
                                map = layerHolder.GetParent();
                                layer = layerHolder;
                            }
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

        /// <summary>
        /// Scroll event handler of the trackBarOpacity control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void trackBarOpacity_Scroll(object sender, EventArgs e)
        {
            SetDirty(true);
            //UpdatePreview();
            labelOpacityPercent.Text = trackBarOpacity.Value + "%";
        }

        /// <summary>
        /// SelectedItemChanged event handler of the symbolSelectorControl control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void symbolSelectorControl_SelectedItemChanged(object sender, EventArgs e)
        {
            // there's no need to update the selector in this case
            SetDirty(true);
        }

        /// <summary>
        /// MouseUp event handler of the trackBarOpacity control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void trackBarOpacity_MouseUp(object sender, MouseEventArgs e)
        {
            if (style.opacity != trackBarOpacity.Value)
            {
                UpdatePreview();
            }
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxAutoAngle control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxAutoAngle_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAngle.Enabled = styleBindingControllerAngle.Enabled = !checkBoxAutoAngle.Checked;
            SetDirty(true);
        }

        /// <summary>
        /// Validating event handler of the textBoxGeomTransform control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxGeomTransform_Validating(object sender, CancelEventArgs e)
        {
            if (isLabelStyle)
            {
                if (!comboBoxGeomTransform.Text.StartsWith("labelpnt") && !comboBoxGeomTransform.Text.StartsWith("labelpoly"))
                {
                    MessageBox.Show("Label styles must have labelpnt or labelpoly geometry transformations specified!", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Validating event handler of the textBoxPattern control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
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

        /// <summary>
        /// SelectedIndexChanged event handler of the comboBoxCategory control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStyleList();
            UpdateEnabledState();
        }

        /// <summary>
        /// SelectedIndexChanged event handler of the listView control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                //STEPH: only applied the property of the selected style when user click on a style, not when dialog first load to display 
                if (!firstLoad)
                {
                    styleObj style = (styleObj)listView.SelectedItems[0].Tag;
                    textBoxPattern.Text = GetPattenString(style.pattern);
                    textBoxGap.Text = style.gap.ToString();
                    if (style.size < 0) // set default 
                        textBoxSize.Text = "8";
                    else
                        textBoxSize.Text = style.size.ToString();

                    textBoxWidth.Text = style.width.ToString();
                    textBoxAngle.Text = style.angle.ToString();
                }
                //STEPH: reset flag
                firstLoad = false;
            }
            UpdatePreview();
            UpdateEnabledState();
            SetDirty(true);
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

        /// <summary>
        /// Validated event handler of the textBoxGeomTransform control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxGeomTransform_Validated(object sender, EventArgs e)
        {
            if (isLabelStyle)
            {
                UpdateStyleList();
                UpdatePreview();
            }
        }
    }
}
