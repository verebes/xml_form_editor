using System;
using System.Collections.Generic;
using System.Text;

namespace XMLFormEditor
{
    public class DefaultControlFactory: IControlFactory
    {
        public XMLControl createControl( string controlType )
        { 
            switch ( controlType)
            {
                case "XMLTextBox":
                    return new XMLTextBox();
                case "XMLLabel":
                    return new XMLLabel();
                case "StaticLabel":
                    return new StaticLabel();
                case "XMLCombo":
                    return new XMLCombo();
                case "XMLList":
                    return new XMLList();
                case "XMLLargeTextBox":
                    return new XMLLargeTextBox();
                case "XMLInsertButton":
                    return new XMLInsertButton();
                case "XMLPager":
                    return new XMLPager();
                case "XMLSchemaControl":
                    return new XMLSchemaControl();
                default:
                    System.Diagnostics.Trace.TraceError("DefaultControlFactory::No control type found: " + controlType);
                    return null;
            }
        }
    }
}
