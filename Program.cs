using System;
using Avalonia.Controls;
using FileFunnel.Models.HardwareService;
using FileFunnel.ViewModels.Windows;
using FileFunnel.Views.Windows;
using StorageMedia.Helpers;

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
                ConfigureModel(services);
                ConfigureViewModels(services);
                ConfigureViews(services);
            }).Build();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();

    private static void ConfigureModel(IServiceCollection services)
    {
        if (PlatformHelper.IsWindows)
            services.AddSingleton<IHardwareScanner, WindowsHardwareScanner>();
        else if (PlatformHelper.IsLinux)
            services.AddSingleton<IHardwareScanner, LinuxHardwareScanner>();
        // else if (PlatformHelper.IsMac)
        //     services.AddSingleton<HardwareScanner, MacHardwareScanner>();
        else
            throw new PlatformNotSupportedException("Unsupported platform");
        Console.WriteLine("Platform: " +
                          (PlatformHelper.IsWindows ? "Windows" : PlatformHelper.IsLinux ? "Linux" : "Mac"));
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<DriveWindowViewModel>();
    }

    private static void ConfigureViews(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<DriveWindow>();
    }
}