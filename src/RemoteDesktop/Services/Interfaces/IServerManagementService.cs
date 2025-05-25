using RemoteDesktop.Models;

using System.Collections.ObjectModel;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Provides methods to manage server groups and servers, including CRUD operations and persistence.
/// </summary>
internal interface IServerManagerService
{
    /// <summary>
    /// Loads all server groups from storage.
    /// </summary>
    /// <returns>A collection of server groups.</returns>
    ObservableCollection<ServerGroup> LoadData();

    /// <summary>
    /// Adds a new server to the specified group.
    /// </summary>
    /// <param name="server">The server to add.</param>
    /// <param name="groupName">The name of the group to which the server will be added.</param>
    void AddServer(Server server, string groupName);

    /// <summary>
    /// Updates the server's group if it has changed.
    /// </summary>
    /// <param name="server">The server to update.</param>
    /// <param name="oldGroupName">The original group name before update.</param>
    void UpdateServer(Server server, string oldGroupName);

    /// <summary>
    /// Deletes the specified server from its group.
    /// </summary>
    /// <param name="server">The server to delete.</param>
    void DeleteServer(Server server);

    /// <summary>
    /// Adds a new server group.
    /// </summary>
    /// <param name="group">The server group to add.</param>
    void AddGroup(ServerGroup group);

    /// <summary>
    /// Updates the group name and refreshes group references in contained servers.
    /// </summary>
    /// <param name="group">The server group to update.</param>
    /// <param name="oldName">The old group name.</param>
    void UpdateGroup(ServerGroup group, string oldName);

    /// <summary>
    /// Deletes the specified server group.
    /// </summary>
    /// <param name="group">The server group to delete.</param>
    void DeleteGroup(ServerGroup group);

    /// <summary>
    /// Moves a server to a different group.
    /// </summary>
    /// <param name="server">The server to move.</param>
    /// <param name="newGroupName">The target group name.</param>
    void MoveServer(Server server, string newGroupName);

    /// <summary>
    /// Persists all server groups and servers to storage.
    /// </summary>
    void SaveData();
}