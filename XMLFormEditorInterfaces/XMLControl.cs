using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace XMLFormEditor
{

    public abstract class XMLControl : XMLFormEditor.IToolBoxItem, XMLFormEditor.IEditorControl, IComparable
    {
        public int CompareTo(Object o) {
            return GetHashCode() - o.GetHashCode();

        }

        private DocumentLayout _parentLayout = null;
        public DocumentLayout ParentLayout
        {
            get { return _parentLayout; }
            set { _parentLayout = value; }
        }

        private ResizeMode _resizeMode = ResizeMode.Both;
        public ResizeMode ResizeMode
        {
            get {
                return _resizeMode;
            }
            set { _resizeMode = value; }
        }



        protected bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        protected Rectangle _clientRect;
        public Rectangle ClientRect
        {
            get { return _clientRect; }
            set { _clientRect = value; }
        }


        public abstract Icon ListIcon
        {
            get;
        }


        public void MoveToAbsolutePos(Point pos)
        {
            _clientRect.Location = pos;

            ParentLayout.RefreshControlPosition(this);        
        }
    


        public void Resize(HandlerType handleType, int deltaX, int deltaY)
        {
            ResizeTool.ResizeRect(ref _clientRect, handleType, deltaX, deltaY);
            
            ParentLayout.RefreshControlPosition(this);
        }

        public Cursor cursor = null;
        protected Icon _newIcon;
        protected String _name = "XMLTextBox";
        protected Font Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);

        
        public XMLControl() 
        {
           _parentLayout = null;
           _resizeMode = ResizeMode.Both;

           _instanceId = _instanceIdCounter;
            ++_instanceIdCounter;            
        }

        ~XMLControl()
        {
            if (_newIcon != null)
            {
                DestroyIcon(_newIcon.Handle);
                _newIcon.Dispose();
                _newIcon = null;
            }

        }

        private static int _instanceIdCounter = 0;
        private int _instanceId = 0;

        public override int GetHashCode()
        {
            return _instanceId;
        }

        #region AbstractMethods

        public abstract Control CreateEditorControl();
        public abstract void UpdateEditorControl(Control EditorControl);
        public abstract XMLPropertyControlBase GetPropertyWindow();
        public abstract void SetDataSource(IDataSourceBase dataSource);

        public virtual XmlElement serializeToXml(XmlDocument document)
        {
            XmlElement element = document.CreateElement("Control");            
            element.SetAttribute("Type", GetType().Name);
            element.SetAttribute("X", ClientRect.X.ToString());
            element.SetAttribute("Y", ClientRect.Y.ToString());
            element.SetAttribute("Width", ClientRect.Width.ToString());
            element.SetAttribute("Height", ClientRect.Height.ToString());

            return element;
        }

        public abstract void deserializeFromXml(XmlNode element);


        public virtual XMLControl Duplicate(System.Drawing.Point position)
        {
            _clientRect.Location = position;
            return null;
        }

        #endregion

        public Cursor getToolBoxCursor()
        {
            if (cursor == null)
            {
                cursor = GenerateCursor();
            }
            return cursor;
        }

        
        public void DrawItem(Graphics g, Size size)
        {
            g.FillRectangle(SystemBrushes.Control, 1, 1, size.Width-1, size.Height-1);
            g.DrawString(_name, SystemFonts.DefaultFont, Brushes.Black, 32, 23);
            if (ListIcon != null)                                 
                g.DrawIcon(ListIcon, 5, (60 - ListIcon.Height) / 2  );
        }

        protected virtual Cursor GenerateCursor()
        {
            string s = string.Format(_name);

            Bitmap imgColor = new Bitmap(200, 32, System.Drawing.Imaging.PixelFormat.Format24bppRgb);



            Brush brush = Brushes.Red;
            SolidBrush brushMask = new SolidBrush(Color.Green);
            
            Graphics g = Graphics.FromImage(imgColor);
                       
            SizeF size = g.MeasureString(s, this.Font);


            if (size.Width > 200) size.Width = 200;
            if (size.Height > 32) size.Height = 32;

            if (ListIcon != null)
            {
                size.Width += 17;
                if (size.Height < 16) size.Height = 16;
                g.FillRectangle(brushMask, 0, 0, (int)size.Width, (int)size.Height);
                g.DrawIcon(ListIcon, 0, 0);
                g.DrawString(s, Font, brush, 17, 0);
            }
            else 
            {
                g.FillRectangle(brushMask, 0, 0, (int)size.Width, (int)size.Height);
                g.DrawString(s, Font, brush, 0, 0);
            }
                       
            
            g.DrawLine(Pens.Black, (int)size.Width / 2, (int)size.Height / 2 - 3, (int)size.Width / 2, (int)size.Height / 2 + 3);
            g.DrawLine(Pens.Black, (int)size.Width / 2 - 3 , (int)size.Height / 2 , (int)size.Width / 2 + 3 , (int)size.Height / 2);


            g.Flush();

            imgColor.MakeTransparent(Color.Green);

            Bitmap usedRegion = imgColor.Clone(new Rectangle(0, 0, (int)size.Width, (int)size.Height), imgColor.PixelFormat);
            _newIcon = Icon.FromHandle(usedRegion.GetHicon());
        
            Cursor curs = new Cursor(_newIcon.Handle);
            return curs;
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        extern static bool DestroyIcon(IntPtr handle);

        protected virtual void OnSourceDocumentListChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }  
        
    }
}
