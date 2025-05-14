namespace StorageMedia;

using System;
using System.Collections.Generic;
using System.IO;

class DriveManagerService
{

    public List<DriveInfo> drives = new List<DriveInfo>();

    public void RefreshDrives()
    {
        drives.Clear();
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                drives.Add(new DiskInfo(drive));
            }
        }
    }
}
