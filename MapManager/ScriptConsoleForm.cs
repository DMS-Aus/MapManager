using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DMS.MapLibrary;
using System.CodeDom.Compiler;
using System.Reflection;
using OSGeo.MapServer;

namespace DMS.MapManager
{
    public partial class ScriptConsoleForm : Form
    {
        private const string scriptPrefix =
        @"
        using System;
        using System.Text;
        using DMS.MapLibrary;
        using OSGeo.MapServer;
        using OSGeo.GDAL;
        using OSGeo.OGR;
        
        namespace DMS.Scripts
        {
            public class Script
            {
                private static StringBuilder msg;
                private static void Write(object message)
                {
                    msg.Append(message.ToString());
                }
                private static void WriteLine(object message)
                {
                    msg.AppendLine(message.ToString());
                }
                public static string Run(mapObj map)
                {
                    msg = new StringBuilder();
        ";
         
        private const string scriptPostfix =
        @"
                    return msg.ToString();
                }
            }
        }
        ";

        private MapObjectHolder mapH;
        private mapObj map;

        public ScriptConsoleForm(MapObjectHolder mapH)
        {
            InitializeComponent();
            this.mapH = mapH;
            this.map = mapH;

            ListSupportedLanguages();
        }

        private void ListSupportedLanguages()
        {
            // Loop through information about all compilers.
            CompilerInfo[] compiler_infos =
                CodeDomProvider.GetAllCompilerInfo();
            foreach (CompilerInfo info in compiler_infos)
            {
                if (info.IsCodeDomProviderTypeValid)
                {
                    // Get information about this compiler.
                    CodeDomProvider provider = info.CreateProvider();
                    
                    //string extensions = "";
                    string default_extension = provider.FileExtension;
                    if (default_extension[0] != '.')
                        default_extension = '.' + default_extension;

                    string default_language =
                        CodeDomProvider.GetLanguageFromExtension(default_extension);

                    toolStripComboBoxLang.Items.Add(default_language);
                }
            }
            if (toolStripComboBoxLang.Items.Count > 0)
                toolStripComboBoxLang.SelectedIndex = 0;
        }


        private void Print(string msg)
        {
            textBoxOutput.Text += msg + "\r\n" ;
        }

        private void Compile(string script)
        {
            try
            {
                textBoxOutput.Text = "";
                
                Assembly asm = null;

                CodeDomProvider provider = CodeDomProvider.CreateProvider(toolStripComboBoxLang.Text);

                CompilerParameters cp = new CompilerParameters
                {
                    GenerateExecutable = false,
                    GenerateInMemory = true
                };

                // Add references to all the assemblies we might need.
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                cp.ReferencedAssemblies.Add(executingAssembly.Location);
                foreach (AssemblyName assemblyName in executingAssembly.GetReferencedAssemblies())
                    cp.ReferencedAssemblies.Add(Assembly.Load(assemblyName).Location);

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromSource(cp, scriptPrefix + script + scriptPostfix);

                if (cr.Errors.Count > 0)
                {
                    // Display compilation errors.
                    StringBuilder builder = new StringBuilder();
                    foreach (CompilerError ce in cr.Errors)
                    {
                        builder.Append(ce.ToString());
                        builder.Append("\n");
                    }
                    Print(builder.ToString());
                    return;
                }
                else
                    asm = cr.CompiledAssembly;

                Type t = asm.GetType("DMS.Scripts.Script");

                MethodInfo method = t.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);
                string msg = method.Invoke(null, new object[] { map }).ToString();
                Print(msg);
                Print("\r\nScript completed successfully!");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
                if (ex.InnerException != null)
                    Print(ex.InnerException.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Compile(textBoxScript.Text);
            mapH.RaisePropertyChanged(this);
        }
    }
}
