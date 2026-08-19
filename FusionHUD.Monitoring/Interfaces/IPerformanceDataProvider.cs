using FusionHUD_Performance_Overlay.Models;

namespace FusionHUD.Monitoring.Interfaces
{
    public interface IPerformanceDataProvider
    {
        PerformanceData GetPerformanceData();
    }
}