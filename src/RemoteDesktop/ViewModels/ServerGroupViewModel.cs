using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using RemoteDesktop.Models;
using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;

using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteDesktop.ViewModels;

internal partial class ServerGroupViewModel(IMessenger messenger) : ObservableValidator, IParameterReceiver
{
    [ObservableProperty]
    private string _title = "Create group";

    [ObservableProperty]
    private string _buttonSuccess = "Create";

    [ObservableProperty]
    [Required(ErrorMessage = "Required field")]
    [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
    [MaxLength(15, ErrorMessage = "Maximum length is 15 characters")]
    private string _name;

    [ObservableProperty]
    private ServerGroup _group;

    [ObservableProperty]
    private ServerGroup _oldGroup;

    public void SetParameter(object parameter)
    {
        if (parameter is not ServerGroup group)
        {
            return;
        }

        Title = "Edit Group";
        ButtonSuccess = "Save changes";

        Name = group.Name;
        OldGroup = group;
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
        SendGroupMessage();

        CloseRequest?.Invoke(true);
    }

    private void FillProperties()
    {
        Group.Name = Name;
    }

    private void SendGroupMessage()
    {
        if (Group is null)
        {
            throw new InvalidOperationException("ServerGroup is not initialized.");
        }

        _ = OldGroup is null
            ? messenger.Send(new ValueChangedMessageEx<ServerGroup>(ChangeAction.Create, Group))
            : messenger.Send(new ValueChangedMessageEx<ServerGroup>(ChangeAction.Update, Group, OldGroup));
    }
}