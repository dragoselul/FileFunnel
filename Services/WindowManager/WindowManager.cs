namespace FileFunnel.Services.WindowManager;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

public class WindowManager : IWindowManager
{
    private readonly IDataTemplate _viewLocator;
    private readonly List<Window> _openWindows = new();

    public WindowManager(IDataTemplate viewLocator)
    {
        _viewLocator = viewLocator;
    }

    public Window CreateWindow(object viewModel)
    {
        // Use your ViewLocator to build the View (Control) for this VM
        if (_viewLocator.Build(viewModel) is not Control view)
            throw new InvalidOperationException($"No view found for {viewModel.GetType()}");

        // Wrap it in a window
        var window = new Window
        {
            Content = view,
            DataContext = viewModel,
            Width = view.Width,  // or default
            Height = view.Height, // you can adjust
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        window.Closed += (_, __) => _openWindows.Remove(window);
        _openWindows.Add(window);
        return window;
    }

    public void ShowWindow(object viewModel)
    {
        var win = CreateWindow(viewModel);
        win.Show();
    }

    public Task<T?> ShowDialog<T>(object viewModel, Window? owner = null)
    {
        var win = CreateWindow(viewModel);
        return win.ShowDialog<T>(owner);  // sets Owner under the hood
    }

    /// <summary>
    /// Example helper: close all non-main windows.
    /// </summary>
    public void CloseAllExcept(Window? mainWindow)
    {
        foreach (var w in _openWindows.ToArray())
            if (w != mainWindow)
                w.Close();
    }
}
