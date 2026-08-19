using FusionHUD_Performance_Overlay.Interfaces;

namespace FusionHUD_Performance_Overlay.RTSS
{
    public class RTSSReader : IRTSSReader
    {
        private readonly RTSSMemoryReader MemoryReader;
        private readonly ForegroundProcessReader ProcessReader;

        public RTSSReader()
        {
            MemoryReader = new RTSSMemoryReader();
            ProcessReader = new ForegroundProcessReader();
        }

        public int GetFPS()
        {
            uint ProcessID = ProcessReader.GetForegroundProcessID();

            return MemoryReader.GetFPS(ProcessID);
        }
    }

}