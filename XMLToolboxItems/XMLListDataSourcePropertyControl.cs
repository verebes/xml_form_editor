using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
    }
}

