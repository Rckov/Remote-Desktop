using RemoteDesktop.Services.Interfaces;

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;

namespace RemoteDesktop.Services.Implementation;

internal class JsonStorageService : IStorageService
{
    public string DataPath { get; }

    public JsonStorageService()
    {
        DataPath = Path.Combine(Bootstrapper.DataPath, "servers.json");
    }

    public T GetData<T>()
    {
        try
        {
            if (File.Exists(DataPath))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                using (var stream = File.OpenRead(DataPath))
                {
                    return (T)serializer.ReadObject(stream);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to load servers data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        return default;
    }

    public void SaveData<T>(T data)
    {
        try
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var stream = File.Create(DataPath))
            {
                serializer.WriteObject(stream, data);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to save servers data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}