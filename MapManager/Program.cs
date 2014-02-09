using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace DMS.MapManager
{
    static class Program
    {
        public static MainForm frmMain = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // setting the current directory equal to the executing directory
            if (Environment.CurrentDirectory != Application.StartupPath)
            {
                Environment.CurrentDirectory = Application.StartupPath;
            }

            // post setup install actions
            if (args.Length > 0)
            {
                if (args[0] == "/postinstall")
                {
                    ReplaceFiles();
                    return;
                }
                else if (args[0] == "/replace")
                {
                    ReplaceFiles();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmMain = new MainForm();
            Application.Run(frmMain);
        }

        static void ReplaceFiles()
        {
            String strFile = File.ReadAllText("templates\\new.map");
            strFile = strFile.Replace("FONTSET \"font.list\"", "FONTSET \"" + (Application.StartupPath + "\\templates\\font.list\"").Replace("\\", "\\\\"));
            strFile = strFile.Replace("SYMBOLSET \"symbols.sym\"", "SYMBOLSET \"" + (Application.StartupPath + "\\templates\\symbols.sym\"").Replace("\\", "\\\\"));
            File.WriteAllText("templates\\new.map", strFile);

            // set references in mmstyles.map
            strFile = File.ReadAllText("templates\\mmstyles.map");
            strFile = strFile.Replace("FONTSET \"font.list\"", "FONTSET \"" + (Application.StartupPath + "\\templates\\font.list\"").Replace("\\", "\\\\"));
            strFile = strFile.Replace("SYMBOLSET \"symbols.sym\"", "SYMBOLSET \"" + (Application.StartupPath + "\\templates\\symbols.sym\"").Replace("\\", "\\\\"));
            File.WriteAllText("templates\\mmstyles.map", strFile);

            // set references in annotation.map
            strFile = File.ReadAllText("templates\\annotation.map");
            strFile = strFile.Replace("FONTSET \"font.list\"", "FONTSET \"" + (Application.StartupPath + "\\templates\\font.list\"").Replace("\\", "\\\\"));
            strFile = strFile.Replace("SYMBOLSET \"symbols.sym\"", "SYMBOLSET \"" + (Application.StartupPath + "\\templates\\symbols.sym\"").Replace("\\", "\\\\"));
            File.WriteAllText("templates\\annotation.map", strFile);
        }
    }
}