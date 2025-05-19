using System;
using FileFunnel.Models;
using FileFunnel.Models.HardwareService;
using FileFunnel.Models.Helpers;
using FileFunnel.ViewModels.Windows;
using FileFunnel.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Avalonia;

namespace FileFunnel;

internal class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((ctx, services) =>
            {
                ConfigureModel(services);
                ConfigureViewModels(services);
                ConfigureViews(services);
            }).Build();

        BuildAvaloniaApp(host.Services).StartWithClassicDesktopLifetime(args);
    }

    private static AppBuilder BuildAvaloniaApp(IServiceProvider services) =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .AfterSetup(_ => App.ServiceProvider = services);

    private static void ConfigureModel(IServiceCollection services)
    {
        if (PlatformHelper.IsWindows)
            services.AddSingleton<IHardwareScanner, WindowsHardwareScanner>();
        else if (PlatformHelper.IsLinux)
            services.AddSingleton<IHardwareScanner, LinuxHardwareScanner>();
        else
            throw new PlatformNotSupportedException("Unsupported platform");
        Console.WriteLine("Platform: " +
                          (PlatformHelper.IsWindows ? "Windows" : PlatformHelper.IsLinux ? "Linux" : "Mac"));
        services.AddSingleton<IDiskManagement, DiskManagement>();
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<DriveWindowViewModel>(provider =>
            new DriveWindowViewModel(provider.GetRequiredService<IDiskManagement>())
        );
    }

    private static void ConfigureViews(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<DriveWindow>();
    }
}