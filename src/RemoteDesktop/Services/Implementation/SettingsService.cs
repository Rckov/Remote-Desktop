using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.IO;
using System.Runtime.Serialization.Json;

namespace RemoteDesktop.Services.Implementation;

internal class SettingsService : ISettingsService
{
    private const string FileName = "settings.json";
    private static readonly string PathFileName = Path.Combine(App.DataPath, FileName);

    public SettingsService()
    {
        Settings = LoadSettings();
    }

    public Settings Settings { get; }

    public Settings LoadSettings()
    {
        if (!File.Exists(PathFileName))
        {
            return new Settings();
        }

        var serializer = new DataContractJsonSerializer(typeof(Settings));
        using var stream = File.OpenRead(PathFileName);

        return (Settings)serializer.ReadObject(stream);
    }

    public void SaveSettings()
    {
        var serializer = new DataContractJsonSerializer(typeof(Settings));
        using var stream = File.Create(PathFileName);

        serializer.WriteObject(stream, Settings);
    }
}