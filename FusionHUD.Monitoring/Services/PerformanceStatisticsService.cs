using FusionHUD.Monitoring.Models;

namespace FusionHUD.Monitoring.Services
{
    public class PerformanceStatisticsService
    {
        private double _CpuUsageTotal;

        private int _CpuUsageSamples;

        private double _CpuTemperatureTotal;

        private int _CpuTemperatureSamples;

        private double _GpuUsageTotal;

        private int _GpuUsageSamples;

        private double _GpuTemperatureTotal;

        private int _GpuTemperatureSamples;

        private double _RamUsageTotal;

        private int _RamUsageSamples;

        private double _FpsTotal;

        private int _FpsSamples;

        public DailyStatistics Statistics { get; } = new DailyStatistics();

        public void UpdateCpuUsage(double CpuUsage)
        {
            _CpuUsageTotal += CpuUsage;

            _CpuUsageSamples++;

            Statistics.CpuUsageAverage = _CpuUsageTotal / _CpuUsageSamples;

            if (CpuUsage > Statistics.CpuUsageMaximum)
            {
                Statistics.CpuUsageMaximum = CpuUsage;
            }
        }

        public void UpdateCpuTemperature(double CpuTemperature)
        {
            _CpuTemperatureTotal += CpuTemperature;

            _CpuTemperatureSamples++;

            Statistics.CpuTemperatureAverage = _CpuTemperatureTotal / _CpuTemperatureSamples;

            if (CpuTemperature > Statistics.CpuTemperatureMaximum)
            {
                Statistics.CpuTemperatureMaximum = CpuTemperature;
            }
        }

        public void UpdateGpuUsage(double GpuUsage)
        {
            _GpuUsageTotal += GpuUsage;

            _GpuUsageSamples++;

            Statistics.GpuUsageAverage = _GpuUsageTotal / _GpuUsageSamples;

            if (GpuUsage > Statistics.GpuUsageMaximum)
            {
                Statistics.GpuUsageMaximum = GpuUsage;
            }
        }

        public void UpdateGpuTemperature(double GpuTemperature)
        {
            _GpuTemperatureTotal += GpuTemperature;

            _GpuTemperatureSamples++;

            Statistics.GpuTemperatureAverage = _GpuTemperatureTotal / _GpuTemperatureSamples;

            if (GpuTemperature > Statistics.GpuTemperatureMaximum)
            {
                Statistics.GpuTemperatureMaximum = GpuTemperature;
            }
        }

        public void UpdateRamUsage(double RamUsage)
        {
            _RamUsageTotal += RamUsage;

            _RamUsageSamples++;

            Statistics.RamUsageAverage = _RamUsageTotal / _RamUsageSamples;

            if (RamUsage > Statistics.RamUsageMaximum)
            {
                Statistics.RamUsageMaximum = RamUsage;
            }
        }

        public void UpdateFps(double Fps)
        {
            if (Fps <= 0)
            {
                return;
            }

            _FpsTotal += Fps;

            _FpsSamples++;

            Statistics.FpsAverage = _FpsTotal / _FpsSamples;

            if (Fps < Statistics.FpsMinimum)
            {
                Statistics.FpsMinimum = Fps;
            }

            if (Fps > Statistics.FpsMaximum)
            {
                Statistics.FpsMaximum = Fps;
            }
        }

        public void Reset()
{
    _CpuUsageTotal = 0;
    _CpuUsageSamples = 0;

    _CpuTemperatureTotal = 0;
    _CpuTemperatureSamples = 0;

    _GpuUsageTotal = 0;
    _GpuUsageSamples = 0;

    _GpuTemperatureTotal = 0;
    _GpuTemperatureSamples = 0;

    _RamUsageTotal = 0;
    _RamUsageSamples = 0;

    _FpsTotal = 0;
    _FpsSamples = 0;

    Statistics.StartTime = DateTime.Now;
    Statistics.Uptime = TimeSpan.Zero;

    Statistics.CpuUsageAverage = 0;
    Statistics.CpuUsageMaximum = 0;

    Statistics.CpuTemperatureAverage = 0;
    Statistics.CpuTemperatureMaximum = 0;

    Statistics.GpuUsageAverage = 0;
    Statistics.GpuUsageMaximum = 0;

    Statistics.GpuTemperatureAverage = 0;
    Statistics.GpuTemperatureMaximum = 0;

    Statistics.RamUsageAverage = 0;
    Statistics.RamUsageMaximum = 0;

    Statistics.FpsAverage = 0;
    Statistics.FpsMinimum = double.MaxValue;
    Statistics.FpsMaximum = 0;
}

public void UpdateUptime()
{
            Statistics.Uptime = DateTime.Now - Statistics.StartTime;
        }
    }

}
