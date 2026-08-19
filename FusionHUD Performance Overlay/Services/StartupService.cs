using FusionHUD_Performance_Overlay.Interfaces;
using Microsoft.Win32;

namespace FusionHUD_Performance_Overlay.Services
{
    public class StartupService : IStartupService
    {
        public void EnableStartup()
        {
            const string Name = "FusionHUD Performance Overlay";

            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            using RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            Key?.SetValue(Name, Path);
        }

    }
}