using System.Collections.Generic;
using System.Threading.Tasks;
using FileFunnel.Models.HardwareService;

namespace FileFunnel.Models;

public class DiskManagement : IDiskManagement
{
    private readonly IHardwareScanner _hardwareScanner;

    public DiskManagement(IHardwareScanner hardwareScanner)
    {
        _hardwareScanner = hardwareScanner;
    }

    public List<DiskInfo> GetDisks()
    {
        _hardwareScanner.GetDisksFromSystem().GetPartitionsForDisk().GetVolumesForDisk();
        return _hardwareScanner.GetDisks();
    }
}