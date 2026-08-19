using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionHUD.Core
{
    public class HardwareData
    {
        public string GPUName { get; set; }

        public string GPUUsage { get; set; }

        public string GPUTemperature { get; set; }

        public string VRAM { get; set; }

        public string CPUUsage { get; set; }

        public string CPUTemperature { get; set; }

        public string RAMUsage { get; set; }

        public string FPS { get; set; }
    }
}
