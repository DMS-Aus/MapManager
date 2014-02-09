using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DMS.MapLibrary
{
    public partial class ColorStopForm : Form
    {
        ColorRampValueList values;
        int index;
        
        public ColorStopForm(ColorRampValueList values, int index)
        {
            this.values = values;
            this.index = index;
            InitializeComponent();

            if (index >= 0)
            {
                colorPickerStopColor.Value = values.Values[index];
                textBoxOffset.Text = values.Keys[index].ToString();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            double value;
            if (!Double.TryParse(textBoxOffset.Text, out value) || value < 0 || value > 100)
            {
                MessageBox.Show("Invalid offset value",
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (index >=0)
            {
                double key = values.Keys[index];
                values.Remove(key);
            }
            else if (values.ContainsKey(value))
                values.Remove(value);

            values.Add(value, colorPickerStopColor.Value);

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
