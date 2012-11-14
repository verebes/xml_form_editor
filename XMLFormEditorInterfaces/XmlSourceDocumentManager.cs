using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace XMLFormEditor
{
    public class XmlSourceDocumentManager
    {        
        private Dictionary<XmlDocument, String> xmlDocument2Filename;
        private Dictionary<String, XmlDocument> fileName2XmlDocument;

        private List<XmlDocument> SourceDocuments;


        public event System.EventHandler OnDocumentListChanged = delegate { };


        private static XmlSourceDocumentManager instance;
        public static XmlSourceDocumentManager Instance()
        {
            if (instance == null)
                instance = new XmlSourceDocumentManager();

            return instance;
        }

        private XmlSourceDocumentManager()
        {
            xmlDocument2Filename = new Dictionary<XmlDocument, String>();
            fileName2XmlDocument = new Dictionary<String, XmlDocument>();
            SourceDocuments = new List<XmlDocument>();
        }


        public void Clear()
        {
            xmlDocument2Filename.Clear();
            fileName2XmlDocument.Clear();
            SourceDocuments.Clear();
        }


        protected ValidationResult currentValidateionResult = null;

        public void AddDocument(string documentFileName)
        {
            if (fileName2XmlDocument.ContainsKey(documentFileName))
                throw new Exception("Document already imported");

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            
            doc.Load( documentFileName);

            SourceDocuments.Add(doc);
            fileName2XmlDocument.Add(documentFileName, doc);
            xmlDocument2Filename.Add(doc, documentFileName);

            OnDocumentListChanged(this, null);
        }

        public void RemoveDocument(string documentFileName)
        {
            XmlDocument doc = fileName2XmlDocument[documentFileName];
            
            SourceDocuments.Remove(doc);
            xmlDocument2Filename.Remove(doc);
            fileName2XmlDocument.Remove(documentFileName);

            OnDocumentListChanged(this, null);
        }

        public void NewDocument(string documentFileName)
        {
            if (fileName2XmlDocument.ContainsKey(documentFileName))
                throw new Exception("Document already imported");

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;

            doc.LoadXml("<document></document>");
            doc.Save(documentFileName);

            SourceDocuments.Add(doc);
            fileName2XmlDocument.Add(documentFileName, doc);
            xmlDocument2Filename.Add(doc, documentFileName);

            OnDocumentListChanged(this, null);           
        }

        public XmlDocument GetDocument( string sourceFileName ) 
        {
            if ( !fileName2XmlDocument.ContainsKey(sourceFileName) )  {
                return null;
            } else {
                return fileName2XmlDocument[sourceFileName];
            }
        }

        public void LoadDocuments()
        {
            foreach ( KeyValuePair<string, XmlDocument> item in fileName2XmlDocument )
            {
                item.Value.Load(item.Key);                
            }
        }

        public void SaveDocuments()
        {
            foreach (KeyValuePair<string, XmlDocument> item in fileName2XmlDocument)
            {
                item.Value.Save(item.Key);
            }
        }

        public List<ValidationResult> ValidateDocuments()
        {
            List<ValidationResult> res = new List<ValidationResult>();
            foreach (KeyValuePair<string, XmlDocument> item in fileName2XmlDocument)
            {
                if (item.Key.ToLower().EndsWith(".xsd"))
                    continue;

                currentValidateionResult = new ValidationResult(item.Key);
                Validate(item.Value);
                res.Add(currentValidateionResult);
            }
            currentValidateionResult = null;
            return res;
        }

        public int DocumentCount
        {
            get {return SourceDocuments.Count; }
        }

        public List<string> GetDocumentNames()
        {
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, XmlDocument> item in fileName2XmlDocument)
            {                           
                ret.Add(item.Key);
            }

            return ret;
        }

        public XmlDocument this [string documentName]
        {
            get
            {
                if ( documentName == null || documentName == "" )
                    return null;
                return fileName2XmlDocument[documentName];
            }
        }

        private void Validate(XmlDocument xmlDoc)
        {
            XmlTextReader reader = null;
            XmlValidatingReader vReader = null;
            try
            {
                reader = new XmlTextReader(new System.IO.StringReader(xmlDoc.OuterXml));
                vReader = new XmlValidatingReader(reader);
                vReader.ValidationType = ValidationType.Schema;
                vReader.ValidationEventHandler += new ValidationEventHandler(this.ValidationCallBack);
                while (vReader.Read()) { }
            }
            catch { }
            finally
            {
                vReader.Close();
                reader.Close();
            }
        }



        protected void ValidationCallBack(object sender, ValidationEventArgs args)
        {            
            string s = "Line: " + args.Exception.LineNumber.ToString();
            s+= " Pos: " + args.Exception.LinePosition.ToString();
            s += ": " + args.Message;

            currentValidateionResult.add( s, args.Severity);
        }
    }


    public class ValidationResult
    {
        public ValidationResult(string fileName)
        {
            _fileName = fileName;
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
        }

        private List<string> _warnings = new List<string>();
        public List<string> Warnings
        {
            get { return _warnings; }
        }

        private List<string> _errors = new List<string>();
        public List<string> Errors
        {
            get { return _errors; }
        }

        public void clear()
        {
            _warnings.Clear();
            _errors.Clear();
        }

        public void add(string message, XmlSeverityType severity)
        {
            if (severity == XmlSeverityType.Warning)
                _warnings.Add(message);
            if (severity == XmlSeverityType.Error)
                _errors.Add(message);
        } 
    }
}
