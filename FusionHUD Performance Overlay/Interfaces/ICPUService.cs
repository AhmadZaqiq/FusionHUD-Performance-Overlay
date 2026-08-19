namespace FusionHUD_Performance_Overlay.Interfaces
{
    public interface ICPUService
    {
        string CPUName { get; }

        float GetCPUUsage();

        string GetCPUTemperature();
    }
}