using RemoteDesktop.ViewModels;

using System.Collections.Generic;

namespace RemoteDesktop.Services.Interfaces;

internal interface IStorageService
{
    void SaveData(IEnumerable<TreeItemViewModel> groups);

    IEnumerable<TreeItemViewModel> LoadData();
}