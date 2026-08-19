using FusionHUD_Performance_Overlay.Interfaces;
using FusionHUD_Performance_Overlay.Models;

namespace FusionHUD_Performance_Overlay.Services
{
    public class PerformanceMonitorService : IPerformanceMonitorService
    {
        private readonly IGPUService _GPUService;

        private readonly ICPUService _CPUService;

        private readonly IRAMService _RAMService;

        private readonly IFPSService _FPSService;

        public PerformanceMonitorService(
            IGPUService GPUService,
            ICPUService CPUService,
            IRAMService RAMService,
            IFPSService FPSService)
        {
            _GPUService = GPUService;

            _CPUService = CPUService;

            _RAMService = RAMService;

            _FPSService = FPSService;
        }

        public PerformanceData GetPerformanceData()
        {
            string GPUName = _GPUService.GPUName;

            GPUName = GPUName.Contains("RTX 4060")
                ? "RTX 4060"
                : GPUName;

            return new PerformanceData
            {
                FPS = _FPSService.GetFPS(),

                GPUName = GPUName,

                GPUUsage = _GPUService.GetGPUUsage(),

                GPUTemperature = _GPUService.GetGPUTemperature(),

                VRAM = _GPUService.GetVRAMUsage(),

                CPUName = _CPUService.CPUName,

                CPUUsage = $"{_CPUService.GetCPUUsage():F0}%",

                CPUTemperature = _CPUService.GetCPUTemperature(),

                RAMUsage = _RAMService.GetRAMUsage()
            };
        }
    }
}
