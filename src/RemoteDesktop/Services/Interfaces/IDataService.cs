using RemoteDesktop.Models;

using System.Collections.Generic;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Provides methods to load and save server groups.
/// </summary>
internal interface IDataService
{
    /// <summary>
    /// Loads server groups from storage.
    /// </summary>
    /// <returns>List of server groups.</returns>
    IEnumerable<ServerGroup> Load();

    /// <summary>
    /// Saves server groups to storage.
    /// </summary>
    /// <param name="groups">Server groups to save.</param>
    void Save(IEnumerable<ServerGroup> groups);
}