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
    /// Converts a collection of <see cref="Server"/> objects into a collection of <see cref="TreeItemViewModel"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="Server"/> objects to convert. Cannot be <see langword="null"/>.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="TreeItemViewModel"/> objects corresponding to the input
    /// <see cref="Server"/> objects.</returns>
    public static IEnumerable<TreeItemViewModel> ToTreeItems(this IEnumerable<Server> items)
    {
        CheckIsNull(items);
        return items.Select(x => new TreeItemViewModel(x));
    }

    /// <summary>
    /// Converts a collection of <see cref="ServerGroup"/> objects into a collection of <see cref="TreeItemViewModel"/> objects.
    /// </summary>
    /// <param name="items">The collection of <see cref="ServerGroup"/> objects to convert. Cannot be <see langword="null"/>.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="TreeItemViewModel"/> objects,  where each item corresponds
    /// to a <see cref="ServerGroup"/> in the input collection.</returns>
    public static IEnumerable<TreeItemViewModel> ToTreeItems(this IEnumerable<ServerGroup> items)
    {
        CheckIsNull(items);
        return items.Select(x => new TreeItemViewModel(x));
    }

    /// <summary>
    /// Validates that the specified value is not null or, in the case of a string, not empty or whitespace.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to check for null or invalid string content.</param>
    /// <param name="paramName">The name of the parameter being validated. This is automatically populated with the caller's member name if not
    /// explicitly provided.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is a string that is empty or consists only of whitespace.</exception>
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