using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Represents a form to get the WMS authentication settings
    /// </summary>
    public partial class CredentialsForm : Form
    {
        XmlProxyUrlResolver resolver;
        public CredentialsForm(string message, XmlProxyUrlResolver resolver)
        {
            InitializeComponent();
            this.resolver = resolver;
            labelMessage.Text = message;
            NetworkCredential cred = (NetworkCredential)resolver.GetCredentials();
            if (cred != null)
            {
                textBoxUser.Text = cred.UserName;
                textBoxPassword.Text = cred.Password;
                checkBoxUseServer.Checked = true;
            }
            else
                checkBoxUseServer.Checked = false;

            comboBoxType.SelectedItem = resolver.ProxyType;

            WebProxy proxy = (WebProxy)resolver.Proxy;
            if (proxy != null)
            {
                textBoxServer.Text = proxy.Address.Host;
                textBoxPort.Text = proxy.Address.Port.ToString();
                NetworkCredential cred2 = (NetworkCredential)proxy.Credentials;
                if (cred2 != null)
                {
                    textBoxProxyUser.Text = cred2.UserName;
                    textBoxProxyPassword.Text = cred2.Password;
                }
                checkBoxUseProxy.Checked = true;
            }
            else
                checkBoxUseProxy.Checked = false;

            UpdateControls();
        }

        private void UpdateControls()
        {
            textBoxUser.Enabled = textBoxPassword.Enabled = checkBoxUseServer.Checked;
            textBoxProxyUser.Enabled = textBoxProxyPassword.Enabled =
                textBoxServer.Enabled = textBoxPort.Enabled = comboBoxType.Enabled = checkBoxUseProxy.Checked;
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxUseServer control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxUseServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        /// <summary>
        /// CheckedChanged event handler of the checkBoxUseProxy control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void checkBoxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        /// <summary>
        /// Common function to validate the integer values in the textBox controls. Called by the textBox event handlers.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void ValidateInteger(object sender, CancelEventArgs e)
        {
            int result;
            if (!int.TryParse(((TextBoxBase)sender).Text, out result))
            {
                MessageBox.Show("Invalid integer number", "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Click event handler of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxUseServer.Checked)
                    resolver.Credentials = new NetworkCredential(textBoxUser.Text, textBoxPassword.Text);
                else
                    resolver.Credentials = null;

                resolver.ProxyType = comboBoxType.Text;

                if (checkBoxUseProxy.Checked)
                {
                    WebProxy proxy = new WebProxy();
                    UriBuilder uri = new UriBuilder(comboBoxType.Text, textBoxServer.Text, int.Parse(textBoxPort.Text));
                    proxy.Address = uri.Uri;
                    proxy.Credentials = resolver.ProxyCredentials =
                        new NetworkCredential(textBoxProxyUser.Text, textBoxProxyPassword.Text);
                    resolver.Proxy = proxy;
                }
                else
                {
                    resolver.Proxy = null;
                    resolver.ProxyCredentials = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                   "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// KeyDown event handler of the CredentialsForm control.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void CredentialsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
