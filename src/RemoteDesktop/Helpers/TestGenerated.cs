using RemoteDesktop.Models;

using System;
using System.Collections.Generic;

namespace RemoteDesktop.Helpers;

internal class TestGenerated
{
    public static IEnumerable<Server> GenerateServers()
    {
        var random = new Random();

        for (int i = 0; i < 5; i++)
        {
            var server = new Server
            {
                Name = $"Сервер {i + 1}",
                Description = $"Описание сервера {i + 1}",
                Host = $"host{i + 1}.com",
                Password = $"password{i + 1}",
                Port = random.Next(1, 1000)
            };

            yield return server;
        }
    }
}