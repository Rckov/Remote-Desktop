using System.Collections.ObjectModel;

namespace RemoteDesktop.Models;

internal class ServerGroup
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ObservableCollection<Server> Servers { get; set; } = [];

    public override string ToString()
    {
        return "Group";
    }
}