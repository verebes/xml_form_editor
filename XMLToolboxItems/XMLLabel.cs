using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace XMLFormEditor
{
    public class XMLLabel: XMLControl
    {

        protected string _XPathExpression;
        protected string _documentName;


        const int _defaultWidth = 200;
        const int _defaultHeight = 20;

        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.label; }
        }


        public XMLLabel ()
        {
            _clientRect =  new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Both;
            _name = "XMLLabel";            
        }

        public override XmlElement serializeToXml( XmlDocument document)
        {
            XmlElement element = base.serializeToXml(document);

            XmlElement DocumentElement = document.CreateElement("SourceDocument");
            DocumentElement.InnerText = _documentName;
            element.AppendChild(DocumentElement);

            XmlElement RowXPathElement = document.CreateElement("XPath");
            RowXPathElement.InnerText = _XPathExpression;
            element.AppendChild(RowXPathElement);

            return element;             
        }

        public override void deserializeFromXml( XmlNode element )
        {
            _documentName = element.ChildNodes[0].InnerText;
            _XPathExpression = element.ChildNodes[1].InnerText;
        }


        public override XMLControl Duplicate(System.Drawing.Point position) 
        {
            XMLLabel newControl = new XMLLabel();
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
            return label;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {
            Label label = EditorControl as Label;
            if (label == null) 
            {
                System.Diagnostics.Trace.WriteLine("XMLLabel::UpdateEditorControl: control type not Label");
                return;
            }
                

            try
            {                
                XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];
                if (source == null)
                    throw new Exception("Invalid document: "+_documentName);

                XPathExpression expr = XPathExpression.Compile(_XPathExpression);
                XPathNavigator navigator = source.CreateNavigator();


                switch (expr.ReturnType)
                { 
                    case XPathResultType.NodeSet:
                        XPathNodeIterator nodes = (XPathNodeIterator)navigator.Evaluate(expr);
                        if (nodes.Count == 0 )
                            throw new Exception("Selected node was not found: " + _XPathExpression);

                        nodes.MoveNext();
                        label.Text = nodes.Current.Value.ToString();
                        break;
                    default:
                        label.Text = navigator.Evaluate(expr).ToString();
                        break;
                }
                
                label.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException)
            {
                label.Text = _XPathExpression;
                label.Enabled = false;
            }
            catch (Exception e)
            {
                label.Text = e.Message ;
                label.Enabled = false;
            }
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            SingleDataSourcePropertyControl wnd = new SingleDataSourcePropertyControl();
            wnd.Text = "Label properties";
            wnd.textBoxXPath.Text = _XPathExpression;
            wnd.cbSourceDocuments.Text = _documentName;
            return wnd;
        }

        public override void SetDataSource(IDataSourceBase dataSource)
        {            
            ISingleDataSource dS = dataSource as ISingleDataSource;
            if (dS == null)
                return;

            _XPathExpression = dS.getXPathExpression();
            _documentName = dS.getDocumentName();
        }
    }
}
