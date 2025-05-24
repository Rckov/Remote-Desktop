using RemoteDesktop.Common;
using RemoteDesktop.Common.Base;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class ServerGroupViewModel(IMessengerService messengerService) : ValidatableViewModel, IParameterReceiver
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