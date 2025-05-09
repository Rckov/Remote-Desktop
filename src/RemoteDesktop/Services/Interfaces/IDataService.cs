using RemoteDesktop.Models;

using System.Collections.Generic;

namespace RemoteDesktop.Services.Interfaces;

internal interface IDataService
{
    ICollection<ServerGroup> Groups { get; }

    void Load();

    void Save();
}