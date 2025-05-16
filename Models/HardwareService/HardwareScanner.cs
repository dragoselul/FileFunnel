using System.Collections.Generic;
using StorageMedia;
using System.Management;
using System;

namespace HardwareService;

/// <summary>
/// HardwareScanner is a class that scans the hardware and detects all the physical drives connected to the system.
/// </summary>
public interface HardwareScanner {
    static abstract IEnumerable<DiskInfo> GetDisks();
    static abstract DiskInfo GetPartitionsForDisk(DiskInfo disk);
    static abstract DiskInfo GetVolumesForDisk(DiskInfo disk);
}