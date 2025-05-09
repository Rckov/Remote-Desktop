using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;

namespace RemoteDesktop.Services.Implementation;

internal class LocalDataServce : IDataService
{
    public string DataPath { get; private set; }
    public ICollection<ServerGroup> Groups { get; private set; }

    public LocalDataServce()
    {
        DataPath = Path.Combine(Bootstrapper.DataPath, "servers.json");
    }

    public void Load()
    {
        try
        {
            if (File.Exists(DataPath))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ServerGroup>));

                using (var stream = File.OpenRead(DataPath))
                {
                    Groups = (List<ServerGroup>)serializer.ReadObject(stream);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to load servers data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void Save()
    {
        try
        {
            var serializer = new DataContractJsonSerializer(typeof(List<ServerGroup>));

            using (var stream = File.Create(DataPath))
            {
                serializer.WriteObject(stream, Groups);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to save servers data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}