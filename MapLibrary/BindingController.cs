using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;
using MapLibrary.Properties;
using System.Drawing;

namespace DMS.MapLibrary
{
    class BindingController : LinkLabel
    {
        protected Control targetControl;
        protected ComboBox itemList;
        protected PictureBox pbox;
        protected MapObjectHolder target;

        public event EventHandler ValueChanged;
        private bool bindingState;

        public Control TargetControl
        {
            get
            {
                return targetControl;
            }
            set
            {
                targetControl = value;
                if (value != null)
                {
                   
                }
            }
        }

        public bool BindingState
        {
            get
            {
                return bindingState; 
            }
            set
            {
                bindingState = value;
                if (itemList != null && pbox != null)
                {
                    itemList.Visible = value;
                    pbox.Visible = value;
                    RaiseValueChanged();
                }
            }
        }

        protected override void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
        {
            base.OnLinkClicked(e);
            BindingState = !BindingState;
        }

        public virtual void InitializeBinding(MapObjectHolder target)
        {
        }

        public virtual void ApplyBinding()
        {
        }

        protected void RaiseValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, null);
        }
    }


    class LabelBindingController : BindingController
    {    
        MS_LABEL_BINDING_ENUM labelBinding;

        public MS_LABEL_BINDING_ENUM LabelBinding
        {
            get
            {
                return labelBinding;
            }
            set
            {
                labelBinding = value;
            }
        }

        public override void ApplyBinding()
        {
            if (target.GetParent().GetParent().GetType() != typeof(layerObj))
                return;
            
            labelObj label = target;
            if (BindingState)
            {
                label.setBinding((int)labelBinding, itemList.SelectedItem.ToString());
            }
            else
            {
                label.removeBinding((int)labelBinding);
            }
        }

        public static void RemoveAllBindings(labelObj label)
        {
            for (int i = 0; i < mapscript.MS_LABEL_BINDING_LENGTH; i++)
            {
                label.removeBinding(i);
            }
        }

        public override void InitializeBinding(MapObjectHolder target)
        {
            this.target = target;
            
            if (target.GetParent().GetParent().GetType() != typeof(layerObj))
            {
                this.Enabled = false;
                return;
            }
            
            layerObj layer = target.GetParent().GetParent();
            labelObj label = target;
            if (itemList == null)
            {
                itemList = new ComboBox();
                itemList.Width = targetControl.Width;
                itemList.Height = targetControl.Height;
                itemList.Location = targetControl.Location;
                itemList.DropDownStyle = ComboBoxStyle.DropDownList;
                itemList.SelectedIndexChanged += new EventHandler(itemList_SelectedIndexChanged);
                targetControl.Parent.Controls.Add(itemList);
                itemList.BringToFront();
                Bitmap bmp = Resources.DataConnection;
                bmp.MakeTransparent(Color.Magenta);
                pbox = new PictureBox();
                pbox.Image = bmp;
                pbox.SizeMode = PictureBoxSizeMode.AutoSize;
                pbox.Location = new System.Drawing.Point(targetControl.Location.X + targetControl.Width + 2,
                    targetControl.Location.Y + (targetControl.Height - pbox.Image.Height) /2);
                targetControl.Parent.Controls.Add(pbox);
                pbox.BringToFront(); 
            }

            BindingState = false;

            itemList.Items.Clear();
            layer.open();
            for (int i = 0; i < layer.numitems; i++)
                itemList.Items.Add(layer.getItem(i));
            if (layer.getResults() == null)
                layer.close(); // close only is no query results
            if (layer.connectiontype == MS_CONNECTION_TYPE.MS_OGR)
            {
                itemList.Items.Add("OGR:LabelFont");
                itemList.Items.Add("OGR:LabelSize");
                itemList.Items.Add("OGR:LabelText");
                itemList.Items.Add("OGR:LabelAngle");
                itemList.Items.Add("OGR:LabelFColor");
                itemList.Items.Add("OGR:LabelBColor");
                itemList.Items.Add("OGR:LabelPlacement");
                itemList.Items.Add("OGR:LabelAnchor");
                itemList.Items.Add("OGR:LabelDx");
                itemList.Items.Add("OGR:LabelDy");
                itemList.Items.Add("OGR:LabelPerp");
                itemList.Items.Add("OGR:LabelBold");
                itemList.Items.Add("OGR:LabelItalic");
                itemList.Items.Add("OGR:LabelUnderline");
                itemList.Items.Add("OGR:LabelPriority");
                itemList.Items.Add("OGR:LabelStrikeout");
                itemList.Items.Add("OGR:LabelStretch");
                itemList.Items.Add("OGR:LabelAdjHor");
                itemList.Items.Add("OGR:LabelAdjVert");
                itemList.Items.Add("OGR:LabelHColor");
                itemList.Items.Add("OGR:LabelOColor");
            }

            string binding = label.getBinding((int)labelBinding);
            if (binding != null)
            {
                itemList.SelectedItem = binding;
                BindingState = true;
            }
            else
            {
                if (itemList.Items.Count > 0)
                    itemList.SelectedIndex = 0;
            }
        }

        void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemList.Visible)
            {
                RaiseValueChanged();
            }
        }
    }

    class StyleBindingController : BindingController
    {
        MS_STYLE_BINDING_ENUM styleBinding;

        public MS_STYLE_BINDING_ENUM StyleBinding
        {
            get
            {
                return styleBinding;
            }
            set
            {
                styleBinding = value;
            }
        }

        public override void ApplyBinding()
        {
            if (target == null)
                return;
            
            if (target.GetParent().GetParent().GetType() != typeof(layerObj))
                return;

            styleObj style = target;
            if (BindingState)
            {
                style.setBinding((int)styleBinding, itemList.SelectedItem.ToString());
            }
            else
            {
                style.removeBinding((int)styleBinding);
            }
        }

        public static void RemoveAllBindings(styleObj style)
        {
            for (int i = 0; i < mapscript.MS_STYLE_BINDING_LENGTH; i++)
            {
                style.removeBinding(i);
            }
        }

        public override void InitializeBinding(MapObjectHolder target)
        {
            this.target = target;

            if (target.GetParent().GetParent().GetType() != typeof(layerObj))
            {
                this.Enabled = false;
                return;
            }

            layerObj layer = target.GetParent().GetParent();
            styleObj style = target;
            if (itemList == null)
            {
                itemList = new ComboBox();
                itemList.Width = targetControl.Width;
                itemList.Height = targetControl.Height;
                itemList.Location = targetControl.Location;
                itemList.DropDownStyle = ComboBoxStyle.DropDownList;
                itemList.SelectedIndexChanged += new EventHandler(itemList_SelectedIndexChanged);
                targetControl.Parent.Controls.Add(itemList);
                itemList.BringToFront();
                Bitmap bmp = Resources.DataConnection;
                bmp.MakeTransparent(Color.Magenta);
                pbox = new PictureBox();
                pbox.Image = bmp;
                pbox.SizeMode = PictureBoxSizeMode.AutoSize;
                pbox.Location = new System.Drawing.Point(targetControl.Location.X + targetControl.Width + 2,
                    targetControl.Location.Y + (targetControl.Height - pbox.Image.Height) / 2);
                targetControl.Parent.Controls.Add(pbox);
                pbox.BringToFront();
            }

            BindingState = false;

            itemList.Items.Clear();
            layer.open();
            for (int i = 0; i < layer.numitems; i++)
                itemList.Items.Add(layer.getItem(i));
            if (layer.getResults() == null)
                layer.close(); // close only is no query results

            if (layer.connectiontype == MS_CONNECTION_TYPE.MS_OGR)
            {
                itemList.Items.Add("OGR:LabelFont");
                itemList.Items.Add("OGR:LabelSize");
                itemList.Items.Add("OGR:LabelText");
                itemList.Items.Add("OGR:LabelAngle");
                itemList.Items.Add("OGR:LabelFColor");
                itemList.Items.Add("OGR:LabelBColor");
                itemList.Items.Add("OGR:LabelPlacement");
                itemList.Items.Add("OGR:LabelAnchor");
                itemList.Items.Add("OGR:LabelDx");
                itemList.Items.Add("OGR:LabelDy");
                itemList.Items.Add("OGR:LabelPerp");
                itemList.Items.Add("OGR:LabelBold");
                itemList.Items.Add("OGR:LabelItalic");
                itemList.Items.Add("OGR:LabelUnderline");
                itemList.Items.Add("OGR:LabelPriority");
                itemList.Items.Add("OGR:LabelStrikeout");
                itemList.Items.Add("OGR:LabelStretch");
                itemList.Items.Add("OGR:LabelAdjHor");
                itemList.Items.Add("OGR:LabelAdjVert");
                itemList.Items.Add("OGR:LabelHColor");
                itemList.Items.Add("OGR:LabelOColor");
            }
            
            string binding = style.getBinding((int)styleBinding);
            if (binding != null)
            {
                itemList.SelectedItem = binding;
                BindingState = true;
            }
            else
            {
                if (itemList.Items.Count > 0)
                    itemList.SelectedIndex = 0;
            }
        }

        void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemList.Visible)
            {
                RaiseValueChanged();
            }
        }
    }
}
