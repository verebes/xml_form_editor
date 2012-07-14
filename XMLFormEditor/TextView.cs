using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Schema;


namespace XMLFormEditor
{
    public partial class TextView : Form
    {
        public TextView()
        {
            InitializeComponent();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ReadOnly)
            {
                richTextBox1.BackColor = SystemColors.Window;
                richTextBox1.ReadOnly = false;
                buttonSave.Enabled = false;
                buttonEdit.Text = "&Ok";
            }
            else
            {                
                try
                {
                    ValidateXml();
                    richTextBox1.BackColor = SystemColors.Control;
                    richTextBox1.ReadOnly = true;
                    buttonSave.Enabled = true;
                    buttonEdit.Text = "&Edit";
                }
                catch (XmlException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void ValidateXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(richTextBox1.Text);
        }
    }
}