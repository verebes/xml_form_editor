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
            numFontSize.Value = (Decimal)textBoxCaption.Font.SizeInPoints;
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



        public void setFont(Font font)
        {
            if ( font != null)
                textBoxCaption.Font = font;
            numFontSize.Value = (Decimal)textBoxCaption.Font.SizeInPoints;
        }        

        public void setColor ( Color color ) {
            textBoxCaption.ForeColor = color;
            panelColor.BackColor = color;
        }

        public void setBackground( Color background ) 
        {
            textBoxCaption.BackColor = background;
            panelBackground.BackColor = background;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            Panel panel = sender as Panel;
            if (panel == null)
                return;

            dlg.Color = panel.BackColor;
            if (  dlg.ShowDialog() == DialogResult.OK ) {
                if (sender == panelColor) {
                    setColor(dlg.Color);
                }

                if (sender == panelBackground) {
                    setBackground(dlg.Color);
                }
            }
        }

        private void bFontSelect_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.Font = textBoxCaption.Font;
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

        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {            
            Font f = new Font(textBoxCaption.Font.FontFamily, (float)numFontSize.Value, GraphicsUnit.Point);
            setFont(f);
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        protected void reset()
        {
            _alignment = ContentAlignment.MiddleLeft;
            setColor(SystemColors.WindowText);
            setBackground(SystemColors.Window);
            setFont(SystemFonts.DefaultFont);
        }
    }
}

