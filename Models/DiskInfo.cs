using System.Collections.Generic;

namespace FileFunnel.Models;

/// <summary>
/// Represents the physical disk (e.g. a USB stick, SSD, HDD).
/// </summary>
public class DiskInfo
{
    public string Name       { get; private set; }    // e.g. "USB Drive", "Samsung SSD 970 EVO"
    public string DeviceId   { get; private set; }    // e.g. "\\\\.\\PhysicalDrive1" on Windows or "/dev/sdb" on Linux
    public string? Model     { get; private set; }    // e.g. "SanDisk Ultra"
    public string? Serial    { get; private set; }    // hardware serial
    public long   Size       { get; private set; }    // total bytes
    public string Status     { get; private set; }    // e.g. "OK", "Degraded", "Error", "Stopping"

    /// <summary>
    /// The partitions/volumes on this disk.
    /// </summary>
    public List<VolumeInfo> Volumes { get; private set; }
    public List<PartitionInfo> Partitions   { get; private set; }

    public DiskInfo(string name, string deviceId, string? model, string? serial, long size, string status)
    {
        Name     = name;
        DeviceId = deviceId;
        Model    = model;
        Serial   = serial;
        Size     = size;
        Status   = status;
        Volumes = new();
        Partitions = new();
    }

    public void Add(PartitionInfo partition)
    {
        Partitions.Add(partition);
    }

    public void Add(VolumeInfo volume)
    {
        Volumes.Add(volume);
    }

    public long GetFreeSpace()
    {
        long freeSpace = 0;
        foreach (var volume in Volumes)
        {
            freeSpace += volume.FreeSpace;
        }
        return freeSpace;
    }
}
