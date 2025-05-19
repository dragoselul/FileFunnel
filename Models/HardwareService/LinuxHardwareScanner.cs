using System.Collections.Generic;
using StorageMedia;

namespace FileFunnel.Models.HardwareService;

public class LinuxHardwareScanner : IHardwareScanner
{
    public IEnumerable<DiskInfo> GetDisks()
    {
        throw new System.NotImplementedException();
    }

    public DiskInfo GetPartitionsForDisk(DiskInfo disk)
    {
        throw new System.NotImplementedException();
    }

    public DiskInfo GetVolumesForDisk(DiskInfo disk)
    {
        throw new System.NotImplementedException();
    }
}