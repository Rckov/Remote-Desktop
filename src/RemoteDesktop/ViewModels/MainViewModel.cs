using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using HandyControl.Data;

using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Parameters;

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using MessageBox = HandyControl.Controls.MessageBox;

namespace RemoteDesktop.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    private readonly IWindowService _windowService;
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;
    private readonly INotificationService _notificationService;

    public MainViewModel(IWindowService windowService, IMessenger messenger, IDataService dataService, INotificationService notificationService)
    {
        _windowService = windowService;
        _messenger = messenger;
        _dataService = dataService;
        _notificationService = notificationService;

        ServersGroups = [.. _dataService.Load().ToTreeItems()];
        ConnectedServers.CollectionChanged += (_, _) => HasConnectedServers = ConnectedServers.Any();

        _messenger.Register<ValueMessage<Server>>(this, (r, msg) => OnServerHandler(msg));
        _messenger.Register<ValueMessage<ServerGroup>>(this, (r, msg) => OnGroupHandler(msg));
    }

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private bool _hasConnectedServers;

    [ObservableProperty]
    private TreeItemViewModel _selectedItem;

    [ObservableProperty]
    private ObservableCollection<TreeItemViewModel> _serversGroups;

    [ObservableProperty]
    private ObservableCollection<ConnectedServerViewModel> _connectedServers = [];

    [RelayCommand]
    private void Connect()
    {
        if (SelectedItem?.Model is not Server server)
        {
            return;
        }

        var newConnection = new ConnectedServerViewModel(server)
        {
            IsSelected = true,
            IsConnected = true
        };
        newConnection.OnDisconnected += Connection_OnDisconnected;

        ConnectedServers.Add(newConnection);
    }

    [RelayCommand]
    internal void Disconnect(ConnectedServerViewModel connection)
    {
        if (connection.IsConnected)
        {
            connection.IsConnected = false;
            ConnectedServers.Remove(connection);
        }
    }

    [RelayCommand]
    private void CreateServer()
    {
        CreateOrUpdateModel<Server, ServerViewModel>();
    }

    [RelayCommand]
    private void CreateGroup()
    {
        CreateOrUpdateModel<ServerGroup, ServerGroupViewModel>();
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedItem == null)
        {
            return;
        }

        var result = SelectedItem.Model switch
        {
            Server server => CreateOrUpdateModel<Server, ServerViewModel>(server),
            ServerGroup group => CreateOrUpdateModel<ServerGroup, ServerGroupViewModel>(group),
            _ => false
        };

        if (result)
        {
            _notificationService.Show($"{SelectedItem.Model} '{SelectedItem.Name}' has been updated.", InfoType.Info);
        }
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedItem == null)
        {
            return;
        }

        switch (SelectedItem.Model)
        {
            case Server server:
                DeleteServer(server);
                break;

            case ServerGroup group:
                DeleteGroup(group);
                break;
        }

        Save();
    }

    public void HandleServerUpdate(Server value, Server oldValue)
    {
        if (value == null || oldValue == null)
        {
            return;
        }

        if (value.GroupName != oldValue.GroupName)
        {
            MoveServer(value, value.GroupName);
        }
        else
        {
            var group = ServersGroups.GetByName(oldValue.GroupName);
            group?.UpdateServer(value, oldValue);
        }
    }

    public void MoveServer(Server server, string newGroupName)
    {
        var oldGroup = ServersGroups.GetByName(server.GroupName);
        var newGroup = ServersGroups.GetByName(newGroupName);

        if (oldGroup == null || newGroup == null || oldGroup == newGroup)
        {
            return;
        }

        oldGroup.RemoveServer(server);
        server.GroupName = newGroupName;
        newGroup.AddServer(server);

        Save();
    }

    public void Save()
    {
        _dataService.Save(ServersGroups.GetGroups());
    }

    private void DeleteServer(Server server)
    {
        if (!Ask($"Are you sure you want to delete the server '{server.Name}'?"))
        {
            return;
        }

        var groupItem = ServersGroups.GetByName(server.GroupName);
        groupItem?.RemoveServer(server);

        _notificationService.Show($"Server '{server.Name}' has been deleted.", InfoType.Info);
    }

    private void DeleteGroup(ServerGroup group)
    {
        if (!Ask($"Are you sure you want to delete the group '{group.Name}'?"))
        {
            return;
        }

        var groupItem = ServersGroups.GetByName(group.Name);
        if (groupItem != null)
        {
            ServersGroups.Remove(groupItem);
        }

        _notificationService.Show($"Group '{group.Name}' has been deleted.", InfoType.Info);
    }

    private void OnServerHandler(ValueMessage<Server> msg)
    {
        switch (msg.Action)
        {
            case ChangeAction.Create:
                HandleServerCreation(msg.Value);
                break;

            case ChangeAction.Update:
                HandleServerUpdate(msg.Value, msg.OldValue);
                break;
        }

        Save();
    }

    private void OnGroupHandler(ValueMessage<ServerGroup> msg)
    {
        switch (msg.Action)
        {
            case ChangeAction.Create:
                HandleGroupCreation(msg.Value);
                break;

            case ChangeAction.Update:
                HandleGroupUpdate(msg.Value, msg.OldValue);
                break;
        }

        Save();
    }

    private void HandleServerCreation(Server value)
    {
        if (value == null)
        {
            return;
        }

        var target = ServersGroups.GetByName(value.GroupName);
        target?.AddServer(value);
    }

    private void HandleGroupCreation(ServerGroup value)
    {
        if (value == null)
        {
            return;
        }

        ServersGroups.Add(new TreeItemViewModel(value));
    }

    private void HandleGroupUpdate(ServerGroup value, ServerGroup oldValue)
    {
        if (value == null || oldValue == null)
        {
            return;
        }

        var target = ServersGroups.GetByName(oldValue.Name);
        target?.UpdateGroup(value);
    }

    partial void OnSearchTextChanged(string value)
    {
        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(value);
        }
    }

    private void Connection_OnDisconnected(object sender, string e)
    {
        if (sender is not ConnectedServerViewModel model)
        {
            return;
        }

        model.OnDisconnected -= Connection_OnDisconnected;

        MessageShow(model.ErrorReason);
        ConnectedServers.Remove(model);
    }

    private bool CreateOrUpdateModel<T, TViewModel>(T model = default) where TViewModel : class
    {
        var list = ServersGroups.GetNames();
        var data = new InputData<T>(model, list);

        return _windowService.ShowDialog<TViewModel>(data) == true;
    }

    private bool Ask(string message, string caption = "Question")
    {
        var info = new MessageBoxInfo
        {
            Message = message,
            Caption = caption,
            Button = MessageBoxButton.YesNo,
            IconBrushKey = ResourceToken.AccentBrush,
            IconKey = ResourceToken.AskGeometry
        };

        return MessageBox.Show(info) == MessageBoxResult.Yes;
    }

    private void MessageShow(string message)
    {
        var info = new MessageBoxInfo
        {
            Message = message,
            Caption = "Error",
            Button = MessageBoxButton.OK,
            IconBrushKey = ResourceToken.AccentBrush,
            IconKey = ResourceToken.ErrorGeometry
        };

        MessageBox.Show(info);
    }
}