using FusionHUD_Performance_Overlay.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FusionHUD_Performance_Overlay.Services
{
    public class FPSService : IFPSService
    {
        private readonly IRTSSReader RTSSReader;

        private readonly List<int> FPSValues = new List<int>();

        private const int SmoothSamples = 5;

        public FPSService(IRTSSReader RTSSReader)
        {
            this.RTSSReader = RTSSReader;
        }

        public string GetFPS()
        {
            int FPS = RTSSReader.GetFPS();

            if (FPS <= 0)
            {
                FPSValues.Clear();

                return "N/A";
            }

            FPSValues.Add(FPS);

            if (FPSValues.Count > SmoothSamples)
            {
                FPSValues.RemoveAt(0);
            }

            int SmoothFPS = (int)FPSValues.Average();

            return SmoothFPS.ToString();
        }

    }
}