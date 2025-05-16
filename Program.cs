namespace FileFunnel;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

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
                
                // Windows
                services.AddSingleton<MainWindow>();
            })
            .Build();

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, shutdownAfterMainWindowClosed: false, desktop =>
        {
            desktop.MainWindow = host.Services.GetRequiredService<MainWindow>();
        });
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
                  .UsePlatformDetect()
                  .LogToTrace();
}