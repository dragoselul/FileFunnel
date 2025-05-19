using System.Collections.Generic;

namespace FileFunnel.Models;

public interface IDiskManagement
{
    public List<DiskInfo> GetDisks();
}