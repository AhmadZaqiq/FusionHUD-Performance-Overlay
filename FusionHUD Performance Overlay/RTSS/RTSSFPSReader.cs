using System;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;

namespace FusionHUD_Performance_Overlay.RTSS
{
    public class RTSSMemoryReader
    {
        private const string SharedMemoryName = "RTSSSharedMemoryV2"; // Name of the shared memory created by RTSS to read FPS data

        private const int FPSOffset = 276; // Offset of the FPS value inside RTSS shared memory entry

        private MemoryMappedFile OpenSharedMemory()
        {
            return MemoryMappedFile.OpenExisting(SharedMemoryName);
        }

        private uint GetEntrySize(MemoryMappedViewAccessor Accessor)
        {
            return Accessor.ReadUInt32(8);
        }

        private uint GetApplicationOffset(MemoryMappedViewAccessor Accessor)
        {
            return Accessor.ReadUInt32(12);
        }

        private uint GetApplicationCount(MemoryMappedViewAccessor Accessor)
        {
            return Accessor.ReadUInt32(16);
        }

        private int FindFPS(MemoryMappedViewAccessor Accessor, uint EntrySize, uint AppOffset, uint AppCount, uint ForegroundPID)
        {
            for (int Index = 0; Index < AppCount; Index++)
            {
                long EntryOffset = AppOffset + (Index * EntrySize); // Calculate current application entry position

                uint PID = Accessor.ReadUInt32(EntryOffset); // Read process ID from RTSS application entry

                if (PID != ForegroundPID)
                {
                    continue;
                }

                int FPS = Accessor.ReadInt32(EntryOffset + FPSOffset) - 1;

                return FPS > 0 ? FPS : 0;
            }

            return 0;
        }

        public int GetFPS(uint ForegroundPID)
        {
            try
            {
                using (MemoryMappedFile Memory = OpenSharedMemory())

                using (MemoryMappedViewAccessor Accessor = Memory.CreateViewAccessor())
                {
                    uint EntrySize = GetEntrySize(Accessor);

                    uint AppOffset = GetApplicationOffset(Accessor);

                    uint AppCount = GetApplicationCount(Accessor);

                    return FindFPS(Accessor, EntrySize, AppOffset, AppCount, ForegroundPID);
                }
            }

            catch (Exception Exception)
            {
                Debug.WriteLine(Exception.Message);

                return 0;
            }
        }

    }
}