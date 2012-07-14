using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;


namespace XMLFormEditor
{
    public class XMLSchemaControl: XMLControl
    {
        protected string _documentName;
        protected string _schemaName;
        protected string _XPathExpression;

        const int _defaultWidth = 200;
        const int _defaultHeight = 21;

        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.pager; }
        }

        
        public XMLSchemaControl()
        {
            _clientRect = new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Horizontal;
            _name = "XMLSchemaControl";            
        }

        public override XmlElement serializeToXml(XmlDocument document)
        {
            XmlElement element = base.serializeToXml(document);

            XmlElement DocumentElement = document.CreateElement("SourceDocument");
            DocumentElement.InnerText = _documentName;
            element.AppendChild(DocumentElement);

            XmlElement SchemaFileElement = document.CreateElement("SchemaFile");
            SchemaFileElement.InnerText = _schemaName;
            element.AppendChild(SchemaFileElement);

            XmlElement XPathElement = document.CreateElement("XPath");
            XPathElement.InnerText = _XPathExpression;
            element.AppendChild(XPathElement);

            return element;
        }

        public override void deserializeFromXml(XmlNode element)
        {
            _documentName = element.ChildNodes[0].InnerText;
            _schemaName = element.ChildNodes[1].InnerText;            
            _XPathExpression = element.ChildNodes[2].InnerText;
        }


        public override XMLControl Duplicate(System.Drawing.Point position)
        {
            XMLSchemaControl newControl = new XMLSchemaControl();
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;
            return newControl;
        }


        private void OnChange(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null)
                return;

            IUpdatableWindow parentWnd = control.Parent.Parent as IUpdatableWindow;

            if (parentWnd == null || _documentName == "")
                return;

            XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];
            
            try
            {
                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    return;

                node.InnerText = control.Text;
                control.Enabled = true;
            }
            catch (System.Xml.XPath.XPathException exception)
            {
                System.Diagnostics.Trace.TraceError("XMLSchemaControl::OnChange: xpathexception: " + exception.Message);
                return;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Trace.TraceError("XMLSchemaControl::OnChange: exception: " + exception.Message);
                return;
            }


            parentWnd.updateVisibleControls();
        }


        public override Control CreateEditorControl()
        {
            MultiControl control = new MultiControl();
            control.Width = _defaultWidth;
            control.Height = _defaultHeight;            
            control.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            control.comboBox.SelectedIndexChanged += OnChange;
            control.textBox.TextChanged += OnChange;
            control.Type = MultiControl.ControlType.ComboBox;
            return control;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {
            MultiControl control = EditorControl as MultiControl;

            if (control == null)
            {
                System.Diagnostics.Trace.WriteLine("XMLSchemaControl::UpdateEditorControl: control type is not valid");
                return;
            }
            
            control.comboBox.SelectedIndexChanged -= OnChange;
            control.textBox.TextChanged -= OnChange;

            try
            {
                XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];
                if (source == null)
                    throw new Exception("Invalid document: " + _documentName);

                source.Schemas.Add("", _schemaName);
                if (!source.Schemas.IsCompiled)
                    source.Schemas.Compile();


                source.Validate(new ValidationEventHandler(this.ValidationCallBack));


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

                control.comboBox.Items.Clear();
                List<string> enumValues = getEnumValues(node);

                if ( enumValues == null || enumValues.Count == 0)
                {
                    control.Type = MultiControl.ControlType.TextBox;
                } else if (enumValues.Count == 1)
                {
                    control.Type = MultiControl.ControlType.Label;
                }
                else
                {
                    control.Type = MultiControl.ControlType.ComboBox;
                    foreach (string s in enumValues)
                        control.comboBox.Items.Add(s);                    
                }

                control.Enabled = true;
                control.Text = node.InnerText;
            }
            catch (System.Xml.XPath.XPathException)
            {
                control.Type = MultiControl.ControlType.Label;
                control.Text = _XPathExpression;
                control.Enabled = false;
            }
            catch (Exception e)
            {
                control.Type = MultiControl.ControlType.Label;
                control.Text = e.Message;
                control.Enabled = false;
            }

            control.comboBox.SelectedIndexChanged += OnChange;
            control.textBox.TextChanged += OnChange;
            
        }


        public override XMLPropertyControlBase GetPropertyWindow()
        {
            SchemaPropertyControl wnd = new SchemaPropertyControl();
            wnd.Text = "SchemaControl properties";
            wnd.cbSourceDocuments.Text = _documentName;
            wnd.textBoxXPath.Text = _XPathExpression;
            wnd.cbSchemaName.Text = _schemaName;
            return wnd;
        }

        public override void SetDataSource(IDataSourceBase dataSource)
        {
            ISchemaControlDataSource dS = dataSource as ISchemaControlDataSource;
            if (dS == null)
                return;

            _documentName = dS.getDocumentName();
            _XPathExpression = dS.getXPathExpression();
            _schemaName = dS.getSchemaName();
        }

        protected void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            string s = "Line: " + args.Exception.LineNumber.ToString();
            s += " Pos: " + args.Exception.LinePosition.ToString();
            s += ": " + args.Message;
        }

        private List<string> getEnumValues(XmlNode node)
        {
            List<string> result = new List<string>();
            
            IXmlSchemaInfo schemaInfo = node.SchemaInfo;
            if (schemaInfo == null)
                return null;


            XmlSchemaSimpleType simpleType;
            if (node.NodeType == XmlNodeType.Attribute)
            {
                XmlSchemaAttribute schemaAttr = schemaInfo.SchemaAttribute;
                simpleType = schemaAttr.AttributeSchemaType as XmlSchemaSimpleType;
            }
            else if (node.NodeType == XmlNodeType.Element)
            {
                XmlSchemaElement schemaElement = schemaInfo.SchemaElement;
                simpleType = schemaElement.ElementSchemaType as XmlSchemaSimpleType;

                if ( simpleType == null )
                {
                    XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
                    simpleType = complexType.BaseSchemaType as XmlSchemaSimpleType;
                }

            }
            else
            {
                return null;
            }


            if (simpleType == null)
                return null;            

            XmlSchemaSimpleTypeRestriction restriction = simpleType.Content as XmlSchemaSimpleTypeRestriction;
            if (restriction == null)
                return null;


            foreach (XmlSchemaObject o in restriction.Facets)
            {
                XmlSchemaEnumerationFacet enums = o as XmlSchemaEnumerationFacet;
                if (enums == null)
                    continue;

                result.Add(enums.Value);
            }

            return result;
        }



    }
}
