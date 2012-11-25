using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Text;


namespace XMLFormEditor
{
   [Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design",
    typeof(System.ComponentModel.Design.IDesigner))]

    public class DocumentVisualizer : UserControl
    {       
       class ControlEqualityComparer : IEqualityComparer<Control>
       {
           public bool Equals(Control c1, Control c2)
           {
               return c1.Handle == c2.Handle;
           }

           public int GetHashCode(Control c) 
           {
               return c.Handle.ToInt32();               
           }
       }


        protected Dictionary<XMLControl, Control> XMLControl2ControlDictionary = new Dictionary<XMLControl, Control>();
        protected Dictionary<Control, XMLControl> Control2XmlControlDictionary = new Dictionary<Control, XMLControl>( new ControlEqualityComparer() );

        protected LineDrawer _lineDrawer;
        public virtual  LineDrawer LineDrawer
        {
            get { return _lineDrawer;  }
            set { _lineDrawer = value; }
        }



        protected DocumentLayout _documentLayout;  
        public virtual DocumentLayout DocumentLayout
        {
            get { return _documentLayout; }
            set { _documentLayout = value; }
        }

        public Point ViewLocation
        {
            get { return _viewRectangle.Location; }
            set { 
                ViewRectangle = new Rectangle(value, ViewRectangle.Size);
            }
        }
       
        protected Rectangle _viewRectangle;
        public virtual Rectangle ViewRectangle
        {
            get { return _viewRectangle; }
            set {
                if (_viewRectangle == value)
                    return;

                _viewRectangle = value;
                recreateControls();
            }
        }

        public DocumentVisualizer() 
        {
            InitializeComponent();
            _lineDrawer = new LineDrawer();
            _viewRectangle = new Rectangle(0,0,Width,Height);
            XmlSourceDocumentManager.Instance().OnSourceDocumentChanged += new EventHandler(DocumentVisualizer_OnSourceDocumentChanged);
        }

        void DocumentVisualizer_OnSourceDocumentChanged(object sender, EventArgs e) {
            updateVisibleControls();
        }

        private int gridSize = 20; 
        public virtual int GridSize
        {
            get { return gridSize; }
            set {
                if (gridSize == value)
                    return;

                gridSize = value;
                if (DrawGrid)
                    Invalidate();                
            }

        }


        #region otherfile
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

       
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DocumentVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "DocumentVisualizer";
            this.Size = new System.Drawing.Size(369, 417);
            this.ResumeLayout(false);

        }
        #endregion

        protected bool _drawGrid = true;
        [Description("Determines whether the form has a grid background"),
         DefaultValue(true),
         Category("Appearance")]

        public virtual bool DrawGrid
        {
            set
            {
                if (_drawGrid == value)
                    return;
                
                _drawGrid = value;
                Invalidate();
            }
            get
            {
                return _drawGrid;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

       private void drawLines(PaintEventArgs e)
       {
           List<LineDrawer.Section> sections = _lineDrawer.getSectionList();
           foreach (LineDrawer.Section section in sections)
           {
               e.Graphics.DrawEllipse(Pens.Blue, section.p1.X - 3, section.p1.Y - 3, 6, 6);
               e.Graphics.DrawEllipse(Pens.Green, section.p2.X - 4, section.p2.Y - 4, 8, 8);
               Pen pen = Pens.Red.Clone() as Pen;
               pen.Width = 2;
               e.Graphics.DrawLine(pen, section.p1, section.p2);
           }            

       }

        private Pen gridPen = new Pen(Color.LightGray, 1);
        protected override void OnPaint(PaintEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("clipRect: "+ e.ClipRectangle.ToString() +  " client" + ClientRectangle.ToString() );

            if (recreateControlsNeeded) {
                doRecreateControls();
            }

            if (updateVisibleControlsNeeded) {
                System.Diagnostics.Trace.WriteLine("calling doUpdateVisibleControls() from DocumentVisualizer's OnPaint()");
                doUpdateVisibleControls();
            }

            e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);
            
            drawLines(e);

            if (DrawGrid)
            {                
                gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                
                for (int i = gridSize - Math.Abs(ViewLocation.X % gridSize); i < Width; i += gridSize)
                {
                    e.Graphics.DrawLine(gridPen, i, e.ClipRectangle.Top, i, e.ClipRectangle.Height);
                }

                for (int i = gridSize - Math.Abs(ViewLocation.Y % gridSize); i < Height; i += gridSize)
                {
                    e.Graphics.DrawLine(gridPen, e.ClipRectangle.Left, i, e.ClipRectangle.Width, i);
                }
            }
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ViewRectangle = new Rectangle(ViewRectangle.Location, Size);
        }


        private void clearControls()
        {

            List<Control> l = new List<Control>();


            foreach (KeyValuePair<XMLControl, Control> pair in XMLControl2ControlDictionary)
            {
                l.Add(pair.Value);                             
            }

            System.Diagnostics.Trace.WriteLine("clearControls");
            XMLControl2ControlDictionary.Clear();
            Control2XmlControlDictionary.Clear();

            foreach (Control c in l)
            {
                c.Parent = null;
                c.Dispose();
            }
        }


        protected bool recreateControlsNeeded = false;
        public virtual void recreateControls()
        {
            if ( !recreateControlsNeeded ) {
                recreateControlsNeeded = true;
                Invalidate();
            }
            recreateControlsNeeded = true;
        }

        public virtual void doRecreateControls()
        {           
            recreateControlsNeeded = false;
            

            System.Diagnostics.Trace.WriteLine("XmlFormVisualizer:recreateControls: " + System.DateTime.Now.ToString() + " ("+ System.DateTime.Now.Millisecond.ToString()+")");
            if (DocumentLayout == null)
                return;

            List<XMLControl> controls = DocumentLayout.Controls(ViewRectangle);
            System.Diagnostics.Trace.WriteLine("calling clearControls()");

            XMLControl[] oldControls = new XMLControl[XMLControl2ControlDictionary.Count];
            XMLControl2ControlDictionary.Keys.CopyTo(oldControls,0);
            Array.Sort(oldControls);
            
            XMLControl[] newControls = controls.ToArray();

            Array.Sort(newControls);

            

            int iNew = 0;
            int iOld = 0;
            while ( iOld < oldControls.Length && iNew < newControls.Length ) {

                int oldHash = oldControls[iOld].GetHashCode();
                int newHash = newControls[iNew].GetHashCode();

                if ( oldHash < newHash ) {
                    DeleteControl(oldControls[iOld]);

                    ++iOld;
                    continue;
                } 
                
                if ( oldHash == newHash ) {
                    UpdateControl(oldControls[iOld]);

                    ++iOld;
                    ++iNew;
                    continue;
                } 
                                               
                AddControl(newControls[iNew]);
                ++iNew;
            } 

            while ( iOld < oldControls.Length )
            {
                DeleteControl(oldControls[iOld]);                
                ++iOld;
            }

            while ( iNew < newControls.Length ) {
                AddControl(newControls[iNew]);
                ++iNew;
            }

            
        }

        private void AddControl(XMLControl xmlControl) {
            System.Diagnostics.Trace.WriteLine("next loop: calling Create");
            Control editControl = xmlControl.CreateEditorControl();
            editControl.SuspendLayout();
            editControl.Location = new Point(xmlControl.ClientRect.Location.X - ViewLocation.X, xmlControl.ClientRect.Location.Y - ViewLocation.Y);

            editControl.Size = xmlControl.ClientRect.Size;
            editControl.Parent = this;
            editControl.TabStop = false;
            System.Diagnostics.Trace.WriteLine("calling ResumeLayout");
            editControl.ResumeLayout();

            if (!XMLControl2ControlDictionary.ContainsKey(xmlControl)) {

                System.Diagnostics.Trace.WriteLine("XMLControl2ControlDictionary.ContainsKey:" + xmlControl.GetHashCode().ToString());
                XMLControl2ControlDictionary.Add(xmlControl, editControl);
                Control2XmlControlDictionary.Add(editControl, xmlControl);

                xmlControl.UpdateEditorControl(editControl);                
            } else {
                System.Diagnostics.Trace.WriteLine("how can this happen.ContainsKey:" + xmlControl.GetHashCode().ToString());
            }
        }

        private void UpdateControl(XMLControl xmlControl) {
            Control editControl = XMLControl2ControlDictionary[xmlControl];
            editControl.Location = new Point(xmlControl.ClientRect.Location.X - ViewLocation.X, xmlControl.ClientRect.Location.Y - ViewLocation.Y);
            editControl.Size = xmlControl.ClientRect.Size;

        }

        private void DeleteControl(XMLControl xmlControl)
        {
            Control c = XMLControl2ControlDictionary[xmlControl];
            
            XMLControl2ControlDictionary.Remove(xmlControl);
            Control2XmlControlDictionary.Remove(c);

            c.Parent = null;
            c.Dispose();
        }

        protected bool updateVisibleControlsNeeded = false;
        public virtual void updateVisibleControls()
        {
            if (!updateVisibleControlsNeeded)
            {
                updateVisibleControlsNeeded = true;
                Invalidate();
            }
            updateVisibleControlsNeeded = true;
        }


        public virtual void doUpdateVisibleControls()
        {
            System.Diagnostics.Trace.WriteLine("doUpdateVisibleControls() was called");
            updateVisibleControlsNeeded = false;

            if (_documentLayout == null)
                return;

            List<XMLControl> controls = _documentLayout.Controls(ViewRectangle);
            foreach (XMLControl ctr in controls)
            {
                System.Diagnostics.Trace.WriteLine("next loop in doUpdateVisibleControls");

                if (XMLControl2ControlDictionary.ContainsKey(ctr))
                {
                    System.Diagnostics.Trace.WriteLine("XMLControl2ControlDictionary.ContainsKey(ctr):" + ctr.GetHashCode().ToString());
                    ctr.UpdateEditorControl(XMLControl2ControlDictionary[ctr]);                    
                }
            }
        }


    }
}
