using FusionHUD_Performance_Overlay.Interfaces;
using FusionHUD_Performance_Overlay.Models;
using System.Windows;

namespace FusionHUD_Performance_Overlay.Services
{
    public class OverlayPositionService : IOverlayPositionService
    {
        public void ApplyPosition(Window Window, OverlayPosition Position)
        {
            Rect WorkArea = SystemParameters.WorkArea;

            switch (Position)
            {
                case OverlayPosition.Left:
                    Window.Left = WorkArea.Left;
                    break;

                case OverlayPosition.Center:
                    Window.Left = WorkArea.Left + (WorkArea.Width - Window.Width) / 2;
                    break;

                case OverlayPosition.Right:
                    Window.Left = WorkArea.Right - Window.Width;
                    break;
            }

            Window.Top = WorkArea.Top;
        }
    }
}