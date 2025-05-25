using Newtonsoft.Json;

using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RemoteDesktop.Services;

/// <summary>
/// Saves and loads ServerGroup collections encrypted in JSON format.
/// </summary>
internal class JsonDataService(string filePath) : IDataService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public IEnumerable<ServerGroup> Load()
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        try
        {
            var encryptedBytes = File.ReadAllBytes(filePath);
            var bytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            var data = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<IEnumerable<ServerGroup>>(data);
        }
        catch
        {
            return [];
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void Save(IEnumerable<ServerGroup> groups)
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        var data = JsonConvert.SerializeObject(groups, settings);
        var bytes = Encoding.UTF8.GetBytes(data);
        var encryptedBytes = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);

        File.WriteAllBytes(filePath, encryptedBytes);
    }
}