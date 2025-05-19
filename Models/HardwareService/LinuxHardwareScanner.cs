using System.Collections.Generic;

namespace FileFunnel.Models.HardwareService;

public class LinuxHardwareScanner : IHardwareScanner
{
    private List<DiskInfo> Disks { get; set; }

    public LinuxHardwareScanner(List<DiskInfo> disks)
    {
        Disks = disks;
    }
    public List<DiskInfo> GetDisks()
    {
        return Disks;
    }

    public IHardwareScanner GetDisksFromSystem()
    {
        throw new System.NotImplementedException();
    }

    public IHardwareScanner GetPartitionsForDisk()
    {
        throw new System.NotImplementedException();
    }

    public IHardwareScanner GetVolumesForDisk()
    {
        throw new System.NotImplementedException();
    }
}