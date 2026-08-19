using System;
using System.Runtime.InteropServices;

namespace FusionHUD_Performance_Overlay.RTSS
{
    public class ForegroundProcessReader
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr WindowHandle, out uint ProcessID);

        public uint GetForegroundProcessID()
        {
            IntPtr WindowHandle = GetForegroundWindow();

            GetWindowThreadProcessId(WindowHandle, out uint ProcessID);

            return ProcessID;
        }

    }
}