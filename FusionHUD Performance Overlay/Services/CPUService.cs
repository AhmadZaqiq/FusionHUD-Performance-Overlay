using FusionHUD_Performance_Overlay.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace FusionHUD_Performance_Overlay.Services
{
    public class CPUService : ICPUService, IDisposable
    {
        private const string AMD_DLL = "FusionHUD.AMD.dll";

        private readonly PerformanceCounter _CPUUsageCounter;

        private readonly List<float> _CPUUsageHistory = new List<float>();

        private readonly List<float> _CPUTemperatureHistory = new List<float>();

        private bool _AMDInitialized;

        private bool _Disposed;

        [DllImport(AMD_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "InitAMDMonitor")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool InitAMDMonitor();

        [DllImport(AMD_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetCPUTemperature")]
        private static extern double GetCPUTemperatureNative();

        [DllImport(AMD_DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ShutdownAMDMonitor")]
        private static extern void ShutdownAMDMonitor();

        public string CPUName => "R5 3600";

        public CPUService()
        {
            _CPUUsageCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            _CPUUsageCounter.NextValue();

            InitializeAMD();
        }

        private void InitializeAMD()
        {
            try
            {
                _AMDInitialized = InitAMDMonitor();
            }
            catch
            {
                _AMDInitialized = false;
            }
        }

        public float GetCPUUsage()
        {
            if (_Disposed)
            {
                return 0;
            }

            try
            {
                float CPUUsage = _CPUUsageCounter.NextValue();

                _CPUUsageHistory.Add(CPUUsage);

                if (_CPUUsageHistory.Count > 5)
                {
                    _CPUUsageHistory.RemoveAt(0);
                }

                return _CPUUsageHistory.Average();
            }
            catch
            {
                return 0;
            }
        }

        public string GetCPUTemperature()
        {
            if (_Disposed || !_AMDInitialized)
            {
                return "N/A";
            }

            try
            {
                float Temperature = (float)GetCPUTemperatureNative();

                if (Temperature <= 0 || Temperature > 150)
                {
                    return "N/A";
                }

                _CPUTemperatureHistory.Add(Temperature);

                if (_CPUTemperatureHistory.Count > 5)
                {
                    _CPUTemperatureHistory.RemoveAt(0);
                }

                float Average = _CPUTemperatureHistory.Average();

                return $"{Average:F0}°C";
            }
            catch
            {
                _AMDInitialized = false;

                return "N/A";
            }
        }

        public void Dispose()
        {
            if (_Disposed)
            {
                return;
            }

            try
            {
                if (_AMDInitialized)
                {
                    ShutdownAMDMonitor();

                    _AMDInitialized = false;
                }
            }
            catch
            {
            }

            _CPUUsageCounter.Dispose();

            _Disposed = true;
        }
    }

}