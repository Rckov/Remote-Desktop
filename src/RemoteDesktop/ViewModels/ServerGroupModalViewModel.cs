using RemoteDesktop.Common;
using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class ServerGroupModalViewModel : BaseViewModel
{
    private IDataService _dataService;

    public ServerGroupModalViewModel(IDataService dataService, ServerGroup group = null)
    {
        _dataService = dataService;

        Group = new();
        group?.CopyPropertiesTo(Group);
    }

    public string Name
    {
        get;
        set => Set(ref field, value);
    }

    public ServerGroup Group { get; private set; }

    public ICommand SaveCommand { get; private set; }

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
        ];

        errors = [.. errors.Where(e => e != null)];

        if (errors.Any())
        {
            ErrorMessageBox(string.Join(Environment.NewLine, errors));
            return;
        }

        if (_dataService.Groups.GroupExists(Name))
        {
            ErrorMessageBox("Group with this name already exists");
            return;
        }

        Group.Name = Name;

        Ok();
    }

    private string ValidateProperty(string columnName)
    {
        // TO DO
        return columnName switch
        {
            nameof(Name) when string.IsNullOrWhiteSpace(Name) => "Name cannot be empty",

            _ => null
        };
    }
}