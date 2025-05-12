using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Models;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal partial class TreeItemViewModel : ObservableObject
{
    [ObservableProperty]
    private object _item;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private bool _isExpanded;

    [ObservableProperty]
    private bool _isVisible;

    [ObservableProperty]
    public ObservableCollection<TreeItemViewModel> _children;

    public TreeItemViewModel(string name, object item)
    {
        Name = name;
        Item = item;

        IsVisible = true;
        IsExpanded = true;
    }

    public TreeItemViewModel(Server server) : this(server.Name, server)
    {
        Children = [];
    }

    public TreeItemViewModel(ServerGroup group) : this(group.Name, group)
    {
        Children = [.. group.Servers.Select(s => new TreeItemViewModel(s))];
    }

    public bool ApplyFilter(string filter)
    {
        var anyChildMatch = false;

        foreach (var child in Children)
        {
            if (child.ApplyFilter(filter))
            {
                anyChildMatch = true;
            }
        }

        IsVisible = Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 || anyChildMatch;

        if (anyChildMatch)
        {
            IsExpanded = true;
        }

        return IsVisible;
    }
}