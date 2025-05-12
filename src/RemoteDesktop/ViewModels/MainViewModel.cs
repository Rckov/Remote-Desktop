using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using RemoteDesktop.Models;
using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;

namespace RemoteDesktop.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    private readonly IWindowService _windowService;
    private readonly IMessenger _messenger;

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private TreeItemViewModel _selectedItem;

    public MainViewModel(IWindowService windowService, IMessenger messenger)
    {
        _windowService = windowService;
        _messenger = messenger;

        _messenger.Register<ValueChangedMessageEx<Server>>(this, (r, msg) => OnServerHandler(msg));
        _messenger.Register<ValueChangedMessageEx<ServerGroup>>(this, (r, msg) => OnGroupHandler(msg));
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; }
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
        _windowService.ShowDialog<ServerViewModel>();
    }

    [RelayCommand]
    private void CreateGroup()
    {
        _windowService.ShowDialog<ServerGroupViewModel>();
    }

    [RelayCommand]
    private void UpdateSelectedModel()
    {
        if (SelectedItem == null)
        {
            return;
        }

        var result = SelectedItem.Item switch
        {
            Server server => _windowService.ShowDialog<ServerViewModel>(server),
            ServerGroup group => _windowService.ShowDialog<ServerGroupViewModel>(group),
            _ => false
        };

        if (result == true)
        {
            // TO DO Save
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

    }

    private void OnGroupHandler(ValueChangedMessageEx<ServerGroup> message)
    {

    }
}