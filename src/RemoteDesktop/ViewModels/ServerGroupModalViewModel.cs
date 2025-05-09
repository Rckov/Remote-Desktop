using RemoteDesktop.Common;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;

using System;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class ServerGroupModalViewModel : BaseViewModel
{
    private IDataService _dataService;

    public ServerGroupModalViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    public string Name { get; private set; }

    public ICommand SaveCommand { get; private set; }

    public override void InitializeCommands()
    {
        base.InitializeCommands();
        SaveCommand = new RelayCommand(Save);
    }

    private void Save()
    {
        throw new NotImplementedException();
    }
}