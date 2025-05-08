using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoteDesktop.Extensions;

internal static class Extensions
{
    public static IEnumerable<ServerGroup> GetServerGroups(this ICollection<TreeItemViewModel> treeItems)
    {
        if (treeItems == null)
        {
            throw new ArgumentNullException(nameof(treeItems));
        }

        foreach (var item in treeItems)
        {
            if (item.Model is ServerGroup group)
            {
                yield return group;
            }
        }
    }

    public static IEnumerable<string> GetServerGroupNames(this ICollection<TreeItemViewModel> treeItems)
    {
        if (treeItems == null)
        {
            throw new ArgumentNullException(nameof(treeItems));
        }

        return treeItems.GetServerGroups().Select(group => group.Name);
    }

    public static TreeItemViewModel FindByName(this ICollection<TreeItemViewModel> treeItems, string name)
    {
        if (treeItems == null)
        {
            throw new ArgumentNullException(nameof(treeItems));
        }

        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return treeItems.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public static void CopyPropertiesTo(this Server source, Server target)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        target.Name = source.Name;
        target.Description = source.Description;
        target.Host = source.Host;
        target.Username = source.Username;
        target.Password = source.Password;
        target.Port = source.Port;
    }
}