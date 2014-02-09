using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapServer;
using System.Xml;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a Form for changing the map view.
    /// </summary>
    public partial class AddWMSLayerDetailsForm : Form
    {
        /// <summary>
        /// Constructs a new ChangeViewForm class.
        /// </summary>
        public AddWMSLayerDetailsForm(XmlDocument doc, string url, string version)
        {
            InitializeComponent();

            textBoxServerURL.Text = url;
            textBoxVersion.Text = version;

            XmlNode node = doc.SelectSingleNode("//Service");
            if (node != null)
            {
                XmlNode n2 = node.SelectSingleNode("Title");
                if (n2 != null)
                    textBoxServerName.Text = n2.InnerText;
                n2 = node.SelectSingleNode("Abstract");
                if (n2 != null)
                    textBoxAbstract.Text = n2.InnerText;
            }

        }

        /// <summary>
        /// Click event handler of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// KeyDown event handler of the AddWMSLayerDetailsForm control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void AddWMSLayerDetailsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}