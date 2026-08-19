namespace FusionHUD_Performance_Overlay.Models
{
    public class PerformanceData
    {
        public string FPS { get; set; }

        public string GPUName { get; set; }
        public string GPUUsage { get; set; }
        public string GPUTemperature { get; set; }
        public string VRAM { get; set; }

        public string CPUName { get; set; }
        public string CPUUsage { get; set; }
        public string CPUTemperature { get; set; }

        public string RAMUsage { get; set; }
    }
}
