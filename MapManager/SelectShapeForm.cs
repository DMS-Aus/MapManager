using DMS.MapLibrary;
using OSGeo.MapServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMS.MapManager
{
    public partial class SelectShapeForm : Form
    {
        private MapControl target;
        public SelectShapeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// FormClosing Event handler for the SelectShapeForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void SelectShapeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        /// <summary>
        /// KeyDown Event handler for the SelectShapeForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void SelectShapeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Gets and sets the target object of the selector.
        /// </summary>
        public MapControl Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;

            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            target.QueryByShape(textBoxShape.Text, checkBoxCenter.Checked);
        }
    }
}
