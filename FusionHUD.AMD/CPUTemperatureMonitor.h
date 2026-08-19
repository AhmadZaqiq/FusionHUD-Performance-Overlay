#pragma once

#ifdef FUSIONHUD_AMD_EXPORTS
#define FUSIONHUD_AMD_API __declspec(dllexport)
#else
#define FUSIONHUD_AMD_API __declspec(dllimport)
#endif

extern "C"
{
    FUSIONHUD_AMD_API bool InitAMDMonitor();
    FUSIONHUD_AMD_API double GetCPUTemperature();
    FUSIONHUD_AMD_API void ShutdownAMDMonitor();
}