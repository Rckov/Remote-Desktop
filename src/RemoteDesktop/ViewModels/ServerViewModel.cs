using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using RemoteDesktop.Models;
using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;

using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteDesktop.ViewModels;

internal partial class ServerViewModel(IMessenger messenger) : ObservableValidator, IParameterReceiver
{
    [ObservableProperty]
    private string _title = "Create server";

    [ObservableProperty]
    private string _buttonSuccess = "Create";

    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
    [MaxLength(20, ErrorMessage = "Maximum length is 20 characters")]
    private string _name;

    [ObservableProperty]
    [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
    [MaxLength(20, ErrorMessage = "Maximum length is 20 characters")]
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
    private Server _server;

    [ObservableProperty]
    private Server _oldServer;

    public void SetParameter(object parameter)
    {
        if (parameter is not Server server)
        {
            return;
        }

        Title = "Edit Server";
        ButtonSuccess = "Save changes";

        Name = server.Name;
        Description = server.Description;
        Host = server.Host;
        Port = server.Port;
        Username = server.Username;
        Password = server.Password;
        GroupName = server.GroupName;

        OldServer = server;
    }

    public event Action<bool> CloseRequest;

    [RelayCommand]
    public void Ok()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return;
        }

        FillProperties();
        SendServerMessage();

        CloseRequest?.Invoke(true);
    }

    private void FillProperties()
    {
        Server.Name = Name;
        Server.Description = Description;
        Server.Host = Host;
        Server.Port = Port;
        Server.Username = Username;
        Server.Password = Password;
        Server.GroupName = GroupName;
    }

    private void SendServerMessage()
    {
        if (Server is null)
        {
            throw new InvalidOperationException("Server is not initialized.");
        }

        _ = OldServer is null
            ? messenger.Send(new ValueChangedMessageEx<Server>(ChangeAction.Create, Server))
            : messenger.Send(new ValueChangedMessageEx<Server>(ChangeAction.Update, Server, OldServer));
    }
}