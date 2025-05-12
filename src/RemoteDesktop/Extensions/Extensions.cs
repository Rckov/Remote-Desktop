using RemoteDesktop.Models;

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

    public static void CheckIsNull<T>(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }
    }
}