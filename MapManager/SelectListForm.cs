using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DMS.MapManager
{
    /// <summary>
    /// ToolWindow to display the list of the selected features.
    /// </summary>
    public partial class SelectListForm : Form
    {
        /// <summary>
        /// Constructs the SelectListForm object.
        /// </summary>
        public SelectListForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// FormClosing Event handler for the SelectListForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void SelectListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        /// <summary>
        /// KeyDown Event handler for the SelectListForm object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">Event parameters.</param>
        private void SelectListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}