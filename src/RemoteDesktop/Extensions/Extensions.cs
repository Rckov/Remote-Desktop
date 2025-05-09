using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoteDesktop.Extensions;

internal static class Extensions
{
    public static bool GroupExists(this ICollection<ServerGroup> groups, string groupName)
    {
        CheckIsNull(groups);
        CheckIsNull(groupName);

        return groups.Any(x => string.Equals(x.Name, groupName, StringComparison.Ordinal));
    }

    public static bool ServerExists(this ICollection<ServerGroup> groups, string groupName, string serverName)
    {
        CheckIsNull(groups);
        CheckIsNull(groupName);
        CheckIsNull(serverName);

        var group = groups.FirstOrDefault(x => string.Equals(x.Name, groupName, StringComparison.Ordinal));

        if (group == null)
        {
            return false;
        }
        else
        {
            return group.Servers.Any(x => string.Equals(x.Name, serverName, StringComparison.Ordinal));
        }
    }

    public static TreeItemViewModel FindByName(this ICollection<TreeItemViewModel> treeItems, string groupName)
    {
        CheckIsNull(treeItems);
        CheckIsNull(groupName);

        return treeItems.FirstOrDefault(x => string.Equals(x.Name, groupName, StringComparison.OrdinalIgnoreCase));
    }

    public static TreeItemViewModel FindParent(this ICollection<TreeItemViewModel> treeItems, TreeItemViewModel treeItem)
    {
        CheckIsNull(treeItems);
        CheckIsNull(treeItem);

        return treeItems.FirstOrDefault(parent => parent.Children.Contains(treeItem));
    }

    public static void CopyPropertiesTo(this Server source, Server target)
    {
        CheckIsNull(source);
        CheckIsNull(target);

        target.Name = source.Name;
        target.Description = source.Description;
        target.Host = source.Host;
        target.Username = source.Username;
        target.Password = source.Password;
        target.Port = source.Port;
    }

    public static void CheckIsNull<T>(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }
    }
}