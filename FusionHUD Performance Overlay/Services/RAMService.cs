using FusionHUD_Performance_Overlay.Interfaces;
using System.Runtime.InteropServices;

namespace FusionHUD_Performance_Overlay.Services
{
    public class RAMService : IRAMService
    {
        public string GetRAMUsage()
        {
            MEMORYSTATUSEX Memory = new MEMORYSTATUSEX();

            Memory.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));

            GlobalMemoryStatusEx(Memory);

            double Used = (Memory.ullTotalPhys - Memory.ullAvailPhys) / 1024.0 / 1024.0 / 1024.0;

            return $"{Used:F1}GB";
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;

            public ulong ullTotalPhys;
            public ulong ullAvailPhys;

            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;

            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;

            public ulong ullAvailExtendedVirtual;
        }

        [DllImport("kernel32.dll")]
        private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX MemoryStatus);

    }
}