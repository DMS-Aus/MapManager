using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a Form for creating a color ramp.
    /// </summary>
    internal partial class ColorRampForm : Form
    {
        ColorRampEditor editor;
        ColorRampValueList values;
        /// <summary>
        /// Constructs a new NewColorRampForm class.
        /// </summary>
        public ColorRampForm(ColorRampEditor editor, string key)
        {
            this.editor = editor;
            
            InitializeComponent();
            if (key != null && ColorRampConverter.ColorRampList.ContainsKey(key))
            {
                values = ColorRampConverter.ColorRampList[key];
                this.Text = "Edit Colour Ramp";
                textBoxName.Text = key;
                textBoxName.Enabled = false;
                if (values.ContainsKey(0))
                    colorPickerStart.Value = values[0];
                if (values.ContainsKey(100))
                    colorPickerEnd.Value = values[100];
            }
            else
            {
                values = new ColorRampValueList();
                values.Add(0, colorPickerStart.Value);
                values.Add(100, colorPickerEnd.Value);
            }

            comboBoxStyle.DataSource = Enum.GetValues(typeof(ColorRampStyle));
            comboBoxStyle.SelectedItem = ColorRampStyle.Gradient;
            UpdateState();
            UpdatePreview();
        }

        public ColorRampValueList Value
        {
            get
            {
                values.Text = textBoxName.Text;
                return values;
            }
        }

        private void UpdateState()
        {
            buttonRemove.Enabled = buttonEdit.Enabled = (listViewStops.SelectedItems.Count > 0);
        }

        private void UpdatePreview()
        {
            Bitmap image = new Bitmap(pictureBoxPreview.Size.Width, pictureBoxPreview.Size.Height);
            editor.PaintValue(values, Graphics.FromImage(image), pictureBoxPreview.ClientRectangle);
            pictureBoxPreview.Image = image;

            listViewStops.Items.Clear();
            imageListPreview.Images.Clear();
            for (int i = 1; i < values.Count - 1; i++)
            {
                Bitmap image2 = new Bitmap(16, 16);
                using (Graphics gfx = Graphics.FromImage(image2))
                using (SolidBrush brush = new SolidBrush(values.Values[i]))
                {
                    gfx.FillRectangle(brush, 0, 0, 16, 16);
                }
                imageListPreview.Images.Add(image2);
                string colorName = new ColorConverter().ConvertToInvariantString(values.Values[i]);
                ListViewItem item = new ListViewItem(colorName, i - 1);
                item.SubItems.Add(values.Keys[i].ToString());
                item.Tag = values.Keys[i];
                listViewStops.Items.Add(item);
            }

            listViewStops.SmallImageList = imageListPreview;
        }

        private void colorPickerStart_ValueChanged(object sender, EventArgs e)
        {
            values.Remove(0);
            values.Add(0, colorPickerStart.Value);
            UpdatePreview();
        }

        private void colorPickerEnd_ValueChanged(object sender, EventArgs e)
        {
            values.Remove(100);
            values.Add(100, colorPickerEnd.Value);
            UpdatePreview();
        }

        private void comboBoxStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            values.Style = (ColorRampStyle)comboBoxStyle.SelectedItem;
            UpdatePreview();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ColorStopForm form = new ColorStopForm(values, -1);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                UpdatePreview();
                UpdateState();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            ColorStopForm form = new ColorStopForm(values, listViewStops.SelectedItems[0].Index + 1);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                UpdatePreview();
                UpdateState();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewStops.SelectedItems)
            {
                values.Remove((double)item.Tag);
            }
            UpdatePreview();
            UpdateState();
        }

        private void listViewStops_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateState();
        }
    }
}