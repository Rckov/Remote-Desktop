using System.Collections.ObjectModel;

namespace RemoteDesktop.Models;

internal class ServerGroup
{
    public string Name { get; set; }
    public ObservableCollection<Server> Servers { get; set; } = [];
}