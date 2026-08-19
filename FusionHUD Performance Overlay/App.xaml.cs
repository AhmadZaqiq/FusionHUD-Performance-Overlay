using FusionHUD_Performance_Overlay.Interfaces;
using FusionHUD_Performance_Overlay.RTSS;
using FusionHUD_Performance_Overlay.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace FusionHUD_Performance_Overlay
{
    public partial class App : Application
    {
        private IHost? _Host;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            HostApplicationBuilder Builder =
                Host.CreateApplicationBuilder();

            Builder.Services.AddSingleton<IGPUService, GPUService>();

            Builder.Services.AddSingleton<ICPUService, CPUService>();

            Builder.Services.AddSingleton<IRAMService, RAMService>();

            Builder.Services.AddSingleton<IFPSService, FPSService>();

            Builder.Services.AddSingleton<IRTSSReader, RTSSReader>();

            Builder.Services.AddSingleton<IPerformanceMonitorService, PerformanceMonitorService>();

            _Host = Builder.Build();

            await _Host.StartAsync();

            IRTSSReader RTSSReader =
                new RTSSReader();

            IFPSService FPSService =
                new FPSService(RTSSReader);

            IGPUService GPUService =
                new GPUService();

            ICPUService CPUService =
                new CPUService();

            IRAMService MemoryService =
                new RAMService();

            IPerformanceMonitorService PerformanceMonitorService =
                new PerformanceMonitorService(
                    GPUService,
                    CPUService,
                    MemoryService,
                    FPSService);

            IHotkeyService HotkeyService =
                new HotkeyService();

            IStartupService StartupService =
                new StartupService();

            IOverlaySettingsService OverlaySettingsService =
                new OverlaySettingsService();

            IOverlayPositionService OverlayPositionService =
                new OverlayPositionService();

            IOverlaySizeService OverlaySizeService =
                new OverlaySizeService();

            IOverlayColorService OverlayColorService =
                new OverlayColorService();

            IOverlayAlignmentService OverlayAlignmentService =
                new OverlayAlignmentService();

            MainWindow Window =
                new MainWindow(
                    HotkeyService,
                    StartupService,
                    PerformanceMonitorService,
                    OverlaySettingsService,
                    OverlayPositionService,
                    OverlaySizeService,
                    OverlayColorService,
                    OverlayAlignmentService);

            Window.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_Host != null)
            {
                await _Host.StopAsync();

                _Host.Dispose();
            }

            base.OnExit(e);
        }
    }
}


