using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace XMLFormEditor
{
    public class DocumentEditorOverlay : UserControl
    {
        private bool _selecting = false;
        private bool _movingControls = false;
        private bool _resizingControls = false;
        private HandlerType _resizeMode = HandlerType.None;
        private bool _moved = false; // moved since last 'mousedown' event
        private Point _dragStartPos;

        private Timer _longClickTimer = new Timer();

        JunctionSelector junctionSelector = new JunctionSelector();

        private DocumentLayout _documentLayout;
        public DocumentLayout DocumentLayout
        {
            get { return _documentLayout; }
            set { _documentLayout = value; }
        }

        private DocumentEditorVisualizer _documentEditor = null;
        public DocumentEditorVisualizer DocumentEditor
        {
            get { return _documentEditor; }
            set { _documentEditor = value; }
        }

        public DocumentEditorOverlay()
        {
            InitializeComponent();

            this.TabStop = true;
            SetStyle(ControlStyles.Selectable, true);
            _selecting = false;
            _movingControls = false;
            _resizingControls = false;
            _moved = false;
            junctionSelector.OnJunctionTypeSelected += new EventHandler(junctionSelector_OnJunctionTypeSelected);
            _longClickTimer.Tick += new EventHandler(_longClickTimer_Tick);
        }

        void _longClickTimer_Tick(object sender, EventArgs e)
        {
            _longClickTimer.Stop();
            MouseDownToAddJunction(lastMousePressEventArg);
            Invalidate();
        }

        void junctionSelector_OnJunctionTypeSelected(object sender, EventArgs e) {

            Point p = junctionSelector.SelectedJunction.position;
            
            LineDrawer.Junction j = new LineDrawer.Junction(junctionSelector.SelectedJunction.type, p);

            if (j.type == LineDrawer.Junction.Type.Invalid)
            {
                DocumentLayout.LineDrawer.RemoveJunction(j);
            } else {
                DocumentLayout.LineDrawer.AddJunction(j);
            }
            _documentEditor.UpdateSectionListNeeded();

        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Name = "DocumentEditorOverlay";
            this.AllowDrop = true;
        }
        #endregion


        MouseEventArgs lastMousePressEventArg = null;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Trace.WriteLine("DocumentEditorOverlay::OnMouseDown");
            base.OnMouseDown(e);

            if ( ModifierKeys == (Keys.Alt | Keys.Control)) {
                MouseDownToAddJunction(e);
                Invalidate();
                return;
            }

            HandlerType ht = _documentLayout.getResizeHandlerType(ViewPoint2LalyoutPoint(e.Location));
            if (ht != HandlerType.None)
                MouseDownOnHandler(e, ht);
            else if (_documentLayout.Controls(ViewPoint2LalyoutPoint(e.Location)).Count == 0)
                MouseDownOnBackground(e);
            else
                MouseDownOnControls(e);

            Invalidate();
        }

        private Point ViewPoint2LalyoutPoint(Point viewPoint)
        {
            Point ret = new Point(viewPoint.X, viewPoint.Y);
            ret.Offset(_documentEditor.ViewLocation);
            return ret;
        }

        private Point LayoutPoint2ViewPoint(Point layoutPoint)
        {
            Point ret = new Point(layoutPoint.X, layoutPoint.Y);
            ret.Offset(-1 * _documentEditor.ViewLocation.X, -1 * _documentEditor.ViewLocation.Y);
            return ret;
        }



        private void MouseDownToAddJunction(MouseEventArgs e)
        {

            //junctionSelector.SelectedJunction.position = new Point(x, y);
            Point p = ViewPoint2LalyoutPoint(new Point(e.Location.X, e.Location.Y));

            if (_documentEditor.SnapToGrid) {
                int gs = _documentEditor.GridSize;
                p.X = ((p.X + gs / 2) / gs) * gs;
                p.Y = ((p.Y + gs / 2) / gs) * gs;
            }

            junctionSelector.SelectedJunction.position = p;
            junctionSelector.showSelector(PointToScreen(e.Location));
        }


        private List<Rectangle> positionList = new List<Rectangle>();

        private void MouseDownOnHandler(MouseEventArgs e, HandlerType handlerType)
        {
            Trace.WriteLine("DocumentEditorOverlay::MouseDownOnHandler");

            List<XMLControl> selectedControls = _documentLayout.SelectedControls(); 
            positionList.Clear();
            foreach (XMLControl control in selectedControls) {
                positionList.Add(new Rectangle(control.ClientRect.Location, control.ClientRect.Size));
            }


            _resizingControls = true;
            _resizeMode = handlerType;
            _moved = false;
            _dragStartPos = ViewPoint2LalyoutPoint(e.Location);
        }
        
        private void MouseDownOnControls(MouseEventArgs e)
        {
            Trace.WriteLine("DocumentEditorOverlay::MouseDownOnControls");
            List<XMLControl> selectedControls = null;
            if (_documentLayout.SelectedControls(ViewPoint2LalyoutPoint(e.Location)).Count == 0 || ModifierKeys == Keys.Control) {
                if (ModifierKeys == Keys.Control) {
                    _documentLayout.ToggleControlSelections(ViewPoint2LalyoutPoint(e.Location));
                } else {
                    _documentLayout.ClearSelection();
                    _documentLayout.SelectControls(ViewPoint2LalyoutPoint(e.Location));
                }


                selectedControls = _documentLayout.SelectedControls();
                if (selectedControls.Count == 1)
                    _documentEditor.CreateControlPropertyWindow(selectedControls[0]);
                else
                    _documentEditor.DestroyPropertyWindow();
            } else {
                selectedControls = _documentLayout.SelectedControls();
            }

            positionList.Clear();

            foreach (XMLControl control in selectedControls) {
                positionList.Add(new Rectangle(control.ClientRect.Location, control.ClientRect.Size));
            }

            _movingControls = true;
            _moved = false;
            
            _dragStartPos = ViewPoint2LalyoutPoint(e.Location);
        }

        private void MouseDownOnBackground(MouseEventArgs e)
        {
            Trace.WriteLine("DocumentEditorOverlay::MouseDownOnBackground");
            if (ModifierKeys != Keys.Control)
                _documentLayout.ClearSelection();            

            _dragStartPos = ViewPoint2LalyoutPoint(e.Location);
            _selecting = true;
            _moved = false;

            lastMousePressEventArg = e;
            _longClickTimer.Interval = 500;
            _longClickTimer.Start();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Trace.WriteLine("DocumentEditorOverlay::OnMouseUp");
            base.OnMouseUp(e);
            _documentEditor.RefreshOverlay();


            junctionSelector.Hide();
            _longClickTimer.Stop();

            // this was in because of clicking when more than one control is selected and we click on an allready selecet control
            // in this case the selection should disappear from the other controls

            //if ( !_moved ) 
            //{
            //    if (ModifierKeys != Keys.Control)
            //        _documentLayout.ClearSelection();

            //    _documentLayout.SelectControls( ViewPoint2LalyoutPoint(e.Location));

            //}

            //_moved = false;
            //Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (lastMousePressEventArg != null)
            {
                int dx = e.Location.X - lastMousePressEventArg.Location.X;
                int dy = e.Location.Y - lastMousePressEventArg.Location.Y;
                if (dx * dx + dy * dy > 25)
                    _longClickTimer.Stop();
            }


            if (!_selecting && !_movingControls && !_resizingControls)
            {
                UpdateCurrentCursor(e);
                return;
            }

            if (_movingControls)                
                MovingControls(e);

            if (_resizingControls)
                ResizingControls(e);            

            _documentEditor.RefreshOverlay();
            Invalidate();
        }

        private void UpdateCurrentCursor(MouseEventArgs e)
        {
            HandlerType handleType = _documentLayout.getResizeHandlerType(ViewPoint2LalyoutPoint(e.Location));
            if (handleType != HandlerType.None)
                Cursor = ResizeTool.getCursor(handleType);
            else if (_documentLayout.Controls(ViewPoint2LalyoutPoint(e.Location)).Count != 0)
                Cursor = Cursors.SizeAll;
            else
                Cursor = Cursors.Arrow;
        }

        private void ResizingControls(MouseEventArgs e)
        {
            if (e.X != _dragStartPos.X || e.Y != _dragStartPos.Y)
            {
                _moved = true;
            }

            int deltaX = e.X - _dragStartPos.X;
            int deltaY = e.Y - _dragStartPos.Y;

            List<Rectangle>.Enumerator it = positionList.GetEnumerator();
            foreach (XMLControl control in _documentLayout.SelectedControls())
            {
                it.MoveNext();

                int dx = deltaX;
                int dy = deltaY;

                if (control.ResizeMode == ResizeMode.None || control.ResizeMode == ResizeMode.Horizontal)
                    dy = 0;

                if (control.ResizeMode == ResizeMode.None || control.ResizeMode == ResizeMode.Vertical)
                    dx = 0;


                Rectangle rect = new Rectangle(it.Current.Location, it.Current.Size);
                int gs = _documentEditor.GridSize;

                if (!_documentEditor.SnapToGrid)
                    gs = 1;

                ResizeTool.ResizeRect(ref rect, _resizeMode, dx, dy,gs);

                control.ClientRect = rect;
            }
        }


        private void MovingControls(MouseEventArgs e)
        {
            Point p = ViewPoint2LalyoutPoint(e.Location);

            if (! _moved && p.X == _dragStartPos.X && p.Y == _dragStartPos.Y)
            {
                return;
            }
            _moved = true;

            int deltaX = p.X - _dragStartPos.X;
            int deltaY = p.Y - _dragStartPos.Y;

            List<Rectangle>.Enumerator it = positionList.GetEnumerator();
            foreach (XMLControl control in _documentLayout.SelectedControls())
            {
                it.MoveNext();                
                Point position = control.ClientRect.Location;

                Point newPos;
               
                if (_documentEditor.SnapToGrid)
                {
                    int gs = _documentEditor.GridSize;
                    newPos = new Point( ((it.Current.Location.X + deltaX) / gs) * gs, ((it.Current.Location.Y + deltaY) / gs) * gs );
                }
                else
                {
                    newPos = new Point(it.Current.Location.X + deltaX, it.Current.Location.Y + deltaY);    
                }
                System.Diagnostics.Trace.WriteLine("pos (2):" + position.X.ToString() + " ; " + position.Y.ToString());

                control.MoveToAbsolutePos(newPos);
            }
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            if (!(_selecting || _movingControls || _resizingControls))
                return;


            if (_movingControls || _resizingControls)
            {
                _movingControls = false;
                _resizingControls = false;

                if (_moved)
                {
                    Trace.WriteLine("DocumentEditorOverlay::OnMouseCaptureChanged: Calling recreateControls");
                    _documentEditor.recreateControls();
                    _moved = false;
                }
            }

            if (_selecting)
            {
                _selecting = false;

                if (ModifierKeys != Keys.Control)
                    _documentLayout.ClearSelection();

                if (ModifierKeys == Keys.Alt) {
                    Rectangle cr = getCurrentSelectionRectangle();
                    Rectangle r = new Rectangle(cr.Location, cr.Size);
                    if ( DocumentEditor.SnapToGrid ) {
                        int gs = DocumentEditor.GridSize;
                        r.X = ((r.X + gs / 2) / gs) * gs;
                        r.Y = ((r.Y + gs / 2) / gs) * gs;
                        r.Width = ((r.Width + gs / 2) / gs) * gs;
                        r.Height = ((r.Height + gs / 2) / gs) * gs;
                    }                    
                    DocumentLayout.LineDrawer.AddRectange(r);
                    _documentEditor.UpdateSectionListNeeded();                    
                    return;
                }

                _documentLayout.SelectControls(getCurrentSelectionRectangle());

                if (_documentLayout.SelectedControls().Count > 1)
                {
                    _documentEditor.DestroyPropertyWindow();
                }
                else if (_documentLayout.SelectedControls().Count == 0)
                {
                    _documentEditor.CreateEditorPropertyWindow();
                }
            }

            Invalidate();
        }

        private Rectangle getCurrentSelectionRectangle()
        {
            Point endPos = Cursor.Position;
            endPos = PointToClient(endPos);

            Point layoutStartPos = _dragStartPos;
            Point layoutEndPos = ViewPoint2LalyoutPoint(endPos);

            int x = Math.Min(layoutStartPos.X, layoutEndPos.X);
            int y = Math.Min(layoutStartPos.Y, layoutEndPos.Y);
            int width = Math.Abs(layoutStartPos.X - layoutEndPos.X);
            int height = Math.Abs(layoutStartPos.Y - layoutEndPos.Y);

            return new Rectangle(x, y, width, height);
        }

        protected void PaintSelection(Graphics g)
        {
            if (_documentLayout == null)
                return;

            Pen selecttionPen = new Pen(Color.Red, 1);
            selecttionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            int cntSelected = 0;

            foreach (XMLControl control in _documentLayout.SelectedControls(_documentEditor.ViewRectangle))
            {
                cntSelected++;
                Rectangle rect = control.ClientRect;
                rect.Location = LayoutPoint2ViewPoint(rect.Location);
                rect.Width += 6;
                rect.Height += 6;
                rect.X -= 3;
                rect.Y -= 3;

                g.DrawRectangle(selecttionPen, rect);
                if (!_moved)
                    PaintResizeHandler(g, rect, control.ResizeMode);
            }
        }

        protected void PaintResizeHandler(Graphics g, Rectangle controlRect, ResizeMode resizeMode)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush, 1);
            SolidBrush brushWhite = new SolidBrush(Color.White);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (!(i == 1 && j == 1) && (resizeMode == ResizeMode.Both || (resizeMode == ResizeMode.Horizontal && j == 1) || (resizeMode == ResizeMode.Vertical && i == 1)))
                    {
                        int x = (controlRect.Width / 2) * i;
                        int y = (controlRect.Height / 2) * j;
                        Rectangle r = new Rectangle(controlRect.Left + x - 2, controlRect.Top + y - 2, 5, 5);
                        g.FillRectangle(brushWhite, r);
                        g.DrawRectangle(pen, r);
                    }
                }

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //            base.OnPaintBackground(e);           
        }


        public bool _storingBmp = false;
        protected Bitmap _storedBmp;
        public void StoreBmp(Rectangle r)
        {
            // return if window is minimized
            if (Parent.ClientRectangle.Width <= 0 || Parent.ClientRectangle.Height <= 0)
                return;

            System.Diagnostics.Debug.WriteLine("StoreBmp: " + DateTime.Now + " (" + System.DateTime.Now.Millisecond.ToString() + ")" + ": " + r.ToString());

            _storingBmp = true;
            if (_storedBmp == null || _storedBmp.Width != Parent.Width || _storedBmp.Height != Parent.Height)
            {
                _storedBmp = new Bitmap(Parent.Width, Parent.Height);
                Parent.DrawToBitmap(_storedBmp, Parent.ClientRectangle);
                System.Diagnostics.Debug.WriteLine("StoreBmp: A");
            }
            else
            {
                Parent.DrawToBitmap(_storedBmp, r);
                System.Diagnostics.Debug.WriteLine("StoreBmp: B");
            }

            System.Diagnostics.Debug.WriteLine("StoreBmp (end): " + DateTime.Now + " (" + System.DateTime.Now.Millisecond.ToString() + ")" + ": " + r.ToString());

            _storingBmp = false;
        }

        public void StoreBmp()
        {
            System.Diagnostics.Trace.WriteLine("StoreBmp() was called. Parent.ClientRectangle: " + Parent.ClientRectangle.ToString());
            StoreBmp(Parent.ClientRectangle);
        }

        private int tmpCount = 0;

        private Bitmap tmpBmp = null;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_storingBmp)
                return;

            if (_storedBmp == null)
                return;

            if (e.ClipRectangle.Width == 0 || e.ClipRectangle.Height == 0)
                return;

            System.Diagnostics.Debug.WriteLine("overlay: " + DateTime.Now + " (" + System.DateTime.Now.Millisecond.ToString() + ")" + " clipRect: " + e.ClipRectangle.ToString() + " client" + ClientRectangle.ToString());

            if ( tmpBmp == null || tmpBmp.Size != _storedBmp.Size ) {
                tmpBmp = new Bitmap(_storedBmp);

            } else {
                Graphics g2 = Graphics.FromImage(tmpBmp);
                g2.DrawImageUnscaled(_storedBmp, 0,0);
                g2.Dispose();
            }
            
            Graphics g = Graphics.FromImage(tmpBmp);            

            if (Focused)
                g.DrawRectangle(Pens.Black, 1,1,Width-2, Height-2);


            if (_selecting)
            {
                Rectangle layoutRect = getCurrentSelectionRectangle();
                layoutRect.Location = LayoutPoint2ViewPoint(layoutRect.Location);
                g.DrawRectangle(Pens.Black, layoutRect);
            }

            PaintSelection(g);
            g.Flush();
            e.Graphics.DrawImageUnscaled(tmpBmp, 0, 0);
            g.Dispose();

            ++tmpCount;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Shift)
            {
                _documentEditor.RemoveSelectedControls();
                Invalidate();
            }
            else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                _documentEditor.DocumentLayout.SelectAllControls();
                Invalidate();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                _documentEditor.DocumentLayout.ClearSelection();
                Invalidate();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }

        protected override bool ProcessDialogKey(Keys keyData) {
            if (keyData == Keys.Left ||
                    keyData == Keys.Right ||
                    keyData == Keys.Up ||
                    keyData == Keys.Down ||
                    keyData == Keys.End ||
                    keyData == Keys.Home) {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
