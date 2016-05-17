using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using OSGeo.MapServer;
using OSGeo.OGR;
using OSGeo.OSR;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;
using System.Reflection;

namespace DMS.MapLibrary
{
    /// <summary>
    /// MapLibrary utility functions.
    /// </summary>
    public static class MapUtils
    {
        [DllImport("mapserver.dll", EntryPoint = "msSetPROJ_LIB", CallingConvention = CallingConvention.Cdecl)]
        private static extern void msSetPROJ_LIB(string proj_lib, string pszRelToPath);

        private static string projLib = null;

        static Random rand;

        /// <summary>
        /// The static constructor of the MapUtils object to initialize the random number generator by default.
        /// </summary>
        static MapUtils()
        {
            rand = new Random((int)DateTime.Now.ToFileTime());
        }

        /// <summary>
        /// Create a new random color.
        /// </summary>
        /// <returns>The new random color</returns>
        public static colorObj GetRandomColor()
        {
            return new colorObj(rand.Next(256), rand.Next(256), rand.Next(256), 0);
        }

        /// <summary>
        /// Create a new random value.
        /// </summary>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>The new random value</returns>
        public static int GetRandomValue(int maxValue)
        {
            return rand.Next(maxValue);
        }

        /// <summary>
        /// Get default color color.
        /// </summary>
        /// <returns>The default color</returns>
        public static void SetDefaultColor(MS_LAYER_TYPE type,  styleObj style)
        {
            // set default color (#4337) to black for line color and white for brush color
            if (type == MS_LAYER_TYPE.MS_LAYER_POLYGON)
            {
                style.color = new colorObj(255, 255, 255, 0);
                style.outlinecolor = new colorObj(0, 0, 0, 0);
                style.symbol = 0;
                style.symbolname = null;
            }
            else
            {
                style.color = new colorObj(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Returns the next name of a class to be added to the layer
        /// </summary>
        /// <param name="layer">The layer referece</param>
        /// <returns>The class name</returns>
        public static string GetClassName(layerObj layer)
        {
            if (layer == null)
                return "";
            
            return "Class (" + Convert.ToString(layer.numclasses - 1) + ")";
        }

        /// <summary>
        /// Create a new classObj with random color setting added to the parent layer.
        /// </summary>
        /// <param name="layer">The parent layer object.</param>
        public static void CreateRandomClass(layerObj layer)
        {
            using (classObj newclass = new classObj(layer))
            {
                newclass.name = GetClassName(layer);
                newclass.template = "query.html";
                styleObj style = new styleObj(newclass);
                style.size = 8; // set default size (#4339)
                SetDefaultColor(layer.type, style);
            }
        }

        public static void SetDefaultLabel(labelObj label, mapObj map)
        {
            label.size = 8;
            label.force = mapscript.MS_FALSE;
            label.partials = mapscript.MS_TRUE;
            label.position = (int)MS_POSITIONS_ENUM.MS_CC;
            // trying to set truetype fonts
            if (map != null)
            {
                string key = null;
                string lastkey = null;
                while ((key = map.fontset.fonts.nextKey(key)) != null)
                {
                    if (File.Exists(map.fontset.fonts.get(key, "")))
                    {
                        lastkey = key;
                        if (string.Compare(key, "arial", true) == 0)
                        {
                            // set the default font to Arial if exists
                            break;
                        }
                    }
                }
                if (lastkey != null)
                {
                    label.font = lastkey;
                    label.size = 8;
                }
            }
        }

        /// <summary>
        /// Initialize the default settings of a newly created labelObj
        /// </summary>
        /// <param name="layer">The parent layer object.</param>
        public static void InitializeDefaultLabel(layerObj layer)
        {
            labelObj label;
            if (layer.getClass(0).numlabels == 0)
            {
                label = new labelObj();
                layer.getClass(0).addLabel(label);
            }
            label = layer.getClass(0).getLabel(0);
            SetDefaultLabel(label, layer.map);
        }
        
        /// <summary>
        /// Generates an unique layer name within a map
        /// </summary>
        /// <param name="map">The map object</param>
        /// <param name="desiredName">The desired layer name</param>
        /// <returns></returns>
        public static string GetUniqueLayerName(mapObj map, string desiredName, int version)
        {
            
            string versionedName = desiredName.Trim();
            if (version > 0)
                versionedName += " (" + version + ")";

            for (int i = 0; i < map.numlayers; i++)
            {
                layerObj layer = map.getLayer(i);
                if (layer.name != null && layer.name.Trim() == versionedName)
                {
                    string desiredBaseName = Regex.Match(versionedName, @"(.*(?=\s\(\d+\))|.*(?!\s\(\d+\)))").Value;
                    return GetUniqueLayerName(map, desiredBaseName, version + 1);
                }
            }
            return versionedName;
        }

        public static bool RenameDuplicatedNames(mapObj map)
        {
            bool hasDuplicate = false;
            for (int i = 0; i < map.numlayers; i++)
            {
                layerObj layer = map.getLayer(i);

                if (layer.name == "__embed__scalebar" || layer.name == "__embed__legend")
                    continue;

                string originalName = layer.name;
                if (originalName == null)
                    originalName = "layer";
                layer.name = null;
                string versionedName = GetUniqueLayerName(map, originalName, 0);
                layer.name = originalName;
                if (originalName.Trim() != versionedName)
                {
                    hasDuplicate = true;
                    layer.name = versionedName;
                }
            }
            return hasDuplicate;
        }

        /// <summary>
        /// Provide mapping between the OGR and MapServer layer types.
        /// </summary>
        /// <param name="type">The OGR layer type to be converted.</param>
        /// <returns>The corresponding MapServer layer type.</returns>
        public static MS_LAYER_TYPE GetLayerType(wkbGeometryType type)
        {
            switch (type)
            {
                case wkbGeometryType.wkbLineString:
                case wkbGeometryType.wkbLinearRing:
                case wkbGeometryType.wkbLineString25D:
                case wkbGeometryType.wkbMultiLineString:
                case wkbGeometryType.wkbMultiLineString25D:
                    return MS_LAYER_TYPE.MS_LAYER_LINE;
                case wkbGeometryType.wkbMultiPoint:
                case wkbGeometryType.wkbMultiPoint25D:
                case wkbGeometryType.wkbPoint:
                case wkbGeometryType.wkbPoint25D:
                    return MS_LAYER_TYPE.MS_LAYER_POINT;
                case wkbGeometryType.wkbMultiPolygon:
                case wkbGeometryType.wkbMultiPolygon25D:
                case wkbGeometryType.wkbPolygon:
                case wkbGeometryType.wkbPolygon25D:
                    return MS_LAYER_TYPE.MS_LAYER_POLYGON;
            }
            // default setting
            return MS_LAYER_TYPE.MS_LAYER_POINT;
        }

        /// <summary>
        /// Setting up the location of the proj4 epsg file
        /// </summary>
        /// <param name="proj_lib">The location of the proj4 files</param>
        public static void SetPROJ_LIB(string proj_lib)
        {
            // preloading the required dll-s
            string version = mapscript.msGetVersion();
            if (version.Contains("SUPPORTS=PROJ"))
            {
                msSetPROJ_LIB(proj_lib, null);
            }
            projLib = proj_lib;
        }

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr GetModuleHandle(string lpModuleName);

        //[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        //static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //public delegate int MethodToInvoke(string sdata);

        //public static int SetCRTEnvironment(string env)
        //{
        //    IntPtr hModule = GetModuleHandle(@"C:\Windows\winsxs\x86_microsoft.vc90.crt_1fc8b3b9a1e18e3b_9.0.30729.4974_none_50940634bcb759cb\msvcr90.dll");
        //    int ret = 0;
        //    if (hModule != IntPtr.Zero)
        //    {
        //        IntPtr fptr = GetProcAddress(hModule, "_putenv");
        //        if (fptr != IntPtr.Zero)
        //        {
        //            MethodToInvoke mi = (MethodToInvoke)Marshal.GetDelegateForFunctionPointer(fptr, typeof(MethodToInvoke));
        //            ret = mi(env);
        //        }
        //    }
        //    else
        //    {
        //        ret = Marshal.GetLastWin32Error();
        //    }
        //    return ret;
        //}

        /// <summary>
        /// Find the required library or have the user to locate it manually.
        /// </summary>
        /// <param name="lib">The name of the library</param>
        public static bool FindLibrary(string lib)
        {

            if (File.Exists(Path.Combine(Environment.SystemDirectory, lib)))
                return true;

            if (File.Exists(Path.Combine(Environment.CurrentDirectory, lib)))
                return true;

            string[] pathes = System.Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process).Split(new char[] { ';' });
            foreach (string path in pathes)
            {
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, lib)))
                    return true;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Please locate " + lib + " required to start the MapManager application!";
            dialog.FileName = lib;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // adding this location to the app PATH
                System.Environment.SetEnvironmentVariable("PATH", Path.GetDirectoryName(dialog.FileName) + ";" +
                    System.Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process), EnvironmentVariableTarget.Process);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Getting the location of the proj4 files.
        /// </summary>
        /// <returns>The location of the proj4 files.</returns>
        public static string GetPROJ_LIB()
        {
            if (projLib == null)
                projLib = Environment.GetEnvironmentVariable("PROJ_LIB");
                
            if (projLib == null)
                return "";
            
            return projLib;
        }
        
        /// <summary>
        /// Opening a new map.
        /// </summary>
        /// <param name="fileName">The map file name.</param>
        /// <returns>The wrapper of the created mapObj.</returns>
        public static MapObjectHolder OpenMap(string fileName)
        {
            return new MapObjectHolder(new mapObj(fileName), null);
        }

        /// <summary>
        /// Saving a map.
        /// </summary>
        /// <param name="map">The mapObj to be saved.</param>
        /// <param name="fileName">The map file name</param>
        public static void SaveMap(mapObj map, string fileName)
        {
            map.save(fileName);
        }

        /// <summary>
        /// Creating a new map.
        /// </summary>
        /// <returns>The wrapper containing the newly created map object.</returns>
        public static MapObjectHolder CreateMap()
        {
            mapObj map = new mapObj(null);
            map.setExtent(0, 0, 100, 100);
            return new MapObjectHolder(map, null);
        }

        /// <summary>
        /// Retrieve the current unit name of the given unit.
        /// </summary>
        /// <param name="map">The given unit.</param>
        /// <returns>The current unit name of the given unit.</returns>
        public static string GetUnitName(MS_UNITS unit)
        {
            switch (unit)
            {
                case MS_UNITS.MS_DD:
                    return "°";
                case MS_UNITS.MS_FEET:
                    return "ft";
                case MS_UNITS.MS_INCHES:
                    return "in";
                case MS_UNITS.MS_KILOMETERS:
                    return "km";
                case MS_UNITS.MS_METERS:
                    return "m";
                case MS_UNITS.MS_MILES:
                    return "mi";
                case MS_UNITS.MS_PERCENTAGES:
                    return "%";
                case MS_UNITS.MS_PIXELS:
                    return "px";
            }
           
            return "";
        }

        /// <summary>
        /// Returns the number of inches represented by a given unit
        /// </summary>
        /// <param name="unit">The unit to use</param>
        /// <returns>The number of inches</returns>
        public static double InchesPerUnit(MS_UNITS unit)
        {
            switch (unit)
            {
                case MS_UNITS.MS_DD:
                    return 4374754;
                case MS_UNITS.MS_FEET:
                    return 12;
                case MS_UNITS.MS_INCHES:
                    return 1;
                case MS_UNITS.MS_KILOMETERS:
                    return 39370.1;
                case MS_UNITS.MS_METERS:
                    return 39.3701;
                case MS_UNITS.MS_MILES:
                    return 63360.0;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// Retrieve the shape type name based on the MapScript type.
        /// </summary>
        /// <param name="map">The MapScript type.</param>
        /// <returns>The shape type name.</returns>
        public static string GetShapeTypeName(MS_SHAPE_TYPE type)
        {
            switch (type)
            {
                case MS_SHAPE_TYPE.MS_SHAPE_POLYGON:
                    return "Polygon";
                case MS_SHAPE_TYPE.MS_SHAPE_LINE:
                    return "Line";
                case MS_SHAPE_TYPE.MS_SHAPE_POINT:
                    return "Point";
                case MS_SHAPE_TYPE.MS_SHAPE_NULL:
                    return "Null";
            }
            
            return "";
        }

        /// <summary>
        /// Retrieve the current unit precision of the map based on the given unit.
        /// </summary>
        /// <param name="map">The given unit.</param>
        /// <returns>The unit precision of the given unit.</returns>
        public static int GetUnitPrecision(MS_UNITS unit)
        {
            switch (unit)
            {
                case MS_UNITS.MS_DD:
                    return 5;
                case MS_UNITS.MS_FEET:
                    return 0;
                case MS_UNITS.MS_INCHES:
                    return 0;
                case MS_UNITS.MS_KILOMETERS:
                    return 3;
                case MS_UNITS.MS_METERS:
                    return 0;
                case MS_UNITS.MS_MILES:
                    return 3;
                case MS_UNITS.MS_PERCENTAGES:
                    return 2;
                case MS_UNITS.MS_PIXELS:
                    return 0;
            }
            
            return 0;
        }

        /// <summary>
        /// Retrieve the MapScript unit value from a proj4 definition.
        /// </summary>
        /// <param name="proj4">The proj4 definition.</param>
        /// <returns>The MapScript unit value</returns>
        public static MS_UNITS GetMapUnitFromProj4(string proj4)
        {
            if (!proj4.Contains("+units"))
                return MS_UNITS.MS_DD;

            if (proj4.Contains("+units=m"))
                return MS_UNITS.MS_METERS;

            if (proj4.Contains("+units=km"))
                return MS_UNITS.MS_KILOMETERS;

            if (proj4.Contains("+units=ft"))
                return MS_UNITS.MS_FEET;

            if (proj4.Contains("+units=in"))
                return MS_UNITS.MS_INCHES;

            return MS_UNITS.MS_DD;
        }

        /// <summary>
        /// This function tries to find out the projection from the EPSG file based on the name or the proj4 parameters
        /// </summary>
        /// <param name="projection">input projection in proj.4 fromat</param>
        /// <param name="proj4">the matching projection in the epsg file</param>
        /// <param name="proj4">epsg</param>
        /// <returns>Projection Name</returns>
        public static string FindProjection(string projection, out string proj4, out int epsg)
        {
            string[] def = null;
            if (projection.Contains("+proj"))
            {
                def = projection.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            // todo: epsg based search
            string projName = "";
            proj4 = "";
            epsg = 0;
            using (Stream s = File.OpenRead(MapUtils.GetPROJ_LIB() + "\\epsg"))
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    string line;
                    int i;
                    int minrank = 100;
                    string line2 = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        int rank = 0;
                        if (def != null)
                        {
                            // proj4 based search
                            for (i = 0; i < def.Length; i++)
                            {
                                if (!line.Contains(def[i]))
                                {
                                    if (def[i].StartsWith("+proj"))
                                        break;
                                    if (def[i].StartsWith("+ellps"))
                                        break;
                                    if (def[i].StartsWith("+zone"))
                                        break;
                                    if (def[i].StartsWith("+datum"))
                                        break;
                                    if (def[i].StartsWith("+units"))
                                        break;
                                    if (def[i].StartsWith("+south"))
                                        break;
                                    if (def[i].StartsWith("+north"))
                                        break;
                                    else ++rank;
                                }
                            }
                            if (i == def.Length)
                            {
                                if (rank < minrank)
                                {
                                    minrank = rank;
                                    projName = line2.Substring(2);
                                    proj4 = line;
                                    if (rank == 0)
                                    {
                                        // found
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // name based search
                            if (line.Contains(projection))
                                break;
                        }
                        if (line.StartsWith("#"))
                            line2 = line;
                    }
                }
            }
            
            string[] proj_defs = proj4.Split(new char[] { '<', '>' });
            if (proj_defs.Length >= 3)
            {
                proj4 = proj_defs[2].Trim();
                int.TryParse(proj_defs[1].Trim(), out epsg);
            }
            
            if (projName != "")
            {
                if (projName.Contains(" / "))
                    return projName;
                if (proj4.Contains("longlat"))
                    return "Longitude-Latitude / " + projName;
                else
                    return "Other Non Geographic / " + projName;
            }
            else
                return projection;
        }

        /// <summary>
        /// Find symbol by name (without adding)
        /// </summary>
        /// <param name="symbolset">The symbolset object</param>
        /// <param name="key_to_find">the symbol name to search for</param>
        /// <returns>Symbol found or null</returns>
        public static symbolObj FindSymbol(symbolSetObj symbolset, string name_to_find)
        {
            if (name_to_find != null)
            {
                for (int i = 0; i < symbolset.numsymbols; i++)
                {
                    symbolObj sym = symbolset.getSymbol(i);
                    if (sym.name != null && sym.name.ToLower() == name_to_find.ToLower())
                        return sym;
                }
            }
            return null;
        }

        /// <summary>
        /// Check whether this layer has a metadata tag
        /// </summary>
        /// <param name="layer">The layer object</param>
        /// <param name="key_to_find">the metadata key to search for</param>
        /// <returns></returns>
        public static bool HasMetadata(layerObj layer, string key_to_find)
        {
            string key = layer.getFirstMetaDataKey();
            while (key != null)
            {
                if (key == key_to_find)
                {
                    return true;
                }
                key = layer.getNextMetaDataKey(key);
            }
            return false;
        }

        /// <summary>
        /// Find a metadata tag
        /// </summary>
        /// <param name="layer">The layer object</param>
        /// <param name="key_to_find">the metadata key to search for</param>
        /// <returns></returns>
        public static string FindMetadata(layerObj layer, string key_to_find)
        {
            string key = layer.getFirstMetaDataKey();
            while (key != null)
            {
                if (key.StartsWith(key_to_find))
                {
                    return key;
                }
                key = layer.getNextMetaDataKey(key);
            }
            return null;
        }

        /// <summary>
        /// Append a value to an existing metadata or create a new tag if that doesn't exist
        /// </summary>
        /// <param name="layer">The layer object</param>
        /// <param name="key">The metadata key</param>
        /// <param name="value">The value to append</param>
        /// <param name="value">The separator to be used when appending</param>
        public static void AppendMetadata(layerObj layer, string key_to_find, string value, string separator)
        {
            string key = layer.getFirstMetaDataKey();
            while (key != null)
            {
                if (key == key_to_find)
                {
                    layer.setMetaData(key_to_find, layer.getMetaData(key_to_find) + separator + value);
                    return;
                }
                key = layer.getNextMetaDataKey(key);
            }
            layer.setMetaData(key_to_find, value);
        }

        /// <summary>
        /// Export a legend image with specific requirements (bug 1015)
        /// </summary>
        /// <param name="map">The map object</param>
        /// <param name="width">The desired legend width</param>
        /// <param name="height">The desired legend height</param>
        /// <returns></returns>
        public static byte[] ExportLegend(mapObj map)
        {
            int width = 10;
            int height = 10;
            Bitmap bmp = null;
            Graphics g = null;
            for (int phase = 0; phase < 2; phase++)
            {
                if (phase == 0)
                    bmp = new Bitmap(100, 100, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                else
                    bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                g = Graphics.FromImage(bmp);

                Stack groupPositions = new Stack(); // for storing the group text positions

                Font legendFont = new Font("MS Sans Sherif", 8); // set default font

                g.Clear(Color.White); // clear the background

                int xPos = 5; // padding
                int yPos = 5;
                int xOffset = 24; // legend indent in pixels
                int yOffset = 18; // item height in pixels

                // force the recalculation of the current scale
                map.setExtent(map.extent.minx, map.extent.miny, map.extent.maxx, map.extent.maxy);

                // start drawing the legend in reverse layer order
                using (intarray ar = map.getLayersDrawingOrder())
                {
                    for (int i = map.numlayers - 1; i >= 0; i--)
                    {
                        layerObj layer = map.getLayer(ar.getitem(i));

                        if (layer.name == "__embed__scalebar" || layer.name == "__embed__legend"
                            || layer.status == mapscript.MS_OFF || layer.name.StartsWith("~"))
                            continue;


                        if (map.scaledenom > 0)
                        {
                            if (layer.maxscaledenom > 0 && map.scaledenom > layer.maxscaledenom)
                                continue;
                            if (layer.minscaledenom > 0 && map.scaledenom <= layer.minscaledenom)
                                continue;
                        }

                        if (layer.maxscaledenom <= 0 && layer.minscaledenom <= 0)
                        {
                            if (layer.maxgeowidth > 0 && ((map.extent.maxx - map.extent.minx) > layer.maxgeowidth))
                                continue;
                            if (layer.mingeowidth > 0 && ((map.extent.maxx - map.extent.minx) < layer.mingeowidth))
                                continue;
                        }

                        // draw raster or WMS layers
                        if (layer.type == MS_LAYER_TYPE.MS_LAYER_RASTER)
                        {
                            if (phase == 1)
                            {
                                g.DrawIcon(global::MapLibrary.Properties.Resources.raster, xPos, yPos);
                                g.DrawString(layer.name, legendFont, Brushes.Black, xPos + 30, yPos + 2);
                            }

                            SizeF size = g.MeasureString(layer.name, legendFont);
                            if (xPos + 30 + size.Width + 5 > width)
                                width = Convert.ToInt32(xPos + 30 + size.Width + 5);

                            yPos += yOffset;
                            continue;
                        }

                        int numClasses = 0;
                        Image legendImage = null;
                        string legendText = null;

                        for (int j = 0; j < layer.numclasses; j++)
                        {
                            classObj layerclass = layer.getClass(j);

                            if (layerclass.name == "EntireSelection" || layerclass.name == "CurrentSelection")
                                continue;

                            if (layerclass.status == mapscript.MS_OFF)
                                continue;

                            if (map.scaledenom > 0)
                            {  /* verify class scale here */
                                if (layerclass.maxscaledenom > 0 && (map.scaledenom > layerclass.maxscaledenom))
                                    continue;
                                if (layerclass.minscaledenom > 0 && (map.scaledenom <= layerclass.minscaledenom))
                                    continue;
                            }

                            if (numClasses == 1)
                            {
                                // draw subclasses
                                xPos += xOffset;

                                if (phase == 1)
                                {
                                    // drawing the first class item (same as the layer)
                                    g.DrawImage(legendImage, xPos, yPos);
                                    g.DrawString(legendText, legendFont,
                                               Brushes.Black, xPos + 30, yPos + 2);
                                }

                                SizeF size = g.MeasureString(legendText, legendFont);
                                if (xPos + 30 + size.Width + 5 > width)
                                    width = Convert.ToInt32(xPos + 30 + size.Width + 5);

                                yPos += yOffset;
                            }

                            ++numClasses; // number of visible classes

                            // creating the treeicons
                            using (classObj def_class = new classObj(null)) // for drawing legend images
                            {
                                using (imageObj image = def_class.createLegendIcon(map, layer, 30, 20))
                                {
                                    // drawing the class icons
                                    layerclass.drawLegendIcon(map, layer, 20, 10, image, 5, 5);
                                    byte[] img = image.getBytes();
                                    using (MemoryStream ms = new MemoryStream(img))
                                    {
                                        legendImage = Image.FromStream(ms);
                                        legendText = layerclass.name;

                                        if (phase == 1)
                                            g.DrawImage(legendImage, xPos, yPos);
                                        if (numClasses > 1)
                                        {
                                            // draw the class item
                                            if (phase == 1)
                                                g.DrawString(layerclass.name, legendFont,
                                                       Brushes.Black, xPos + 30, yPos + 3);

                                            SizeF size = g.MeasureString(layerclass.name, legendFont);
                                            if (xPos + 30 + size.Width + 5 > width)
                                                width = Convert.ToInt32(xPos + 30 + size.Width + 5);
                                        }
                                        else
                                        {
                                            // draw the layer item
                                            if (phase == 1)
                                                g.DrawString(layer.name, legendFont,
                                                       Brushes.Black, xPos + 30, yPos + 3);

                                            SizeF size = g.MeasureString(layer.name, legendFont);
                                            if (xPos + 30 + size.Width + 5 > width)
                                                width = Convert.ToInt32(xPos + 30 + size.Width + 5);

                                            if (string.Compare(layer.styleitem, "AUTO", true) == 0)
                                            {
                                                yPos += yOffset;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            yPos += yOffset;
                        }

                        if (numClasses > 1)
                            xPos -= xOffset;
                    }
                }
                height = yPos + 5;
            }

            g.Flush();
            MemoryStream ms2 = new MemoryStream();
            bmp.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
            return ms2.ToArray();
        }

        public static symbolObj CloneSymbol(symbolObj origsym)
        {
            symbolObj sym = new symbolObj(origsym.name, origsym.imagepath);
            sym.anchorpoint_x = origsym.anchorpoint_x;
            sym.anchorpoint_y = origsym.anchorpoint_y;
            sym.type = origsym.type;
            sym.font = origsym.font;
            sym.filled = origsym.filled;
            sym.character = origsym.character;
            sym.transparent = origsym.transparent;
            sym.transparentcolor = origsym.transparentcolor;
            sym.setPoints(origsym.getPoints());
            sym.sizex = origsym.sizex;
            sym.sizey = origsym.sizey;
            sym.inmapfile = origsym.inmapfile;
            return sym;
        }

        public static void TestMSSQLConnection(string connection, string data)
        {
            mapObj samplemap = new mapObj(null);
            layerObj samplelayer = new layerObj(samplemap);
            samplelayer.connectiontype = MS_CONNECTION_TYPE.MS_PLUGIN;
            samplelayer.plugin_library = samplelayer.plugin_library_original = "msplugin_mssql2008.dll";

            samplelayer.connection = connection;
            samplelayer.data = data;
            try
            {
                samplelayer.open();
            }
            finally
            {
                samplelayer.close();
            }
        }
    }
}
