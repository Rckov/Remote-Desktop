using RemoteDesktop.Common;
using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class ServerModalViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    public ServerModalViewModel(IDataService dataService, Server server = null)
    {
        _dataService = dataService;

        Server = new();
        server?.CopyPropertiesTo(Server);

        Port = 3389;
        Groups = [.. _dataService.Groups.Select(x => x.Name)];
    }

    public ICommand SaveCommand { get; private set; }

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

    public override void InitializeCommands()
    {
        base.InitializeCommands();
        SaveCommand = new RelayCommand(Save);
    }

    private void Save()
    {
        // TO DO
        IList<string> errors =
        [
            ValidateProperty(nameof(Name)),
            ValidateProperty(nameof(Host)),
            ValidateProperty(nameof(Username)),
            ValidateProperty(nameof(Password)),
            ValidateProperty(nameof(Port)),
            ValidateProperty(nameof(SelectedGroup)),
        ];

        errors = [.. errors.Where(e => e != null)];

        if (errors.Any())
        {
            ErrorMessageBox(string.Join(Environment.NewLine, errors));
            return;
        }

        if (_dataService.Groups.ServerExists(SelectedGroup, Name))
        {
            ErrorMessageBox("Server with this name already exists in the selected group");
            return;
        }

        Server.Name = Name;
        Server.Host = Host;
        Server.Description = Description;
        Server.Username = Username;
        Server.Password = Password;
        Server.Port = Port;

        Ok();
    }

    private string ValidateProperty(string columnName)
    {
        // TO DO
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
}