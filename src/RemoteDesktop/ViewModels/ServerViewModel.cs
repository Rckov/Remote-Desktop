using RemoteDesktop.Common;
using RemoteDesktop.Common.Base;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class ServerViewModel(IMessengerService messengerService) : ValidatableViewModel, IParameterReceiver
{
    [Required(ErrorMessage = "Required field")]
    public string Name
    {
        get;
        set => Set(ref field, value);
    }

    public string Description
    {
        get;
        set => Set(ref field, value);
    }

    [Required(ErrorMessage = "Required field")]
    public string Host
    {
        get;
        set => Set(ref field, value);
    }

    [Required(ErrorMessage = "Required field")]
    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535")]
    public int Port
    {
        get;
        set => Set(ref field, value);
    }

    [Required(ErrorMessage = "Required field")]
    public string Username
    {
        get;
        set => Set(ref field, value);
    }

    [Required(ErrorMessage = "Required field")]
    public string Password
    {
        get;
        set => Set(ref field, value);
    }

    [Required(ErrorMessage = "Required field")]
    public string GroupName
    {
        get;
        set => Set(ref field, value);
    }

    public ObservableCollection<string> GroupNames { get; } = [];

    public event Action<bool> CloseRequest;

    public ICommand SaveCommand { get; private set; }

    public override void InitializeCommands()
    {
        SaveCommand = new RelayCommand(Save);
    }

    private void Save()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return;
        }

        CloseRequest?.Invoke(true);
    }

    public void SetParameter(object parameter = null)
    {
        throw new NotImplementedException();
    }
}