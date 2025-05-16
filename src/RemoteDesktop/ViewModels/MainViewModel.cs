using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using RemoteDesktop.Models;
using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    private readonly IWindowService _window;
    private readonly IMessenger _messenger;
    private readonly INotificationService _notification;
    private readonly IDataService _data;

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private TreeItemViewModel _selectedItem;

    public MainViewModel(
        IWindowService window,
        IMessenger messenger,
        INotificationService notification,
        IDataService dataService)
    {
        _window = window;
        _messenger = messenger;
        _notification = notification;
        _data = dataService;

        var loadedGroups = _data.Load();
        ServersGroups = new ObservableCollection<TreeItemViewModel>(
            loadedGroups.Select(g => new TreeItemViewModel(g))
        );

        _messenger.Register<ValueChangedMessageEx<Server>>(this, (r, msg) => OnServerHandler(msg));
        _messenger.Register<ValueChangedMessageEx<ServerGroup>>(this, (r, msg) => OnGroupHandler(msg));
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; } = [];
    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; } = [];

    [RelayCommand]
    private void Connect()
    {
        throw new NotImplementedException();
    }

    internal void Diconnect(ConnectedServerViewModel model)
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void CreateServer()
    {
        var groups = ServersGroups.Select(x => x.Name);
        _window.ShowDialog<ServerViewModel>(groups);
    }

    [RelayCommand]
    private void CreateGroup()
    {
        var groups = ServersGroups.Select(x => x.Name);
        _window.ShowDialog<ServerGroupViewModel>(groups);
    }

    [RelayCommand]
    private void UpdateSelectedModel()
    {
        if (SelectedItem == null)
        {
            return;
        }
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
        var server = message.Value;
        var groupVm = ServersGroups
            .FirstOrDefault(vm => vm.Item is ServerGroup g && g.Name == server.GroupName);

        if (groupVm is null)
            return;

        switch (message.Action)
        {
            case ChangeAction.Create:
                groupVm.Children.Add(new TreeItemViewModel(server));
                if (groupVm.Item is ServerGroup group)
                    group.Servers.Add(server);
                break;

            case ChangeAction.Update:
                var existingVm = groupVm.Children.FirstOrDefault(vm => vm.Item == message.OldValue);
                if (existingVm != null)
                {
                    existingVm.Item = server;
                    existingVm.Name = server.Name;

                    if (groupVm.Item is ServerGroup g)
                    {
                        var index = g.Servers.IndexOf(message.OldValue);
                        if (index >= 0)
                            g.Servers[index] = server;
                    }
                }
                break;
        }

        SaveState();
    }

    private void OnGroupHandler(ValueChangedMessageEx<ServerGroup> message)
    {
        switch (message.Action)
        {
            case ChangeAction.Create:
                ServersGroups.Add(new TreeItemViewModel(message.Value));
                break;

            case ChangeAction.Update:
                var existingVm = ServersGroups.FirstOrDefault(vm => vm.Item == message.OldValue);
                if (existingVm != null)
                {
                    existingVm.Item = message.Value;
                    existingVm.Name = message.Value.Name;

                    existingVm.Children.Clear();
                    foreach (var server in message.Value.Servers)
                        existingVm.Children.Add(new TreeItemViewModel(server));
                }
                break;
        }

        SaveState();
    }

    private void SaveState()
    {
        var groups = ServersGroups
            .Where(vm => vm.Item is ServerGroup)
            .Select(vm => (ServerGroup)vm.Item);

        _data.Save(groups);
    }
}