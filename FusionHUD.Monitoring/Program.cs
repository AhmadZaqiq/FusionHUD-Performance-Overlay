using FusionHUD.Monitoring.Interfaces;
using FusionHUD.Monitoring.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder Builder =
    Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

Builder.Services.AddSingleton<PerformanceStatisticsService>();

Builder.Services.AddSingleton<IDailyReportService, DailyReportService>();

Builder.Services.AddSingleton<IPerformanceDataProvider, PerformanceDataProvider>();

Builder.Services.AddHostedService<Worker>();

IHost AppHost = Builder.Build();

await AppHost.RunAsync();
