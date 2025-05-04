using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;

namespace RemoteDesktop.Services.Implementation;

internal class SettingsService : ISettingsService
{
    public string SettingPath { get; }

    public SettingsService()
    {
        SettingPath = Path.Combine(Bootstrapper.SettingPath, "settings.json");
        Settings = LoadSettings();
    }

    public Settings Settings { get; }

    public Settings LoadSettings()
    {
        try
        {
            if (File.Exists(SettingPath))
            {
                var serializer = new DataContractJsonSerializer(typeof(Settings));

                using (var stream = File.OpenRead(SettingPath))
                {
                    return (Settings)serializer.ReadObject(stream);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to load settings.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        return new();
    }

    public void SaveSettings()
    {
        try
        {
            var serializer = new DataContractJsonSerializer(typeof(Settings));

            using (var stream = File.Create(SettingPath))
            {
                serializer.WriteObject(stream, Settings);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to save settings.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}