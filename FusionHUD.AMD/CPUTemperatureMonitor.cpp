#include "pch.h"
#include "CPUTemperatureMonitor.h"

#include "IPlatform.h"
#include "IDeviceManager.h"
#include "ICPUEx.h"

static IPlatform* g_platform = nullptr;
static ICPUEx* g_cpu = nullptr;

extern "C" __declspec(dllexport)
bool InitAMDMonitor()
{
	try
	{
		IPlatform& Platform = GetPlatform();

		if (!Platform.Init(nullptr, true))
		{
			return false;
		}

		IDeviceManager& Manager = Platform.GetIDeviceManager();

		IDevice* Device = Manager.GetDevice(dtCPU, 0);

		if (Device == nullptr)
		{
			Platform.UnInit();

			return false;
		}

		ICPUEx* CPU = dynamic_cast<ICPUEx*>(Device);

		if (CPU == nullptr)
		{
			Platform.UnInit();

			return false;
		}

		g_platform = &Platform;
		g_cpu = CPU;

		return true;
	}
	catch (...)
	{
		g_platform = nullptr;
		g_cpu = nullptr;

		return false;
	}
}

extern "C" __declspec(dllexport)
double GetCPUTemperature()
{
	if (g_cpu == nullptr)
	{
		return -1.0;
	}

	CPUParameters Parameters{};

	int Result = g_cpu->GetCPUParameters(Parameters);

	if (Result != 0)
	{
		return -1.0;
	}

	return Parameters.dTemperature;
}

extern "C" __declspec(dllexport)
void ShutdownAMDMonitor()
{
	if (g_platform != nullptr)
	{
		g_platform->UnInit();
	}

	g_cpu = nullptr;
	g_platform = nullptr;
}