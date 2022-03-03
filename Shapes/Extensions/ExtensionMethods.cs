using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Runtime.InteropServices;

namespace Shapes.Extensions
{
    public static class ExtensionMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();
        public static void ShowWindow(this Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            helper.Owner = GetForegroundWindow();
            window.ShowDialog();
        }
    }
}
