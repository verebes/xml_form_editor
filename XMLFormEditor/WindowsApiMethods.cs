using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using System.Runtime.InteropServices;

    
public sealed class WindowsApiMethods
{

    private const int WM_SETREDRAW = 0x000B;
    public static void SuspendDrawing(Control c)
    {

        if (c == null)
            throw new ArgumentNullException("c");
        
        WindowsApiMethods.SendMessage(c.Handle, (Int32)WM_SETREDRAW, (Int32)0, (Int32)0);
}

 

    public static void ResumeDrawing(Control c)
    {
        if (c == null)
            throw new ArgumentNullException("c");
        
        WindowsApiMethods.SendMessage(c.Handle, (Int32)WM_SETREDRAW, (Int32)1, (Int32)0);
        c.Refresh();
    }

    [DllImport("User32")]
    private static extern IntPtr SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);


    private WindowsApiMethods()
    {
    }

}
