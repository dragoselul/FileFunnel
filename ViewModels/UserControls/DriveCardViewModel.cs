using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace FileFunnel.ViewModels.UserControls;

public partial class DriveCardViewModel : ViewModelBase
{
    public string Name { get; set; } = "Samsung 990 Pro";
    public string DeviceId { get; set; } = "0-1";
    public string Model { get; set; } = "990 Pro";
    public string Serial { get; set; } = "1234567890";
    public string Size { get; set; } = "2TB";
    public string Status { get; set; } = "OK";
    public string NoOfVolumes { get; set; } = "2";
    public string NoOfPartitions { get; set; } = "2";
    
    public string DriveIconPath { get; set; } = "avares://FileFunnel/Assets/DriveIcons/ssd.png";

    public DriveCardViewModel()
    {
        
    }
}