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

namespace DMS.MapLibrary
{
    /// <summary>
    /// UserControl to provide Individual value theme support.
    /// </summary>
    public partial class IndividualValuesTheme : UserControl, IPropertyEditor, IWizard
    {
        MapObjectHolder target;
        layerObj layer;
        layerObj newLayer;

        /// <summary>
        /// Constructor.
        /// </summary>
        public IndividualValuesTheme()
        {
            InitializeComponent();
            layerControl.ShowCheckBoxes = false;
            layerControl.ShowClasses = true;
            layerControl.ShowStyles = true;
            layerControl.ShowLabels = true;
            layerControl.ShowRootObject = false;
            layerControl.EditProperties += new EditPropertiesEventHandler(layerControl_EditProperties);
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

        /// <summary>
        /// Create the theme according to the individual values of the layer contents
        /// </summary>
        private MapObjectHolder CreateLayerTheme()
        {
            if (layer == null)
                return null;

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

            SortedDictionary<string,string> items = new SortedDictionary<string,string>();
            int i = 0;

            shapeObj shape;

            layer.open();

            layer.whichShapes(layer.getExtent());

            if (checkBoxClassItem.Checked)
                layer.classitem = comboBoxColumns.SelectedItem.ToString();

            while ((shape = layer.nextShape()) != null)
            {
                string value = shape.getValue(comboBoxColumns.SelectedIndex);
                if (checkBoxZero.Checked && (value == "" || value == ""))
                    continue;

                if (!items.ContainsValue(value))
                {
                    if (i == 100)
                    {
                        if (MessageBox.Show("The number of the individual values is greater than 100 would you like to continue?","MapManager",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                    
                    items.Add(value, value);
                    
                    ++i;
                }
            }

            if (layer.getResults() == null)
                layer.close(); // close only is no query results

            i = 0;
            foreach (string value in items.Keys)
            {
                double percent = ((double)(i + 1)) / items.Count * 100;
                
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

                classobj.name = value;
                if (checkBoxClassItem.Checked)
                    classobj.setExpression(value);
                else
                    classobj.setExpression("('[" + comboBoxColumns.SelectedItem + "]' = '" + value + "')");

                for (int j = 0; j < classobj.numstyles; j++)
                {
                    styleObj style = classobj.getStyle(j);
                    style.color = colorRampPickerColor.GetMapColorAtValue(percent);
                    style.outlinecolor = colorRampPickerOutlineColor.GetMapColorAtValue(percent);

                    if (checkBoxFirstOnly.Checked)
                        break;
                }
                ++i;
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

            layer.open();
            for (int i = 0; i < layer.numitems; i++)
            {
                comboBoxColumns.Items.Add(layer.getItem(i));
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

        private void checkBoxClassItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateThemeView();
        }
    }
}
