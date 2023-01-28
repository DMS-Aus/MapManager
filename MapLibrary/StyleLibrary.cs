using System;
using System.Collections.Generic;
using System.Text;
using OSGeo.MapServer;
using System.IO;
using Microsoft.Win32;

namespace DMS.MapLibrary
{
    public static class StyleLibrary
    {
        private static MapObjectHolder styles;
        private static mapObj map;
        private static string styleLibFileName;
        private static string symbolsetFileName;
        private static string fontsetFileName;
        
        static StyleLibrary()
        {
            symbolsetFileName = null;
        }

        public static MapObjectHolder Styles
        {
            get
            {
                return styles;
            }
        }

        public static bool HasSymbol(string name)
        {
            return MapUtils.FindSymbol(map.symbolset, name) != null;
        }

        public static void ExpandFontStyles()
        {
            string symbolSetFileContents = File.ReadAllText(symbolsetFileName);
            string fontSetFileContents = File.ReadAllText(fontsetFileName);
            StringBuilder newSymbols = new StringBuilder();
            StringBuilder newFonts = new StringBuilder();
            
            for (int i = 0; i < map.numlayers; i++)
            {
                layerObj layer = map.getLayer(i);
                if (MapUtils.HasMetadata(layer, "character-count"))
                {
                    string charcount = layer.metadata.get("character-count", null);
                    int num;
                    if (layer.numclasses == 1 && charcount != null && int.TryParse(charcount, out num))
                    {
                        classObj classobj = layer.getClass(0);
                        if (!fontSetFileContents.Contains(classobj.name))
                        {   
                            throw new Exception("Invalid font reference in mmstyles.map: " + classobj.name + ". The fontset file should contain an entry for this font name.");
                        }
                        for (int c = 33; c < 33 + num; c++)
                        {
                            string symbolname = classobj.name + "-" + c.ToString();

                            if (!symbolSetFileContents.Contains(symbolname))
                            {
                                symbolObj sym = new symbolObj(symbolname, null);
                                sym.character = "&#" + c.ToString() + ";";
                                sym.type = (int)MS_SYMBOL_TYPE.MS_SYMBOL_TRUETYPE;
                                sym.font = classobj.name;
                                sym.inmapfile = 0;
                                map.symbolset.appendSymbol(sym);
                                newSymbols.Append(String.Format("SYMBOL{0}  NAME \"{1}\"{0}  TYPE TRUETYPE{0}  ANTIALIAS TRUE{0}  CHARACTER \"{2}\"{0}  FONT \"{3}\"{0}END{0}", Environment.NewLine, symbolname, sym.character, sym.font));
                            }
                            if (c > 33)
                            {
                                // the first class is already inserted
                                classObj class2 = classobj.clone();
                                class2.name = symbolname;
                                styleObj style2 = class2.getStyle(0);
                                style2.setSymbolByName(map, symbolname);
                                layer.insertClass(class2, -1);
                            }
                            else
                            {
                                styleObj style2 = classobj.getStyle(0);
                                style2.setSymbolByName(map, symbolname);
                            }
                        }
                        if (!classobj.name.EndsWith("-33"))
                            classobj.name += "-33";
                    }
                    layer.metadata.remove("character-count");
                }
            }
            if (newSymbols.Length > 0)
            {
                // writing the new symbols to the symbolset file
                int lastpos = symbolSetFileContents.LastIndexOf("END", StringComparison.InvariantCultureIgnoreCase);
                symbolSetFileContents = symbolSetFileContents.Substring(0, lastpos) + newSymbols.ToString() + "END";
                File.WriteAllText(symbolsetFileName, symbolSetFileContents);
            }
            if (newFonts.Length > 0)
            {
                // writing the new fonts to the fontset file
                File.WriteAllText(fontsetFileName, fontSetFileContents + newFonts.ToString());
            }
        }

        public static void Load(string fileName)
        {
            styles = MapUtils.OpenMap(fileName);
            map = styles;
            styleLibFileName = fileName;
            
            // get symbolset file name
            string styleDir = Path.GetDirectoryName(fileName);
            if (File.Exists(map.symbolset.filename))
            {
                symbolsetFileName = map.symbolset.filename;
            }
            else
            {
                throw new Exception("Cannot load Symbolset file." + map.symbolset.filename);
            }

            if (File.Exists(map.fontset.filename))
            {
                fontsetFileName = map.fontset.filename;
            }
            else
            {
                throw new Exception("Cannot load Fontset file. " + map.fontset.filename);
            }
            
            ExpandFontStyles();
        }

        public static string StyleLibFileName
        {
            get
            {
                return styleLibFileName;
            }
        }

        public static string SymbolsetFileName
        {
            get
            {
                return symbolsetFileName;
            }
        }

        public static string FontsetFileName
        {
            get
            {
                return fontsetFileName;
            }
        }

        public static void Save()
        {
            SaveAs(styleLibFileName);
        }

        public static void SaveAs(string fileName)
        {
            map.save(fileName);
        }

        public static void Refresh()
        {
            Load(styleLibFileName);
        }

        /// <summary>
        /// Loading the text contents based on the current map configuration
        /// </summary>
        public static string LoadTextContents()
        {
            string txt = "";
            // saving the map into a temporary file
            if (map != null)
                txt = map.convertToString();
            return txt;
        }

        /// <summary>
        /// Applying the text contents based on the current map configuration
        /// </summary>
        public static void ApplyTextContents(string text)
        {
            map = mapscript.msLoadMapFromString(text, map != null? map.mappath : null, null);
            styles = new MapObjectHolder(map, null);
        }
    }
}
