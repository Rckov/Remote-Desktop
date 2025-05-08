using RemoteDesktop.Components.Commands;
using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels.Dialogs;

internal class ServerEditViewModel : BaseViewModel, IDataErrorInfo
{
    private bool _isValidationActive = false;

    public ServerEditViewModel(IEnumerable<string> groups, Server server = null)
    {
        Groups = [.. groups];
        Server = new Server();

        if (server is not null)
        {
            server.CopyPropertiesTo(Server);
        }

        Port = 3389;
    }

    public event Action<bool> CloseRequest;

    public ICommand SaveCommand { get; private set; }
    public ICommand CloseCommand { get; private set; }

    public Server Server { get; private set; }

    public ObservableCollection<string> Groups { get; private set; }

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

    public string Host
    {
        get;
        set => Set(ref field, value);
    }

    public string Username
    {
        get;
        set => Set(ref field, value);
    }

    public string Password
    {
        get;
        set => Set(ref field, value);
    }

    public int Port
    {
        get;
        set => Set(ref field, value);
    }

    public string SelectedGroup
    {
        get;
        set => Set(ref field, value);
    }

    public string this[string columnName]
    {
        get => ValidateProperty(columnName);
    }

    public string Error { get; }

    protected override void InitializeCommands()
    {
        SaveCommand = new RelayCommand(Save, HasErrors);
        CloseCommand = new RelayCommand(Close);
    }

    private void Save()
    {
        _isValidationActive = true;

        if (HasErrors())
        {
            return;
        }

        Server.Name = Name;
        Server.Host = Host;
        Server.Description = Description;
        Server.Username = Username;
        Server.Password = Password;
        Server.Port = Port;

        CloseRequest?.Invoke(true);
    }

    private void Close()
    {
        CloseRequest?.Invoke(false);
    }

    private string ValidateProperty(string columnName)
    {
        if (!_isValidationActive)
        {
            return null;
        }

        return columnName switch
        {
            nameof(Name) when string.IsNullOrWhiteSpace(Name) => "Name cannot be empty",
            nameof(Host) when string.IsNullOrWhiteSpace(Host) => "Host cannot be empty",
            nameof(Username) when string.IsNullOrWhiteSpace(Username) => "Username cannot be empty",
            nameof(Password) when string.IsNullOrWhiteSpace(Password) => "Password cannot be empty",
            nameof(Port) when Port < 1 || Port > 65535 => "Port must be between 1 and 65535",
            nameof(SelectedGroup) when string.IsNullOrWhiteSpace(SelectedGroup) => "Please select a server group",

            _ => null
        };
    }

    private bool HasErrors()
    {
        return !string.IsNullOrEmpty(this[nameof(Name)]) ||
               !string.IsNullOrEmpty(this[nameof(Host)]) ||
               !string.IsNullOrEmpty(this[nameof(Username)]) ||
               !string.IsNullOrEmpty(this[nameof(Password)]) ||
               !string.IsNullOrEmpty(this[nameof(Port)]) ||
               !string.IsNullOrEmpty(this[nameof(SelectedGroup)]);
    }
}