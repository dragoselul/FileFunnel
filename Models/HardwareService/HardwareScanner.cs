using System.Collections.Generic;
using StorageMedia;

namespace FileFunnel.Models.HardwareService;

/// <summary>
/// HardwareScanner is a class that scans the hardware and detects all the physical drives connected to the system.
/// </summary>
public interface IHardwareScanner {
    IEnumerable<DiskInfo> GetDisks();
    DiskInfo GetPartitionsForDisk(DiskInfo disk);
    DiskInfo GetVolumesForDisk(DiskInfo disk);
}