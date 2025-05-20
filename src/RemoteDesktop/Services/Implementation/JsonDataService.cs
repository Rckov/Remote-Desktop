using Newtonsoft.Json;

using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace RemoteDesktop.Services.Implementation;

internal class JsonDataService(string filePath) : IDataService
{
    public IEnumerable<ServerGroup> Load()
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        try
        {
            var data = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<ServerGroup>>(data);
        }
        catch
        {
            return [];
        }
    }

    public void Save(IEnumerable<ServerGroup> groups)
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        var data = JsonConvert.SerializeObject(groups, settings);
        File.WriteAllText(filePath, data);
    }
}