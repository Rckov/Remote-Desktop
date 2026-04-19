using RemoteDesktop.Models;

using System.Collections.Generic;

namespace RemoteDesktop.Services.Abstractions;

internal interface IDataService
{
    (List<ServerGroup> Groups, List<Server> Servers) Load();
    void Save(IEnumerable<ServerGroup> groups, IEnumerable<Server> servers);
}
