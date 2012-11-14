using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class SingleDataSourcePropertyControl : XMLPropertyControlBase, ISingleDataSource
    {
        public SingleDataSourcePropertyControl()
        {
            InitializeComponent();
            title.Text = "Text box properties";
            FillDocumentCombo(); 
        }

        public new string Text
        {
            get { return title.Text; }
            set { title.Text = value; }
        }

        public string getDocumentName()
        {
            return cbSourceDocuments.Text;
        }

        public string getXPathExpression()
        {
            return textBoxXPath.Text;
        }


        public override event DataSourceChangedDelegate OnDataSourceChanged;
        private void bApply_Click(object sender, EventArgs e)
        {
            OnDataSourceChanged();
        }

        protected override void OnDocumentListChanged(object sender, EventArgs e)
        {
            FillDocumentCombo();
        }

        private void FillDocumentCombo()
        {
            cbSourceDocuments.Items.Clear();
            List<string> docNames = XmlSourceDocumentManager.Instance().GetDocumentNames();
            foreach (string s in docNames)
            {
                cbSourceDocuments.Items.Add(s);
            }
        }

        private void buttonShowXMLTree_Click(object sender, EventArgs e)
        {
            XMLTreeDialog xmlTree = new XMLTreeDialog();
            xmlTree.Document = XmlSourceDocumentManager.Instance().GetDocument( cbSourceDocuments.Text );
            if ( xmlTree.ShowDialog() == DialogResult.OK ) {
                textBoxXPath.Text = xmlTree.Selection;
            }
        }
    }
}
