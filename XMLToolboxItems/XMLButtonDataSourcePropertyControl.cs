using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class XMLButtonDataSourcePropertyControl : XMLFormEditor.SingleDataSourcePropertyControl, IButtonDataSource
    {
        public XMLButtonDataSourcePropertyControl()
        {
            InitializeComponent();
            title.Text = "Button properties";
        }

        public string getInsertText()
        {
            return richTextBoxInsertXml.Text;
        }
    }
}

