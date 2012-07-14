using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XMLFormEditor
{
    public class DocumentLayoutCollection
    {
        private List<DocumentLayout> _layouts;
        
        private string _layoutFileName;
        public string LayoutFileName
        {
            get { return _layoutFileName; }
        }

        private System.Xml.XmlDocument _layoutXmlDocument;


        public IControlFactory controlFactory;

        public DocumentLayoutCollection()
        {
            _layoutXmlDocument = new XmlDocument();            
            _layouts = new List<DocumentLayout>();
        }

        public DocumentLayout this [int i]
        {
            get { return _layouts[i]; }
        }

        public int LayoutCount
        {
            get { return _layouts.Count; }                
        }

        public DocumentLayout CreateDocumentLayout()
        { 
            DocumentLayout layout = new DocumentLayout();
            layout.controlFactory = controlFactory;
            _layouts.Add(layout);

            return layout;
        }


        public void AddDocumentLayout( DocumentLayout layout )
        {
            _layouts.Add(layout);
        }

        public void RemoveDocumentLayout(DocumentLayout layout)
        {
            _layouts.Remove(layout);
        }

        public void RemoveDocumentLayout(int layoutIndex)
        {
            _layouts.RemoveAt(layoutIndex);
        }


        public bool Save()
        {
            if (_layoutFileName == null || _layoutFileName == "")
                return false;

            _layoutXmlDocument.RemoveAll();            

            XmlElement rootElement = _layoutXmlDocument.CreateElement("Document");
            _layoutXmlDocument.AppendChild(rootElement);
                
            // saving the source document paths to the output xml
            List<string> docNames = XmlSourceDocumentManager.Instance().GetDocumentNames();
            foreach (string name in docNames)
            {
                XmlElement source = _layoutXmlDocument.CreateElement("source");
                source.InnerText = name;
                rootElement.AppendChild(source);
            }            

            // saving the content of the forms
            foreach ( DocumentLayout layout in _layouts )
            {
                rootElement.AppendChild(layout.SerializeToXml( _layoutXmlDocument ));
            }
            
            
            _layoutXmlDocument.Save(_layoutFileName);
            
            return true;
        }

        public void New()
        {
            _layoutFileName = "";
            XmlSourceDocumentManager.Instance().Clear();
            _layouts.Clear();

            for (int i = 0; i < 2; i++)
            {

                DocumentLayout layout = new DocumentLayout();
                layout.controlFactory = controlFactory;

                _layouts.Add(layout);
            }
        }

        public void Load(string fileName)
        {
            _layoutXmlDocument.Load(fileName);          
            _layoutFileName = fileName;


            _layouts.Clear();
            XmlSourceDocumentManager.Instance().Clear();

            XmlElement rootElement = _layoutXmlDocument.DocumentElement;

            foreach ( XmlNode node in rootElement.ChildNodes )
            {
                if (node.Name.ToLower() == "source") 
                {
                    XmlSourceDocumentManager.Instance().AddDocument(node.InnerText);
                }
                else if (node.Name.ToLower() == "form")
                {
                    DocumentLayout layout = new DocumentLayout();
                    layout.controlFactory = controlFactory;

                    layout.deserializeFromXml(node, false);

                    _layouts.Add(layout);
                }
            }


        }

        public void SaveAs(string fileName)
        {
            _layoutFileName = fileName;
            Save();
        }
    }
}
