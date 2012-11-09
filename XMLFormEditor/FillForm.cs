using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XMLFormEditor
{
    public partial class FillForm : Form
    {
        private DocumentLayoutCollection documentLayouts;
        private List<ScrollablePage> scrollablePages = new List<ScrollablePage>();

        public FillForm()
        {
            InitializeComponent();
            documentLayouts = new DocumentLayoutCollection();
            documentLayouts.controlFactory = new DefaultControlFactory();
            
            ProcessCommandLine();                
        }

        private void ProcessCommandLine()
        {
            if (Environment.GetCommandLineArgs().GetLength(0) == 1)
            {
                documentLayouts.New();
                createEditors();
            } 
            else if (Environment.GetCommandLineArgs().GetLength(0) == 2) 
            {
                string fileName = Environment.GetCommandLineArgs()[1];
                openFile(fileName);
            }        
        }
 
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addPage()
        {
            int tabIndex = tabControl1.TabPages.Count;

            tabControl1.TabPages.Add(documentLayouts[tabIndex].LayoutName);
            ScrollablePage page = new ScrollablePage();
            page.Dock = DockStyle.Fill;
            scrollablePages.Add(page);
            page.documentLayout = documentLayouts[tabIndex];
            page.Parent = tabControl1.TabPages[tabIndex];      
        }

        private void createEditors()
        {
            WindowsApiMethods.SuspendDrawing(panel1);
            scrollablePages.Clear();
            tabControl1.TabPages.Clear();

            for ( int i = 0 ; i< documentLayouts.LayoutCount ; i++ )
            {
                addPage();
            }

            WindowsApiMethods.ResumeDrawing(panel1);            
        }

        public void openFile(string fileName)
        {            
            try
            {
                MainForm.writeRecentFileToRegistry(fileName);

                string path = System.IO.Path.GetDirectoryName(fileName);
                Environment.CurrentDirectory = path;

                documentLayouts.Load(fileName);
                Text = "Xml Form Editor - " + fileName;

                String dir = Path.GetDirectoryName(fileName);
                Directory.SetCurrentDirectory(dir);
            }            
            catch (Exception e)
            {                
                MessageBox.Show(String.Format("Document could not be opened!\n\n{0} ", e.Message));
                documentLayouts.New();
            }

            createEditors();
        }

        private void openLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOpenDialog();
        }

        public bool ShowOpenDialog()
        {
            System.Windows.Forms.OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Xml From Edit files (*.xfe)|*.xfe|All files (*.*)|*.*";
            openDialog.FilterIndex = 0;
            openDialog.DefaultExt = "xfe";
            openDialog.AddExtension = true;


            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                openFile(openDialog.FileName);
                return true;
            }
            return false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex >= 0 && tabControl1.SelectedIndex < scrollablePages.Count)
            {
                scrollablePages[tabControl1.SelectedIndex].documentFormVisualizer.updateVisibleControls();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlSourceDocumentManager.Instance().SaveDocuments();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void reloadOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reload the original document?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                XmlSourceDocumentManager.Instance().LoadDocuments();
                if (tabControl1.SelectedIndex >= 0 && tabControl1.SelectedIndex < scrollablePages.Count)
                {
                    scrollablePages[tabControl1.SelectedIndex].documentFormVisualizer.updateVisibleControls();
                }

            }
        }

        private void FillForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save?", "Exit ...", MessageBoxButtons.YesNoCancel);
            switch (dialogResult)
            {
                case DialogResult.Yes:    
                    XmlSourceDocumentManager.Instance().SaveDocuments();
                    break;
                case DialogResult.No:
                    e.Cancel = false;
                   break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidationResultDialog dialog = new ValidationResultDialog();
            dialog.ValidationResults = XmlSourceDocumentManager.Instance().ValidateDocuments();
            dialog.ShowWarnings = false;
            dialog.ShowDialog();
        }

        private void strictValidateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidationResultDialog dialog = new ValidationResultDialog();
            dialog.ValidationResults = XmlSourceDocumentManager.Instance().ValidateDocuments();
            dialog.ShowWarnings = true;
            dialog.ShowDialog();
        }


    }
}