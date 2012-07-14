using System;
using System.Collections.Generic;
using System.Text;

namespace XMLFormEditor
{   
    public interface IControlFactory
    {
        XMLControl createControl(string controlType);
    }
}
