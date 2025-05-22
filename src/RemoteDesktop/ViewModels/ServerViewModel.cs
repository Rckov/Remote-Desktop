using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using RemoteDesktop.Models;
using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Parameters;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RemoteDesktop.ViewModels;

internal partial class ServerViewModel(IMessenger messenger) : ObservableValidator, IParameterReceiver
{
    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    private string _name;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    private string _host;

    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535")]
    private int _port = 3389;

    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    private string _username;

    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    private string _password;

    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    private string _groupName;

    [ObservableProperty]
    private ObservableCollection<string> _groupNames;

    private Server _oldServer;

    public event Action<bool> CloseRequest;

    [RelayCommand]
    public void Ok()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return;
        }

        var server = new Server
        {
            Name = Name,
            Description = Description,
            Host = Host,
            Port = Port,
            Username = Username,
            Password = Password,
            GroupName = GroupName
        };

        _ = _oldServer is null
            ? messenger.Send(new ValueMessage<Server>(ChangeAction.Create, server))
            : messenger.Send(new ValueMessage<Server>(ChangeAction.Update, server, _oldServer));

        CloseRequest?.Invoke(true);
    }

    public void SetParameter(object parameter = null)
    {
        if (parameter is not InputData<Server> data)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        GroupNames = [.. data.Names];

        if (data.Value == null)
        {
            return;
        }

        _oldServer = data.Value;

        Name = _oldServer.Name;
        Description = _oldServer.Description;
        Host = _oldServer.Host;
        Port = _oldServer.Port;
        Username = _oldServer.Username;
        Password = _oldServer.Password;
        GroupName = _oldServer.GroupName;
    }
}