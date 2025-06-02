using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FileFunnel.Services.WindowManager;

namespace FileFunnel.ViewModels.Windows;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    private readonly IWindowManager _windowManager;
    private readonly DriveWindowViewModel _driveWindowViewModel;

    public MainWindowViewModel(IWindowManager viewManager, DriveWindowViewModel driveWindowViewModel)
    {
        _windowManager = viewManager;
        _driveWindowViewModel = driveWindowViewModel;
    }
    
    [RelayCommand]
    private void OpenDriveWindow()
    {
        _windowManager.ShowWindow(_driveWindowViewModel);
    }
}