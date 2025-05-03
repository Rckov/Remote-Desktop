using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace RemoteDesktop.Services.Implementation;

internal class JsonStorageService : IStorageService
{
    private const string FileName = "servers.json";
    private static readonly string PathFileName = Path.Combine(App.DataPath, FileName);

    public void SaveData(IEnumerable<TreeItemViewModel> groups)
    {
        var serializer = new DataContractJsonSerializer(typeof(IEnumerable<TreeItemViewModel>));
        using var stream = File.Create(PathFileName);

        serializer.WriteObject(stream, groups);
    }

    public IEnumerable<TreeItemViewModel> LoadData()
    {
        if (!File.Exists(PathFileName))
        {
            return [];
        }

        var serializer = new DataContractJsonSerializer(typeof(IEnumerable<TreeItemViewModel>));
        using var stream = File.OpenRead(PathFileName);

        return (IEnumerable<TreeItemViewModel>)serializer.ReadObject(stream);
    }
}