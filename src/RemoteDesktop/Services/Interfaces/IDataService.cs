using RemoteDesktop.Models;

using System.Collections.Generic;

namespace RemoteDesktop.Services.Interfaces;

internal interface IDataService
{
    IList<ServerGroup> Load();

    void Save(IEnumerable<ServerGroup> groups);
}