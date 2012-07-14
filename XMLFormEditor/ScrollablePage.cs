using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class ScrollablePage : UserControl
    {
        public ScrollablePage()
        {
            InitializeComponent();
            hScrollBar.LargeChange = documentForm.Width;
            vScrollBar.LargeChange = documentForm.Height;
        }

        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type != ScrollEventType.EndScroll)
                return;

            documentForm.ViewLocation = new Point(hScrollBar.Value, vScrollBar.Value);
        }

        private void scrollablePage_Resize(object sender, EventArgs e)
        {
            vScrollBar.LargeChange = documentForm.Height;
            hScrollBar.LargeChange = documentForm.Width;
        }


        public DocumentLayout documentLayout
        {
            get { return documentForm.DocumentLayout; }
            set { 
                documentForm.DocumentLayout = value;
                hScrollBar.Minimum = documentForm.DocumentLayout.Size.Left;
                vScrollBar.Minimum = documentForm.DocumentLayout.Size.Top;
                hScrollBar.Maximum = documentForm.DocumentLayout.Size.Right;
                vScrollBar.Maximum = documentForm.DocumentLayout.Size.Bottom;


                documentForm.DocumentLayout.OnSizeChanged += delegate(object sender)
                {
//                    hScrollBar.Minimum = documentForm.DocumentLayout.Size.Left - documentForm.Width / 2;
//                    vScrollBar.Minimum = documentForm.DocumentLayout.Size.Top - documentForm.Height / 2; ;
                    hScrollBar.Minimum = documentForm.DocumentLayout.Size.Left;
                    vScrollBar.Minimum = documentForm.DocumentLayout.Size.Top;

                    hScrollBar.Maximum = documentForm.DocumentLayout.Size.Right + documentForm.Width / 2;
                    vScrollBar.Maximum = documentForm.DocumentLayout.Size.Bottom + documentForm.Height / 2;
                };
            }
        }

        public DocumentFormVisualizer documentFormVisualizer
        {
            get { return documentForm; }
        }

    }
}
