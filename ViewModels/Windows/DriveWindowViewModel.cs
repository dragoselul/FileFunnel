using System.Collections.Generic;
using System.IO;
using FileFunnel.Models;

namespace FileFunnel.ViewModels.Windows;

public partial class DriveWindowViewModel : ViewModelBase
{
    private readonly IDiskManagement _diskManagementService;
    public DriveWindowViewModel(IDiskManagement _diskManagementService)
    {
        this._diskManagementService = _diskManagementService;
        // this._diskManagementService.GetDisks();
    }
}