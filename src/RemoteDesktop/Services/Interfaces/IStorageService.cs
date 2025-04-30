using RemoteDesktop.Models;

using System.Collections.Generic;

namespace RemoteDesktop.Services.Interfaces;

internal interface IStorageService
{
    void SaveData(IEnumerable<ServerGroup> groups);

    IEnumerable<ServerGroup> LoadData();
}