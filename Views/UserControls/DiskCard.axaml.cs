using Avalonia;
using Avalonia.Controls;
using StorageMedia;

namespace FileFunnel.Views.UserControls;

public partial class DiskCard : UserControl
{
    public static readonly StyledProperty<DiskInfo> diskInfo =
        AvaloniaProperty.Register<DiskCard, DiskInfo>(nameof(DiskInfo));
    public DiskCard()
    {
        InitializeComponent();
    }
}