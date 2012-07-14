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
            StaticLabel newControl = new StaticLabel();
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;            
            return newControl;
        }



        public override Control CreateEditorControl()
        {
            Label label = new Label();
            label.Width = _defaultWidth;
            label.Height = _defaultHeight;
            label.Text = _text;
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
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            StaticLabelPropertyControl wnd = new StaticLabelPropertyControl();
            wnd.Text = "Static label properties";
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
