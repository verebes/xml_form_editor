using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.IO;

namespace XMLFormEditor
{
    public partial class EditorForm : Form
    {

        private DocumentLayoutCollection documentLayouts;
        private List<ScrollableEditor> scrollableEditors = new List<ScrollableEditor>();

        public EditorForm()
        {
            InitializeComponent();

            ToolBoxInitializer toolBoxInit = new ToolBoxInitializer(toolBoxControl1);
            toolBoxInit.InitializeToolBox();         
            
            documentLayouts = new DocumentLayoutCollection();
            documentLayouts.controlFactory = new DefaultControlFactory();
            documentLayouts.New();
            createEditors();
        }

        
        private void UpdateMenuState()
        {
            bool enableMenus = 
                documentLayouts.LayoutCount > tabControl1.SelectedIndex && 
                tabControl1.SelectedIndex>=0  &&                 
                scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.Focused ;

            cutToolStripMenuItem1.Enabled = enableMenus;
            copyToolStripMenuItem.Enabled = enableMenus;
            deleteToolStripMenuItem.Enabled = enableMenus;
            pasteToolStripMenuItem.Enabled = enableMenus;
            clearSelectionToolStripMenuItem.Enabled = enableMenus;
            arrangeLeftToolStripMenuItem.Enabled = enableMenus;
            arrangeRightToolStripMenuItem.Enabled = enableMenus;

            snapToGridToolStripMenuItem.Enabled = true;
            showBackgroundImageToolStripMenuItem.Enabled = true;
            drawGridToolStripMenuItem.Enabled = true;
            setGridSizeToolStripMenuItem.Enabled = true;
            showLinesToolStripMenuItem.Enabled = true;
            showJunctionsToolStripMenuItem.Enabled = true;
            
            
            selectAllToolStripMenuItem.Enabled = enableMenus;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOpenDialog();
        }


        private void buttonAddDocument_Click(object sender, EventArgs e)
        {
            AddXmlDocument();
        }

       
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            documentLayouts.New();
            listBoxDocumentSources.Items.Clear();
            createEditors();
        }

        private void saveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlSourceDocumentManager.Instance().SaveDocuments();

                if (!documentLayouts.Save())
                    saveAsToolStripMenuItem_Click(sender, e);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Xml From Edit files (*.xfe)|*.xfe|All files (*.*)|*.*";
            saveDialog.FilterIndex = 0;
            saveDialog.DefaultExt = "xfe";
            saveDialog.AddExtension = true;


            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                documentLayouts.SaveAs(saveDialog.FileName);
                MainForm.writeRecentFileToRegistry(saveDialog.FileName);

                String dir = Path.GetDirectoryName(saveDialog.FileName);
                Directory.SetCurrentDirectory(dir);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                scrollableEditors[i].documentEditorVisualizer.PropertyWindowVisible = ( tabControl1.SelectedIndex == i );
            }

            if ( tabControl1.SelectedIndex >= 0 && tabControl1.SelectedIndex < scrollableEditors.Count)
                toolBoxControl1.Editor = scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer;                
            else
                toolBoxControl1.Editor = null;

            listBoxPageNames.SelectedIndex = tabControl1.SelectedIndex;

            UpdateMenuState();
        }

        private void addPage()
        {
            int tabIndex = tabControl1.TabPages.Count;

            tabControl1.TabPages.Add(documentLayouts[tabIndex].LayoutName);
            ScrollableEditor editor = new ScrollableEditor();
            editor.TabStop = false;
            editor.Dock = DockStyle.Fill;
            scrollableEditors.Add(editor);                        
            editor.documentLayout = documentLayouts[tabIndex];
            editor.documentEditorVisualizer.PropertyWindowPlace = panel4;
            editor.Parent = tabControl1.TabPages[tabIndex];

            editor.documentEditorVisualizer.BackgroundVisible = showBackgroundImageToolStripMenuItem.Checked;
            editor.documentEditorVisualizer.SnapToGrid = snapToGridToolStripMenuItem.Checked;
            editor.documentEditorVisualizer.DrawGrid = drawGridToolStripMenuItem.Checked;
            editor.documentEditorVisualizer.GridSize = gridSize;
            editor.documentEditorVisualizer.LinesVisible = showLinesToolStripMenuItem.Checked;
            editor.documentEditorVisualizer.JunctionsVisible = showJunctionsToolStripMenuItem.Checked;

            editor.Enter += delegate { 
                editor.documentEditorVisualizer.Focus();
                UpdateMenuState();

            };

            editor.documentEditorVisualizer.Leave += delegate {
                UpdateMenuState();
            };

            UpdateMenuState();

        }

        private void createEditors()
        {
            WindowsApiMethods.SuspendDrawing(panel6);
            WindowsApiMethods.SuspendDrawing(listBoxPageNames);

            tabControl1.SelectedIndexChanged -= tabControl1_SelectedIndexChanged;

            Cursor prevCurs = Cursor;
            Cursor = Cursors.WaitCursor;


            foreach (ScrollableEditor editor in scrollableEditors)           
                editor.documentEditorVisualizer.DestroyPropertyWindow();


            scrollableEditors.Clear();
            tabControl1.TabPages.Clear();            
            listBoxPageNames.Items.Clear();
            for ( int i = 0 ; i< documentLayouts.LayoutCount ; i++ )
            {
                listBoxPageNames.Items.Add(documentLayouts[i].LayoutName);
                addPage();
            }


            if (scrollableEditors.Count > 0)
                toolBoxControl1.Editor = scrollableEditors[0].documentEditorVisualizer;
            WindowsApiMethods.ResumeDrawing(panel6);
            WindowsApiMethods.ResumeDrawing(listBoxPageNames);

            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;

            if (scrollableEditors.Count > 0)
                scrollableEditors[0].documentEditorVisualizer.Refresh();

            Cursor = prevCurs;

            UpdateMenuState();
            buttonRemovePage.Enabled = false;
        }
        
        public void openFile(string fileName)
        {
            System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(fileName));
            listBoxDocumentSources.Items.Clear();

            try
            {
                documentLayouts.Load(fileName);
                MainForm.writeRecentFileToRegistry(fileName);

                List<string> docNames = XmlSourceDocumentManager.Instance().GetDocumentNames();
                foreach ( string name in docNames)
                {
                    listBoxDocumentSources.Items.Add(name);
                }
                Text = "Xml Form Editor - " + fileName;
            }            
            catch (Exception e)
            {                
                MessageBox.Show(String.Format("Document could not be opened!\n\n{0} ", e.Message));
                documentLayouts.New();
            }

            createEditors();
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



        private void AddXmlDocument()
        {
            if (documentLayouts.LayoutFileName == null || documentLayouts.LayoutFileName == "")
            {
                MessageBox.Show("Please save the document before adding xml document!");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Multiselect = true;
            dialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "xml";
            dialog.AddExtension = true;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            
            foreach (string s in dialog.FileNames)
            {
                try
                {
                    string relativeFileName = EvaluateRelativePath(documentLayouts.LayoutFileName, s);

                    XmlSourceDocumentManager.Instance().AddDocument(relativeFileName);
                    listBoxDocumentSources.Items.Add(relativeFileName);
                }
                catch (System.Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("An error occurred: {0}",e.Message),"Error");
                }
            }
                        
        }

        private void RemoveXmlDocument()
        {
            if (listBoxDocumentSources.SelectedIndex >= 0)
            {
                XmlSourceDocumentManager.Instance().RemoveDocument(listBoxDocumentSources.SelectedItem.ToString());
                listBoxDocumentSources.Items.RemoveAt(listBoxDocumentSources.SelectedIndex);
            }
        }

        private void NewXmlDocument()
        {
            if (documentLayouts.LayoutFileName == null || documentLayouts.LayoutFileName == "")
            {
                MessageBox.Show("Please save the document before adding xml document!");
                return;
            }

            String dir = Path.GetDirectoryName(documentLayouts.LayoutFileName);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = dir;
            dialog.RestoreDirectory = true;
            dialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "xml";            
            dialog.AddExtension = true;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                string relativeFileName = EvaluateRelativePath(documentLayouts.LayoutFileName, dialog.FileName);
                
                XmlSourceDocumentManager.Instance().NewDocument(relativeFileName);
                listBoxDocumentSources.Items.Add(relativeFileName);
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("An error occurred: {0}", e.Message), "Error");
            }

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
                AddXmlDocument();

            if (e.KeyData == Keys.Delete)
                RemoveXmlDocument();
        }


        private void buttonDocumentSourceRemove_Click(object sender, EventArgs e)
        {
            RemoveXmlDocument();            
        }

        private void listBoxDocumentSources_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxDocumentSources.SelectedItem == null)
                return;

            TextView tv = new TextView();
            string fileName = listBoxDocumentSources.SelectedItem.ToString();
            System.IO.StreamReader sr = System.IO.File.OpenText(fileName);            
            tv.richTextBox1.Text = sr.ReadToEnd();
            sr.Close();
            if (tv.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(fileName, tv.richTextBox1.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }

                XmlSourceDocumentManager.Instance().LoadDocuments();
                scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.Refresh();
            }
        }

        private void splitter5_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Black);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Rectangle r = new Rectangle(e.ClipRectangle.Location, e.ClipRectangle.Size);
            r.Height = r.Height - 1;
            r.Width = r.Width - 2;
            e.Graphics.DrawRectangle(p, r);
        }

        private void buttonAddPage_Click(object sender, EventArgs e)
        {
            InputTextDialog inputTextDialog = new InputTextDialog();
            if (inputTextDialog.ShowDialog() != DialogResult.OK)
                return;
            
            if (listBoxPageNames.Items.Contains(inputTextDialog.Value) || inputTextDialog.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Error: page name must be non empty and unique", "Error");
            }
            else
            {
                listBoxPageNames.Items.Add(inputTextDialog.Value);

                DocumentLayout layout = documentLayouts.CreateDocumentLayout();
                layout.LayoutName = inputTextDialog.Value;
                addPage();
                if (documentLayouts.LayoutCount == 1 )
                    toolBoxControl1.Editor = scrollableEditors[0].documentEditorVisualizer;
            }
            
        }

        private void buttonRemovePage_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            
            int index = listBoxPageNames.SelectedIndex;

            if (index<0 || index>= documentLayouts.LayoutCount )
                return;

            listBoxPageNames.SelectedItem.ToString();
            documentLayouts.RemoveDocumentLayout(index);
            tabControl1.TabPages.RemoveAt(index);
            listBoxPageNames.Items.RemoveAt(index);
            scrollableEditors[index].documentEditorVisualizer.DestroyPropertyWindow();
            scrollableEditors.RemoveAt(index);

            UpdateMenuState();
        }

        private void listBoxPageNames_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPageNames.SelectedIndex >= 0 && listBoxPageNames.SelectedIndex < documentLayouts.LayoutCount)
            {
                tabControl1.SelectedIndex = listBoxPageNames.SelectedIndex;
            }
        }

        private void EditorForm_Shown(object sender, EventArgs e)
        {
            if (scrollableEditors.Count > 0)
            {
                Trace.WriteLine("EditForm::EditorForm_Shown: Calling recreateControls");
                scrollableEditors[0].documentEditorVisualizer.recreateControls();
            }
        }

        private void listBoxPageNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemovePage.Enabled = (listBoxPageNames.SelectedIndex != -1);            
        }

        private void buttonNewDocument_Click(object sender, EventArgs e)
        {
            NewXmlDocument();           
        }


        private static string EvaluateRelativePath(string mainDirPath, string absoluteFilePath)
        {

            string[] firstPathParts = mainDirPath.Trim(System.IO.Path.DirectorySeparatorChar).Split(System.IO.Path.DirectorySeparatorChar);
            string[] secondPathParts = absoluteFilePath.Trim(System.IO.Path.DirectorySeparatorChar).Split(System.IO.Path.DirectorySeparatorChar);

            int sameCounter = 0;
            for (int i = 0; i < Math.Min(firstPathParts.Length, secondPathParts.Length); i++)
            {
                if (!firstPathParts[i].ToLower().Equals(secondPathParts[i].ToLower()))
                {
                    break;
                }
                sameCounter++;
            }

            if (sameCounter == 0)
            {
                return absoluteFilePath;
            }

            string newPath = String.Empty;

            for (int i = sameCounter; i < firstPathParts.Length-1; i++)
            {
                    newPath += "..";
                    newPath += System.IO.Path.DirectorySeparatorChar;
            }

            for (int i = sameCounter; i < secondPathParts.Length-1; i++)
            {
                newPath += secondPathParts[i];
                newPath += System.IO.Path.DirectorySeparatorChar;
            }

            newPath += secondPathParts[secondPathParts.Length-1];

            return newPath;
        }


        private System.Xml.XmlDocument clipboardDoc = null;
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                clipboardDoc.DocumentElement.Attributes["Name"].Value = documentLayouts[tabControl1.SelectedIndex].LayoutName;
                documentLayouts[tabControl1.SelectedIndex].ClearSelection();
                documentLayouts[tabControl1.SelectedIndex].deserializeFromXml(clipboardDoc.DocumentElement, true);
                scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.recreateControls();                
            }
            catch (Exception)
            { 
            
            }

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clipboardDoc = new System.Xml.XmlDocument();

            clipboardDoc.AppendChild(documentLayouts[tabControl1.SelectedIndex].SerializeSelectedToXml(clipboardDoc));
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            documentLayouts[tabControl1.SelectedIndex].SelectAllControls();
            scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.Refresh();
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            documentLayouts[tabControl1.SelectedIndex].ClearSelection();
            scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( !scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.Focused  ) {
                return;
            }

            documentLayouts[tabControl1.SelectedIndex].RemoveSelectedControls();
            scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.recreateControls();            
        }

        private void arrangeLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            documentLayouts[tabControl1.SelectedIndex].ArrangeSelectedControls( ArrangeType.Left  );
            scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.recreateControls();            
        }


        private void arrangeRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            documentLayouts[tabControl1.SelectedIndex].ArrangeSelectedControls(ArrangeType.Right);
            scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.recreateControls();            
        }


        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tabControl1.Focused)
                return;

            clipboardDoc = new System.Xml.XmlDocument();
            clipboardDoc.AppendChild(documentLayouts[tabControl1.SelectedIndex].SerializeSelectedToXml(clipboardDoc));
            documentLayouts[tabControl1.SelectedIndex].RemoveSelectedControls();
            scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.recreateControls();            
        }

        private void snapToGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            snapToGridToolStripMenuItem.Checked = !snapToGridToolStripMenuItem.Checked;
            foreach (ScrollableEditor editor in scrollableEditors)
            {
                    editor.documentEditorVisualizer.SnapToGrid = snapToGridToolStripMenuItem.Checked;
            }            
        }

        protected int gridSize = 10;
        public int GridSize {
            get {
                return gridSize;
            }
            set {
                if (gridSize == value)
                    return;
                
                gridSize = value;
                foreach (ScrollableEditor editor in scrollableEditors) {
                    editor.documentEditorVisualizer.GridSize = gridSize;
                }
            }
        }

        private void setGridSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OptionsForm optionsForm = new OptionsForm();            

            if (optionsForm.ShowDialog() == DialogResult.OK)
            {
                GridSize = Convert.ToInt32(optionsForm.textBoxGridSize.Text);                
            }
        }

        private void drawGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawGridToolStripMenuItem.Checked = !drawGridToolStripMenuItem.Checked;
            foreach (ScrollableEditor editor in scrollableEditors)
            {
                editor.documentEditorVisualizer.DrawGrid = drawGridToolStripMenuItem.Checked;                
            }

        }

        private void applyXSLTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openXsltDialog = new OpenFileDialog();
                openXsltDialog.Title = "Select XSLT file";
                openXsltDialog.Filter = "XSLT transformations files (*.xsl)|*.xsl|All files (*.*)|*.*";
                openXsltDialog.FilterIndex = 0;
                openXsltDialog.DefaultExt = "xsl";
                openXsltDialog.AddExtension = true;

                if (openXsltDialog.ShowDialog() != DialogResult.OK)
                    return;

                XsltSettings setting = new XsltSettings(true, true);
                XslCompiledTransform transform = new XslCompiledTransform();

                transform.Load(openXsltDialog.FileName, setting, null);

                OpenFileDialog openXMLDialog = new OpenFileDialog();
                openXMLDialog.Title = "Select file to be transformed";
                openXMLDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openXMLDialog.FilterIndex = 0;
                openXMLDialog.DefaultExt = "xml";
                openXMLDialog.AddExtension = true;


                if (openXMLDialog.ShowDialog() != DialogResult.OK)
                    return;

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Save the output Xml Form Editor File";
                saveDialog.RestoreDirectory = true;
                saveDialog.Filter = "Xfe files (*.xfe)|*.xfe|All files (*.*)|*.*";
                saveDialog.FilterIndex = 0;
                saveDialog.DefaultExt = "xfe";
                saveDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(openXMLDialog.FileName);
                saveDialog.AddExtension = true;


                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(openXMLDialog.FileName));

                XsltArgumentList args = new XsltArgumentList();

                string relativeFileName = EvaluateRelativePath(saveDialog.FileName, openXMLDialog.FileName);

                args.AddParam("InputXMLRelativeFileName", "",  relativeFileName);
                args.AddParam("InputXMLAbslouteFileName", "", openXMLDialog.FileName);

                System.IO.FileStream fs = new System.IO.FileStream(saveDialog.FileName, System.IO.FileMode.Create);
                transform.Transform(openXMLDialog.FileName,args, fs);
                fs.Close();
                openFile(saveDialog.FileName);
            }
            catch (System.Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void showBackgroundImageToolStripMenuItem_Click(object sender, EventArgs e) {            
            showBackgroundImageToolStripMenuItem.Checked = !showBackgroundImageToolStripMenuItem.Checked;
            foreach (ScrollableEditor editor in scrollableEditors) {
                editor.documentEditorVisualizer.BackgroundVisible = showBackgroundImageToolStripMenuItem.Checked;
            }
            if (tabControl1.SelectedIndex >= 0)
                scrollableEditors[tabControl1.SelectedIndex].documentEditorVisualizer.Invalidate();
        }

        private void showJunctionsToolStripMenuItem_Click(object sender, EventArgs e) {
            showJunctionsToolStripMenuItem.Checked = !showJunctionsToolStripMenuItem.Checked;
            foreach (ScrollableEditor editor in scrollableEditors) {
                editor.documentEditorVisualizer.JunctionsVisible = showJunctionsToolStripMenuItem.Checked;
            }
        }

        private void showLinesToolStripMenuItem_Click(object sender, EventArgs e) {
            showLinesToolStripMenuItem.Checked = !showLinesToolStripMenuItem.Checked;
            foreach (ScrollableEditor editor in scrollableEditors) {
                editor.documentEditorVisualizer.LinesVisible = showLinesToolStripMenuItem.Checked;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
            doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);

            PrintPreviewDialog ppDialog = new PrintPreviewDialog();
            ppDialog.Document  = doc;
            ppDialog.ShowDialog();

        }

        void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("valami", new Font("Arial", 24),Brushes.Black, 10,10);
        }

    }
}