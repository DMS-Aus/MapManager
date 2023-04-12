using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using OSGeo.MapServer;
using OSGeo.GDAL;
using OSGeo.OGR;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using DMS;
using DMS.MapLibrary;

namespace DMS.MapManager
{
    public class TileGenerator
    {
        mapObj map;
        string id;

        public TileGenerator(string mapfile, string savePath, string tileListId)
        {
            try
            {
                id = tileListId;

                string version = mapscript.msGetVersion();

                if (Directory.Exists(Environment.CurrentDirectory + "\\gdalplugins"))
                    Gdal.SetConfigOption("GDAL_DRIVER_PATH", Environment.CurrentDirectory + "\\gdalplugins");

                Gdal.AllRegister();
                Ogr.RegisterAll();
                mapscript.SetEnvironmentVariable("CURL_CA_BUNDLE=" + Environment.CurrentDirectory + "\\curl-ca-bundle.crt");

                MapUtils.SetPROJ_DATA(Environment.CurrentDirectory + "\\ProjLib");

                Gdal.SetConfigOption("GDAL_DATA", Environment.CurrentDirectory);

                map = new mapObj(mapfile);

                ProcessTileList(savePath, tileListId);
            }
            catch (Exception ex)
            {
                ExceptionDump(ex);
            }
        }

        private void ProcessTileList(string savePath, string tileListId)
        {
            try
            {
                // read the tile settings
                string tileSettingsFile = savePath + "\\TilingSettings";
                FileStream fsreader = new FileStream(tileSettingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryFormatter formatter = new BinaryFormatter();
                TileSettings tileSettings = (TileSettings)formatter.Deserialize(fsreader);
                fsreader.Close();

                // read tile list for generation from file
                string tileDataFile = savePath + "\\TilingList" + tileListId;
                BinaryReader bsreader = new BinaryReader(File.Open(tileDataFile, FileMode.Open));
                try
                {
                    while (true)
                    {
                        GenerateTile(tileSettings.startX, tileSettings.startY, tileSettings.initTileGap, tileSettings.imageSize, tileSettings.imageBuffer, tileSettings.savePath, bsreader.ReadInt32(), bsreader.ReadInt32(), bsreader.ReadInt32(), tileSettings.imageFormat);
                    }
                }
                catch (Exception) // need to find nicer way to do this
                {
                    bsreader.Close();
                }

                // when process is complete delete the list input
                File.Delete(tileDataFile);
            }
            catch (Exception ex)
            {
                ExceptionDump(ex);
            }
        }

        private void GenerateTile(double startX, double startY, double initTileGap, int imageSize, int imageBuffer, string savePath, int z, int x, int y, string imageFormat)
        {
            try
            {
                // active map variable
                map.setSize(imageSize + (imageBuffer * 2), imageSize + (imageBuffer * 2));

                // 0/0/0 tile variables
                double startPointX = startX;
                double startPointY = startY;
                double initGap = initTileGap; // inital distance between tiles, for level 0 is max meters - min meters

                // meta-buffer to fix labeling as a multiplier
                double buffer = 1 + ((double)imageBuffer / (double)imageSize);

                // find the spacing between each tile for level
                double gap = (initGap / Math.Pow(2, z));

                // buffer in meters for each level
                double buffermeters = (gap * buffer) - gap;

                // set map extents for tile x values
                map.extent.minx = (startPointX + (gap * x)) - buffermeters;
                map.extent.maxx = (startPointX + (gap * x) + gap) + buffermeters;

                // set map extents for tile y values
                map.extent.miny = (startPointY + (gap * y)) - buffermeters;
                map.extent.maxy = (startPointY + (gap * y) + gap) + buffermeters;

                // generate map image
                using (imageObj image = map.draw())
                {
                    Image mapImage;

                    byte[] img = image.getBytes();
                    using (MemoryStream ms = new MemoryStream(img))
                    {
                        mapImage = Image.FromStream(ms);
                        ms.Flush();

                        // clip buffer area off generated image
                        if (!(buffer == 0))
                        {
                            Rectangle cropRect = new Rectangle(imageBuffer, imageBuffer, imageSize, imageSize);
                            Bitmap bmpImage = new Bitmap(mapImage);
                            Bitmap bmpCrop = bmpImage.Clone(cropRect, bmpImage.PixelFormat);
                            mapImage = (Image)(bmpCrop);
                        }

                        // save image to disk in TMS format location
                        System.IO.Directory.CreateDirectory(savePath + "\\" + z + "\\" + x);
                        if (imageFormat == "png")
                        {
                            mapImage.Save(savePath + "\\" + z + "\\" + x + "\\" + y + "." + imageFormat, ImageFormat.Png);
                        }
                        if (imageFormat == "jpg")
                        {
                            mapImage.Save(savePath + "\\" + z + "\\" + x + "\\" + y + "." + imageFormat, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionDump(ex);
            }
        }

        private void ExceptionDump(Exception ex)
        {
            MessageBox.Show(ex.ToString());
            Environment.Exit(1);
        }
    }

    [Serializable()]
    public class TileSettings
    {
        public double startX, startY, initTileGap;
        public int imageSize, imageBuffer, depth, processes;
        public string savePath, imageFormat, settingsName;
        public bool overwrite, snapWorld;

        public TileSettings()
        {
        }

        public TileSettings(string p10)
        {
            settingsName = p10;
        }

        public TileSettings(double p1, double p2, double p3, int p4, int p5, string p6, string p7, int p8, bool p9, string p10, int p11, bool p12)
        {
            startX = p1;
            startY = p2;
            initTileGap = p3;
            imageSize = p4;
            imageBuffer = p5;
            savePath = p6;
            imageFormat = p7;
            depth = p8;
            overwrite = p9;
            settingsName = p10;
            processes = p11;
            snapWorld = p12;
        }
    }
}
