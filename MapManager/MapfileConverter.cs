using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using OSGeo.MapServer;
using System.Collections;
using System.Text.RegularExpressions;

namespace DMS.MapManager
{
    class MapfileConverter
    {
        StringBuilder output = new StringBuilder();
        StringBuilder changelog = new StringBuilder();

        string[] groupkeywords = new string[] { "\\MAP", "\\MAP\\LAYER", "\\MAP\\SYMBOL", "\\MAP\\LAYER\\CLASS", "\\MAP\\LAYER\\CLUSTER", "\\MAP\\LAYER\\CLASS\\STYLE", "\\MAP\\LAYER\\CLASS\\LABEL", "\\MAP\\LAYER\\CLASS\\LABEL\\STYLE", "\\MAP\\LAYER\\CLASS\\LEADER", "\\MAP\\LAYER\\CLASS\\METADATA", "\\MAP\\LAYER\\CLASS\\LEADER\\STYLE", "\\MAP\\PROJECTION", "\\MAP\\LAYER\\PROJECTION", "\\MAP\\LAYER\\FEATURE", "\\MAP\\LAYER\\GRID", "\\MAP\\LAYER\\JOIN", "\\MAP\\OUTPUTFORMAT", "\\MAP\\WEB\\METADATA", "\\MAP\\LAYER\\METADATA", "\\MAP\\LEGEND", "\\MAP\\LEGEND\\LABEL", "\\MAP\\QUERYMAP", "\\MAP\\SCALEBAR", "\\MAP\\SCALEBAR\\LABEL", "\\MAP\\WEB", "\\MAP\\REFERENCE" };

        string[] expressionkeywords = new string[] { "\\MAP\\LAYER\\CLASS\\EXPRESSION", "\\MAP\\LAYER\\CLASS\\TEXT", "\\MAP\\LAYER\\CLASS\\LABEL\\EXPRESSION", "\\MAP\\LEGEND\\LABEL\\EXPRESSION", "\\MAP\\SCALEBAR\\LABEL\\EXPRESSION", "\\MAP\\LAYER\\CLASS\\LABEL\\TEXT", "\\MAP\\LEGEND\\LABEL\\TEXT", "\\MAP\\SCALEBAR\\LABEL\\TEXT", "\\MAP\\LAYER\\CLUSTER\\GROUP", "\\MAP\\LAYER\\CLUSTER\\FILTER", "\\MAP\\LAYER\\FILTER" };

        public MapfileConverter()
        {
        }

        private void AppendLog(string message)
        {
            changelog.Append(" - ");
            changelog.Append(message);
            changelog.AppendLine();
        }

        private void AppendLog(string message, int lineNumber)
        {
            changelog.Append(" - ");
            changelog.Append(message);
            changelog.Append(" (line: ");
            changelog.Append(lineNumber);
            changelog.Append(")");
            changelog.AppendLine();
        }

        private void AppendLog(string message, int lineNumberStart, int lineNumberEnd)
        {
            changelog.Append(" - ");
            changelog.Append(message);
            changelog.Append(" (lines: ");
            changelog.Append(lineNumberStart);
            changelog.Append("-");
            changelog.Append(lineNumberEnd);
            changelog.Append(")");
            changelog.AppendLine();
        }

        private bool IsExpressionKeyword(string keyword)
        {
            foreach (string s in expressionkeywords)
            {
                if (keyword == s)
                {
                    return true;
                }
            }
            return false;
        }
              
        public void Parse(string contents, bool ignoreVersion)
        {
            string fontsetPath = Application.StartupPath + "\\templates\\font.list";
            string symbolsetPath = Application.StartupPath + "\\templates\\symbols.sym";
            string path = "";

            StringBuilder outputformat = new StringBuilder();

            mapObj template = null;

            if (File.Exists(Application.StartupPath + "\\templates\\new.map"))
            {
                template = new mapObj(Application.StartupPath + "\\templates\\new.map");

                fontsetPath = template.fontset.filename;
                symbolsetPath = template.symbolset.filename;
            }
            else
            {
                template = new mapObj(null);
                template.setSymbolSet(symbolsetPath);
                template.setFontSet(fontsetPath);
            }

            string mapfileContents = contents.ToUpper(); // for string comparison
            string fonts = File.ReadAllText(fontsetPath);

            using (StringReader r = new StringReader(contents))
            {
                string line;
                int lineNumber = 0;
                int groupStartLineNumber = 0;
                while ((line = r.ReadLine()) != null)
                {
                    ++lineNumber;
                    // replace trailing \" with \\"
                    if (line != Regex.Replace(line, "(?<=[^\\\\])\\\\\"\\s*\\Z", "\\\\\""))
                    {
                        line = Regex.Replace(line, "(?<=[^\\\\])\\\\\"\\s*\\Z", "\\\\\"");
                        AppendLog("Escape trailing \\\" chars ", lineNumber);
                    }
                    
                    string[] values = line.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                    string key;
                    if (values.Length > 0)
                        key = values[0].ToUpper();
                    else
                        key = "";

                    if (!IsExpressionKeyword(path + "\\" + key))
                    {
                        if (Regex.IsMatch(line, "[^\\\\]\\\\[^\\\\]"))
                        {
                            // escape single backslashes for non expressions
                            line = line.Replace("\\", "\\\\");
                            AppendLog("Escape \\ characters ", lineNumber);
                        }
                    }
                    else
                    {
                        int pos = line.IndexOf(key) + key.Length + 1;
                        string val = line.Substring(pos).Trim();
                        // trying to identify the expression
                        if (Regex.IsMatch(val, @"\(.*\)"))
                        {
                            // MS_EXPRESSION
                            AppendLog("Modify expression to string (please review!): " + val, lineNumber);
                            line = line.Substring(0, pos) + "\"" + val.Substring(1, val.Length - 2) + "\"";
                        }
                        //else if (Regex.IsMatch(val, @"\/[^\/]*\/"))
                        //{
                        //    // MS_REGEX
                        //    AppendLog("Modify regex expression to string (please review!): " + val, lineNumber);
                        //    line = line.Substring(0, pos) + "\"" + val.Substring(1, val.Length - 2) + "\"";
                        //}
                    }

                    // set up path of the node
                    foreach (string s in groupkeywords)
                    {
                        if ((path + "\\" + key) == s)
                        {
                            groupStartLineNumber = lineNumber;
                            path = s;
                            break;
                        }
                    }

                    if (key == "END" && path.LastIndexOf('\\') > 0)
                    {
                        if (path == "\\MAP\\OUTPUTFORMAT")
                        {
                            if (outputformat.ToString().Contains("PC256"))
                            {
                                AppendLog("Remove deprecated outputformat with IMAGEMODE PC256", groupStartLineNumber, lineNumber);
                            }
                            else
                            {
                                output.AppendLine(outputformat.ToString());
                                output.AppendLine(line);
                            }
                            path = path.Substring(0, path.LastIndexOf('\\'));
                            continue;
                        }
                        path = path.Substring(0, path.LastIndexOf('\\'));
                    }
                    
                    // Test elements
                    if (path == "\\MAP\\OUTPUTFORMAT")
                    {
                        if (key == "OUTPUTFORMAT")
                            outputformat.Length = 0;
                            
                        outputformat.AppendLine(line);
                    }
                    else if (key == "FONTSET" && line.Substring(line.IndexOf("FONTSET") + 8).ToUpper().Trim(new char[] { '\'', '\"', ' ' }) != fontsetPath.ToUpper().Replace("\\", "\\\\"))
                    {
                        AppendLog("Upgrade fontset location", lineNumber);
                        output.AppendLine("  FONTSET \"" + fontsetPath.Replace("\\", "\\\\") + "\"");
                    }
                    else if (key == "SYMBOLSET" && line.Substring(line.IndexOf("SYMBOLSET") + 10).ToUpper().Trim(new char[] { '\'', '\"', ' ' }) != symbolsetPath.ToUpper().Replace("\\", "\\\\"))
                    {
                        AppendLog("Upgrade symbolset location", lineNumber);
                        output.AppendLine("  SYMBOLSET \"" + symbolsetPath.Replace("\\", "\\\\") + "\"");
                    }
                    else if (key == "RESOLUTION" && !mapfileContents.Contains("DEFRESOLUTION "))
                    {
                        output.AppendLine(line);
                        AppendLog("Add DEFRESOLUTION " + values[1], lineNumber);
                        output.AppendLine("  DEFRESOLUTION " + values[1]);
                    }
                    else if (key == "MAP")
                    {
                        output.AppendLine(line); // add MAP
                        if (!mapfileContents.Contains("PIXELADJUSTMENT "))
                        {
                            AppendLog("Add PIXELADJUSTMENT 0");
                            output.AppendLine("  PIXELADJUSTMENT 0");
                        }
                    }
                    else if (key == "IMAGETYPE")
                    {
                        List<string> f = new List<string>(new string[] {"png", "jpeg", "gif", "png8", "png24", "pdf", "svg", "cairopng", "gtiff", "kml", "kmz"});

                        //outputFormatObj[] formats = template.outputformatlist;
                        //for (int i = 0; i < formats.Length; i++)
                        //{
                        //    if (!f.Contains(formats[i].name))
                        //        f.Add(formats[i].name);
                        //}
                        for (int i = 0; i < template.numoutputformats; i++)
                        {
                            outputFormatObj format = template.getOutputFormat(i);
                            if (!f.Contains(format.name))
                                f.Add(format.name);
                        }

                        string imageType = values[1].Trim(new char[] { '\'', '\"' }).ToLower();
                        if (!f.Contains(imageType))
                        {
                            AppendLog("Change image type: " + imageType + " -> png", lineNumber);
                            output.AppendLine("  IMAGETYPE png");
                        }
                        else
                            output.AppendLine(line);
                    }
                    else if (key == "BACKGROUNDCOLOR" && (path == "\\MAP\\LAYER\\CLASS\\LABEL" || path == "\\MAP\\SCALEBAR\\LABEL"))
                    {
                        AppendLog("Remove label BACKGROUNDCOLOR " + values[1], lineNumber);
                    }
                    else if (key == "BACKGROUNDSHADOWCOLOR" && (path == "\\MAP\\LAYER\\CLASS\\LABEL" || path == "\\MAP\\SCALEBAR\\LABEL"))
                    {
                        AppendLog("Remove label BACKGROUNDSHADOWCOLOR " + values[1], lineNumber);
                    }
                    else if (key == "BACKGROUNDSHADOWSIZE" && (path == "\\MAP\\LAYER\\CLASS\\LABEL" || path == "\\MAP\\SCALEBAR\\LABEL"))
                    {
                        AppendLog("Remove label BACKGROUNDSHADOWSIZE " + values[1], lineNumber);
                    }
                    else if (!ignoreVersion && key == "\"MAPMANAGER_VERSION\"" && path == "\\MAP\\WEB\\METADATA" && values[1].Trim(new char[] { '\'', '\"' }).StartsWith("1.0"))
                    {
                        changelog.Length = 0; // no upgrading required
                        return;
                    }
                    else if (key == "DRIVER" && path == "\\MAP\\OUTPUTFORMAT")
                    {
                        List<string> drivers = new List<string>(new string[] { "GD/PC256", "GD/GIF", "GD/PNG", "AGG/PNG8", "AGG/PNG", "AGG/JPEG", "CAIRO/PNG", "CAIRO/JPEG", "CAIRO/PDF", "CAIRO/SVG", "OGL/PNG", "KML", "KMZ" });

                        string driverName = values[1].Trim(new char[] { '\'', '\"' }).ToUpper();
                        if (driverName.StartsWith("GDAL/") || driverName.StartsWith("/OGR") || drivers.Contains(driverName))
                            output.AppendLine(line);
                        else
                        {
                            AppendLog("Change OUTPUTFORMAT/DRIVER " + values[1] + " -> AGG/PNG", lineNumber);
                            output.AppendLine("  DRIVER \"AGG/PNG\"");
                        }
                    }
                    else if ((key == "PATTERN" || key == "POSITION" || key == "GAP" || key == "LINECAP" || key == "LINEJOIN" || key == "LINEJOINMAXSIZE") && path == "\\MAP\\SYMBOL")
                    {
                        AppendLog("Remove " + key + " from the SYMBOL section", lineNumber);
                    }
                    else if (key == "FONT" && path == "\\MAP\\LAYER\\CLASS\\LABEL")
                    {
                        if (!fonts.Contains(values[1].Trim(new char[] { '\'', '\"' }).ToLower()))
                        {
                            AppendLog("Change missing font " + values[1] + " to arial", lineNumber);
                            output.AppendLine("  FONT \"arial\"");
                        }
                        else
                            output.AppendLine(line); // no change done
                    }
                    else if (key == "LABELANGLEITEM" && path == "\\MAP\\LAYER")
                    {
                        AppendLog("Remove LABELANGLEITEM " + values[1], lineNumber);
                    }
                    else if (key == "LABELSIZEITEM" && path == "\\MAP\\LAYER")
                    {
                        AppendLog("Remove LABELSIZEITEM " + values[1], lineNumber);
                    }
                    else if (key == "SYMBOL" && path == "\\MAP\\LAYER\\CLASS\\STYLE")
                    {
                        string symbolName = values[1].Trim(new char[] { '\'', '\"' }).ToLower();
                        String newSymbolName = symbolName;
                        if (symbolName == "grenze2")
                            newSymbolName = "Rectangle";
                        else if (symbolName == "ellipse-flach")
                            newSymbolName = "Ellipse";
                        else if (symbolName == "dreieck")
                            newSymbolName = "Triangle";
                        else if (symbolName == "zelt")
                            newSymbolName = "Tent";
                        else if (symbolName == "quadrat")
                            newSymbolName = "Square";
                        else if (symbolName == "kreuz1")
                            newSymbolName = "Cross";
                        else if (symbolName == "kreuz1")
                            newSymbolName = "Cross";
                        else if (symbolName == "kreuz2")
                            newSymbolName = "Cross-2";
                        else if (symbolName == "kreuz4")
                            newSymbolName = "Cross-3";
                        else if (symbolName == "haus")
                            newSymbolName = "House";
                        else if (symbolName == "sechseck")
                            newSymbolName = "Hexagon";
                        else if (symbolName == "stern")
                            newSymbolName = "Star";
                        else if (symbolName == "MapInfo-Pen-3")
                            newSymbolName = "Dot-1";
                        else if (symbolName == "MapInfo-Pen-4")
                            newSymbolName = "Dash-1";
                        else if (symbolName == "MapInfo-Pen-5")
                            newSymbolName = "Dash-2";
                        else if (symbolName == "MapInfo-Pen-6")
                            newSymbolName = "Dash-3";
                        else if (symbolName == "MapInfo-Pen-7")
                            newSymbolName = "Dash-5";
                        else if (symbolName == "MapInfo-Pen-9")
                            newSymbolName = "Dash-7";
                        else if (symbolName == "MapInfo-Pen-10")
                            newSymbolName = "Dash-6";
                        else if (symbolName == "MapInfo-Pen-11")
                            newSymbolName = "Dash-7";
                        else if (symbolName == "MapInfo-Pen-12")
                            newSymbolName = "Dash-8";
                        else if (symbolName == "MapInfo-Pen-13")
                            newSymbolName = "Dash-9";
                        else if (symbolName == "MapInfo-Pen-14")
                            newSymbolName = "DotDash-2";
                        else if (symbolName == "MapInfo-Pen-15")
                            newSymbolName = "DotDash-2";
                        else if (symbolName == "MapInfo-Pen-16")
                            newSymbolName = "DashDash-1";
                        else if (symbolName == "MapInfo-Pen-18")
                            newSymbolName = "DotDash-2";
                        else if (symbolName == "MapInfo-Pen-19")
                            newSymbolName = "DotDash-2";
                        else if (symbolName == "MapInfo-Pen-20")
                            newSymbolName = "DoubleDotDash-1";
                        else if (symbolName == "MapInfo-Pen-21")
                            newSymbolName = "DoubleDotDash-2";
                        else if (symbolName == "MapInfo-Pen-22")
                            newSymbolName = "DoubleDotDash-2";
                        else if (symbolName == "MapInfo-Pen-23")
                            newSymbolName = "DotDash-1";
                        else if (symbolName == "MapInfo-Pen-24")
                            newSymbolName = "DoubleDotDash-1";
                        else if (symbolName == "MapInfo-Pen-25")
                            newSymbolName = "DotDash-1";
                        else if (symbolName == "MapInfo-Brush-3")
                            newSymbolName = "Horizontal-1";
                        else if (symbolName == "MapInfo-Brush-4")
                            newSymbolName = "Vertical-1";
                        else if (symbolName == "MapInfo-Brush-5")
                            newSymbolName = "RightDiag-1";
                        else if (symbolName == "MapInfo-Brush-6")
                            newSymbolName = "LeftDiag-1";
                        else if (symbolName == "MapInfo-Brush-7")
                            newSymbolName = "Grid-1";
                        else if (symbolName == "MapInfo-Brush-8")
                            newSymbolName = "GridDiag-1";
                        else if (symbolName == "MapInfo-Brush-15")
                            newSymbolName = "LeftDiag-1";
                        else if (symbolName == "MapInfo-Brush-19")
                            newSymbolName = "Horizontal-1";
                        else if (symbolName == "MapInfo-Brush-20")
                            newSymbolName = "Horizontal-2";
                        else if (symbolName == "MapInfo-Brush-21")
                            newSymbolName = "Horizontal-1";
                        else if (symbolName == "MapInfo-Brush-22")
                            newSymbolName = "Horizontal-1";
                        else if (symbolName == "MapInfo-Brush-23")
                            newSymbolName = "Horizontal-1";
                        else if (symbolName == "MapInfo-Brush-26")
                            newSymbolName = "Vertical-1";
                        else if (symbolName == "MapInfo-Brush-27")
                            newSymbolName = "Vertical-1";
                        else if (symbolName == "MapInfo-Brush-28")
                            newSymbolName = "Vertical-1";
                        else if (symbolName == "MapInfo-Brush-29")
                            newSymbolName = "RightDiag-1";
                        else if (symbolName == "MapInfo-Brush-30")
                            newSymbolName = "RightDiag-1";
                        else if (symbolName == "MapInfo-Brush-31")
                            newSymbolName = "RightDiag-1";
                        else if (symbolName == "MapInfo-Brush-32")
                            newSymbolName = "RightDiag-1";
                        else if (symbolName == "MapInfo-Brush-34")
                            newSymbolName = "LeftDiag-1";
                        else if (symbolName == "MapInfo-Brush-35")
                            newSymbolName = "LeftDiag-1";
                        else if (symbolName == "MapInfo-Brush-36")
                            newSymbolName = "LeftDiag-1";
                        else if (symbolName == "MapInfo-Brush-37")
                            newSymbolName = "LeftDiag-1";
                        else if (symbolName == "MapInfo-Brush-39")
                            newSymbolName = "Grid-1";
                        else if (symbolName == "MapInfo-Brush-40")
                            newSymbolName = "Grid-2";
                        else if (symbolName == "MapInfo-Brush-41")
                            newSymbolName = "Grid-1";
                        else if (symbolName == "MapInfo-Brush-42")
                            newSymbolName = "Grid-2";
                        else if (symbolName == "MapInfo-Brush-43")
                            newSymbolName = "Grid-1";
                        else if (symbolName == "MapInfo-Brush-44")
                            newSymbolName = "GridDiag-1";
                        else if (symbolName == "MapInfo-Brush-45")
                            newSymbolName = "GridDiag-2";
                        else if (symbolName == "MapInfo-Brush-46")
                            newSymbolName = "GridDiag-1";
                        else if (symbolName == "MapInfo-Brush-47")
                            newSymbolName = "Ticks-1";
                        else if (symbolName == "MapInfo-Brush-51")
                            newSymbolName = "Ticks-1";
                        else if (symbolName == "MapInfo-Brush-63")
                            newSymbolName = "Dots-2";
                        else if (symbolName == "MapInfo-Brush-70")
                            newSymbolName = "Dots-1";
                        else if (symbolName == "MapInfo-Brush-137")
                            newSymbolName = "Dots-2";
                        if (MapLibrary.MapUtils.FindSymbol(template.symbolset, newSymbolName) == null)
                        {
                            int symbolIndex;
                            if (int.TryParse(newSymbolName, out symbolIndex))
                            {
                                // remove symbol when symbol was 0 - no symbol
                                if (symbolIndex == 0)
                                {
                                    AppendLog("Remove symbol with name: " + symbolName, lineNumber);
                                    continue;
                                }
                            }
                            newSymbolName = "default-marker";
                        }

                        if (symbolName != newSymbolName)
                        {
                            AppendLog("Rename symbol reference: " + symbolName + " -> " + newSymbolName, lineNumber);
                            output.AppendLine("  SYMBOL \'" + newSymbolName + "\'");
                        }
                    }
                    else
                        output.AppendLine(line); // not change done
                }
            }
        }

        public bool HasToConvert()
        {
            return (changelog.Length > 0);
        }

        public string GetChangeLog()
        {
            return changelog.ToString();
        }

        public string GetMapFile()
        {
            return output.ToString();
        }
    }
}
