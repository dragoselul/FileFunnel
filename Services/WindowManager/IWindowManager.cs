using System.Threading.Tasks;
using Avalonia.Controls;

namespace FileFunnel.Services.WindowManager;

public interface IWindowManager
{
    /// <summary>
    /// Creates (but does not show) a window for the given VM.
    /// </summary>
    Window CreateWindow(object viewModel);

    /// <summary>
    /// Shows a non-modal window.
    /// </summary>
    void ShowWindow(object viewModel);

    /// <summary>
    /// Shows a dialog and returns the dialog result.
    /// </summary>
    Task<T?> ShowDialog<T>(object viewModel, Window? owner = null);
}
