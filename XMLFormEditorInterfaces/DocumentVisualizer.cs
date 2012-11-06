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
            _viewRectangle = new Rectangle(0,0,Width,Height);            
        }

        private int gridSize = 20; 
        public int GridSize
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

        bool _drawGrid = true;
        [Description("Determines whether the form has a grid background"),
         DefaultValue(true),
         Category("Appearance")]

        public bool DrawGrid
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
            clearControls();
            foreach (XMLControl xmlControl in controls)
            {
                System.Diagnostics.Trace.WriteLine("next loop: calling Create");
                Control editControl = xmlControl.CreateEditorControl();                
                editControl.SuspendLayout();
                editControl.Location = new Point(xmlControl.ClientRect.Location.X - ViewLocation.X, xmlControl.ClientRect.Location.Y - ViewLocation.Y);
                
                editControl.Size = xmlControl.ClientRect.Size;                
                editControl.Parent = this;
                System.Diagnostics.Trace.WriteLine("calling ResumeLayout");
                editControl.ResumeLayout();

                if (!XMLControl2ControlDictionary.ContainsKey(xmlControl))
                {

                    System.Diagnostics.Trace.WriteLine("XMLControl2ControlDictionary.ContainsKey:" + xmlControl.GetHashCode().ToString());
                    XMLControl2ControlDictionary.Add(xmlControl, editControl);
                    Control2XmlControlDictionary.Add(editControl, xmlControl);
                } else {
                    System.Diagnostics.Trace.WriteLine("how can this happen.ContainsKey:" + xmlControl.GetHashCode().ToString());                    
                }
            }
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
