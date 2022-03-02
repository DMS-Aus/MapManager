using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Xml.Serialization;
using DMS.MapLibrary;
using MapManager;

namespace DMS.MapManager
{
    /// <summary>
    /// A class to hold, serialize and deserialize the application settings.
    /// </summary>
    [Serializable]
    public class AppSettings
    {
        /// <summary>
        /// The current size of the MRU list.
        /// </summary>
        public int MRUSize;
        /// <summary>
        /// The MRU list.
        /// </summary>
        public ArrayList MRU;
        /// <summary>
        /// Enable/Disable to load the recent map automatically on the program start.
        /// </summary>
        public bool AutoLoadRecent;
        /// <summary>
        /// Enable/Disable to convent mapfile on loading.
        /// </summary>
        public bool EnableConversion;
        /// <summary>
        /// Path to the text editor to edit map file.
        /// </summary>
        public string TextEditor;
        /// <summary>
        /// The propertybag of the form settings.
        /// </summary>
        public FormSettings MainFormSettings;
        /// <summary>
        /// Enable/Disable showing the checkboxes in the layer control.
        /// </summary>
        public bool LayerControlShowCheckBoxes;
        /// <summary>
        /// Enable/Disable showing the root node in the layer control.
        /// </summary>
        public bool LayerControlShowRootNode;
        /// <summary>
        /// Enable/Disable showing the class nodes in the layer control.
        /// </summary>
        public bool LayerControlShowClasses;
        /// <summary>
        /// Enable/Disable showing the style nodes in the layer control.
        /// </summary>
        public bool LayerControlShowStyles;
        /// <summary>
        /// Enable/Disable showing the label nodes in the layer control.
        /// </summary>
        public bool LayerControlShowLabels;

        /// <summary>
        /// The background color of the layer control
        /// </summary>
        [XmlIgnore()]
        public Color LayerControlBackColor;

        /// <summary>
        /// The background color of the layer control
        /// </summary>
        [XmlElement("LayerControlBackColor")]
        public int _LayerControlBackColor
        {
            get
            {
                return LayerControlBackColor.ToArgb();
            }
            set
            {
                LayerControlBackColor = Color.FromArgb(value);
            }
        }

        public ColorRampList ColorRampList;

        /// <summary>
        /// List of tile pre-configured tile settings
        /// </summary>
        [XmlArrayItem(typeof(TileSettings))]
        public ArrayList tileSettingsArray;

        /// <summary>
        /// Add the specified tileSettings object to list
        /// </summary>
        /// <param name="fileName">The TileSettings object to add</param>
        public void AddToTilePreset(TileSettings tileSettings)
        {
           tileSettingsArray.Add(tileSettings);
        }

        /// <summary>
        /// Removes a specified tileSettings object from list
        /// </summary>
        /// <param name="fileName">The TileSettings.settingsName string to remove</param>
        public void RemoveTilePreset(string name)
        {
            TileSettings toDeleteRef = null;
            foreach (TileSettings tileSettings in tileSettingsArray)
            {
                if (tileSettings.settingsName == name)
                {
                    toDeleteRef = tileSettings;
                }
            }
            tileSettingsArray.Remove(toDeleteRef);
        }
       
        /// <summary>
        /// Constructs a new AppSettings object.
        /// </summary>
        public AppSettings()
        {
            // setting up the initial values
            MRUSize = 10;
            MRU = new ArrayList();
            AutoLoadRecent = false;
            EnableConversion = true;
            TextEditor = null;
            LayerControlBackColor = Color.FromArgb(255,255,255);
            LayerControlShowCheckBoxes = true;
            LayerControlShowRootNode = true;
            LayerControlShowClasses = true;
            LayerControlShowStyles = true;
            LayerControlShowLabels = true;
            tileSettingsArray = new ArrayList();
        }

        /// <summary>
        /// Add the specified file to the Most Recent Unit (MRU) list.
        /// </summary>
        /// <param name="fileName">The file name to be added.</param>
        public void AddToMRU(string fileName)
        {
            if (fileName == null)
                return;

            foreach (object o in MRU)
            {
                if (fileName == (string)o)
                {
                    MRU.Remove(o);
                    break;
                }
            }
            while (MRU.Count >= MRUSize)
                MRU.RemoveAt(0);

            MRU.Add(fileName);
        }

        /// <summary>
        /// Get the recently opened map from the MRU list.
        /// </summary>
        /// <returns>The recently opened map from the MRU list.</returns>
        public string GetRecentMap()
        {
            if (MRU.Count > 0)
                return (string)MRU[MRU.Count - 1];
            else return null;
        }
    }

    /// <summary>
    /// A class to hold, serialize and deserialize the main form settings.
    /// </summary>
    [Serializable]
    public class FormSettings
    {
        /// <summary>
        /// The width of the form.
        /// </summary>
        public int Width;
        /// <summary>
        /// The height of the form.
        /// </summary>
        public int Height;
        /// <summary>
        /// The visible state form.
        /// </summary>
        public bool Visible;
        /// <summary>
        /// The left position of the form.
        /// </summary>
        public int Left;
        /// <summary>
        /// The top position of the form.
        /// </summary>
        public int Top;

        /// <summary>
        /// Constructs a new FormSettings object.
        /// </summary>
        public FormSettings()
        {
        }

        /// <summary>
        /// Save the parameters of the form into the property bag.
        /// </summary>
        /// <param name="form">The form to be saved.</param>
        /// <param name="parent">The parent object of the form.</param>
        public void Save(Form form, Form parent)
        {
            Width = form.Width;
            Height = form.Height;
            Left = form.Left;
            Top = form.Top;
            if (parent != null)
            {
                Left -= parent.Left;
                Top -= parent.Top;
            }
        }

        /// <summary>
        /// Load the parameters of the form from the property bag.
        /// </summary>
        /// <param name="form">The form that has to be updated.</param>
        /// <param name="parent">The parent object of the form.</param>
        public void Load(Form form, Form parent)
        {
            form.Width = Width < 800? 800: Width;
            form.Height = Height < 600? 600 : Height;
            //form.StartPosition = FormStartPosition.Manual;
            //Rectangle rect = Screen.PrimaryScreen.Bounds;
            //if (Left + Width > rect.Width || Top + Height > rect.Height)
            //{
            //    // main window falls outside of the primary screen
            //    Left = rect.Width - Width;
            //    Top = rect.Height - Height;
            //}
            //form.Left = Left;
            //form.Top = Top;
            //if (parent != null)
            //{
            //    form.Left += parent.Left;
            //    form.Top += parent.Top;
            //}
        }
    }
}
