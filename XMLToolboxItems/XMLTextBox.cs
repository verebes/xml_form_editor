using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormEditor
{
    public class XMLTextBox: XMLControl
    {
        protected string _XPathExpression;
        protected string _documentName;


        const int _defaultWidth = 200;
        const int _defaultHeight = 20;
        
        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.textbox; }
        }


        public XMLTextBox ()
        {
            _clientRect =  new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Horizontal;
            _name = "XMLTextbox";
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
            XMLTextBox newControl = new XMLTextBox();
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;
            return newControl;
        }


        private void OnChange(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;

            IUpdatableWindow parentWnd = textBox.Parent as IUpdatableWindow;

            if (parentWnd == null || _documentName=="")
                return;


            XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];

            try
            {
                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    return;

                node.InnerText = textBox.Text;
                textBox.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException exception)
            {
                System.Diagnostics.Trace.TraceError("XmlFormRditor::OnChange: xpathexception: " + exception.Message);
                return;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Trace.TraceError("XmlFormRditor::OnChange: exception: " + exception.Message);
                return;
            }


            parentWnd.updateVisibleControls();            
        }


        public override Control CreateEditorControl()
        {
            TextBox textBox = new TextBox();            
            textBox.Width = _defaultWidth;
            textBox.Height = _defaultHeight;
            textBox.TextChanged += OnChange;
            return textBox;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {
            TextBox textBox = EditorControl as TextBox;
            if (textBox == null) 
            {
                System.Diagnostics.Trace.WriteLine("XMLTextBox::UpdateEditorControl: control type not TextBox");
                return;
            }

            textBox.TextChanged -= OnChange;

            try
            {                
                XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];
                if (source == null)
                    throw new Exception("Invalid document: "+_documentName);

                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    throw new Exception("Selected node was not found: " + _XPathExpression);

                
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Element)
                    {
                        throw new Exception("Node has child element so content can not be edited");
                    }
                }

                textBox.Text = node.InnerText;                
                textBox.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException)
            {
                textBox.Text = _XPathExpression;
                textBox.Enabled = false;
            }
            catch (Exception e)
            {
                textBox.Text = e.Message ;
                textBox.Enabled = false;
            }

            textBox.TextChanged += OnChange;
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            SingleDataSourcePropertyControl wnd = new SingleDataSourcePropertyControl();
            wnd.Text = "Text box properties";
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
