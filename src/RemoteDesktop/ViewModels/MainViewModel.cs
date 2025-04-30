using RemoteDesktop.Infrastructure;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private IMessenger _messenger;
    private IThemeManager _themeManager;
    private IStorageService _storage;

    public MainViewModel(IMessenger messenger, IThemeManager themeManager, IStorageService storage)
    {
        _messenger = messenger;
        _themeManager = themeManager;
        _storage = storage;

        ServersGroups = [.. _storage.LoadData().Select(x => new TreeItemViewModel(x))];

        ServersGroups.Add(new TreeItemViewModel(new ServerGroup()
        {
            Name = "Test",
            Servers =
            [
                new Server()
                {
                    Name = "Server Test 1"
                }
            ]
        }));

        ConnectCommand = new RelayCommand(ServerConnect);
        ThemeChangeCommand = new RelayCommand(ThemeChange);
    }

    public TreeItemViewModel SelectedTreeItem
    {
        get;
        set => Set(ref field, value);
    }

    public ConnectedServerViewModel ActiveConnect
    {
        get;
        set => Set(ref field, value);
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; set; } = [];
    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; set; } = [];

    public ICommand ConnectCommand { get; }
    public ICommand ThemeChangeCommand { get; }

    public string SearchText
    {
        get;
        set
        {
            if (Set(ref field, value))
            {
                UpdateFilter(field);
            }
        }
    }

    public void ServerConnect()
    {
        if (SelectedTreeItem.Model is not Server server)
        {
            return;
        }

        if (ConnectedServers.Any(model => model.Name == server.Name))
        {
            return;
        }

        var con = new ConnectedServerViewModel(server);
        con.Connect();

        ConnectedServers.Add(con);
    }

    public void ServerDiconnect(ConnectedServerViewModel model)
    {
        if (!ConnectedServers.Contains(model))
        {
            return;
        }

        if (model.IsConnected)
        {
            model.Disconnect();
        }

        ConnectedServers.Remove(model);
    }

    private void UpdateFilter(string pattern)
    {
        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(pattern);
        }
    }

    private void ThemeChange()
    {
        var curTheme = _themeManager.CurrentTheme;
        var newTheme = curTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light;

        _themeManager.Apply(newTheme);
    }
}