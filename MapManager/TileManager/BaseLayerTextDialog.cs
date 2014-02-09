using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DMS.MapManager.TileManager
{
    public partial class BaseLayerTextDialog : Form
    {
        // globals
        string mapfilepath;

        public BaseLayerTextDialog(string mapfile, string baseLayerText)
        {
            InitializeComponent();
            mapfilepath = mapfile;
            txtConfigManager.Text = baseLayerText;
        }

        private void BaseLayerTextDialog_Load(object sender, EventArgs e)
        {
            // highlight some text red which the user must replace themselves
            Font boldfont = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            string boldstring;

            boldstring = "<enter your hosted tile folder here>";
            if (txtConfigManager.Find(boldstring) > 0)
            {
                int start = txtConfigManager.Find(boldstring);
                txtConfigManager.SelectionStart = start;
                txtConfigManager.SelectionLength = boldstring.Length;
                txtConfigManager.SelectionFont = boldfont;
                txtConfigManager.SelectionColor = Color.DarkRed;
            }

            boldstring = mapfilepath.Substring(mapfilepath.LastIndexOf("\\") + 1);
            if (txtConfigManager.Find(boldstring) > 0)
            {
                int start = txtConfigManager.Find(boldstring);
                txtConfigManager.SelectionStart = start;
                txtConfigManager.SelectionLength = boldstring.Length;
                txtConfigManager.SelectionFont = boldfont;
                txtConfigManager.SelectionColor = Color.DarkRed;
            }
            txtConfigManager.SelectionLength = 0;
        }
    }
}
