using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using HandyControl.Themes;

using RemoteDesktop.Models;
using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;

namespace RemoteDesktop.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    private readonly IWindowService _window;
    private readonly IMessenger _messenger;
    private readonly INotificationService _notification;

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private TreeItemViewModel _selectedItem;

    public MainViewModel(IWindowService window, IMessenger messenger, INotificationService notification)
    {
        _window = window;
        _messenger = messenger;
        _notification = notification;

        _messenger.Register<ValueChangedMessageEx<Server>>(this, (r, msg) => OnServerHandler(msg));
        _messenger.Register<ValueChangedMessageEx<ServerGroup>>(this, (r, msg) => OnGroupHandler(msg));
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; }
    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; } = [];

    [RelayCommand]
    private void Connect()
    {
        var curTheme = Theme.GetSkin(null);
    }

    internal void Diconnect(ConnectedServerViewModel model)
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void CreateServer()
    {
        _window.ShowDialog<ServerViewModel>();
    }

    [RelayCommand]
    private void CreateGroup()
    {
        _window.ShowDialog<ServerGroupViewModel>();
    }

    [RelayCommand]
    private void UpdateSelectedModel()
    {
        if (SelectedItem == null)
        {
            return;
        }

    }

    [RelayCommand]
    private void ChangeTheme()
    {
    }

    partial void OnSearchTextChanged(string value)
    {
        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(value);
        }
    }

    private void OnServerHandler(ValueChangedMessageEx<Server> message)
    {
    }

    private void OnGroupHandler(ValueChangedMessageEx<ServerGroup> message)
    {
    }
}