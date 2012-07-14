using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using XMLFormEditor;

namespace XMLFormEditor
{
    public delegate void DataSourceChangedDelegate();

    public partial class XMLPropertyControlBase : UserControl, IDataSourceBase
    {
        public virtual event DataSourceChangedDelegate OnDataSourceChanged;
            
        protected virtual void OnDocumentListChanged(object sender, EventArgs e)
        { 
        
        }

        public XMLPropertyControlBase()
        {
            InitializeComponent();
            XmlSourceDocumentManager.Instance().OnDocumentListChanged += OnDocumentListChanged;
        }

    }
}
