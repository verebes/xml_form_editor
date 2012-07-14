using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormEditor
{
    public class XMLPager : XMLControl
    {
        protected string _documentName;
        protected string _XPathExpression;

        protected string _PageCountDocument;
        protected string _PageCountXPath;

        const int _defaultWidth = 200;
        const int _defaultHeight = 20;

        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.pager; }
        }


        public XMLPager()
        {
            _clientRect = new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Horizontal;
            _name = "XMLPager";
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

            XmlElement PageCountDocumentElement = document.CreateElement("PageCountDocument");
            PageCountDocumentElement.InnerText = _PageCountDocument;
            element.AppendChild(PageCountDocumentElement);

            XmlElement PageCountXPathElement = document.CreateElement("PageCountXPath");
            PageCountXPathElement.InnerText = _PageCountXPath;
            element.AppendChild(PageCountXPathElement);

            return element;
        }

        public override void deserializeFromXml(XmlNode element)
        {
            _documentName = element.ChildNodes[0].InnerText;
            _XPathExpression = element.ChildNodes[1].InnerText;
            _PageCountDocument = element.ChildNodes[2].InnerText;
            _PageCountXPath = element.ChildNodes[3].InnerText;
        }


        public override XMLControl Duplicate(System.Drawing.Point position)
        {
            XMLPager newControl = new XMLPager();
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;
            return newControl;
        }


        private void OnChange(object sender, EventArgs e)
        {
            NumericUpDown pager = sender as NumericUpDown;
            if (pager == null)
                return;

            IUpdatableWindow parentWnd = pager.Parent as IUpdatableWindow;

            if (parentWnd == null || _documentName == "")
                return;

            XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];

            try
            {
                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    return;

                node.InnerText = pager.Value.ToString();
                pager.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException exception)
            {
                System.Diagnostics.Trace.TraceError("XMLPager::OnChange: xpathexception: " + exception.Message);
                return;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Trace.TraceError("XMLPager::OnChange: exception: " + exception.Message);
                return;
            }


            parentWnd.updateVisibleControls();
        }


        public override Control CreateEditorControl()
        {
            NumericUpDown pager = new NumericUpDown();
            pager.Width = _defaultWidth;
            pager.Height = _defaultHeight;
            pager.ValueChanged += OnChange;
            pager.Minimum = 1;
            return pager;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {
            NumericUpDown pager = EditorControl as NumericUpDown;
            if (pager == null)
            {
                System.Diagnostics.Trace.WriteLine("XMLPager::UpdateEditorControl: control type is not Pager");
                return;
            }

            pager.ValueChanged -= OnChange;

            try
            {
                XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];
                if (source == null)
                    throw new Exception("Invalid document: " + _documentName);

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

                XmlDocument PageCountSourceDocument = XmlSourceDocumentManager.Instance()[_PageCountDocument];
                if (PageCountSourceDocument == null)
                    throw new Exception("Invalid document: " + _PageCountDocument);


                System.Xml.XPath.XPathExpression expr = System.Xml.XPath.XPathExpression.Compile(_PageCountXPath);
                System.Xml.XPath.XPathNavigator navigator = PageCountSourceDocument.CreateNavigator();


                Double val = (double)navigator.Evaluate(expr);
                pager.Maximum = Convert.ToDecimal(val);
                pager.Value = Convert.ToInt32(node.InnerText);
                pager.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException)
            {
                pager.Text = _XPathExpression;
                pager.Enabled = false;
            }
            catch (Exception e)
            {
                pager.Text = e.Message;
                pager.Enabled = false;
            }

            pager.ValueChanged += OnChange;
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            PagerPropertyControl wnd = new PagerPropertyControl();
            wnd.Text = "Pager properties";
            wnd.cbSourceDocuments.Text = _documentName;
            wnd.textBoxXPath.Text = _XPathExpression;                        
            wnd.cbPageCountDocument.Text = _PageCountDocument;
            wnd.textBoxPageCountXPath.Text = _PageCountXPath;
            return wnd;
        }

        public override void SetDataSource(IDataSourceBase dataSource)
        {
            IPagerDataSource dS = dataSource as IPagerDataSource;
            if (dS == null)
                return;

            _documentName = dS.getDocumentName();
            _XPathExpression = dS.getXPathExpression();
            _PageCountDocument = dS.getPagerDocumentName();
            _PageCountXPath = dS.getPagerXPathExpression();
        }

    }
}
