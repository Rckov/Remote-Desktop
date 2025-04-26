using RemoteDesktop.Helpers;
using RemoteDesktop.Infrastructure;
using RemoteDesktop.Models;
using RemoteDesktop.Models.Base;
using RemoteDesktop.Services.Interfaces;

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : ObservableObject
{
    private IMessenger _messenger;
    private IThemeManager _themeManager;

    public MainViewModel(IMessenger messenger, IThemeManager themeManager)
    {
        _messenger = messenger;
        _themeManager = themeManager;

        ServersGroups = [.. TestGenerated.GenerateServerGroups(10).Select(g => new TreeItemViewModel(g))];

        ConnectCommand = new RelayCommand(ServerConnect);
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
}