using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormEditor
{
    public class XMLDeleteButton : XMLControl
    {

        protected string _XPathExpression;
        protected string _documentName;

        const int _defaultWidth = 200;
        const int _defaultHeight = 20;

        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.button; }
        }


        public XMLDeleteButton()
        {
            _clientRect = new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Both;
            _name = "XMLDeleteButton";            
        }

        public override XmlElement serializeToXml(XmlDocument document)
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

        public override void deserializeFromXml(XmlNode element)
        {
            _documentName = element.ChildNodes[0].InnerText;
            _XPathExpression = element.ChildNodes[1].InnerText;
        }


        public override XMLControl Duplicate(System.Drawing.Point position)
        {
            XMLDeleteButton newControl = new XMLDeleteButton();
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;
            return newControl;
        }

        private void OnChange(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            IUpdatableWindow parentWnd = button.Parent as IUpdatableWindow;

            if (parentWnd == null || _documentName == null || _documentName == "" )
                return;


            XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];

            try
            {
                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    return;

                node.ParentNode.RemoveChild(node);                
            }
            catch (System.Xml.XPath.XPathException)
            {
                return;
            }


            parentWnd.updateVisibleControls();
        }


        public override Control CreateEditorControl()
        {
            Button button = new Button();
            button.FlatStyle = FlatStyle.System;
            button.Width = _defaultWidth;
            button.Height = _defaultHeight;
            button.Click += OnChange;
            return button;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {
            Button button = EditorControl as Button;
            if (button == null)
            {
                System.Diagnostics.Trace.WriteLine("XMLDeleteButton::UpdateEditorControl: control type not Label");
                return;
            }


            try
            {
                XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];
                if (source == null)
                    throw new Exception("Invalid document: " + _documentName);

                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    throw new Exception("Selected node was not found: " + _XPathExpression);

                button.Text = "Delete";
                button.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException)
            {
                button.Text = _XPathExpression;
                button.Enabled = false;
            }
            catch (Exception e)
            {
                button.Text = e.Message;
                button.Enabled = false;
            }
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {            
            SingleDataSourcePropertyControl wnd = new SingleDataSourcePropertyControl();
            wnd.Text = "Button properties";
            wnd.textBoxXPath.Text = _XPathExpression;
            wnd.cbSourceDocuments.Text = _documentName;
            return wnd;
        }

        public override void SetDataSource(IDataSourceBase dataSource)
        {
            IButtonDataSource dS = dataSource as IButtonDataSource;
            if (dS == null)
                return;

            _XPathExpression = dS.getXPathExpression();
            _documentName = dS.getDocumentName();
        }
    }
}
