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
    }
}

