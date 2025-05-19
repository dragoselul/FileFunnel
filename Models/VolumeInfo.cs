namespace FileFunnel.Models;
/// <summary>
/// A single volume/partition on a disk.
/// </summary>
public class VolumeInfo
{
    public string VolumeGuid    { get; private set; }    // \\?\Volume{…}\ or UUID on Linux
    public string FileSystem    { get; private set; }    // NTFS, ext4, FAT32, etc.
    public long   TotalSize     { get; private set; }
    public long   FreeSpace     { get; private set; }

    /// <summary>
    /// All mount points (letters or paths) where this volume is accessible.
    /// </summary>
    public System.Collections.Generic.List<MountInfo> Mounts { get; set; } = new();

    public VolumeInfo(string volumeGuid, string fileSystem, long totalSize, long freeSpace)
    {
        VolumeGuid = volumeGuid;
        FileSystem = fileSystem;
        TotalSize  = totalSize;
        FreeSpace  = freeSpace;
    }
}
