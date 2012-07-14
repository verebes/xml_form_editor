using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace XMLFormEditor
{
    public class XMLCombo: XMLControl
    {
        protected string _documentName;
        protected string _XPathExpression;        
        protected string _listDocumentName;
        protected string _listXPathExpression;
        protected string _captionXPathExpression;
        protected string _valueXPathExpression;


        const int _defaultWidth = 200;
        const int _defaultHeight = 20;


        protected Dictionary<string, string> caption2ValueDictionary = new Dictionary<string,string>();
        protected Dictionary<string, string> value2CaptionDictionary = new Dictionary<string, string>();


        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.combo; }
        }


        public XMLCombo ()
        {
            _clientRect =  new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Horizontal;
            _name = "XMLCombo";
        }

        private XmlElement appendNode(XmlDocument document, string nodeName, string innerText, XmlNode parent)
        {
            XmlElement element = document.CreateElement(nodeName);
            element.InnerText = innerText;
            parent.AppendChild(element);
            return element;
        }

        public override XmlElement serializeToXml( XmlDocument document)
        {
            XmlElement element = base.serializeToXml(document);            
            
            appendNode(document, "SourceDocument", _documentName, element);
            appendNode(document, "XPath", _XPathExpression, element);            

            XmlElement Listelement = document.CreateElement("List");
            element.AppendChild(Listelement);

            appendNode(document, "SourceDocument", _listDocumentName, Listelement);
            appendNode(document, "XPath", _listXPathExpression, Listelement);
            appendNode(document, "CaptionXPath", _captionXPathExpression, Listelement);
            appendNode(document, "ValueXPath", _valueXPathExpression, Listelement);
            
            return element;             
        }

        public override void deserializeFromXml( XmlNode element )
        {
            _documentName = element.ChildNodes[0].InnerText;
            _XPathExpression = element.ChildNodes[1].InnerText;
            _listDocumentName = element.ChildNodes[2].ChildNodes[0].InnerText;
            _listXPathExpression = element.ChildNodes[2].ChildNodes[1].InnerText;
            _captionXPathExpression = element.ChildNodes[2].ChildNodes[2].InnerText;
            _valueXPathExpression = element.ChildNodes[2].ChildNodes[3].InnerText;
        }


        public override XMLControl Duplicate(System.Drawing.Point position) 
        {
            XMLCombo newControl = new XMLCombo();            
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;
            return newControl;
        }


        private void OnChange(object sender, EventArgs e)
        {            
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
                return;

            IUpdatableWindow parentWnd = comboBox.Parent as IUpdatableWindow;

            if (parentWnd == null || _documentName==null || _listDocumentName==null || _documentName=="" ||_listDocumentName=="" )
                return;


            XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];

            try
            {
                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    return;

                if (caption2ValueDictionary.ContainsKey(comboBox.Text))
                    node.InnerText = caption2ValueDictionary[comboBox.Text];
            }
            catch (System.Xml.XPath.XPathException)
            {
                MessageBox.Show(_XPathExpression);
                comboBox.SelectedIndexChanged -= OnChange;
                comboBox.SelectedIndex = -1;
                comboBox.SelectedIndexChanged += OnChange;
                return;
            }
            catch (Exception)
            {
                MessageBox.Show(_XPathExpression);
                comboBox.SelectedIndexChanged -= OnChange;
                comboBox.SelectedIndex = -1;
                comboBox.SelectedIndexChanged += OnChange;
                return;
            }


            parentWnd.updateVisibleControls();
        }


        public override Control CreateEditorControl()
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Width = _defaultWidth;
            comboBox.Height = _defaultHeight;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;            
            comboBox.SelectedIndexChanged += OnChange;
            return comboBox;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {            
            ComboBox comboBox = EditorControl as ComboBox;
            if (comboBox == null) 
            {
                System.Diagnostics.Trace.WriteLine("XMLComboBox::UpdateEditorControl: control type not Combo");
                return;
            }

            comboBox.TextChanged -= OnChange;
            comboBox.SelectedIndexChanged -= OnChange;

            try
            {
                XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];

                if (source == null)
                    throw new Exception("Invalid document: " + _documentName);

                XmlDocument listSource = XmlSourceDocumentManager.Instance()[_listDocumentName];

                if (listSource == null)
                    throw new Exception("Invalid list document: " + _listDocumentName);


                XmlNodeList nodeList = listSource.SelectNodes(_listXPathExpression);

                comboBox.BeginUpdate();
                comboBox.Items.Clear();
                caption2ValueDictionary.Clear();
                value2CaptionDictionary.Clear();

                bool uniqueValueList = true;



                XPathExpression exprCaption = XPathExpression.Compile(_captionXPathExpression);
                XPathExpression exprValue = XPathExpression.Compile(_valueXPathExpression);


                foreach (XmlNode node in nodeList)
                {

                    XPathNavigator navigatorCaption = node.CreateNavigator();
                    XPathNavigator navigatorValue = node.CreateNavigator();

                    string caption = "";
                    string value = "";

                    switch (exprCaption.ReturnType)
                    {
                        case XPathResultType.NodeSet:
                            XPathNodeIterator nodes = (XPathNodeIterator)navigatorCaption.Evaluate(exprCaption);
                            if (nodes.Count == 0)
                                throw new Exception("Selected node was not found: " + _XPathExpression);

                            nodes.MoveNext();
                            caption = nodes.Current.Value.ToString();
                            break;
                        default:
                            caption = navigatorCaption.Evaluate(exprCaption).ToString();
                            break;
                    }

                    switch (exprValue.ReturnType)
                    {
                        case XPathResultType.NodeSet:
                            XPathNodeIterator nodes = (XPathNodeIterator)navigatorValue.Evaluate(exprValue);
                            if (nodes.Count == 0)
                                throw new Exception("Selected node was not found: " + _XPathExpression);

                            nodes.MoveNext();
                            value = nodes.Current.Value.ToString();
                            break;
                        default:
                            value = navigatorValue.Evaluate(exprValue).ToString();
                            break;
                    }

                    caption2ValueDictionary.Add(caption, value);
                    try
                    {
                        value2CaptionDictionary.Add(value, caption);
                    }
                    catch (ArgumentException)
                    {
                        value2CaptionDictionary.Clear();
                        uniqueValueList = false;

                    }

                
                    comboBox.Items.Add(caption);
                }
                comboBox.EndUpdate();


                XmlNode xpathNode = source.SelectSingleNode(_XPathExpression);
                if (xpathNode == null)
                    throw new Exception("Selected node was not found: " + _XPathExpression);

                if (uniqueValueList && value2CaptionDictionary.ContainsKey(xpathNode.InnerText))
                    comboBox.Text = value2CaptionDictionary[xpathNode.InnerText];
                else
                    comboBox.SelectedIndex = -1;

                comboBox.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException e)
            {
                comboBox.Text = e.Message;
                comboBox.Enabled = false;
            }
            catch (Exception e)
            {
                comboBox.Text = e.Message;
                comboBox.Enabled = false;
            }
            finally
            {
                comboBox.EndUpdate();
            }

            comboBox.SelectedIndexChanged += OnChange;
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            XMLListDataSourcePropertyControl wnd = new XMLListDataSourcePropertyControl();
            wnd.Text = "Combo properties";
            wnd.textBoxXPath.Text = _XPathExpression;
            wnd.cbSourceDocuments.Text = _documentName;
            wnd.cbListSourceDocument.Text = _listDocumentName;
            wnd.textBoxList.Text = _listXPathExpression;
            wnd.textBoxCaption.Text = _captionXPathExpression;
            wnd.textBoxValue.Text = _valueXPathExpression;
            return wnd;
        }

        public override void SetDataSource(IDataSourceBase dataSource)
        {            
            IListDataSource dS = dataSource as IListDataSource;
            if (dS == null)
                return;

            _XPathExpression = dS.getXPathExpression();
            _documentName = dS.getDocumentName();
            _listDocumentName = dS.getListDocumentName();
            _listXPathExpression = dS.getListXPathExpression();
            _valueXPathExpression = dS.getListValueXPathExpression();
            _captionXPathExpression = dS.getListCaptionXPathExpression();            
        }

    }
}
