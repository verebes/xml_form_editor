using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Specialized;

namespace XMLFormEditor
{
    public class XMLGrid: XMLControl
    {
        protected string _documentName;
        protected string _XPathExpression;        
        protected string _listDocumentName;
        protected string _listXPathExpression;
        protected string _captionXPathExpression;
        protected string _valueXPathExpression;


        const int _defaultWidth = 200;
        const int _defaultHeight = 200;


        protected StringDictionary caption2ValueDictionary = new StringDictionary();
        protected StringDictionary value2CaptionDictionary = new StringDictionary();


        public override Icon ListIcon
        {
            get { return XMLToolboxItems.Properties.Resources.combo; }
        }


        public XMLGrid ()
        {
            _clientRect =  new Rectangle(0, 0, _defaultWidth, _defaultHeight);
            ResizeMode = ResizeMode.Both;
            _name = "XMLGrid";
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
            XMLGrid newControl = new XMLGrid();            
            Rectangle r = ClientRect;
            r.Location = position;
            newControl.ClientRect = r;
            return newControl;
        }


        private void OnChange(object sender, EventArgs e)
        {                        
            DataGridView dataGrid = sender as DataGridView;
            if (dataGrid == null)
                return;

            IUpdatableWindow parentWnd = dataGrid.Parent as IUpdatableWindow;

            if (parentWnd == null || _documentName==null || _listDocumentName==null || _documentName=="" ||_listDocumentName=="" )
                return;


            XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];

            try
            {
                XmlNode node = source.SelectSingleNode(_XPathExpression);
                if (node == null)
                    return;

                if (caption2ValueDictionary.ContainsKey(dataGrid.Text))
                    node.InnerText = caption2ValueDictionary[dataGrid.Text];
            }
            catch (System.Xml.XPath.XPathException)
            {
                MessageBox.Show(_XPathExpression);
                //dataGrid.SelectedIndexChanged -= OnChange;
                //dataGrid.SelectedIndex = -1;
                //dataGrid.SelectedIndexChanged += OnChange;
                return;
            }
            catch (Exception)
            {
                MessageBox.Show(_XPathExpression);
                //dataGrid.SelectedIndexChanged -= OnChange;
                //dataGrid.SelectedIndex = -1;
                //dataGrid.SelectedIndexChanged += OnChange;
                return;
            }


            parentWnd.updateVisibleControls();
        }


        public override Control CreateEditorControl()
        {
            DataGridView dataGrid = CreateControl();

            AutoSizeCells(dataGrid);

            dataGrid.Rows[0].Cells[0].Value = "xxxx";
            dataGrid.Rows[1].Cells[1].Value = "xxxx";
            dataGrid.Rows[2].Cells[2].Value = "xxxx";

            //dataGrid.DropDownStyle = ComboBoxStyle.DropDownList;            
            //dataGrid.SelectedIndexChanged += OnChange;            
            return dataGrid;
        }

        private DataGridView CreateControl() {
            DataGridView dataGrid = new DataGridView();

            dataGrid.BorderStyle = BorderStyle.FixedSingle;
            dataGrid.Width = _defaultWidth;
            dataGrid.Height = _defaultHeight;
            dataGrid.Columns.Add("alma", "headerText");
            dataGrid.Columns.Add("barack", "headerText2");
            dataGrid.Columns.Add("korte", "headerText3");
            dataGrid.ScrollBars = ScrollBars.None;
            dataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGrid.ShowEditingIcon = false;
            dataGrid.Rows.Add(3);
            dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGrid.RowHeadersWidth = 4;
            dataGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGrid_RowPrePaint);
            dataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGrid_CellFormatting);
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToOrderColumns = false;
            dataGrid.AllowUserToResizeColumns = false;
            dataGrid.AllowUserToResizeRows = false;
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.MultiSelect = false;
            dataGrid.ShowRowErrors = false;
            dataGrid.RowHeadersVisible = false;

            dataGrid.Resize += new EventHandler(dataGrid_Resize);
            return dataGrid;
        }

        void dataGrid_Resize(object sender, EventArgs e) {
            DataGridView dataGrid = sender as DataGridView;
            if ( dataGrid == null ) {
                return;
            }

            AutoSizeCells(dataGrid);
        }

        private static void AutoSizeCells(DataGridView dataGrid) {
            int newWidth = (dataGrid.Width) / dataGrid.Columns.Count -1;
            for (int i = 0; i < dataGrid.Columns.Count; ++i) {
                dataGrid.Columns[i].Width = newWidth;
            }
            
            int newHeight = (dataGrid.Height )/ dataGrid.Rows.Count -1;
            for (int i = 0; i < dataGrid.Rows.Count; ++i) {
                dataGrid.Rows[i].Height= newHeight;
            }

        }

        void dataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            DataGridView dataGrid = sender as DataGridView;
            if ( dataGrid == null )
                return;

                dataGrid.Rows[e.RowIndex].HeaderCell.Value = e.RowIndex.ToString();
        }

        void dataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) {
            e.PaintHeader(DataGridViewPaintParts.Background
                | DataGridViewPaintParts.Border
                | DataGridViewPaintParts.Focus
                | DataGridViewPaintParts.SelectionBackground
                | DataGridViewPaintParts.ContentForeground);
            

            e.Handled = false;
        }

        public override void UpdateEditorControl(Control EditorControl)
        {            
            DataGridView dataGrid = EditorControl as DataGridView;
            if (dataGrid == null) 
            {
                System.Diagnostics.Trace.WriteLine("XMLComboBox::UpdateEditorControl: control type not Combo");
                return;
            }

            //dataGrid.TextChanged -= OnChange;
            //dataGrid.SelectedIndexChanged -= OnChange;

            //try
            //{
            //    XmlDocument source = XmlSourceDocumentManager.Instance()[_documentName];

            //    if (source == null)
            //        throw new Exception("Invalid document: " + _documentName);

            //    XmlDocument listSource = XmlSourceDocumentManager.Instance()[_listDocumentName];

            //    if (listSource == null)
            //        throw new Exception("Invalid list document: " + _listDocumentName);


            //    XmlNodeList nodeList = listSource.SelectNodes(_listXPathExpression);

            //    dataGrid.BeginUpdate();
            //    dataGrid.Items.Clear();
            //    caption2ValueDictionary.Clear();
            //    value2CaptionDictionary.Clear();

            //    bool uniqueValueList = true;



            //    XPathExpression exprCaption = XPathExpression.Compile(_captionXPathExpression);
            //    XPathExpression exprValue = XPathExpression.Compile(_valueXPathExpression);


            //    foreach (XmlNode node in nodeList)
            //    {

            //        XPathNavigator navigatorCaption = node.CreateNavigator();
            //        XPathNavigator navigatorValue = node.CreateNavigator();

            //        string caption = "";
            //        string value = "";

            //        switch (exprCaption.ReturnType)
            //        {
            //            case XPathResultType.NodeSet:
            //                XPathNodeIterator nodes = (XPathNodeIterator)navigatorCaption.Evaluate(exprCaption);
            //                if (nodes.Count == 0)
            //                    throw new Exception("Selected node was not found: " + _XPathExpression);

            //                nodes.MoveNext();
            //                caption = nodes.Current.Value.ToString();
            //                break;
            //            default:
            //                caption = navigatorCaption.Evaluate(exprCaption).ToString();
            //                break;
            //        }

            //        switch (exprValue.ReturnType)
            //        {
            //            case XPathResultType.NodeSet:
            //                XPathNodeIterator nodes = (XPathNodeIterator)navigatorValue.Evaluate(exprValue);
            //                if (nodes.Count == 0)
            //                    throw new Exception("Selected node was not found: " + _XPathExpression);

            //                nodes.MoveNext();
            //                value = nodes.Current.Value.ToString();
            //                break;
            //            default:
            //                value = navigatorValue.Evaluate(exprValue).ToString();
            //                break;
            //        }

            //        caption2ValueDictionary.Add(caption, value);
            //        try
            //        {
            //            value2CaptionDictionary.Add(value, caption);
            //        }
            //        catch (ArgumentException)
            //        {
            //            value2CaptionDictionary.Clear();
            //            uniqueValueList = false;

            //        }

                
            //        dataGrid.Items.Add(caption);
            //    }
            //    dataGrid.EndUpdate();


            //    XmlNode xpathNode = source.SelectSingleNode(_XPathExpression);
            //    if (xpathNode == null)
            //        throw new Exception("Selected node was not found: " + _XPathExpression);

            //    if (uniqueValueList && value2CaptionDictionary.ContainsKey(xpathNode.InnerText))
            //        dataGrid.Text = value2CaptionDictionary[xpathNode.InnerText];
            //    else
            //        dataGrid.SelectedIndex = -1;

            //    dataGrid.Enabled = true;
            //}
            //catch (System.Xml.XPath.XPathException e)
            //{
            //    dataGrid.Text = e.Message;
            //    dataGrid.Enabled = false;
            //}
            //catch (Exception e)
            //{
            //    dataGrid.Text = e.Message;
            //    dataGrid.Enabled = false;
            //}
            //finally
            //{
            //    dataGrid.EndUpdate();
            //}

            //dataGrid.SelectedIndexChanged += OnChange;
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
