using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public class DocumentFormVisualizer : DocumentVisualizer, IUpdatableWindow
    {

        public override DocumentLayout DocumentLayout
        {
            set
            {
                base.DocumentLayout = value;

                recreateControls();
            }
        }

        public override void doRecreateControls()
        {            
            base.doRecreateControls();
            updateVisibleControls();
         
            foreach (KeyValuePair<Control, XMLControl> p in Control2XmlControlDictionary)
            {
                p.Key.TabStop = true;
            }

        }
    }
}
