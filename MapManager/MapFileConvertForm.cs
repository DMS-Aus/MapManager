using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DMS.MapManager
{
    /// <summary>
    /// A form for displaying mapfile conversion information.
    /// </summary>
    public partial class MapFileConvertForm : Form
    {
        AppSettings settings;
        DMS.MapLibrary.ColorRampPicker colorRampPicker;
        ListBox listBoxColorRamp;

        /// <summary>
        /// Constructs a new MapFileConvertForm object.
        /// </summary>
        /// <param name="settings">The property bag for holding the settings.</param>
        public MapFileConvertForm(string message)
        {
            InitializeComponent();
            textBoxMessage.Text = message;
        }

        /// <summary>
        /// KeyDown event handler for the MapFileConvertForm object.
        /// </summary>
        /// <param name="sender">The source object of thi
        private void MapFileConvertForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}