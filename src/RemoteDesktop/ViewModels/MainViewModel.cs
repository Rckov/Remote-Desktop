using HandyControl.Data;

using RemoteDesktop.Common;
using RemoteDesktop.Common.Base;
using RemoteDesktop.Common.Parameters;
using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private readonly IWindowService _windowService;
    private readonly INotificationService _notificationService;
    private readonly IServerManagerService _managementService;

    public MainViewModel(IWindowService windowService, INotificationService notificationService, IServerManagerService managementService)
    {
        _windowService = windowService;
        _notificationService = notificationService;
        _managementService = managementService;

        var serverGroups = managementService.LoadData();
        ServersGroups = new ObservableCollection<TreeItemViewModel>(serverGroups.ToTreeItems());
    }

    public string SearchText
    {
        get;
        set
        {
            if (Set(ref field, value))
            {
                OnSearchTextChanged(value);
            }
        }
    }

    public bool HasConnectedServers
    {
        get;
        set => Set(ref field, value);
    }

    public TreeItemViewModel SelectedItem
    {
        get;
        set => Set(ref field, value);
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; }

    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; } = [];

    public ICommand ConnectCommand { get; private set; }
    public ICommand CreateServerCommand { get; private set; }
    public ICommand CreateServerGroupCommand { get; private set; }
    public ICommand EditCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public override void InitializeCommands()
    {
        ConnectCommand = new RelayCommand(Connect);
        CreateServerCommand = new RelayCommand(CreateServer);
        CreateServerGroupCommand = new RelayCommand(CreateGroup);
        EditCommand = new RelayCommand(Edit);
        DeleteCommand = new RelayCommand(Delete);
    }

    private void Connect()
    {
        if (SelectedItem?.ItemModel is not Server server)
        {
            return;
        }

        if (ConnectedServers.Any(x => x.Server.Host == server.Host))
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

    public void Disconnect(ConnectedServerViewModel connection)
    {
        throw new NotImplementedException();
    }

    private void CreateServer()
    {
        var newServer = new Server();
        if (OpenWindow<Server, ServerViewModel>(newServer))
        {
            _managementService.AddServer(newServer, newServer.GroupName);
            _managementService.SaveData();

            _notificationService.Show($"Server '{newServer.Name}' added to group '{newServer.GroupName}'.", InfoType.Success);
        }
    }

    private void CreateGroup()
    {
        var newGroup = new ServerGroup();
        if (OpenWindow<ServerGroup, ServerGroupViewModel>(newGroup))
        {
            _managementService.AddGroup(newGroup);
            _managementService.SaveData();

            ServersGroups.Add(new TreeItemViewModel(newGroup));

            _notificationService.Show($"Group '{newGroup.Name}' created.", InfoType.Success);
        }
    }

    private void Edit()
    {
        if (SelectedItem == null)
        {
            return;
        }

        switch (SelectedItem.ItemModel)
        {
            case Server server:
                EditServer(server);
                break;

            case ServerGroup group:
                EditGroup(group);
                break;
        }
    }

    private void Delete()
    {
        if (SelectedItem == null)
        {
            return;
        }

        switch (SelectedItem.ItemModel)
        {
            case Server server:
                if (_notificationService.Ask($"Delete server '{server.Name}'?"))
                {
                    DeleteServer(server);
                }
                break;

            case ServerGroup group:
                if (_notificationService.Ask($"Delete group '{group.Name}' and all its servers?"))
                {
                    DeleteGroup(group);
                }
                break;
        }
    }

    private void EditServer(Server server)
    {
        var oldGroupName = server.GroupName;

        if (OpenWindow<Server, ServerViewModel>(server))
        {
            _managementService.UpdateServer(server, oldGroupName);
            _managementService.SaveData();

            SelectedItem.Name = server.Name;

            _notificationService.Show($"Server '{server.Name}' updated.", InfoType.Info);
        }
    }

    private void EditGroup(ServerGroup group)
    {
        var oldName = group.Name;

        if (OpenWindow<ServerGroup, ServerGroupViewModel>(group))
        {
            _managementService.UpdateGroup(group, oldName);
            _managementService.SaveData();

            SelectedItem.Name = group.Name;

            _notificationService.Show($"Group renamed to '{group.Name}'.", InfoType.Info);
        }
    }

    private void DeleteServer(Server server)
    {
        _managementService.DeleteServer(server);
        _managementService.SaveData();

        _notificationService.Show($"Server '{server.Name}' deleted.", InfoType.Info);
    }

    private void DeleteGroup(ServerGroup group)
    {
        _managementService.DeleteGroup(group);
        _managementService.SaveData();

        ServersGroups.Remove(SelectedItem);

        _notificationService.Show($"Group '{group.Name}' deleted.", InfoType.Info);
    }

    private void OnSearchTextChanged(string value)
    {
        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(value);
        }
    }

    private bool OpenWindow<T, TViewModel>(T model = default) where TViewModel : class
    {
        var names = ServersGroups.Select(x => x.Name);
        var inputData = new InputData<T>(model, names);

        return _windowService.ShowDialog<TViewModel>(inputData) == true;
    }

    private void Connection_OnDisconnected(object sender, string message)
    {
        if (sender is not ConnectedServerViewModel model)
        {
            return;
        }

        _notificationService.MessageShow(model.ErrorReason);

        model.OnDisconnected -= Connection_OnDisconnected;
        ConnectedServers.Remove(model);
    }
}