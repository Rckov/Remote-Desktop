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

internal class ServerViewModel : ValidatableViewModel, IParameterReceiver
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

    public Server Server { get; private set; }
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

        Server.Name = Name;
        Server.Description = Description;
        Server.Host = Host;
        Server.Port = Port;
        Server.Username = Username;
        Server.Password = Password;
        Server.GroupName = GroupName;

        CloseRequest?.Invoke(true);
    }

    public void SetParameter(object parameter = null)
    {
        if (parameter is not InputData<Server> data)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        Server = data.Value;
        GroupNames = [.. data.Names];

        Name = Server.Name;
        Description = Server.Description;
        Host = Server.Host;
        Port = Server.Port;
        Username = Server.Username;
        Password = Server.Password;
        GroupName = Server.GroupName;
    }
}