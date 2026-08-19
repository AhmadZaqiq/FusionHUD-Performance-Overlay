using FusionHUD_Performance_Overlay.Models;
using System.Windows;

namespace FusionHUD_Performance_Overlay.Interfaces
{
    public interface IOverlayAlignmentService
    {
        void ApplyAlignment(Window Window, OverlayPosition Position);
    }
}