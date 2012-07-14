using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            
        }

        private void textBoxGridSize_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            errorProvider1.Clear();
            try
            {
                errorProvider1.SetError(textBoxGridSize, "Grid size should be a number graiter or equal to 10");
                if (Convert.ToInt32(textBoxGridSize.Text) < 10)
                    throw new Exception("Grid size must be graiter or equal to 10");

                    errorProvider1.SetError(textBoxGridSize, "");

            }
            catch (Exception)
            {
                e.Cancel = true;
            }
        }
    }
}