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
        ServersGroups = TestGenerated.GenerateServerGroups(10);

        foreach (var item in TestGenerated.GenerateServers(ServersGroups, 5))
        {
            ServerConnect(item.Server);
        }
    }

    public ObservableCollection<ServerGroup> ServersGroups { get; set; } = [];
    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; set; } = [];

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
}