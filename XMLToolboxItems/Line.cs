using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormEditor
{
    public class Line: XMLControl
    {        
        protected string _text;

        const int _defaultWidth = 200;
        const int _defaultHeight = 20;

        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.label; }
        }

        public Line ()
        {
            _clientRect =  new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Both;
            _name = "LineLabel";
            _text = "Line";            
        }

        public override XmlElement serializeToXml( XmlDocument document)
        {
            XmlElement element = base.serializeToXml(document);

            XmlElement DocumentElement = document.CreateElement("Text");
            DocumentElement.InnerText = _text;
            element.AppendChild(DocumentElement);

            return element;             
        }

        public override void deserializeFromXml( XmlNode element )
        {
            _text = element.ChildNodes[0].InnerText;            
        }


        public override XMLControl Duplicate(System.Drawing.Point position) 
        {
            Line newControl = new Line();
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;            
            return newControl;
        }



        public override Control CreateEditorControl()
        {
            Panel panel = new Panel();
            panel.Width = _defaultWidth;
            panel.Height = _defaultHeight;
            panel.BackColor = Color.DarkGray;            
            return panel;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {
            Panel panel = EditorControl as Panel;
            if (panel == null) 
            {
                System.Diagnostics.Trace.WriteLine("Line::UpdateEditorControl: control type not Label");
                return;
            }            
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            StaticLabelPropertyControl wnd = new StaticLabelPropertyControl();
            wnd.Text = "Line label properties";
            wnd.textBoxCaption.Text = _text;
            return wnd;
        }

        public override void SetDataSource(IDataSourceBase dataSource)
        {
            IStaticLabelDataSource dS = dataSource as IStaticLabelDataSource;
            if (dS == null)
                return;

            _text = dS.getCaption();
        }
    }
}
