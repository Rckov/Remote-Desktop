using RemoteDesktop.Models;
using RemoteDesktop.Models.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal class TreeItemViewModel : ObservableObject
{
    public string Name { get; }
    public ObservableCollection<TreeItemViewModel> Children { get; }

    private bool _isVisible = true;

    public bool IsVisible
    {
        get => _isVisible;
        set => Set(ref _isVisible, value);
    }

    private bool _isExpanded;

    public bool IsExpanded
    {
        get => _isExpanded;
        set => Set(ref _isExpanded, value);
    }

    public TreeItemViewModel(ServerGroup group)
    {
        Name = group.Name;
        Children = [.. group.Servers.Select(s => new TreeItemViewModel(s))];
    }

    public TreeItemViewModel(Server server)
    {
        Name = server.Name;
        Children = [];
    }

    public bool ApplyFilter(string filter)
    {
        var anyChildMatch = false;
        var selfMatch = Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;

        foreach (var child in Children)
        {
            if (child.ApplyFilter(filter))
            {
                anyChildMatch = true;
            }
        }

        IsVisible = selfMatch || anyChildMatch;

        if (anyChildMatch)
        {
            IsExpanded = true;
        }

        return IsVisible;
    }
}