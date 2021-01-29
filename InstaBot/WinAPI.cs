using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace InstaBot
{
    public class WinAPI
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public  static extern int SetForegroundWindow(IntPtr hWnd); 

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowPos(
    IntPtr hWnd,
    IntPtr hWndInsertAfter,
  int X,
  int Y,
  int cx,
  int cy,
  uint uFlags
);


        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow(); 
    }
}
