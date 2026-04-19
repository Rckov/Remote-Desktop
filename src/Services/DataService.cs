using RemoteDesktop.Models;
using RemoteDesktop.Services.Abstractions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace RemoteDesktop.Services;

internal class DataService : IDataService
{
    private static readonly string FilePath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "data.dat");

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public (List<ServerGroup> Groups, List<Server> Servers) Load()
    {
        if (!File.Exists(FilePath))
            return ([], []);

        try
        {
            var json = Decrypt(File.ReadAllBytes(FilePath));
            var dto = JsonSerializer.Deserialize<ServerData>(json);

            return dto is null ? ([], []) : (dto.Groups, dto.Servers);
        }
        catch
        {
            return ([], []);
        }
    }

    public void Save(IEnumerable<ServerGroup> groups, IEnumerable<Server> servers)
    {
        var dto = new ServerData { Groups = [..groups], Servers = [..servers] };
        File.WriteAllBytes(FilePath, Encrypt(JsonSerializer.Serialize(dto, JsonOptions)));
    }

    private static byte[] Encrypt(string value) =>
        ProtectedData.Protect(Encoding.UTF8.GetBytes(value), null, DataProtectionScope.CurrentUser);

    private static string Decrypt(byte[] data) =>
        Encoding.UTF8.GetString(ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser));
}
