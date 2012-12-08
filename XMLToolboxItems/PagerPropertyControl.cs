using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class PagerPropertyControl : SingleDataSourcePropertyControl, IPagerDataSource
    {
        public PagerPropertyControl()
        {
            InitializeComponent();
            title.Text = "Pager properties";
            FillListCombo();
        }

        public string getPagerDocumentName()
        {
            return cbPageCountDocument.Text;
        }

        public string getPagerXPathExpression()
        {
            return textBoxPageCountXPath.Text;
        }

        protected override void OnDocumentListChanged(object sender, EventArgs e)
        {
            base.OnDocumentListChanged(sender, e);
            FillListCombo();
        }

        private void FillListCombo()
        {
            cbPageCountDocument.Items.Clear();
            List<string> docNames = XmlSourceDocumentManager.Instance().GetDocumentNames();
            foreach (string s in docNames)
            {
                cbPageCountDocument.Items.Add(s);
            }
        }

        private string between( string before, string str, string after )
        {
            int bl = before.Length;
            int al = after.Length;
            int sl = str.Length;

            
            if ( sl > (bl + al) && 
                str.Substring(0, bl).ToLower() == before.ToLower() && 
                str.Substring(sl - al, al).ToLower() == after.ToLower())
            {
                return str.Substring(bl, sl - (bl + al));                
            }
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XMLTreeDialog xmlTree = new XMLTreeDialog();
            xmlTree.Document = XmlSourceDocumentManager.Instance().GetDocument(cbSourceDocuments.Text);

            string xpath = between("count(", textBoxPageCountXPath.Text.Trim(), ")");
            if (xpath == "") {
                xpath = textBoxPageCountXPath.Text;
            }

            xmlTree.selectNodeByXPath(xpath);
            if (xmlTree.ShowDialog() == DialogResult.OK)
            {
                XmlSourceDocumentManager.Instance().SaveDocuments();
                string selectedPath = xmlTree.Selection;
                int lastBracket = selectedPath.LastIndexOf('[');
                int lastSlash = selectedPath.LastIndexOf('/');
                if ( lastBracket > lastSlash) {
                    selectedPath = selectedPath.Substring(0, lastBracket);
                }

                textBoxPageCountXPath.Text = "count(" + selectedPath + ")";                
            }
            else
            {
                XmlSourceDocumentManager.Instance().LoadDocuments();
            }
        }
    }
}

