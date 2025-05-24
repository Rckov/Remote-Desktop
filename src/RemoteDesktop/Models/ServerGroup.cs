using System.Collections.ObjectModel;

namespace RemoteDesktop.Models;

/// <summary>
/// Represents a group of remote desktop servers.
/// </summary>
internal class ServerGroup
{
    /// <summary>
    /// Name of the group.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Optional description for the group.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// List of servers in this group.
    /// </summary>
    public ObservableCollection<Server> Servers { get; set; } = [];

    public override string ToString()
    {
        return "Group";
    }
}