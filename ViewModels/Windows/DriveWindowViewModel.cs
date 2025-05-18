using System.Collections.Generic;
using System.IO;

namespace FileFunnel.ViewModels.Windows;

public class DriveWindowViewModel : ViewModelBase
{
    public List<DriveInfo> Drives { get; set; } = new List<DriveInfo>();
    
    public DriveWindowViewModel()
    {
        Drives = new List<DriveInfo>();
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                Drives.Add(drive);
            }
        }
    }
}