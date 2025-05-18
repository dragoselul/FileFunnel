using Avalonia.Controls;
using FileFunnel.ViewModels.Windows;
using FileFunnel.Views.Windows;

namespace FileFunnel;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using FileFunnel.ViewModels;
using FileFunnel.Views;

internal class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((ctx, services) =>
            {
                // Core services
                // services.AddSingleton<IFileSorter, FileSorter>();
                // services.AddSingleton<IDriveScanner, DriveScannerWorker>();
                
                // ViewModels
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<DriveWindowViewModel>();
                
                // Windows
                services.AddSingleton<MainWindow>();
                services.AddSingleton<DriveWindow>();
            })
            .Build();

        // BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, desktop =>
        // {
        //     desktop.MainWindow = host.Services.GetRequiredService<MainWindow>();
        // });
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
                  .UsePlatformDetect()
                  .LogToTrace();
}