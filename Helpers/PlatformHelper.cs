using System.Runtime.InteropServices;

namespace FileFunnel.Models.Helpers;

public static class PlatformHelper
{
    public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    public static bool IsLinux   => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    public static bool IsMac     => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
}
