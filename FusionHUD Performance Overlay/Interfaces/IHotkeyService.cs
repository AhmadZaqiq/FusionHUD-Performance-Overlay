using FusionHUD_Performance_Overlay.Models;
using System;

namespace FusionHUD_Performance_Overlay.Interfaces
{
    public interface IHotkeyService
    {
        void Register(IntPtr Handle);

        OverlayHotkeyAction? GetHotkeyAction(int Message,IntPtr WParam);
    }
}