using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormEditor
{
    public class StaticLabel: XMLControl
    {        
        protected string _text;
        protected Font _font;
        protected Color _color;
        protected Color _background;
        protected ContentAlignment _alignment;


        const int _defaultWidth = 200;
        const int _defaultHeight = 20;

        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.label; }
        }

        public StaticLabel ()
        {
            _clientRect =  new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Both;
            _name = "StaticLabel";
            _text = "Label";
            _font = null;
            _alignment = ContentAlignment.MiddleLeft;
        }

        public override XmlElement serializeToXml( XmlDocument document)
        {
            XmlElement element = base.serializeToXml(document);

            XmlElement DocumentElement = document.CreateElement("Text");

            XmlAttribute color = document.CreateAttribute("Color");
            ColorConverter converter = new ColorConverter();
            color.Value = converter.ConvertToString(null, System.Globalization.CultureInfo.InvariantCulture, _color);
            DocumentElement.Attributes.Append(color);

            XmlAttribute background = document.CreateAttribute("Background");
            background.Value = converter.ConvertToString(null, System.Globalization.CultureInfo.InvariantCulture, _background);
            DocumentElement.Attributes.Append(background);

            if (_font != null)
            {
                XmlAttribute font = document.CreateAttribute("Font");
                FontConverter fontConverter = new FontConverter();
                font.Value = fontConverter.ConvertToString(null, System.Globalization.CultureInfo.InvariantCulture, _font);                
                DocumentElement.Attributes.Append(font);
            }

            XmlAttribute alignment = document.CreateAttribute("Alignment");

            alignment.Value = AlignmentString(_alignment);
            DocumentElement.Attributes.Append(alignment);


            DocumentElement.InnerText = _text;
            element.AppendChild(DocumentElement);

            return element;             
        }


        public override void deserializeFromXml( XmlNode element )
        {
            if (element.ChildNodes.Count < 1)
                return;

            XmlElement e = element.ChildNodes[0] as XmlElement;
            if (e == null)
            {
                return;
            }


            _text = e.InnerText;

            FontConverter fontConverter = new FontConverter();
            ColorConverter colorConverter = new ColorConverter();

            if (e.HasAttribute("Color")) {                
                //_color = (Color)colorConverter.ConvertFromString(e.Attributes["Color"].Value);
                _color  = (Color)colorConverter.ConvertFromString(null, System.Globalization.CultureInfo.InvariantCulture, e.Attributes["Color"].Value);
            }

            if (e.HasAttribute("Background") ) {                
                _background = (Color)colorConverter.ConvertFromString(null,System.Globalization.CultureInfo.InvariantCulture, e.Attributes["Background"].Value);
            }

            if (e.HasAttribute("Font")) {
                _font = fontConverter.ConvertFromString(null, System.Globalization.CultureInfo.InvariantCulture, e.Attributes["Font"].Value ) as Font;
            } else {
                _font = SystemFonts.DefaultFont;
            }

            if ( e.HasAttribute("Alignment"))
                _alignment =  StringToAlignment(e.Attributes["Alignment"].Value);

        }


        public override XMLControl Duplicate(System.Drawing.Point position) 
        {
            StaticLabel newControl = new StaticLabel();
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;
            newControl._color = _color;
            newControl._background = _background;
            newControl._alignment = _alignment;
            newControl._font = _font;
            return newControl;
        }



        public override Control CreateEditorControl()
        {
            Label label = new Label();
            label.Width = _defaultWidth;
            label.Height = _defaultHeight;
            label.Text = _text;
            label.ForeColor = _color;
            label.BackColor = _background;
            label.TextAlign = _alignment;
            label.Font = _font;
            return label;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {
            Label label = EditorControl as Label;
            if (label == null) 
            {
                System.Diagnostics.Trace.WriteLine("StaticLabel::UpdateEditorControl: control type not Label");
                return;
            }

            label.Text = _text;
            label.ForeColor = _color;
            label.BackColor = _background;
            label.TextAlign = _alignment;
            label.Font = _font;            
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            StaticLabelPropertyControl wnd = new StaticLabelPropertyControl();
            wnd.Text = "Static label properties";
            wnd.textBoxCaption.Text = _text;            
            wnd.setFont(_font);
            wnd.setBackground(_background);
            wnd.setColor(_color);
            wnd._alignment = _alignment;
            
            return wnd;
        }

        public override void SetDataSource(IDataSourceBase dataSource)
        {
            IStaticLabelDataSource dS = dataSource as IStaticLabelDataSource;
            if (dS == null)
                return;

            _text = dS.getCaption();
            _color = dS.getColor();
            _background = dS.getBackground();
            _alignment = dS.getAlignment();
            _font = dS.getFont();
        }

        protected static string AlignmentString(ContentAlignment alignment)
        {
            string horizontal = "Left";
            string vertical = "Middle";
            if (alignment == ContentAlignment.TopLeft || alignment == ContentAlignment.MiddleLeft || alignment == ContentAlignment.BottomLeft)
            {
                horizontal = "Left";
            }
            if (alignment == ContentAlignment.TopRight || alignment == ContentAlignment.MiddleRight || alignment == ContentAlignment.BottomRight)
            {
                horizontal = "Right";
            }
            if (alignment == ContentAlignment.TopCenter || alignment == ContentAlignment.MiddleCenter || alignment == ContentAlignment.BottomCenter)
            {
                horizontal = "Center";
            }

            if (alignment == ContentAlignment.TopLeft || alignment == ContentAlignment.TopCenter || alignment == ContentAlignment.TopRight)
            {
                vertical = "Top";
            }
            if (alignment == ContentAlignment.MiddleLeft || alignment == ContentAlignment.MiddleCenter || alignment == ContentAlignment.MiddleRight)
            {
                vertical = "Middle";
            }
            if (alignment == ContentAlignment.BottomLeft || alignment == ContentAlignment.BottomCenter || alignment == ContentAlignment.BottomRight)
            {
                vertical = "Bottom";
            }

            return vertical + horizontal;
        }

        protected static ContentAlignment StringToAlignment(string str)
        {
            if (str == "TopLeft")
                return ContentAlignment.TopLeft;

            if (str == "TopCenter")
                return ContentAlignment.TopCenter;

            if (str == "TopRight")
                return ContentAlignment.TopRight;

            if (str == "MiddleLeft")
                return ContentAlignment.MiddleLeft;

            if (str == "MiddleCenter")
                return ContentAlignment.MiddleCenter;

            if (str == "MiddleRight")
                return ContentAlignment.MiddleRight;

            if (str == "BottomLeft")
                return ContentAlignment.BottomLeft;

            if (str == "BottomCenter")
                return ContentAlignment.BottomCenter;

            if (str == "BottomRight")
                return ContentAlignment.BottomRight;

            return ContentAlignment.MiddleLeft;
        }
    }
}
