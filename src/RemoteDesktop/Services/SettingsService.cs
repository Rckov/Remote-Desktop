using Newtonsoft.Json;

using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.IO;

namespace RemoteDesktop.Services;

/// <summary>
/// Manages loading and saving application settings to a JSON file.
/// </summary>
internal class SettingsService(string filePath) : ISettingsService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public Settings Settings { get; private set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void LoadSettings()
    {
        if (!File.Exists(filePath))
        {
            Settings = new();
            return;
        }

        try
        {
            var data = File.ReadAllText(filePath);
            Settings = JsonConvert.DeserializeObject<Settings>(data) ?? new();
        }
        catch
        {
            Settings = new();
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void SaveSettings()
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        var data = JsonConvert.SerializeObject(Settings, settings);
        File.WriteAllText(filePath, data);
    }
}