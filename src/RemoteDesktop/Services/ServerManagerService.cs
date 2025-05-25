using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.Services;

/// <summary>
/// Manages server groups and servers: add, update, delete, move, and persist changes.
/// </summary>
internal class ServerManagerService : IServerManagerService
{
    private readonly ObservableCollection<ServerGroup> _serverGroups;
    private readonly IDataService _dataService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerManagerService"/> class and loads existing data.
    /// </summary>
    /// <param name="dataService">Service used for data persistence.</param>
    public ServerManagerService(IDataService dataService)
    {
        _dataService = dataService;
        _serverGroups = new ObservableCollection<ServerGroup>(_dataService.Load());
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public ObservableCollection<ServerGroup> LoadData() => _serverGroups;

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void SaveData()
    {
        _dataService.Save(_serverGroups);
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void AddServer(Server server, string groupName)
    {
        var group = _serverGroups.FirstOrDefault(g => g.Name == groupName);
        group?.Servers.Add(server);
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void UpdateServer(Server server, string oldGroupName)
    {
        if (server.GroupName != oldGroupName)
        {
            MoveServer(server, server.GroupName);
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void DeleteServer(Server server)
    {
        var group = _serverGroups.FirstOrDefault(g => g.Name == server.GroupName);
        group?.Servers.Remove(server);
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void AddGroup(ServerGroup group)
    {
        _serverGroups.Add(group);
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void UpdateGroup(ServerGroup group, string oldName)
    {
        if (group.Name != oldName)
        {
            foreach (var server in group.Servers)
            {
                server.GroupName = group.Name;
            }
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void DeleteGroup(ServerGroup group)
    {
        _serverGroups.Remove(group);
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void MoveServer(Server server, string newGroupName)
    {
        var oldGroup = _serverGroups.FirstOrDefault(g => g.Name == server.GroupName);
        var newGroup = _serverGroups.FirstOrDefault(g => g.Name == newGroupName);

        if (oldGroup != null && newGroup != null && oldGroup != newGroup)
        {
            oldGroup.Servers.Remove(server);
            newGroup.Servers.Add(server);
            server.GroupName = newGroupName;
        }
    }
}