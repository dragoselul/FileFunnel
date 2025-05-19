using System.IO;

namespace FileFunnel.Models;

/// <summary>
/// A single mount point for a volume.
/// </summary>
public class MountInfo
{
    public string MountPoint   { get; set; }    // e.g. "D:\" or "/media/user/USB1"
    public DriveType DriveType { get; set; }    // on Windows: Removable, Fixed, Network...
    public bool      IsReady   { get; set; }    // has media been removed?
}