using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using FileFunnel.ViewModels.Windows;
using FileFunnel.Views.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace FileFunnel;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (Design.IsDesignMode || ServiceProvider is null)
        {
            base.OnFrameworkInitializationCompleted();
            return;
        }
        
        var locator = ServiceProvider.GetRequiredService<IDataTemplate>();
        DataTemplates.Add(locator);
        var lifetime = (IClassicDesktopStyleApplicationLifetime)ApplicationLifetime!;
        lifetime.MainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        lifetime.MainWindow.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();
        base.OnFrameworkInitializationCompleted();
        
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}