using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormEditor
{
    public partial class XMLListDataSourcePropertyControl : SingleDataSourcePropertyControl, IListDataSource
    {
        public XMLListDataSourcePropertyControl()
        {
            InitializeComponent();
            title.Text = "Combo properties";
            FillListCombo();
        }

        public string getListDocumentName()
        {
            return cbListSourceDocument.Text;
        }

        public string getListXPathExpression()
        {
            return textBoxList.Text;
        }

        public string getListCaptionXPathExpression()
        {
            return textBoxCaption.Text;
        }

        public string getListValueXPathExpression()
        {
            return textBoxValue.Text; 
        }
        protected override void OnDocumentListChanged(object sender, EventArgs e)
        {
            base.OnDocumentListChanged(sender, e);
            FillListCombo();
        }

        private void FillListCombo()
        {
            cbListSourceDocument.Items.Clear();
            List<string> docNames = XmlSourceDocumentManager.Instance().GetDocumentNames();
            foreach (string s in docNames)
            {
                cbListSourceDocument.Items.Add(s);
            }
        }

        private void bListExpression_Click(object sender, EventArgs e)
        {
            XMLTreeDialog xmlTree = new XMLTreeDialog();
            xmlTree.Document = XmlSourceDocumentManager.Instance().GetDocument(cbListSourceDocument.Text);
            xmlTree.selectNodeByXPath( textBoxList.Text );
            if (xmlTree.ShowDialog() == DialogResult.OK)
            {
                XmlSourceDocumentManager.Instance().SaveDocuments();
                textBoxList.Text = XMLTreeDialog.cutLastBracketIfExists(xmlTree.Selection);
            }
            else
            {
                XmlSourceDocumentManager.Instance().LoadDocuments();
            }
        }

        private void showRelativeXmlTree ( TextBox textBox ) {
            XMLTreeDialog xmlTree = new XMLTreeDialog();
            xmlTree.Document = XmlSourceDocumentManager.Instance().GetDocument(cbListSourceDocument.Text);
            XmlNode node = xmlTree.selectNodeByXPath(textBoxList.Text);
            if (node != null)
            {
                xmlTree.markNode();
                xmlTree.selectNodeByXPath(textBox.Text, node);
            }

            if (xmlTree.ShowDialog() == DialogResult.OK)
            {
                XmlSourceDocumentManager.Instance().SaveDocuments();
                textBox.Text = XMLTreeDialog.cutLastBracketIfExists(xmlTree.Selection);
            }
            else
            {
                XmlSourceDocumentManager.Instance().LoadDocuments();
            }
        }

        private void bCaptionExpression_Click(object sender, EventArgs e)
        {
            showRelativeXmlTree(textBoxCaption);
        }

        private void bValueExpression_Click(object sender, EventArgs e)
        {
            showRelativeXmlTree(textBoxValue);
        }
    }
}

