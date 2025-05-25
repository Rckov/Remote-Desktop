using RemoteDesktop.Common;
using RemoteDesktop.Common.Base;
using RemoteDesktop.Common.Parameters;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class ServerGroupViewModel : ValidatableViewModel, IParameterReceiver
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

    public ServerGroup ServerGroup { get; private set; }
    public ObservableCollection<string> GroupNames { get; private set; }

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

        ServerGroup.Name = Name;
        ServerGroup.Description = Description;

        CloseRequest?.Invoke(true);
    }

    public void SetParameter(object parameter = null)
    {
        if (parameter is not InputData<ServerGroup> data)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        ServerGroup = data.Value;
        GroupNames = [.. data.Names];

        Name = ServerGroup.Name;
        Description = ServerGroup.Description;
    }
}