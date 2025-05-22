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

public partial class ServerGroupViewModel(IMessenger messenger) : ObservableValidator, IParameterReceiver
{
    [ObservableProperty]
    [CustomValidation(typeof(ServerGroupViewModel), nameof(ValidateName))]
    private string _name;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private ObservableCollection<string> _groupNames;

    private ServerGroup _oldGroup;

    public event Action<bool> CloseRequest;

    [RelayCommand]
    public void Ok()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return;
        }

        var group = new ServerGroup
        {
            Name = Name,
            Description = Description,
        };

        _ = _oldGroup is null
            ? messenger.Send(new ValueMessage<ServerGroup>(ChangeAction.Create, group))
            : messenger.Send(new ValueMessage<ServerGroup>(ChangeAction.Update, group, _oldGroup));

        CloseRequest?.Invoke(true);
    }

    public void SetParameter(object parameter = null)
    {
        if (parameter is not InputData<ServerGroup> data)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        GroupNames = [.. data.Names];

        if (data.Value == null)
        {
            return;
        }

        _oldGroup = data.Value;

        Name = _oldGroup.Name;
        Description = _oldGroup.Description;
    }

    public static ValidationResult ValidateName(string name, ValidationContext context)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new("Required field");
        }

        var instance = (ServerGroupViewModel)context.ObjectInstance;

        if (instance.GroupNames.Contains(name))
        {
            return new("This name already exists");
        }

        return ValidationResult.Success;
    }
}