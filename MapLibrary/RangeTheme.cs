using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using OSGeo.MapServer;
using System.Globalization;

namespace DMS.MapLibrary
{
    /// <summary>
    /// UserControl to provide Range theme support.
    /// </summary>
    public partial class RangeTheme : UserControl, IPropertyEditor, IWizard
    {
        MapObjectHolder target;
        layerObj layer;
        layerObj newLayer;

        // field statistics
        double[] maxvalue;
        double[] minvalue;
        string[] fieldName;
        bool[] isNumber;
        bool[] isInteger;
        int[] valueCount;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RangeTheme()
        {
            InitializeComponent();
            layerControl.ShowCheckBoxes = false;
            layerControl.ShowClasses = true;
            layerControl.ShowStyles = true;
            layerControl.ShowLabels = true;
            layerControl.ShowRootObject = false;
            layerControl.EditProperties += new EditPropertiesEventHandler(layerControl_EditProperties);
            comboBoxMode.SelectedIndex = 0;
            SelectPage(1);
        }

        /// <summary>
        /// EditProperties event handler of the layerControl control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="target">The target object to be edited.</param>
        void layerControl_EditProperties(object sender, MapObjectHolder target)
        {
            try
            {
                MapPropertyEditorForm mapPropertyEditor = new MapPropertyEditorForm(target);
                mapPropertyEditor.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private double[] CalculateEqualInterval(int classes, int index)
        {
            // Equal interval algorithm
            //
            // Returns breaks based on dividing the range ('minimum' to 'maximum')
            // into 'classes' parts.

            double step = (maxvalue[index] - minvalue[index]) / classes;

            double[] breaks = new double[classes+1];
            double value = minvalue[index];
            for (int i = 0; i < classes; i++)
            {
                if (isInteger[index])
                    breaks[i] = Math.Round(value);
                else
                    breaks[i] = value;

                value += step;
            }

            // floating point arithmetics is not precise:
            // set the last break to be exactly maximum so we do not miss it
            breaks[classes] = maxvalue[index];
            return breaks;
        }

        /// <summary>
        /// Create the theme according to the individual values of the layer contents
        /// </summary>
        private MapObjectHolder CreateLayerTheme()
        {
            if (layer == null)
                return null;
            
            int index;

            for (index = 0; index < fieldName.Length; index++)
            {
                if (fieldName[index] == comboBoxColumns.Text)
                    break;
            }
            if (index == fieldName.Length)
                return null;

            NumberFormatInfo ni = new NumberFormatInfo();
            ni.NumberDecimalSeparator = ".";
            mapObj map = target.GetParent();

            // create a new map object
            mapObj newMap = new mapObj(null);
            newMap.units = MS_UNITS.MS_PIXELS;
            map.selectOutputFormat(map.imagetype);
            // copy symbolset
            for (int s = 1; s < map.symbolset.numsymbols; s++)
            {
                symbolObj origsym = map.symbolset.getSymbol(s);
                newMap.symbolset.appendSymbol(MapUtils.CloneSymbol(origsym));
            }
            // copy the fontset
            string key = null;
            while ((key = map.fontset.fonts.nextKey(key)) != null)
                newMap.fontset.fonts.set(key, map.fontset.fonts.get(key, ""));

            newLayer = new layerObj(newMap);
            newLayer.type = layer.type;
            newLayer.status = mapscript.MS_ON;
            newLayer.connectiontype = MS_CONNECTION_TYPE.MS_INLINE;
            // add the classObj and styles
            classObj classobj;
            if (checkBoxKeepStyles.Checked)
            {
                classobj = layer.getClass(0).clone();
                classobj.setExpression(""); // remove expression to have the class shown
                // bindings are not supported with sample maps
                for (int s = 0; s < classobj.numstyles; s++)
                    StyleBindingController.RemoveAllBindings(classobj.getStyle(s));
                for (int l = 0; l < classobj.numlabels; l++)
                    LabelBindingController.RemoveAllBindings(classobj.getLabel(l));
                newLayer.insertClass(classobj, -1);
            }
            else
            {
                classobj = new classObj(newLayer);
                classobj.name = MapUtils.GetClassName(newLayer);
                styleObj style = new styleObj(classobj);
                style.size = 8; // set default size (#4339) 

                if (layer.type == MS_LAYER_TYPE.MS_LAYER_POINT)
                {
                    // initialize with the default marker if specified in the symbol file for point symbols
                    symbolObj symbol;
                    for (int s = 0; s < map.symbolset.numsymbols; s++)
                    {
                        symbol = map.symbolset.getSymbol(s);

                        if (symbol.name == "default-marker")
                        {
                            style.symbol = s;
                            break;
                        }
                    }
                }
                MapUtils.SetDefaultColor(layer.type, style);
            }

            // calculate breaks
            int classes = (int)numericUpDownClasses.Value;
            double[] breaks = null;
            if (comboBoxMode.SelectedIndex == 0)
                breaks = CalculateEqualInterval(classes, index);

            if (breaks == null)
                return null;
            
            for (int i = 0; i < classes; i++)
            {
                double percent = ((double)(i + 1)) / classes * 100;
                // creating the corresponding class object
                if (i > 0)
                {
                    classobj = classobj.clone();
                    // bindings are not supported with sample maps
                    for (int s = 0; s < classobj.numstyles; s++)
                        StyleBindingController.RemoveAllBindings(classobj.getStyle(s));
                    for (int l = 0; l < classobj.numlabels; l++)
                        LabelBindingController.RemoveAllBindings(classobj.getLabel(l));
                    newLayer.insertClass(classobj, -1);
                }

                classobj.name = breaks[i].ToString(ni) + " - " + breaks[i + 1].ToString(ni);
                classobj.setExpression("(([" + comboBoxColumns.SelectedItem + "] >= " 
                    + breaks[i].ToString(ni) + ") && ([" + comboBoxColumns.SelectedItem + "] <= " 
                    + breaks[i+1].ToString(ni) + "))");
                for (int j = 0; j < classobj.numstyles; j++)
                {
                    styleObj style = classobj.getStyle(j);
                    style.color = colorRampPickerColor.GetMapColorAtValue(percent);
                    style.outlinecolor = colorRampPickerOutlineColor.GetMapColorAtValue(percent);
                    style.backgroundcolor = colorRampPickerBackgroundColor.GetMapColorAtValue(percent);

                    if (checkBoxFirstOnly.Checked)
                        break;
                }
            }

            return new MapObjectHolder(newLayer, new MapObjectHolder(newMap, null));
        }

        #region IPropertyEditor Members

        /// <summary>
        /// Cancel the pending changes in the underlying object.
        /// </summary>
        public void CancelEditing()
        {
        }

        /// <summary>
        /// Have the editor to update the theme on the layer object.
        /// </summary>
        public void UpdateValues()
        {
            if (newLayer != null)
            {
                // remove the auto style from this layer
                layer.styleitem = null;
                
                while (layer.numclasses > 0)
                    layer.removeClass(layer.numclasses - 1);

                for (int i = 0; i < newLayer.numclasses; i++)
                {
                    classObj classobj = newLayer.getClass(i).clone();
                    // bindings are not supported with sample maps
                    for (int s = 0; s < classobj.numstyles; s++)
                        StyleBindingController.RemoveAllBindings(classobj.getStyle(s));
                    for (int l = 0; l < classobj.numlabels; l++)
                        LabelBindingController.RemoveAllBindings(classobj.getLabel(l));
                    layer.insertClass(classobj, -1);
                }

                if (target != null)
                    target.RaisePropertyChanged(this);
            }
        }

        /// <summary>
        /// Returns the modify flag.
        /// </summary>
        /// <returns>The actual value of the modify flag.</returns>
        public bool IsDirty()
        {
            return true;
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

            shapeObj shape;

            layer.open();

            // Get layer statistics
            maxvalue = new double[layer.numitems];
            minvalue = new double[layer.numitems];
            fieldName = new string[layer.numitems];
            isNumber = new bool[layer.numitems];
            isInteger = new bool[layer.numitems];
            valueCount = new int[layer.numitems];
            int i;

            for (i = 0; i < layer.numitems; i++)
            {
                isNumber[i] = true;
                isInteger[i] = true;
                valueCount[i] = 0;
                fieldName[i] = layer.getItem(i);
            }
            
            layer.whichShapes(layer.getExtent());

            NumberFormatInfo ni = new NumberFormatInfo();
            ni.NumberDecimalSeparator = ".";

            double val;
            while ((shape = layer.nextShape()) != null)
            {
                for (i = 0; i < layer.numitems; i++)
                {
                    if (isNumber[i])
                    {
                        string v = shape.getValue(i);
                        if (v != "" && Double.TryParse(v, NumberStyles.Any, ni, out val))
                        {
                            if (val % 1 != 0)
                                isInteger[i] = false;

                            if (valueCount[i] == 0)
                            {
                                maxvalue[i] = val;
                                minvalue[i] = val;
                            }
                            else
                            {
                                if (maxvalue[i] < val) maxvalue[i] = val;
                                if (minvalue[i] > val) minvalue[i] = val;
                            }
                            ++valueCount[i];
                        }
                        else
                        {
                            isNumber[i] = false;
                            isInteger[i] = false;
                        }
                    }
                }
                
            }

            for (i = 0; i < layer.numitems; i++)
            {
                if (isNumber[i])
                    comboBoxColumns.Items.Add(fieldName[i]);
            }
            if (comboBoxColumns.Items.Count > 0)
                comboBoxColumns.SelectedIndex = 0;

            if (layer.getResults() == null)
                layer.close(); // close only is no query results
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
                    if (value.GetType() != typeof(layerObj))
                        throw new ApplicationException("Invalid object type.");
                    layer = value;
                    target = value;
                    RefreshView();
                    return;
                }
                layer = null;
                target = null;
            }
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties = delegate { };

        #endregion

        #region IWizard Members

        /// <summary>
        /// Invoke the finish action of the Wizard.
        /// </summary>
        public void Finish()
        {
            UpdateValues();
        }

        /// <summary>
        /// Selecting a new page of the Wizard.
        /// </summary>
        /// <param name="page">The page number to be selected.</param>
        public void SelectPage(int page)
        {
            if (page >= 2)
            {
                WizardPage2.BringToFront();
                UpdateThemeView();
            }
            else
            {
                WizardPage1.BringToFront();
            }

            this.Refresh();
        }

        /// <summary>
        /// Returns the page count of the Wizard.
        /// </summary>
        /// <returns>The total number of the pages.</returns>
        public int GetPageCount()
        {
            return 2;
        }

        #endregion

        /// <summary>
        /// Re-creating the theme view
        /// </summary>
        private void UpdateThemeView()
        {
            layerControl.Target = CreateLayerTheme();
            layerControl.Target.PropertyChanged += new EventHandler(Target_PropertyChanged);
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxKeepStyles control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxKeepStyles_CheckedChanged(object sender, EventArgs e)
        {
            UpdateThemeView();
        }

        /// <summary>
        /// PropertyChanged event handler of the target class
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void Target_PropertyChanged(object sender, EventArgs e)
        {
            layerControl.RefreshView();
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxFirstOnly control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxFirstOnly_CheckedChanged(object sender, EventArgs e)
        {
            UpdateThemeView();
        }

        private void colorRampPickerColor_ValueChanged(object sender, EventArgs e)
        {
            UpdateThemeView();
        }

        private void colorRampPickerOutlineColor_ValueChanged(object sender, EventArgs e)
        {
            UpdateThemeView();
        }

        private void colorRampPickerBackgroundColor_ValueChanged(object sender, EventArgs e)
        {
            UpdateThemeView();
        }
    }
}
