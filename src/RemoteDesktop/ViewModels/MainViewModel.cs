using RemoteDesktop.Helpers;
using RemoteDesktop.Models;
using RemoteDesktop.Models.Base;

using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        ServersGroups = [.. TestGenerated.GenerateServerGroups(10).Select(g => new TreeItemViewModel(g))];
    }

    public ConnectedServerViewModel ActiveConnect { get; set; }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; set; } = [];
    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; set; } = [];

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

    public void ServerConnect(Server server)
    {
        if (ConnectedServers.Any(model => model.Equals(server)))
        {
            return;
        }

        var model = new ConnectedServerViewModel(server);
        model.Connect();

        ConnectedServers.Add(model);
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
        var filter = pattern ?? string.Empty;

        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(filter);
        }
    }
}