using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class StaticLabelPropertyControl : XMLFormEditor.XMLPropertyControlBase, IStaticLabelDataSource
    {
        public StaticLabelPropertyControl()
        {
            InitializeComponent();
        }

        public override event DataSourceChangedDelegate OnDataSourceChanged;
        private void button1_Click(object sender, EventArgs e)
        {
            OnDataSourceChanged();
        }

        public string getCaption()
        {
            return textBoxCaption.Text;
        }
    }
}

