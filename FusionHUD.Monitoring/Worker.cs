using FusionHUD.Monitoring.Interfaces;
using FusionHUD.Monitoring.Services;
using FusionHUD_Performance_Overlay.Models;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _Logger;

    private readonly IPerformanceDataProvider _PerformanceDataProvider;

    private readonly PerformanceStatisticsService _PerformanceStatisticsService;

    private readonly IDailyReportService _DailyReportService;

    private DateTime _CurrentDate;

    private DateTime _LastReportTime;

    public Worker(
        ILogger<Worker> Logger,
        IPerformanceDataProvider PerformanceDataProvider,
        PerformanceStatisticsService PerformanceStatisticsService,
        IDailyReportService DailyReportService)
    {
        _Logger = Logger;

        _PerformanceDataProvider = PerformanceDataProvider;

        _PerformanceStatisticsService = PerformanceStatisticsService;

        _DailyReportService = DailyReportService;

        _CurrentDate = DateTime.Now.Date;
        _LastReportTime = DateTime.Now;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            DateTime Today = DateTime.Now.Date;
            DateTime Now = DateTime.Now;

            if (Now - _LastReportTime >= TimeSpan.FromSeconds(30))
            {
                string Report =
                    _DailyReportService.CreateReport(
                        _PerformanceStatisticsService.Statistics);

                _Logger.LogInformation("{Report}", Report);

                _PerformanceStatisticsService.Reset();

                _CurrentDate = Today;
                _LastReportTime = Now;
            }

            PerformanceData PerformanceData =
                _PerformanceDataProvider.GetPerformanceData();

            if (double.TryParse(
                PerformanceData.CPUUsage.Replace("%", ""),
                out double CpuUsage))
            {
                _PerformanceStatisticsService.UpdateCpuUsage(CpuUsage);
            }

            if (double.TryParse(
                PerformanceData.GPUUsage.Replace("%", ""),
                out double GpuUsage))
            {
                _PerformanceStatisticsService.UpdateGpuUsage(GpuUsage);
            }

            if (double.TryParse(
                PerformanceData.CPUTemperature.Replace("°C", ""),
                out double CpuTemperature))
            {
                _PerformanceStatisticsService.UpdateCpuTemperature(
                    CpuTemperature);
            }

            if (double.TryParse(
                PerformanceData.GPUTemperature.Replace("°C", ""),
                out double GpuTemperature))
            {
                _PerformanceStatisticsService.UpdateGpuTemperature(
                    GpuTemperature);
            }

            if (double.TryParse(
                PerformanceData.RAMUsage.Replace("GB", ""),
                out double RamUsage))
            {
                _PerformanceStatisticsService.UpdateRamUsage(RamUsage);
            }

            if (double.TryParse(
                PerformanceData.FPS,
                out double FpsValue))
            {
                _PerformanceStatisticsService.UpdateFps(FpsValue);
            }

            _PerformanceStatisticsService.UpdateUptime();

            await Task.Delay(1000, stoppingToken);
        }
    }
}
