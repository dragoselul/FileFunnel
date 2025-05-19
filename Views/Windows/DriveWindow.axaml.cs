using Avalonia.Controls;
using FileFunnel.ViewModels;
using FileFunnel.ViewModels.Windows;

namespace FileFunnel.Views.Windows;

public partial class DriveWindow : Window
{
    public DriveWindow()
    {
        InitializeComponent();
        // DataContext = new DriveWindowViewModel();
    }
}