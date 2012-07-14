using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;


namespace XMLFormEditor
{
    public partial class MainForm : Form
    {
        const string ListItemNew = "< New >";
        const string ListItemOpen = "< Open >";

        public MainForm()
        {
            InitializeComponent();
            
            fillRecentFilesListboxFromRegistry();

        }


        private void fillRecentFilesListboxFromRegistry()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(ListItemNew);
            listBox1.Items.Add(ListItemOpen);

            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"Software\XmlFormEditor\recent");            
            string[] keyNames = regKey.GetValueNames();
            foreach (string name in keyNames)
            {
                if (!name.StartsWith("File"))
                    continue;

                try
                {
                    Convert.ToInt32(name.Substring(4));
                }
                catch (Exception)
                {
                    continue;
                }

                if (regKey.GetValueKind(name) != RegistryValueKind.String)
                    continue;

                if (System.IO.File.Exists(regKey.GetValue(name).ToString()))
                    listBox1.Items.Insert(2,regKey.GetValue(name).ToString());
                else
                    regKey.DeleteValue(name);
            }

            listBox1.SelectedIndex = 0;

            regKey.Close();        
        }

        public static void writeRecentFileToRegistry( string fileName)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"Software\XmlFormEditor\recent");

            string[] valNames = regKey.GetValueNames();
            int max = 0;
            foreach(string name in valNames )
            {
                if (!name.StartsWith("File"))
                    continue;

                try
                {
                    int id = Convert.ToInt32(name.Substring(4));
                    if (id > max)
                        max = id;

                    if (regKey.GetValue(name).ToString() == fileName)
                        return;
                }
                catch (Exception)
                {
                    continue;
                }              
            }

            max++;
            regKey.SetValue(string.Format("File{0}", max), fileName, RegistryValueKind.String);
            regKey.Close();
         }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;


            EditorForm form = new EditorForm();
            form.Closed += delegate { 
                fillRecentFilesListboxFromRegistry(); 
                Show(); 
            };

            if (listBox1.SelectedItem.ToString() == ListItemNew)
            {
                Hide();
                form.Show();
                //form.WindowState = FormWindowState.Maximized;
            }
            else if (listBox1.SelectedItem.ToString() == ListItemOpen)
            {                
                if (form.ShowOpenDialog())
                {
                    Hide();
                    form.Show();
                    //form.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {                
                form.openFile(listBox1.SelectedItem.ToString());
                Hide();                
                form.Show();
                //form.WindowState = FormWindowState.Maximized;
            }
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            if (listBox1.SelectedItem.ToString() == ListItemNew)
                return;

            FillForm form = new FillForm();
            form.Closed += delegate {                  
                fillRecentFilesListboxFromRegistry();
                Show();
            };
            if (listBox1.SelectedItem.ToString() == ListItemOpen)
            {
                if (form.ShowOpenDialog())
                {                    
                    Hide();                    
                    form.WindowState = FormWindowState.Maximized;
                    form.Show();
                }
            }
            else
            {
                form.openFile(listBox1.SelectedItem.ToString());
                Hide();                
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( listBox1.SelectedIndex == -1 ){
                buttonEdit.Enabled = false;
                buttonFill.Enabled = false;

                AcceptButton = buttonFill;
            }
            else if (listBox1.SelectedItem.ToString() == ListItemNew )
            {
                buttonEdit.Enabled = true;
                buttonFill.Enabled = false;

                AcceptButton = buttonEdit;
            }
            else
            {
                buttonEdit.Enabled = true;
                buttonFill.Enabled = true;

                AcceptButton = buttonFill;
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == ListItemNew)
                buttonEdit_Click(sender, e);
            else
                buttonFill_Click(sender, e);
        }
    }
}