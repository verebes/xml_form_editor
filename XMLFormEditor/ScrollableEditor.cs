using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class ScrollableEditor : UserControl
    {
        public ScrollableEditor()
        {
            InitializeComponent();
            hScrollBar.LargeChange = documentEditor.Width;
            vScrollBar.LargeChange = documentEditor.Height;


            this.documentEditor.MouseWheel += new MouseEventHandler(documentEditor_MouseWheel);
        }


        void documentEditor_MouseWheel(object sender, MouseEventArgs e)
        {
            ScrollBar scrollBar;

            //querying whether ctrl key is pressed
            int keyState = WindowsApiMethods.GetKeyState(0x11);
            if ( (keyState & 0x0100) == 0x0100)
            {
                scrollBar = hScrollBar;
            } else {
                scrollBar = vScrollBar;
            }
            
            int newVal = scrollBar.Value - e.Delta * SystemInformation.MouseWheelScrollLines / 10;
            if (newVal >= scrollBar.Minimum && newVal <= scrollBar.Maximum)
                scrollBar.Value = newVal;

        }
        


        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type != ScrollEventType.EndScroll)
                return;
            Rectangle docSize = documentEditor.DocumentLayout.Size;
            documentEditor.ViewLocation = new Point( docSize.Left + hScrollBar.Value,  docSize.Top + vScrollBar.Value);
        }


        private void scrollBar_ValueChanged(object sender, EventArgs e)
        {
            documentEditor.ViewLocation = new Point(hScrollBar.Value, vScrollBar.Value);
        }


        private static int margin = 20;
        protected override void OnResize(EventArgs e)
        {                       
            documentEditor.ClientSize = new Size(Width - vScrollBar.Width, Height - hScrollBar.Height);            
            documentEditor.Left = 0;
            documentEditor.Top = 0;

            hScrollBar.Width = documentEditor.Width;
            hScrollBar.Top = documentEditor.Height +1;
            hScrollBar.Left = 0;
           
            vScrollBar.Height = documentEditor.Height;
            vScrollBar.Left = documentEditor.Width +1 ;
            vScrollBar.Top = 0;


            vScrollBar.LargeChange = documentEditor.Height;
            hScrollBar.LargeChange = documentEditor.Width;

            if (documentEditor.DocumentLayout != null) {
                Rectangle docSize = documentEditor.DocumentLayout.Size;

                if (vScrollBar.Maximum < documentEditor.Height) {
                    vScrollBar.Value = 0;
                }

                if (hScrollBar.Maximum < documentEditor.Width) {                    
                    hScrollBar.Value = 0;
                }

                documentEditor.ViewLocation = new Point(docSize.Left + hScrollBar.Value, docSize.Top + vScrollBar.Value);
            }

            base.OnResize(e);
        }



        public DocumentLayout documentLayout
        {
            get { return documentEditor.DocumentLayout; }
            set {                
                documentEditor.DocumentLayout = value;
                Rectangle docSize = documentEditor.DocumentLayout.Size;
                hScrollBar.Minimum = 0;
                vScrollBar.Minimum = 0;
                hScrollBar.Maximum = docSize.Width + margin;
                vScrollBar.Maximum = docSize.Height + margin;

                documentEditor.ViewLocation = new Point(docSize.Left + hScrollBar.Value, docSize.Top + vScrollBar.Value);                

                documentEditor.DocumentLayout.OnSizeChanged += delegate(object sender)
                {
                        Rectangle size = documentEditor.DocumentLayout.Size;
                        hScrollBar.Minimum = 0;
                        vScrollBar.Minimum = 0;
                        hScrollBar.Maximum = size.Width + margin;
                        vScrollBar.Maximum = size.Height + margin;

                        documentEditor.ViewLocation = new Point(size.Left + hScrollBar.Value, size.Top + vScrollBar.Value);
                };
            }
        }

        public DocumentEditorVisualizer documentEditorVisualizer
        {
            get { return documentEditor; }
        }
    }
}
