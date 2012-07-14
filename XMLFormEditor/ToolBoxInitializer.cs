using System;
using System.Collections.Generic;
using System.Text;

namespace XMLFormEditor
{
    public class ToolBoxInitializer
    {
        private XmlEditorToolBox _toolBox;

        public ToolBoxInitializer ( XmlEditorToolBox toolBox) {
            _toolBox = toolBox;
        }

        public void InitializeToolBox()
        {
            _toolBox.AddItem(new StaticLabel());
            _toolBox.AddItem(new XMLLabel());
            _toolBox.AddItem(new XMLTextBox());
            _toolBox.AddItem(new XMLCombo());
            _toolBox.AddItem(new XMLList());
            _toolBox.AddItem(new XMLLargeTextBox());
            _toolBox.AddItem(new XMLInsertButton());
            _toolBox.AddItem(new XMLPager());
            _toolBox.AddItem(new XMLSchemaControl());
        }
    }
}


