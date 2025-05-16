using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RemoteDesktop.Services.Implementation;

internal class JsonDataService(string filePath) : IDataService
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public IList<ServerGroup> Load()
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<ServerGroup>>(json, _options) ?? [];
        }
        catch (Exception ex) when (ex is IOException or JsonException)
        {
            return [];
        }
    }

    public void Save(IEnumerable<ServerGroup> groups)
    {
        var json = JsonSerializer.Serialize(groups, _options);
        File.WriteAllText(filePath, json);
    }
}