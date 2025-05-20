using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RemoteDesktop.Extensions;

internal static class Extensions
{
    public static IEnumerable<TreeItemViewModel> ToTreeItems(this IEnumerable<Server> items)
    {
        CheckIsNull(items);
        return items.Select(x => new TreeItemViewModel(x));
    }

    public static IEnumerable<TreeItemViewModel> ToTreeItems(this IEnumerable<ServerGroup> items)
    {
        CheckIsNull(items);
        return items.Select(x => new TreeItemViewModel(x));
    }

    public static IEnumerable<string> GetNames(this ICollection<TreeItemViewModel> items)
    {
        CheckIsNull(items);
        return items.Select(x => x.Name);
    }

    public static IEnumerable<ServerGroup> GetGroups(this IEnumerable<TreeItemViewModel> items)
    {
        CheckIsNull(items);
        return items.Select(x => x.Model as ServerGroup);
    }

    public static TreeItemViewModel GetByName(this IEnumerable<TreeItemViewModel> items, string name)
    {
        CheckIsNull(items);
        CheckIsNull(name);
        return items.FirstOrDefault(x => x.Name == name);
    }

    public static Server Clone(this Server server)
    {
        return new Server
        {
            Name = server.Name,
            Description = server.Description,
            Host = server.Host,
            Username = server.Username,
            Password = server.Password,
            Port = server.Port,
            GroupName = server.GroupName
        };
    }

    public static void CheckIsNull<T>(T value, [CallerMemberName] string paramName = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }

        if (value is string s && string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentException("String parameter cannot be null, empty, or whitespace.", paramName);
        }
    }
}