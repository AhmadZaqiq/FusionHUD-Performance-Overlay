using FusionHUD_Performance_Overlay.Models;

namespace FusionHUD_Performance_Overlay.Interfaces
{
    public interface IPerformanceMonitorService
    {
        PerformanceData GetPerformanceData();
    }
}
