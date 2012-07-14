using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class InputTextDialog : Form
    {
        public InputTextDialog()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return textBox1.Text;  }
            set { textBox1.Text = value; }
        }

    }
}