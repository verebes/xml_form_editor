using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class XMLEditorPropertyControl : XMLPropertyControlBase
    {
        public XMLEditorPropertyControl()
        {
            InitializeComponent();
            title.Text = "Page properties";
        }

        public override event DataSourceChangedDelegate OnDataSourceChanged;
        private void button1_Click(object sender, EventArgs e)
        {
            OnDataSourceChanged();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            errorProvider1.Clear();            
            try 
            {
                errorProvider1.SetError(textBoxLeft, "Left should be a number");
                Convert.ToInt32(textBoxLeft.Text);
                errorProvider1.SetError(textBoxLeft, "");
                
                errorProvider1.SetError(textBoxTop, "Top should be a number");
                Convert.ToInt32(textBoxTop.Text);
                errorProvider1.SetError(textBoxTop, "");

                errorProvider1.SetError(textBoxRight, "Right should be a number");
                Convert.ToInt32(textBoxRight.Text);
                errorProvider1.SetError(textBoxRight, "");

                errorProvider1.SetError(textBoxBottom, "Bottom should be a number");
                Convert.ToInt32(textBoxBottom.Text);
                errorProvider1.SetError(textBoxBottom, "");

            }
            catch ( Exception)
            {                            
                e.Cancel = true;
            }
        }

        private void bFileSelect_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select background image";
            openFileDialog.Filter = "Images (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.AddExtension = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;


            try {
                Bitmap bitmap = new Bitmap(openFileDialog.FileName);
                textBoxLeft.Text = "0";
                textBoxTop.Text = "0";
                textBoxRight.Text = bitmap.Width.ToString();
                textBoxBottom.Text = bitmap.Height.ToString();
            } catch (System.Exception) {
                return;
            }

            textBoxBackgroundImage.Text = openFileDialog.FileName;            
        }
    }
}
