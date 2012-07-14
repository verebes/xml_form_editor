using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace XMLFormEditor
{
    public interface IEditorControl
    {
        System.Windows.Forms.Control CreateEditorControl();
        XMLPropertyControlBase GetPropertyWindow();
        XmlElement serializeToXml(XmlDocument document);
    }
}
