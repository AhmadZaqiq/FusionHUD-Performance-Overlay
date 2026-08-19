namespace FusionHUD_Performance_Overlay.Interfaces
{
    public interface IGPUService
    {
        string GPUName { get; }

        string GetGPUUsage();

        string GetGPUTemperature();

        string GetVRAMUsage();
    }
}
