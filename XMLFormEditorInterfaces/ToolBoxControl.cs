using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class XmlEditorToolBox : UserControl
    {

        private int _itemHeight = 45;
        private System.Collections.Generic.List<IToolBoxItem> items = new List<IToolBoxItem>();        

        private IToolBoxItem _dragItem;
        public XmlEditorToolBox()
        {
            _itemHeight = 45;

            DoubleBuffered = false;
            InitializeComponent();

            vScrollBar1.LargeChange = (Height / _itemHeight);
            vScrollBar1.Maximum = 0;
        }

        private DocumentEditorVisualizer _editor;
        public DocumentEditorVisualizer Editor
        {
            get { return _editor; }
            set {_editor = value; }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
         
        }

        protected override void  OnPaint(PaintEventArgs e)
        {

            Bitmap tmpBmp = new Bitmap(Width, Height, e.Graphics);            
            Graphics tmpGr = Graphics.FromImage(tmpBmp);
            tmpGr.Clear(BackColor);

            int itemWidth = this.Width - vScrollBar1.Width -1;
            Rectangle itemRect = new Rectangle(0,0, itemWidth , _itemHeight);

            int first = vScrollBar1.Value;
            int last = Math.Min(first + Height / _itemHeight + 1 , items.Count-1);

            for (int i = first; i <= last; i++ )
            {
                e.Graphics.DrawRectangle(System.Drawing.Pens.Black, itemRect);

                Bitmap itemBmp = new Bitmap(itemWidth, _itemHeight, e.Graphics);
                Graphics gr = Graphics.FromImage(itemBmp);
                items[i].DrawItem(gr, itemBmp.Size);

                tmpGr.DrawImageUnscaled(itemBmp, itemRect);


                itemRect.Offset(0, _itemHeight);
            }
            
            e.Graphics.DrawImageUnscaled(tmpBmp,0,0);
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            startDrag( new Point(e.X, e.Y));
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
 	        base.OnMouseCaptureChanged(e);
            endDrag( Cursor.Position );
        }


        private void startDrag( Point cursorPos )
        {            
            IToolBoxItem item = getItemAtPos( cursorPos);
            if (item == null)
            {
                return;
            }

            if (Editor == null)
                return;

            _dragItem = item;
            Cursor = item.getToolBoxCursor();           
        }

        private void endDrag( Point cursorPos )
        {
            if (_dragItem == null)
                return;

            if (Editor == null)
            {
                System.Diagnostics.Trace.WriteLine("XMLFormEditor::endDrag: editor is null");
                return;
            }


            Rectangle r = Editor.DisplayRectangle;
            Point editorLocation = Editor.PointToScreen(new Point(0, 0));
            r.Offset( editorLocation);            
            if (r.Contains(cursorPos)) 
            {                
                cursorPos.Offset( -1 * editorLocation.X, -1 * editorLocation.Y  );
                Editor.DropToolBoxItem(_dragItem, cursorPos);
            }

            _dragItem = null;

            Cursor = Cursors.Default;
        }

        private IToolBoxItem getItemAtPos ( Point cursorPos ) {
            int itemIndex = cursorPos.Y / _itemHeight + vScrollBar1.Value;
            if (itemIndex < items.Count)
            {                
                return items[itemIndex];
            }
            else 
            {
                return null;
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }


        private void vScrollBar1_Resize(object sender, EventArgs e)
        {
            int nrOfItems = (Height / _itemHeight);
            int spaceLeft = nrOfItems - (items.Count - vScrollBar1.Value -1);
            
            vScrollBar1.LargeChange = nrOfItems;
            vScrollBar1.Value = Math.Max(0, vScrollBar1.Value - spaceLeft);    

            Invalidate();
        }


        public void AddItem(IToolBoxItem toolBoxItem)
        {
            items.Add(toolBoxItem);
            vScrollBar1.Maximum = items.Count -1;
        }
    }
}
