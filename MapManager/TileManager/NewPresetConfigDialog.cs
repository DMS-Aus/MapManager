using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DMS.MapManager.TileManager
{
    public partial class NewPresetConfigDialog : Form
    {
        public NewPresetConfigDialog()
        {
            InitializeComponent();
        }

        private void btnNewPreset_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void NewPresetConfigDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
