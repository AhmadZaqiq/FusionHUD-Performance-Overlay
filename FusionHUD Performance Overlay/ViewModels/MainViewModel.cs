using FusionHUD_Performance_Overlay.Interfaces;
using FusionHUD_Performance_Overlay.Models;
using FusionHUD_Performance_Overlay.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FusionHUD_Performance_Overlay.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IPerformanceMonitorService _PerformanceMonitorService;

        private string _OverlayText;

        private IFPSService _FPSService;

        public string OverlayText
        {
            get
            {
                return _OverlayText;
            }

            set
            {
                _OverlayText = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IPerformanceMonitorService PerformanceMonitorService)
        {
            _PerformanceMonitorService = PerformanceMonitorService;
        }

        public void Update()
        {
            PerformanceData Data = _PerformanceMonitorService.GetPerformanceData();

            string GPUName = Data.GPUName.Contains("RTX 4060") ? "RTX 4060" : Data.GPUName;

            OverlayText =$"FPS {Data.FPS} | {GPUName} {Data.GPUUsage} {Data.GPUTemperature} {Data.VRAM} | {Data.CPUName} {Data.CPUUsage} {Data.CPUTemperature} | RAM {Data.RAMUsage}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

    }
}