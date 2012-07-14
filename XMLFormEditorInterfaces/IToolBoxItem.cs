using System;
using System.Windows.Forms;
using System.Drawing;

namespace XMLFormEditor
{
    public interface IToolBoxItem
    {
        Cursor getToolBoxCursor();
        XMLControl Duplicate(System.Drawing.Point position);

        void DrawItem(Graphics g, Size size);
    }
}
