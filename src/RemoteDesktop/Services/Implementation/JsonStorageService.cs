using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace RemoteDesktop.Services.Implementation;

internal class JsonStorageService : IStorageService
{
    private const string FileName = "servers.json";

    public void SaveData(IEnumerable<ServerGroup> groups)
    {
        var serializer = new DataContractJsonSerializer(typeof(IEnumerable<ServerGroup>));
        using var stream = File.Create(FileName);

        serializer.WriteObject(stream, groups);
    }

    public IEnumerable<ServerGroup> LoadData()
    {
        if (!File.Exists(FileName))
        {
            return [];
        }

        var serializer = new DataContractJsonSerializer(typeof(List<ServerGroup>));
        using var stream = File.OpenRead(FileName);

        return (IEnumerable<ServerGroup>)serializer.ReadObject(stream);
    }
}