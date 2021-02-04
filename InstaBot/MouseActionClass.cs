using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InstaBot
{
    internal class MouseAction
    {
        /// <summary>
        /// Immitates mouse`s buttons pressing
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("User32.Dll")]
        private static extern long SetCursorPos(uint x, uint y);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        //
        public static void MousePressLeft(uint x, uint y, Random random)
        {
            SetCursorPos(x, y);

            Thread.Sleep(random.Next(2000, 2500));  // immitates pause like a human

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }
    }
}
