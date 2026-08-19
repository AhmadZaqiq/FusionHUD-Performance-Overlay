using FusionHUD.Monitoring.Interfaces;
using FusionHUD_Performance_Overlay.Interfaces;
using FusionHUD_Performance_Overlay.Models;

namespace FusionHUD.Monitoring.Services
{
    public class PerformanceDataProvider : IPerformanceDataProvider
    {
        private readonly IPerformanceMonitorService _PerformanceMonitorService;

        public PerformanceDataProvider(
            IPerformanceMonitorService PerformanceMonitorService)
        {
            _PerformanceMonitorService = PerformanceMonitorService;
        }

        public PerformanceData GetPerformanceData()
        {
            return _PerformanceMonitorService.GetPerformanceData();
        }
    }
}
