using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using System.Diagnostics;

namespace XMLFormEditor
{
    public class DocumentEditorVisualizer : DocumentVisualizer, IUpdatableWindow
    {
        protected DocumentEditorOverlay _editorOverlay;
        protected XMLPropertyControlBase _currentPropertyWindow;
        protected Control _propertyWindowPlace;

        public Control PropertyWindowPlace
        {
            get { return _propertyWindowPlace; }
            set { _propertyWindowPlace = value; }
        }

        public bool PropertyWindowVisible
        {
            get { return _currentPropertyWindow != null && _currentPropertyWindow.Visible; }
            set {
                if (_currentPropertyWindow != null)
                    _currentPropertyWindow.Visible = value;
            }
        }

        public override Rectangle ViewRectangle
        {
            get { return base.ViewRectangle; }
            set {
                base.ViewRectangle = value;
                if (_editorOverlay != null)
                    _editorOverlay.Invalidate();
            }
        }

        private bool snapToGrid = true;
        public bool SnapToGrid
        {
            get { return snapToGrid; }
            set { snapToGrid = value; }
        }


        public override bool DrawGrid {
            set {
                base.DrawGrid = value;
                
            }
        }


        protected override void OnResize(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnResize: " + ClientRectangle);

            base.OnResize(e);
            storeNeeded = true;
            RefreshOverlay();
            
        }

        public override DocumentLayout DocumentLayout
        {
            set
            {
                _editorOverlay.DocumentLayout = value;
                base.DocumentLayout = value;

                Trace.WriteLine("DocumentEditor::DocumentLayout: Calling recreateControls");
                recreateControls();
            }
        }


        public DocumentEditorVisualizer()
        {
            _editorOverlay  = new DocumentEditorOverlay();
            _editorOverlay.DocumentEditor = this;            
            this.DrawGrid = true;
            DoubleBuffered = false;
            
            _editorOverlay.Parent = this;
            storeNeeded = true;
            RefreshOverlay();            
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.DrawGrid = false;
            this.Name = "DocumentEditorVisualizer";
            this.ResumeLayout(false);            
        }


        public void MoveControl(XMLControl xmlControl,  int deltaX, int deltaY)
        {
            System.Diagnostics.Trace.WriteLine("MoveControl 1: " + xmlControl.GetHashCode().ToString());
            
            Control control = XMLControl2ControlDictionary[xmlControl];
            Point pos = control.Location;
            pos.X += deltaX;
            pos.Y += deltaY;
            control.Location = pos;
        }

        public void ResizeControl(XMLControl xmlControl, HandlerType handleType, int deltaX, int deltaY)
        {
            System.Diagnostics.Trace.WriteLine("MoveControl 1: " + xmlControl.GetHashCode().ToString());
            Control control = XMLControl2ControlDictionary[xmlControl];
            Rectangle rect = new Rectangle(control.Location, control.Size);
            ResizeTool.ResizeRect(ref rect, handleType, deltaX, deltaY);

            control.SuspendLayout();
            control.Location = rect.Location;
            control.Size = rect.Size;
            control.ResumeLayout(false);
        }

        public void DropToolBoxItem(IToolBoxItem toolBoxItem, Point position)
        {
            if (! ( toolBoxItem is IEditorControl ))
                return;

            Cursor = Cursors.WaitCursor;


            Point layoutPosition = new Point(position.X + ViewLocation.X, position.Y + ViewLocation.Y);
            XMLControl xmlConrtol = toolBoxItem.Duplicate(layoutPosition);
            DocumentLayout.AddControl( xmlConrtol );
            
            IEditorControl xmlControl = toolBoxItem as IEditorControl;
            Control editControl = xmlControl.CreateEditorControl();            
            editControl.Left = position.X;
            editControl.Top = position.Y;
            editControl.Parent = this;
            editControl.Visible = true;
            editControl.TabStop = false;

            System.Diagnostics.Trace.WriteLine("DropToolBoxItem 3");
            XMLControl2ControlDictionary.Add(xmlConrtol, editControl);
            Control2XmlControlDictionary.Add(editControl, xmlConrtol);

            xmlConrtol.UpdateEditorControl(editControl);
            
            storeNeeded = true;
            RefreshOverlay();

            Cursor = Cursors.Arrow;
        }

        public void RemoveSelectedControls()
        {
            DocumentLayout.RemoveSelectedControls();
            Trace.WriteLine("DocumentEditor::RemoveSelectedControls: Calling recreateControls");
            recreateControls();
        }

        private void ApplyDataSource( IDataSourceBase dataSource)
        {
            foreach (XMLControl ctr in _documentLayout.SelectedControls() )
            {
                ctr.SetDataSource(dataSource);
            }

            foreach (XMLControl ctr in _documentLayout.SelectedControls(ViewRectangle))
            {

                System.Diagnostics.Trace.WriteLine("ApplyDataSource 4");
                ctr.UpdateEditorControl(XMLControl2ControlDictionary[ctr]);
                _editorOverlay.Invalidate(XMLControl2ControlDictionary[ctr].ClientRectangle);
            }

            storeNeeded = true;
            RefreshOverlay();
            
        }

        private void ApplyEditorSettings(XMLEditorPropertyControl propertyControl)
        {
            _documentLayout.MinimalSize = new Rectangle(
                Convert.ToInt32(propertyControl.textBoxLeft.Text),
                Convert.ToInt32(propertyControl.textBoxTop.Text),
                Convert.ToInt32(propertyControl.textBoxRight.Text) - Convert.ToInt32(propertyControl.textBoxLeft.Text),
                Convert.ToInt32(propertyControl.textBoxBottom.Text) - Convert.ToInt32(propertyControl.textBoxTop.Text)
            );

            storeNeeded = true;
        }
        
        public void CreateControlPropertyWindow(IEditorControl xmlControl)
        {
            XMLPropertyControlBase newPropertyWindow = null;
            if (_propertyWindowPlace != null)
            {
                newPropertyWindow = xmlControl.GetPropertyWindow();                
                newPropertyWindow.Dock = DockStyle.Fill;               
                newPropertyWindow.OnDataSourceChanged += delegate { ApplyDataSource(_currentPropertyWindow); };

                newPropertyWindow.Parent = _propertyWindowPlace;
                newPropertyWindow.Show();
            }


            if (_currentPropertyWindow != null)
            {
                _currentPropertyWindow.Hide();
                _currentPropertyWindow.Dispose();
            }
            
            _currentPropertyWindow = newPropertyWindow;
        }

        public void CreateEditorPropertyWindow()
        {

            XMLEditorPropertyControl editorPropertyWindow = null;
            if (_propertyWindowPlace != null)
            {
                editorPropertyWindow = new XMLEditorPropertyControl();                
                editorPropertyWindow.textBoxLeft.Text = DocumentLayout.Size.Left.ToString();
                editorPropertyWindow.textBoxTop.Text = DocumentLayout.Size.Top.ToString();
                editorPropertyWindow.textBoxRight.Text = DocumentLayout.Size.Right.ToString();
                editorPropertyWindow.textBoxBottom.Text = DocumentLayout.Size.Bottom.ToString();


                editorPropertyWindow.Parent = _propertyWindowPlace;
                editorPropertyWindow.Dock = DockStyle.Fill;
                editorPropertyWindow.OnDataSourceChanged += delegate { ApplyEditorSettings(editorPropertyWindow); };

                editorPropertyWindow.Show();
            }

            if (_currentPropertyWindow != null)
            {
                _currentPropertyWindow.Hide();                
                _currentPropertyWindow.Dispose();
            }
            _currentPropertyWindow = editorPropertyWindow;

        }

        public void DestroyPropertyWindow()
        {   
            if (_currentPropertyWindow != null)
                _currentPropertyWindow.Dispose();

            _currentPropertyWindow = null;        
        }


        private bool refreshOverlayNeeded = false;
        private void RefreshOverlay ()
        {
            if ( !refreshOverlayNeeded ) {
                refreshOverlayNeeded = true;
                Invalidate();
            }
            refreshOverlayNeeded = true;           
        }

        private bool storeNeeded = false;
        private bool paintingOverlay = false;
        private void doRefreshOverlay() 
        {
            Trace.TraceError("doRefreshOverlay");
            refreshOverlayNeeded = false;

            if (_editorOverlay == null)
                return;

            paintingOverlay = true;

            _editorOverlay.SuspendLayout();
            _editorOverlay.Left = 0;
            _editorOverlay.Top = 0;
            _editorOverlay.Width = ClientRectangle.Width;
            _editorOverlay.Height = ClientRectangle.Height;
            _editorOverlay.ResumeLayout(false);

            if (!_refreshing)                
                Update();

            if (storeNeeded)
            {
                storeNeeded = false;
                _editorOverlay.StoreBmp();
            }
//            _editorOverlay.BringToFront();
//            _editorOverlay.Focus(); // after adding new control the now control gets the focus so we focus back the overlay control

            _editorOverlay.Invalidate();
            paintingOverlay = false;
        }
        
        public override void doRecreateControls()
        {            
            base.doRecreateControls();

            updateVisibleControls();
            storeNeeded = true;
            RefreshOverlay();
            
            foreach (KeyValuePair<Control, XMLControl> p in Control2XmlControlDictionary) 
            { 
                p.Key.TabStop = false;
            }

            System.Diagnostics.Trace.WriteLine("DocumentEditor:recreateControls (end): " + System.DateTime.Now.ToString() + " (" + System.DateTime.Now.Millisecond.ToString() + ")");
        }


        public override void updateVisibleControls()
        {
            base.updateVisibleControls();
            storeNeeded = true;
            refreshOverlayNeeded = true;
        }

        protected bool _refreshing = false;
        public override void Refresh()
        {
            base.Refresh();
            _refreshing = true;
            storeNeeded = true;
            RefreshOverlay();
            _refreshing = false;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (recreateControlsNeeded) 
            {
                doRecreateControls();
            }

            if (updateVisibleControlsNeeded)
            {
                System.Diagnostics.Trace.WriteLine("calling doUpdateVisibleControls() from DocumentEditor's OnPaint()");
                doUpdateVisibleControls();
                storeNeeded = true;
            }

            if (refreshOverlayNeeded)
            {
                doRefreshOverlay();
            }

            if (_editorOverlay._storingBmp)
            {
                base.OnPaint(e);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Skip");
            }            
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            RefreshOverlay();
        }

        protected override void OnEnter(EventArgs e)
        {
            if (_editorOverlay != null)
                _editorOverlay.Focus();

            RefreshOverlay();
        }

        protected override void OnLeave(EventArgs e)
        {
            RefreshOverlay();

            base.OnLeave(e);
        }

        public override bool Focused {
            get {
                return _editorOverlay.Focused;
            }
        }

    }
}
