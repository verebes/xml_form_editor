using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Xml;

namespace XMLFormEditor
{

    // The orders of HandlerType values are strictly fix since we iterate through on it.
    // The iteration order is: first, second, third column from top to bottom skipping the center
    public enum HandlerType { None, TopLeft, Left, BottomLeft, Top, Bottom, TopRight, Right, BottomRight };
    public enum ResizeMode {  None, Horizontal, Vertical, Both };

    public enum ArrangeType { Left, Right };

    public class ResizeTool
    {
        public static void ResizeRect(ref Rectangle rect, HandlerType handleType, int deltaX, int deltaY, int gs)
        {
            Point position = rect.Location;
            Size size = rect.Size;
            switch (handleType)
            {
                case HandlerType.Top:
                case HandlerType.TopLeft:
                case HandlerType.TopRight:
                    int origPos = position.Y;
                    position.Y += deltaY;
                    if (gs > 1)
                        position.Y = (position.Y / gs) * gs;

                    if (position.Y > origPos + size.Height - 1) {
                        position.Y = origPos + size.Height - 1;
                    }


                    size.Height -= position.Y - origPos;
                    break;
                case HandlerType.Bottom:
                case HandlerType.BottomLeft:
                case HandlerType.BottomRight:
                    size.Height += deltaY;

                    if ( gs >1 ) 
                        size.Height -= ( position.Y + size.Height ) % gs;

                    break;
                default:
                    break;
            }

            switch (handleType)
            {
                case HandlerType.Left:
                case HandlerType.TopLeft:
                case HandlerType.BottomLeft:

                    int origPos = position.X;
                    position.X += deltaX;
                    if (position.X > origPos + size.Width - 1) {
                        position.X = origPos + size.Width - 1;
                    }

                    if (gs > 1)
                        position.X = (position.X / gs) * gs;

                    size.Width -= position.X - origPos;
                    break;
                case HandlerType.Right:
                case HandlerType.TopRight:
                case HandlerType.BottomRight:
                    size.Width += deltaX;
                    if (gs > 1)
                        size.Width -= (position.X + size.Width) % gs;

                    break;
                default:
                    break;
            }


            if (size.Width <= 1)
                size.Width = 1;

            if (size.Height <= 1)
                size.Height = 1;

            rect.Location = position;
            rect.Size = size;
        }

        public static Cursor getCursor( HandlerType handleType)
        { 
            switch ( handleType )
            {
            	case HandlerType.Top: 
                case HandlerType.Bottom:                    
                    return Cursors.SizeNS;

            	case HandlerType.Left: 
                case HandlerType.Right:
                    return Cursors.SizeWE;
            		
                case HandlerType.TopLeft:
                case HandlerType.BottomRight:
                    return Cursors.SizeNWSE;
                    
                case HandlerType.TopRight:
                case HandlerType.BottomLeft:
                    return Cursors.SizeNESW;                    

            	default:
            		return Cursors.Default;
            }                    

        }
    }
    public delegate void SizeChangedDelegate(object sender);    

    public class DocumentLayout
    {
        protected List<XMLControl> _xmlControlList =  new List<XMLControl>();
        public IControlFactory controlFactory;


        protected static int _OverallDocumentLayoutCount = 0;

        public DocumentLayout()
        {
            _OverallDocumentLayoutCount++;
            _layoutName = string.Format("Doc: {0}", _OverallDocumentLayoutCount);
        }

        private string _layoutName;
        public string LayoutName
        {
            get { return _layoutName; }
            set { _layoutName = value; }
        }

        private string _backgroundImage = "";
        public string BackgroundImage {
            get {
                return _backgroundImage;
            }
            set {
                _backgroundImage = value;
                OnSizeChanged(this);
            }
        }
        private Rectangle _minimalSize = new Rectangle(0,0,0,0);
        public Rectangle MinimalSize
        {
            set { 
                _minimalSize = value;
                OnSizeChanged(this);
            }
            get { return _minimalSize;}
        }


        protected LineDrawer _lineDrawer = new LineDrawer();
        public virtual LineDrawer LineDrawer {
            get {
                return _lineDrawer;
            }
            set {
                _lineDrawer = value;
            }
        }


        public Rectangle Size
        {
            get {
                Rectangle ret = MinimalSize;

                foreach (XMLControl control in _xmlControlList)
                {
                    ret = Rectangle.Union(ret, control.ClientRect);
                }
                List<LineDrawer.Section> sectionList = _lineDrawer.getSectionList();
                foreach ( LineDrawer.Section section in sectionList) 
                {                    
                    Rectangle r1 = new Rectangle(section.p1.X -4, section.p1.Y -4, 9 , 9);
                    Rectangle r2 = new Rectangle(section.p2.X - 4, section.p2.Y - 4, 9, 9);
                    
                    ret = Rectangle.Union(ret, r1);
                    ret = Rectangle.Union(ret, r2);
                }
                
                return ret;             
            }
        }

        public XmlElement SerializeToXml( XmlDocument document )
        {
            XmlElement element = document.CreateElement("Form");
            element.SetAttribute("Name", LayoutName);
            element.SetAttribute("BackgroundImage", BackgroundImage);
            
            foreach (XMLControl control in _xmlControlList)
            {               
                element.AppendChild(control.serializeToXml( document ));
            }

            element.AppendChild(_lineDrawer.serializeToXml(document));

            return element;
        }

        public XmlElement SerializeSelectedToXml(XmlDocument document)
        {
            XmlElement element = document.CreateElement("Form");
            element.SetAttribute("Name", LayoutName);
            element.SetAttribute("BackgroundImage", BackgroundImage);

            foreach (XMLControl control in _xmlControlList)
            {
                if (control.Selected)
                    element.AppendChild(control.serializeToXml(document));
            }

            return element;
        }


        public void deserializeFromXml(XmlNode element, bool selected)
        {
            LayoutName = element.Attributes["Name"].Value;
            try {
                BackgroundImage = element.Attributes["BackgroundImage"].Value;
            } 
            catch (Exception e )
            {
                BackgroundImage = "";
            }

            Rectangle clientRect = new Rectangle();
            foreach ( XmlNode node in element.ChildNodes)
            {
                if (node.Name == "Control") {
                    XMLControl control = controlFactory.createControl(node.Attributes["Type"].Value);

                    clientRect.X = Convert.ToInt32(node.Attributes["X"].Value);
                    clientRect.Y = Convert.ToInt32(node.Attributes["Y"].Value);
                    clientRect.Width = Convert.ToInt32(node.Attributes["Width"].Value);
                    clientRect.Height = Convert.ToInt32(node.Attributes["Height"].Value);

                    control.ClientRect = clientRect;
                    control.Selected = selected;
                    control.deserializeFromXml(node);
                    AddControl(control);
                }

                if (node.Name == "Junctions") {
                    LineDrawer.deserializeFromXml(node);
                }

            }
        }


        public void AddControl( XMLControl control)
        {
            control.ParentLayout = this;
            _xmlControlList.Add(control);
            
            OnSizeChanged(this);
        }

        public void SelectControls(System.Drawing.Rectangle selectionArea)
        {
            foreach (XMLControl control in _xmlControlList)
            {
                if (control.ClientRect.IntersectsWith(selectionArea))
                {
                    control.Selected = true;
                }
            }
        }

        public void ToggleControlSelections(System.Drawing.Point selectionPoint)
        {
            foreach (XMLControl control in _xmlControlList) {
                if (control.ClientRect.Contains(selectionPoint)) {
                    control.Selected = !control.Selected;
                }
            }
        }

        public void SelectControls(System.Drawing.Point selectionPoint)
        {
            foreach (XMLControl control in _xmlControlList)
            {
                if (control.ClientRect.Contains(selectionPoint))
                {
                    control.Selected = true;
                }
            }
        }

        public void SelectAllControls()
        {
            foreach (XMLControl control in _xmlControlList)
            {
                control.Selected = true;
            }
        }

        public void ClearSelection()
        {
            foreach (XMLControl control in _xmlControlList)
            {
                control.Selected = false;
            }
        }

        public List<XMLControl> Controls(Point point)
        {
            List<XMLControl> retVal = new List<XMLControl>();
            foreach ( XMLControl control in _xmlControlList )
            {
                if (control.ClientRect.Contains(point)) 
                {
                    retVal.Add(control);
                }
            }
            return retVal;        
        }

        public List<XMLControl> Controls(int X, int Y)
        {
            return Controls(new Point(X, Y));
        }

        public List<XMLControl> Controls(Rectangle rect)
        {
            List<XMLControl> retVal = new List<XMLControl>();
            foreach ( XMLControl control in _xmlControlList )
            {
                if ( control.ClientRect.IntersectsWith(rect) ) 
                {
                    retVal.Add(control);
                }
            }

            return retVal;
        }

        public List<XMLControl> Controls()
        {
            List<XMLControl> retVal = new List<XMLControl>();
            foreach (XMLControl control in _xmlControlList)
                    retVal.Add(control);

            return retVal;
        }

        public List<XMLControl> SelectedControls()
        {
            List<XMLControl> retVal = new List<XMLControl>();
            foreach (XMLControl control in _xmlControlList)
            {
                if (control.Selected)
                {
                    retVal.Add(control);
                }
            }
            return retVal;
        }

        public List<XMLControl> SelectedControls(Point point)
        {
            List<XMLControl> retVal = new List<XMLControl>();
            foreach (XMLControl control in _xmlControlList)
            {
                if ( control.Selected && control.ClientRect.Contains(point))
                {
                    retVal.Add(control);
                }
            }
            return retVal;            
        }

        public List<XMLControl> SelectedControls(Rectangle rect)
        {
            List<XMLControl> retVal = new List<XMLControl>();
            foreach (XMLControl control in _xmlControlList)
            {
                if (control.Selected && control.ClientRect.IntersectsWith(rect))
                {
                    retVal.Add(control);
                }
            }
            return retVal;
        }

        

        public HandlerType getResizeHandlerType(Point point)
        {
            HandlerType res = HandlerType.None;

            foreach (XMLControl control in _xmlControlList ) 
            {                
                if (control.Selected)
                {
                    Rectangle controlRect = control.ClientRect;
                    ResizeMode resizeMode = control.ResizeMode;
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            if (i == 1 && j == 1)
                                continue;

                            res++; //this is where we use the order of the enumeration values
                            if ( (resizeMode == ResizeMode.Both || (resizeMode == ResizeMode.Horizontal && j == 1) || (resizeMode == ResizeMode.Vertical && i == 1)))
                            {                                
                                int x = (controlRect.Width /2) * i;
                                int y = (controlRect.Height /2) * j;
                                Rectangle r = new Rectangle(controlRect.Left + x -4 , controlRect.Top + y -4, 9, 9); //this is a larger area than the drawen one to be easyer to select the handler
                                if (r.Contains(point))
                                    return res;
                            }
                        }
                }
                res = HandlerType.None;
            }

            return res;
        }

        public void MoveSelectedControls(int deltaX, int deltaY)
        {
            foreach (XMLControl control in _xmlControlList)
            {
                if (control.Selected)
                {
                    Point localtion = control.ClientRect.Location;
                    localtion.X += deltaX;
                    localtion.Y += deltaY;

                }
            }

            OnSizeChanged( this );
        }

        public void RemoveSelectedControls()
        {
            int i = 0;
            while ( i < _xmlControlList.Count )
            {
                if (_xmlControlList[i].Selected)
                    _xmlControlList.RemoveAt(i);
                else
                    i++;
            }
            
            OnSizeChanged(this);
        }
        
        public void ArrangeSelectedControls(ArrangeType direction)
        {
            int left = 0;
            int right = 0;
            
            bool found = false;
            int i = 0;
            while (i < _xmlControlList.Count)
            {
                if (_xmlControlList[i].Selected)
                {
                    if (!found) {
                        left = _xmlControlList[i].ClientRect.X;
                        right = _xmlControlList[i].ClientRect.X + _xmlControlList[i].ClientRect.Width;
                        found = true;
                    }
                    Rectangle r = _xmlControlList[i].ClientRect;

                    switch (direction)
                    { 
                        case ArrangeType.Left:
                            r.X = left;
                            break;
                        case ArrangeType.Right:
                            r.X = right - _xmlControlList[i].ClientRect.Width;
                            break;
                    }

                    
                    _xmlControlList[i].ClientRect = r;
                }

                i++;                
            }

            OnSizeChanged(this);
        }


        public void RefreshControlPosition( XMLControl xmlControl )
        {
            OnSizeChanged(this);
        }

        
        public event SizeChangedDelegate OnSizeChanged = delegate( object sender ) { };
    }
}
