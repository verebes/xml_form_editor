using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class SchemaPropertyControl : SingleDataSourcePropertyControl, ISchemaControlDataSource
    {
        public SchemaPropertyControl()
        {
            InitializeComponent();
            title.Text = "SchemaControl properties";
            FillListCombo();
        }

        public string getSchemaName()
        {
            return cbSchemaName.Text;
        }

        protected override void OnDocumentListChanged(object sender, EventArgs e)
        {
            base.OnDocumentListChanged(sender, e);
            FillListCombo();
        }

        private void FillListCombo()
        {
            cbSchemaName.Items.Clear();
            List<string> docNames = XmlSourceDocumentManager.Instance().GetDocumentNames();
            foreach (string s in docNames)
            {
                cbSchemaName.Items.Add(s);
            }
        }
    }
}

