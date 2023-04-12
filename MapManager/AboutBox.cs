using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using OSGeo.MapServer;
using OSGeo.GDAL;
using System.Text;
using OSGeo.OGR;
using System.Runtime.InteropServices;

namespace DMS.MapManager
{
    /// <summary>
    /// Provides the about box for the application
    /// </summary>
    partial class AboutBox : Form
    {
        [DllImport("proj_9_1", EntryPoint = "?pj_get_release@@YAPEBDXZ")]
        private static extern IntPtr pj_get_release();

        [DllImport("geos_c", EntryPoint = "GEOSversion")]
        private static extern IntPtr GEOSversion();

        [DllImport("zlib", EntryPoint = "zlibVersion")]
        private static extern IntPtr zlibVersion();

        [DllImport("libcurl", EntryPoint = "curl_version")]
        private static extern IntPtr curl_version();

        /// <summary>
        /// Function to determine which platform we're on
        /// </summary>
        private static string GetPlatform()
        {
            return Environment.Is64BitProcess ? "x64" : "x86";
        }

        /// <summary>
        /// Collect the versions of the dependent libraries
        /// </summary>
        /// <returns></returns>
        private string CollectVersionInfo()
        {
            Gdal.AllRegister();
            
            StringBuilder s = new StringBuilder();
            s.AppendLine("MapServer version " + mapscript.MS_VERSION);
            s.AppendLine(Gdal.VersionInfo("GDAL_RELEASE_NAME"));
            try 
            {
                s.AppendLine("proj " + Marshal.PtrToStringAnsi(pj_get_release()));
            }
            catch (Exception)
            {
                // library not available
            }
            try 
            {
                s.AppendLine("geos " + Marshal.PtrToStringAnsi(GEOSversion()));
            }
            catch (Exception)
            {
                // library not available
            }
            try 
            {
                s.AppendLine("zlib " + Marshal.PtrToStringAnsi(zlibVersion()));
            }
            catch (Exception)
            {
                // library not available
            }
            try
            {
                s.AppendLine(Marshal.PtrToStringAnsi(curl_version()));
            }
            catch (Exception)
            {
                // library not available
            }
           
            return s.ToString();
        }
        
        /// <summary>
        /// Constructs a new AboutBox object.
        /// </summary>
        public AboutBox()
        {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct + " (" + GetPlatform() + ")";

            // Extract the date and time from the assembly version
            string[] aver = AssemblyVersion.Split(new char[] { '.' });
            DateTime date = new DateTime(2000, 1, 1);
            date = date.AddDays(int.Parse(aver[2]));
            //date = date.AddSeconds(int.Parse(aver[3]) * 2);

            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;

            this.textBoxVersions.Text = CollectVersionInfo();
            
            string version = mapscript.msGetVersion();
            this.textBoxDescription.Text = version.Substring(version.IndexOf("OUTPUT")).Replace(" ", "\r\n");

            StringBuilder s = new StringBuilder();
            for (int i = 0; i < Gdal.GetDriverCount(); i++)
            {
                s.AppendLine(Gdal.GetDriver(i).LongName);
            }
            this.textBoxDescription1.Text = s.ToString();

            s = new StringBuilder();
            Ogr.RegisterAll();
            for (int i = 0; i < Ogr.GetDriverCount(); i++)
            {
                s.AppendLine(Ogr.GetDriver(i).name);
            }
            this.textBoxDescription2.Text = s.ToString();
        }

        #region Assembly Attribute Accessors

        /// <summary>
        /// Accessor for the AssemblyTitle attribute.
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Accessor for the AssemblyVersion attribute.
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Accessor for the AssemblyDescription attribute.
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Accessor for the AssemblyProduct attribute.
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Accessor for the AssemblyCopyright attribute.
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Accessor for the AssemblyCompany attribute.
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        /// <summary>
        /// KeyDown event handler of the AboutBox object.
        /// </summary>
        /// <param name="sender">The source object of this event.</param>
        /// <param name="e">The event parameters.</param>
        private void AboutBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
