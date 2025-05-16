using System.IO;

public enum FileType
{
    Unknown,      // fallback for unrecognized types
    Document,     // .docx, .pdf, .txt, .xlsx, .pptx, etc.
    Image,        // .jpg, .png, .gif, .bmp, .svg
    Video,        // .mp4, .mkv, .avi, .mov
    Audio,        // .mp3, .wav, .flac, .aac
    Archive,      // .zip, .rar, .7z, .tar.gz
    Executable,   // .exe, .msi, .sh, .bin
    Code,         // .cs, .js, .py, .java, .cpp, .html, .css
    Config,       // .json, .yaml, .xml, .ini
    Log,          // .log, .txt (if used for logging)
    System,       // system files like .dll, .sys, hidden OS stuff
    Temp,         // temporary files: .tmp, ~ files
    Font,         // .ttf, .otf
    VectorGraphic,// .svg, .eps, .pdf when used as vector art
    Shortcut,     // .lnk, .desktop
    Database,
    WebPage
}

public static class FileTypeExtensions
{

    public static FileType GetFileType(this string fileName)
    {
        var ext = Path.GetExtension(fileName)?.ToLowerInvariant() ?? "";

        return ext switch
        {
            // Documents
            ".doc"  or ".docx" or ".dotx" or ".docm" or
            ".pdf"  or ".txt"  or ".rtf"  or ".xls"  or
            ".xlsx" or ".xlsm" or ".xlsb" or ".xltx" or
            ".ppt"  or ".pptx" or ".pptm" or ".potx" or
            ".odp"  or ".ods"  or ".odt"  or ".odg"  or
            ".odm"  or ".csv"  or ".tsv"  or ".md"   or
            ".markdown" or ".rst" or ".tex"  or
            ".html" or ".htm"  or ".xhtml" or ".epub" or
            ".mobi" or ".azw"  or ".azw3" or ".chm"  or
            ".xps"  or ".tif"  or ".tiff" or ".djvu" =>
                FileType.Document,

            // Images
            ".jpg"  or ".jpeg" or ".png"  or ".gif"  or
            ".bmp"  or ".svg" =>
                FileType.Image,

            // Videos
            ".mp4"  or ".mkv"  or ".avi"  or ".mov" =>
                FileType.Video,

            // Audio
            ".mp3"  or ".wav"  or ".flac" or ".aac" =>
                FileType.Audio,

            // Archives
            ".zip"  or ".rar"  or ".7z"   or ".tar"  or
            ".gz"   or ".tar.gz" =>
                FileType.Archive,

            // Executables & installers
            ".exe"  or ".msi"  or ".sh"   or ".bin" =>
                FileType.Executable,

            // Code
            ".cs"   or ".js"   or ".ts"   or ".py"   or
            ".java" or ".cpp"  or ".c"    or ".html" or
            ".css"  or ".json" or ".xml"  or ".yaml" or
            ".yml"  or ".ini"  or ".toml" =>
                FileType.Code,

            // Logs
            ".log" =>
                FileType.Log,

            // Fonts
            ".ttf"  or ".otf" =>
                FileType.Font,

            // Vector graphics
            ".eps"  =>
                FileType.VectorGraphic,

            // Shortcuts
            ".lnk"  or ".desktop" =>
                FileType.Shortcut,

            // Temp files
            ".tmp"  or ".~"   =>
                FileType.Temp,

            // System files
            ".dll"  or ".sys" or ".drv"  or ".cab" =>
                FileType.System,

            _ =>
                FileType.Unknown
        };
    }

    // Example: categorize media types
    public static bool IsMedia(this FileType ft) =>
        ft == FileType.Image
     || ft == FileType.Video
     || ft == FileType.Audio;

    // Get a default folder name for each type
    public static string ToFolderName(this FileType ft) =>
        ft switch
        {
            FileType.Document      => "Documents",
            FileType.Image         => "Pictures",
            FileType.Video         => "Videos",
            FileType.Audio         => "Music",
            FileType.Archive       => "Archives",
            FileType.Code          => "Code",
            FileType.Config        => "Config",
            FileType.Log           => "Logs",
            FileType.Font          => "Fonts",
            FileType.VectorGraphic => "VectorGraphics",
            FileType.Shortcut      => "Shortcuts",
            _                      => "Misc"
        };

    // Maybe you want a method for duplicate handling
    public static bool ShouldArchiveDuplicates(this FileType ft) =>
        ft == FileType.Document
     || ft == FileType.Image
     || ft == FileType.Video;
}
