using FusionHUD_Performance_Overlay.Interfaces;
using NvAPIWrapper.GPU;
using System.Collections.Generic;
using System.Linq;

namespace FusionHUD_Performance_Overlay.Services
{
    public class GPUService : IGPUService
    {
        private PhysicalGPU _GraphicsCard;

        private List<float> _GPUUsageHistory = new List<float>();

        private List<float> _GPUTemperatureHistory = new List<float>();

        public GPUService()
        {
            Initialize();
        }

        public string GPUName { get; private set; } = "N/A";

        public void Initialize()
        {
            PhysicalGPU[] GPUs = PhysicalGPU.GetPhysicalGPUs();

            if (GPUs.Length > 0)
            {
                _GraphicsCard = GPUs[0];

                GPUName = _GraphicsCard.FullName;
            }
        }

        public string GetGPUUsage()
        {
            if (_GraphicsCard == null)
            {
                return "N/A";
            }

            try
            {
                GPUUsageInformation Usage = _GraphicsCard.UsageInformation;

                float GPUUsage = Usage.GPU.Percentage;

                _GPUUsageHistory.Add(GPUUsage);

                if (_GPUUsageHistory.Count > 5)
                {
                    _GPUUsageHistory.RemoveAt(0);
                }

                float Average = _GPUUsageHistory.Average();

                return $"{Average:F0}%";
            }
            catch
            {
                return "N/A";
            }
        }

        public string GetVRAMUsage()
        {
            if (_GraphicsCard == null)
            {
                return "N/A";
            }

            try
            {
                GPUMemoryInformation Memory = _GraphicsCard.MemoryInformation;

                double Total = Memory.DedicatedVideoMemoryInkB / 1024.0 / 1024.0;

                double Available = Memory.CurrentAvailableDedicatedVideoMemoryInkB / 1024.0 / 1024.0;

                double Used = Total - Available;

                return $"{Used:F1}GB";
            }
            catch
            {
                return "N/A";
            }
        }

        public string GetGPUTemperature()
        {
            if (_GraphicsCard == null)
            {
                return "N/A";
            }

            try
            {
                GPUThermalInformation Thermal = _GraphicsCard.ThermalInformation;

                GPUThermalSensor Sensor = Thermal.ThermalSensors.First();

                float Temperature = Sensor.CurrentTemperature;

                _GPUTemperatureHistory.Add(Temperature);

                if (_GPUTemperatureHistory.Count > 5)
                {
                    _GPUTemperatureHistory.RemoveAt(0);
                }

                float Average = _GPUTemperatureHistory.Average();

                return $"{Average:F0}°C";
            }
            catch
            {
                return "N/A";
            }
        }

    }
}