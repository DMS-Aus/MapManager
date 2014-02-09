using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DMS.MapLibrary
{
    public partial class AddStyleCategoryForm : Form
    {
        int charCount;
        
        public AddStyleCategoryForm(string categoryName)
        {
            InitializeComponent();

            comboBoxFonts.Items.Add("(Empty Category)");

            textBoxFontCategoryName.Text = categoryName;

            // loading the fontset
            if (StyleLibrary.FontsetFileName != null)
            {
                using (StringReader r = new StringReader(File.ReadAllText(StyleLibrary.FontsetFileName)))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        string[] vals = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (vals.Length >= 2)
                        {
                            comboBoxFonts.Items.Add(vals[0]);
                        }
                    }
                }
            }
            comboBoxFonts.SelectedIndex = 0;

            buttonCharMap.Visible = File.Exists(Environment.SystemDirectory + "\\charmap.exe");
        }

        public string CategoryName
        {
            get
            {
                return textBoxFontCategoryName.Text;
            }
        }

        public string CategoryType
        {
            get
            {
                return comboBoxFonts.Text;
            }
        }

        public int CharCount
        {
            get
            {
                return charCount;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxFonts.SelectedIndex > 0)
            {
                if (!int.TryParse(textBoxCharCount.Text, out charCount))
                {
                    MessageBox.Show("Invalid number: " + textBoxCharCount.Text, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBoxFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFonts.SelectedIndex > 0)
            {
                // font category selected
                textBoxCharCount.Enabled = true;
                buttonCharMap.Enabled = true;

                if (textBoxFontCategoryName.Text == "New Category")
                    textBoxFontCategoryName.Text = comboBoxFonts.Text;
            }
            else
            {
                textBoxCharCount.Enabled = false;
                buttonCharMap.Enabled = false;
            }
        }

        private void buttonCharMap_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("charmap.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to start character map, " + ex.Message, "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
