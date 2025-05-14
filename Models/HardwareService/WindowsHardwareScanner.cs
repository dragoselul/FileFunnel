using System.Collections.Generic;
using StorageMedia;
using System.Management;
using System;

namespace HardwareService;

public class WindowsHardwareScanner : HardwareScanner
{
    public static IEnumerable<DiskInfo> GetDisks()
    {
        var list = new List<DiskInfo>();
        using var searcher = new ManagementObjectSearcher(
            "SELECT DeviceID, Model, SerialNumber, Size FROM Win32_DiskDrive");
        
        foreach (ManagementObject disk in searcher.Get())
        {
            list.Add(new DiskInfo (
                disk["Name"]?.ToString() ?? "Unknown",
                disk["DeviceID"]?.ToString() ?? "",
                disk["Model"]?.ToString() ?? "Unknown",
                disk["SerialNumber"]?.ToString() ?? "Unknown",
                Convert.ToInt64(disk["Size"] ?? 0),
                disk["Status"]?.ToString() ?? "Unknown"
            ));
        }
        return list;
    }

    public static DiskInfo GetPartitionsForDisk(DiskInfo disk)
    {
        // Associate Win32_DiskDrive -> Win32_DiskPartition
        string query = 
          "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + disk.DeviceId +
          "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition";
        
        using var searcher = new ManagementObjectSearcher(query);
        foreach (ManagementObject part in searcher.Get())
        {
            disk.Add(new PartitionInfo (
                part["DeviceID"]?.ToString() ?? "",
                Convert.ToInt64(part["Size"] ?? 0),
                Convert.ToInt32(part["Bootable"] ?? 0),
                Convert.ToInt32(part["Index"] ?? 0),
                part["Type"]?.ToString() ?? ""
            ));
        }
        return disk;
    }

    public static DiskInfo GetVolumesForDisk(DiskInfo disk)
    {
        // Associate Win32_DiskPartition -> Win32_LogicalDisk (volume)
        foreach (var partition in disk.Partitions)
        {
            // Skip empty partitions
            if (partition.Size == 0)
                continue;

            // Associate Win32_DiskPartition -> Win32_LogicalDisk
            string query = 
            "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition.PartitionId +
            "'} WHERE AssocClass = Win32_LogicalDiskToPartition";

            using var searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject vol in searcher.Get())
            {
                var driveLetter = vol["DeviceID"]?.ToString() + "\\";  // e.g. "D:\"
                var dri = new System.IO.DriveInfo(driveLetter);
                disk.Add(new VolumeInfo (
                    GetVolumeGuidViaWmi(driveLetter.TrimEnd('\\')),
                    dri.DriveFormat,
                    dri.TotalSize,
                    dri.AvailableFreeSpace
                ));
            }
        }
        return disk;
    }

    private static string GetVolumeGuidViaWmi(string driveLetter)
    {
        var q = $"SELECT DeviceID FROM Win32_Volume WHERE DriveLetter = '{driveLetter}:'";
        using var s = new ManagementObjectSearcher(q);
        foreach (ManagementObject v in s.Get())
            return v["DeviceID"]?.ToString();
        return "Unknown";
    }
}