using FusionHUD_Performance_Overlay.Models;
using System.Windows;

namespace FusionHUD_Performance_Overlay.Interfaces
{
    public interface IOverlaySizeService
    {
        void ApplySize(Window Window, OverlaySize Size);
    }
}