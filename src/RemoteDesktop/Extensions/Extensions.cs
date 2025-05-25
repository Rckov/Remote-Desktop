using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RemoteDesktop.Extensions;

internal static class Extensions
{
    /// <summary>
    /// Converts a collection of <see cref="Server"/> into <see cref="TreeItemViewModel"/>.
    /// </summary>
    /// <param name="items">Collection of servers, must not be null.</param>
    public static IEnumerable<TreeItemViewModel> ToTreeItems(this IEnumerable<Server> items)
    {
        CheckIsNull(items);
        return items.Select(x => new TreeItemViewModel(x));
    }

    /// <summary>
    /// Converts a collection of <see cref="ServerGroup"/> into <see cref="TreeItemViewModel"/>.
    /// </summary>
    /// <param name="items">Collection of server groups, must not be null.</param>
    public static IEnumerable<TreeItemViewModel> ToTreeItems(this IEnumerable<ServerGroup> items)
    {
        CheckIsNull(items);
        return items.Select(x => new TreeItemViewModel(x));
    }

    /// <summary>
    /// Checks if the value is null.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="paramName">Parameter name for exception (auto-filled).</param>
    /// <exception cref="ArgumentNullException">If value is null.</exception>
    /// <exception cref="ArgumentException">If string is empty or whitespace.</exception>
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