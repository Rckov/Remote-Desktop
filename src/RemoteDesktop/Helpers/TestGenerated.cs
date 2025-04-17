using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.Helpers;

internal class TestGenerated
{
    private static readonly Random _random = new();

    public static ObservableCollection<ConnectedServerViewModel> GenerateServers(ObservableCollection<ServerGroup> groups, int count)
    {
        var servers = groups.SelectMany(g => g.Servers);
        var serversGenrated = Enumerable.Range(0, count).Select(r => servers.ElementAt(r));

        return [.. serversGenrated.Select(x => new ConnectedServerViewModel(x))];
    }

    public static ObservableCollection<ServerGroup> GenerateServerGroups(int count)
    {
        var groups = new ObservableCollection<ServerGroup>();

        for (var i = 0; i < count; i++)
        {
            var group = new ServerGroup
            {
                Name = $"Group {i}",
                Servers = GenerateServers(_random.Next(1, 6))
            };

            groups.Add(group);
        }

        return groups;
    }

    private static ObservableCollection<Server> GenerateServers(int count)
    {
        var servers = new ObservableCollection<Server>();

        for (var i = 0; i < count; i++)
        {
            var server = new Server
            {
                Name = $"Server {i}",
                Description = $"Description {i}",
                Host = $"server{i}.example.com",
                Port = 10000,
            };

            servers.Add(server);
        }

        return servers;
    }

}