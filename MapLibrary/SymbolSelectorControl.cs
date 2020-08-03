using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;
using System.IO;
using System.Runtime.InteropServices;

namespace DMS.MapLibrary
{
    /// <summary>
    /// User Control to select a symbol from the pre-definied symbol set.
    /// </summary>
    public partial class SymbolSelectorControl : UserControl, IPropertyEditor
    {
        private MapObjectHolder target;
        private mapObj map;
        private layerObj layer;
        private styleObj style;
        private bool dirtyFlag = false;
        private MapControl mapControl;
        private int imageSize = 48;

        /// <summary>
        /// The SelectedItemChanged event handler. Raised when a new item is selected in the list.
        /// </summary>
        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// Constructs the SymbolSelectorControl object.
        /// </summary>
        public SymbolSelectorControl()
        {
            InitializeComponent();
            mapControl = new MapControl();
            // setting up the parameters of thumbnail images
            this.mapControl.Size = new System.Drawing.Size(imageSize, imageSize);
            this.mapControl.Gap = 4;
            SetSpacing((short)(imageSize + 10), (short)(imageSize + 10));
            imageList.ImageSize = new Size(imageSize, imageSize);
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
            SendMessage(this.listViewPreview.Handle, LVM_SETICONSPACING, 0, y * 65536 + x);
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
        /// Setting a symbol item in the list of the symbols.
        /// </summary>
        /// <param name="name">The name of the symbol item.</param>
        private ListViewItem SetSymbolItem(string name, int index)
        {
            mapControl.RefreshView();
            if (listView.Items.Count > index)
            {
                // update existing item
                imageList.Images[index] = mapControl.MapImage;
                listView.Items[index].Tag = name;
                listView.Items[index].ToolTipText = name;
                return listView.Items[index];
            }
            else
            {
                // add new item
                imageList.Images.Add(mapControl.MapImage);
                ListViewItem item = new ListViewItem("", imageList.Images.Count - 1);
                item.ToolTipText = name;
                item.Tag = name;
                listView.Items.Add(item);
                return item;
            }         
        }


        #region IPropertyEditor Members

        /// <summary>
        /// Let the editor to update the modified values to the underlying object.
        /// </summary>
        public void UpdateValues()
        {
            if (map == null)
                return;
            
            if (listView.SelectedItems.Count > 0)
            {
                styleObj style = target;
                mapObj map2;
                if (target.GetParent().GetType() == typeof(labelObj))
                    map2 = target.GetParent().GetParent().GetParent().GetParent();
                else
                    map2 = target.GetParent().GetParent().GetParent();
                string symbolName = (string)listView.SelectedItems[0].Tag;
                if (symbolName == "(default)")
                {
                    style.symbol = 0;
                }
                else
                {
                    style.setSymbolByName(map2, symbolName);
                }
            }
        }

        /// <summary>
        /// Cancel the pending changes in the underlying object.
        /// </summary>
        public void CancelEditing()
        {
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

        #region IMapControl Members

        /// <summary>
        /// Refresh the control according to the underlying object.
        /// </summary>
        public void RefreshView()
        {
            if (map != null)
            {
                symbolObj symbol;
                string selectedSymbol = style.symbolname;
                int selected = style.symbol;
                MS_LAYER_TYPE layerType = layer.type;

                ListViewItem selectedItem = null;

                int itemIndex = 0;

                for (int i = 0; i < map.symbolset.numsymbols; i++)
                {
                    symbol = map.symbolset.getSymbol(i);

                    if (symbol.name == null)
                        symbol.name = "Symbol" + i;

                    if ((MS_SYMBOL_TYPE)symbol.type == MS_SYMBOL_TYPE.MS_SYMBOL_TRUETYPE)
                    {
                        // looking up the referred font file and skip if not exists
                        string fontFile = map.fontset.fonts.get(symbol.font, null);
                        if (fontFile == null)
                            continue;
                        if (!File.Exists(fontFile))
                            continue;
                    }
                    else if ((MS_SYMBOL_TYPE)symbol.type == MS_SYMBOL_TYPE.MS_SYMBOL_PIXMAP)
                    {
                        // looking up the referred image file and skip if not exists
                        if (symbol.imagepath == null)
                            continue;
                        if (!File.Exists(map.mappath + symbol.imagepath))
                            continue;
                    }

                    // pre-filter mapinfo auto styles
                    if (symbol.name.StartsWith("mapinfo-brush") && layerType != MS_LAYER_TYPE.MS_LAYER_POLYGON)
                        continue;

                    if (symbol.name.StartsWith("mapinfo-pen") && layerType != MS_LAYER_TYPE.MS_LAYER_LINE)
                        continue;

                    if (i == 0)
                    {
                        style.symbol = 0;

                        if (selected == i && listView.Items.Count == itemIndex)
                            selectedItem = SetSymbolItem("(default)", itemIndex);
                        else
                            SetSymbolItem("(default)", itemIndex);
                    }
                    else
                    {
                        style.symbol = i;
                        
                        if ((selected == i || string.Compare(selectedSymbol, symbol.name) == 0)
                            && listView.Items.Count == itemIndex)
                            selectedItem = SetSymbolItem(symbol.name, itemIndex);
                        else
                            SetSymbolItem(symbol.name, itemIndex);
                    }

                    ++itemIndex;
                }

                listView.LargeImageList = imageList;
                listViewPreview.LargeImageList = imageList;

                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                    selectedItem.EnsureVisible();
                }

                this.listView.Refresh();
                UpdatePreview();
            }
            SetDirty(false);
        }

        /// <summary>
        /// Gets and sets the target object of the editor.
        /// </summary>
        public MapObjectHolder Target
        {
            get
            {
                return mapControl.Target;
            }
            set
            {
                if (value == null)
                {
                    mapControl.Target = null;
                    style = null;
                    map = null;
                    target = null;
                    return;
                }
                if (value.GetType() == typeof(styleObj))
                {
                    mapControl.Target = value;
                    style = mapControl.Target;
                    if (mapControl.Target.GetParent().GetType() == typeof(labelObj))
                    {
                        layer = mapControl.Target.GetParent().GetParent().GetParent();
                        map = mapControl.Target.GetParent().GetParent().GetParent().GetParent();
                    }
                    else
                    {
                        layer = mapControl.Target.GetParent().GetParent();
                        map = mapControl.Target.GetParent().GetParent().GetParent();
                    }
                    target = value;
                }
                else
                    throw new Exception("Invalid target type: " + value.GetType());
            }
        }

        /// <summary>
        /// The EditProperties event handler. Called when a child object should be edited
        /// </summary>
        public event EditPropertiesEventHandler EditProperties;

        #endregion

        /// <summary>
        /// SelectedIndexChanged event handler of the listView control
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDirty(true);
            if (SelectedItemChanged != null)
                SelectedItemChanged(this, null);
            UpdatePreview();
        }

        /// <summary>
        /// Update the preview with the currently selected list item.
        /// </summary>
        private void UpdatePreview()
        {
            listViewPreview.Items.Clear();
            if (listView.SelectedItems.Count > 0)
                listViewPreview.Items.Add((ListViewItem)listView.SelectedItems[0].Clone());
        }
    }
}
