using System;
using FileFunnel.Models;
using FileFunnel.Models.HardwareService;
using FileFunnel.Models.Helpers;
using FileFunnel.ViewModels.Windows;
using FileFunnel.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using FileFunnel.Services.WindowManager;

namespace FileFunnel;

internal class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((ctx, services) =>
            {
                ConfigureServices(services);
                ConfigureViewModels(services);
                ConfigureViews(services);
            }).Build();
        BuildAvaloniaApp(host.Services).StartWithClassicDesktopLifetime(args);
    }

    //This overload of the Avalonia app is required for the intelij designer to work
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .AfterSetup(_ => App.ServiceProvider = null!);

    private static AppBuilder BuildAvaloniaApp(IServiceProvider services) =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .AfterSetup(_ => App.ServiceProvider = services);

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IDataTemplate, ViewLocator>();
        services.AddSingleton<IWindowManager, WindowManager>(provider =>
            new WindowManager(provider.GetRequiredService<IDataTemplate>())
        );
        if (PlatformHelper.IsWindows)
            services.AddSingleton<IHardwareScanner, WindowsHardwareScanner>();
        else if (PlatformHelper.IsLinux)
            services.AddSingleton<IHardwareScanner, LinuxHardwareScanner>();
        else
            throw new PlatformNotSupportedException("Unsupported platform");
        Console.WriteLine("Platform: " +
                          (PlatformHelper.IsWindows ? "Windows" : PlatformHelper.IsLinux ? "Linux" : "Mac"));
        services.AddSingleton<IDiskManagement, DiskManagement>(provider =>
            new DiskManagement(provider.GetRequiredService<IHardwareScanner>()));
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.AddSingleton<DriveWindowViewModel>(provider =>
            new DriveWindowViewModel(provider.GetRequiredService<IDiskManagement>())
        );
        services.AddSingleton<MainWindowViewModel>(provider => new MainWindowViewModel(
            provider.GetRequiredService<IWindowManager>(),
            provider.GetRequiredService<DriveWindowViewModel>()));
    }

    private static void ConfigureViews(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>(provider => new MainWindow()
        {
            DataContext = provider.GetRequiredService<MainWindowViewModel>()
        });
        services.AddSingleton<DriveWindow>(provider => new DriveWindow()
        {
            DataContext = provider.GetRequiredService<DriveWindowViewModel>()
        });
    }
}