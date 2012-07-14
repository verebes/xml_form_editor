using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XMLFormEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] argv = Environment.GetCommandLineArgs();
            int argc = argv.GetLength(0);

            Form mainWindow;

            if (argc == 1) {
                mainWindow = new MainForm();    
            } 
            else if (argc == 2)
            {
                FillForm fillForm = new FillForm();
                fillForm.openFile(argv[1]);
                fillForm.WindowState = FormWindowState.Maximized;
                mainWindow = fillForm;
            }
            else if (argc == 3)
            {
                if (argv[1] != "/E" && argv[1] != "/e")
                {
                    InvalidCommandLineArgument();
                    return;
                }


                EditorForm editorForm = new EditorForm();
                editorForm.openFile(argv[2]);
                editorForm.WindowState = FormWindowState.Maximized;
                mainWindow = editorForm;
            }
            else 
            {
                InvalidCommandLineArgument();
                return;
            }

            Application.Run(mainWindow);
        }

        private static void InvalidCommandLineArgument()
        {
            string s = "Invalid command line argument! \r\n";
            s += "Usage: XMLFormEditor.exe [/E] <filename.xfe> \r\n";
            s += "Switches\r\n";
            s += "/E - Editor mode";

            System.Windows.Forms.MessageBox.Show(s);
        }

    }
}