using System;
using System.Collections.Generic;
using System.Management;

namespace FileFunnel.Models.HardwareService;
#pragma warning disable CA1416
public class WindowsHardwareScanner : IHardwareScanner
{
    private List<DiskInfo> Disks { get; set; }
    public WindowsHardwareScanner()
    {
        Disks = new List<DiskInfo>();
    }
    
    public List<DiskInfo> GetDisks()
    {
        return Disks;
    }

    public IHardwareScanner GetDisksFromSystem()
    {
        var list = new List<DiskInfo>();
        using var searcher = new ManagementObjectSearcher(
            "SELECT DeviceID, Model, SerialNumber, Size FROM Win32_DiskDrive");

        foreach (var o in searcher.Get())
        {
            var disk = (ManagementObject)o;
            list.Add(new DiskInfo(
                disk["Name"]?.ToString() ?? "Unknown",
                disk["DeviceID"]?.ToString() ?? "",
                disk["Model"]?.ToString() ?? "Unknown",
                disk["SerialNumber"]?.ToString() ?? "Unknown",
                Convert.ToInt64(disk["Size"] ?? 0),
                disk["Status"]?.ToString() ?? "Unknown"
            ));
        }

        return this;
    }

    public IHardwareScanner GetPartitionsForDisk()
    {
        // Associate Win32_DiskDrive -> Win32_DiskPartition
        foreach (var disk in Disks)
        {
            // Associate Win32_DiskDrive -> Win32_DiskPartition
            string query =
                "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + disk.DeviceId +
                "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition";

            using var searcher = new ManagementObjectSearcher(query);
            foreach (var o in searcher.Get())
            {
                var part = (ManagementObject)o;
                disk.Add(new PartitionInfo(
                    part["DeviceID"]?.ToString() ?? "",
                    Convert.ToInt64(part["Size"] ?? 0),
                    Convert.ToBoolean(part["Bootable"] ?? false),
                    Convert.ToInt32(part["Index"] ?? 0),
                    part["Type"]?.ToString() ?? ""
                ));
            }
        }

        return this;
    }

    public IHardwareScanner GetVolumesForDisk()
    {
        foreach (var disk in Disks)
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
                foreach (var o in searcher.Get())
                {
                    var vol = (ManagementObject)o;
                    var driveLetter = vol["DeviceID"]?.ToString() + "\\"; // e.g. "D:\"
                    var dri = new System.IO.DriveInfo(driveLetter);
                    disk.Add(new VolumeInfo(
                        GetVolumeGuidViaWmi(driveLetter.TrimEnd('\\')),
                        dri.DriveFormat,
                        dri.TotalSize,
                        dri.AvailableFreeSpace
                    ));
                }
            }
        }

        return this;
    }

    private static string GetVolumeGuidViaWmi(string driveLetter)
    {
        var q = $"SELECT DeviceID FROM Win32_Volume WHERE DriveLetter = '{driveLetter}:'";
        using var s = new ManagementObjectSearcher(q);
        foreach (var o in s.Get())
        {
            var v = (ManagementObject)o;
            return v["DeviceID"]?.ToString() ?? "Unknown";
        }

        return "Unknown";
    }
}
#pragma warning restore CA1416