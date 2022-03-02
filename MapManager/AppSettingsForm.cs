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
    /// A form for setting the application parameters.
    /// </summary>
    public partial class AppSettingsForm : Form
    {
        AppSettings settings;
        DMS.MapLibrary.ColorRampPicker colorRampPicker;
        ListBox listBoxColorRamp;

        /// <summary>
        /// Constructs a new AppSettingsForm object.
        /// </summary>
        /// <param name="settings">The property bag for holding the settings.</param>
        public AppSettingsForm(AppSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadState();

            colorRampPicker = new DMS.MapLibrary.ColorRampPicker();
            listBoxColorRamp = colorRampPicker.GetListBox();
            panelColorRamp.Controls.Add(this.listBoxColorRamp);
            listBoxColorRamp.Dock = DockStyle.Fill;
            listBoxColorRamp.Visible = true;
            listBoxColorRamp.SelectedIndexChanged += new EventHandler(listBoxColorRamp_SelectedIndexChanged);
            UpdateColorRampState();
            colorRampPicker.UpdateListBox(listBoxColorRamp);
        }

        /// <summary>
        /// Update the enabled state of the colorramp buttons
        /// </summary>
        private void UpdateColorRampState()
        {
            buttonEditColorRamp.Enabled = buttonDeleteColorRamp.Enabled = listBoxColorRamp.SelectedIndex >= 0;
        }

        /// <summary>
        /// SelectedIndexChanged event handler for the listBoxColorRamp object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        void listBoxColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateColorRampState();
        }

        /// <summary>
        /// Click event handler for the buttonNewColorRamp object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonNewColorRamp_Click(object sender, EventArgs e)
        {
            colorRampPicker.NewValue();
            colorRampPicker.UpdateListBox(listBoxColorRamp);
        }

        /// <summary>
        /// Click event handler for the buttonEditColorRamp object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonEditColorRamp_Click(object sender, EventArgs e)
        {
            colorRampPicker.EditValue(listBoxColorRamp.SelectedItem.ToString());
            colorRampPicker.UpdateListBox(listBoxColorRamp);
        }

        /// <summary>
        /// Click event handler for the buttonDeleteColorRamp object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonDeleteColorRamp_Click(object sender, EventArgs e)
        {
            colorRampPicker.DeleteValue(listBoxColorRamp.SelectedItem.ToString());
            colorRampPicker.UpdateListBox(listBoxColorRamp);
        }

        /// <summary>
        /// Loading the state of the control from the property bag. 
        /// </summary>
        private void LoadState()
        {
            checkBoxAutoLoadRecent.Checked = settings.AutoLoadRecent;
            checkBoxEnableConversion.Checked = settings.EnableConversion;
            colorPickerLayerBackgroundColor.Value = settings.LayerControlBackColor;
            checkBoxCheck.Checked = settings.LayerControlShowCheckBoxes;
            checkBoxClasses.Checked = settings.LayerControlShowClasses;
            checkBoxRoot.Checked = settings.LayerControlShowRootNode;
            checkBoxStyles.Checked = settings.LayerControlShowStyles;
            checkBoxLabels.Checked = settings.LayerControlShowLabels;
            textBoxEditor.Text = settings.TextEditor;
        }

        /// <summary>
        /// Saving the state of the control to the property bag. 
        /// </summary>
        private void SaveState()
        {
            settings.AutoLoadRecent = checkBoxAutoLoadRecent.Checked;
            settings.LayerControlBackColor = colorPickerLayerBackgroundColor.Value;
            settings.LayerControlShowCheckBoxes = checkBoxCheck.Checked;
            settings.LayerControlShowClasses = checkBoxClasses.Checked;
            settings.LayerControlShowRootNode = checkBoxRoot.Checked;
            settings.LayerControlShowStyles = checkBoxStyles.Checked;
            settings.LayerControlShowLabels = checkBoxLabels.Checked;
            settings.TextEditor = textBoxEditor.Text.Trim();
        }

        /// <summary>
        /// Click event handler for the buttonOK object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveState();
            this.Close();
        }

        /// <summary>
        /// Click event handler for the buttonCancel object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Validating event handler for the textBoxEditor object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void textBoxEditor_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxEditor.Text.Trim().Length == 0)
                return;
            
            if (!File.Exists(textBoxEditor.Text.Trim()))
            {
                MessageBox.Show("The specified file doesn't exist!",
                    "MapManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Click event handler for the buttonEditor object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void buttonEditor_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Executable Files|*.exe";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxEditor.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// KeyDown event handler for the AppSettingsForm object.
        /// </summary>
        /// <param name="sender">The source object of thi
        private void AppSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}