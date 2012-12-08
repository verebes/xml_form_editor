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

        public Font getFont()
        {
            return textBoxCaption.Font;
        }

        public ContentAlignment getAlignment()
        {
            return _alignment;
        }

        public Color getColor()
        {
            return textBoxCaption.ForeColor;
        }

        public Color getBackground()
        {
            return textBoxCaption.BackColor;
        }


        private void panel1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (  dlg.ShowDialog() == DialogResult.OK ) {
                if (sender == panelColor)
                {
                    panelColor.BackColor = dlg.Color;
                    textBoxCaption.ForeColor = dlg.Color;
                }

                if (sender == panelBackground)
                {
                    panelBackground.BackColor = dlg.Color;
                    textBoxCaption.BackColor = dlg.Color;
                }
            }
        }

        private void bFontSelect_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();           
            if ( dlg.ShowDialog() == DialogResult.OK ) {
                textBoxCaption.Font = dlg.Font;
                numFontSize.Value = (Decimal)dlg.Font.SizeInPoints;
            }
        }

        public ContentAlignment _alignment;
        private void rbTop_CheckedChanged(object sender, EventArgs e)
        {
            _alignment = ContentAlignment.MiddleLeft;

            if (rbLeft.Checked) {
                if ( rbTop.Checked )
                    _alignment = ContentAlignment.TopLeft;
                
                if (rbMiddle.Checked)
                    _alignment = ContentAlignment.MiddleLeft;
                
                if (rbBottom.Checked)
                    _alignment = ContentAlignment.BottomLeft;
            }

            if (rbCenter.Checked)
            {
                if (rbTop.Checked)
                    _alignment = ContentAlignment.TopCenter;

                if (rbMiddle.Checked)
                    _alignment = ContentAlignment.MiddleCenter;

                if (rbBottom.Checked)
                    _alignment = ContentAlignment.BottomCenter;
            }

            if (rbRight.Checked)
            {
                if (rbTop.Checked)
                    _alignment = ContentAlignment.TopRight;

                if (rbMiddle.Checked)
                    _alignment = ContentAlignment.MiddleRight;

                if (rbBottom.Checked)
                    _alignment = ContentAlignment.BottomRight;
            }            
        }
    }
}

