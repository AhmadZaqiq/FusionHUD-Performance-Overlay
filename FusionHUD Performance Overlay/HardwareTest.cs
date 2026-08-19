using LibreHardwareMonitor.Hardware;
using System.Diagnostics;
using System.Linq;

public static class HardwareTest
{
    public static void Run()
    {
        var computer = new Computer
        {
            IsCpuEnabled = true
        };

        try
        {
            computer.Open();

            Debug.WriteLine("================================");
            Debug.WriteLine("LIBRE HARDWARE MONITOR TEST");
            Debug.WriteLine("================================");

            var cpu = computer.Hardware
                .FirstOrDefault(h => h.HardwareType == HardwareType.Cpu);

            if (cpu == null)
            {
                Debug.WriteLine("CPU NOT FOUND!");
                return;
            }

            Debug.WriteLine($"CPU: {cpu.Name}");
            Debug.WriteLine($"Identifier: {cpu.Identifier}");

            cpu.Update();

            Debug.WriteLine("");
            Debug.WriteLine("CPU SENSORS:");

            foreach (var sensor in cpu.Sensors)
            {
                Debug.WriteLine(
                    $"{sensor.SensorType,-15} | " +
                    $"{sensor.Name,-25} | " +
                    $"Value: {sensor.Value}");
            }

            Debug.WriteLine("");
            Debug.WriteLine("TEMPERATURE SENSORS:");

            foreach (var sensor in cpu.Sensors
                         .Where(s => s.SensorType == SensorType.Temperature))
            {
                Debug.WriteLine(
                    $"{sensor.Name} = {sensor.Value}°C");
            }

            Debug.WriteLine("================================");
        }
        finally
        {
            computer.Close();
        }
    }
}