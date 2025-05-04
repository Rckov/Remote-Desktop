using RemoteDesktop.Models;
using RemoteDesktop.Models.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal class TreeItemViewModel : ObservableObject
{
    public TreeItemViewModel(string name, object model)
    {
        Name = name;
        Model = model;

        IsVisible = true;
        IsExpanded = true;
    }

    public TreeItemViewModel(ServerGroup group) : this(group.Name, group)
    {
        Children = [.. group.Servers.Select(s => new TreeItemViewModel(s))];
    }

    public TreeItemViewModel(Server server) : this(server.Name, server)
    {
        Children = [];
    }

    public string Name
    {
        get;
        set => Set(ref field, value);
    }

    public object Model { get; private set; }

    public ObservableCollection<TreeItemViewModel> Children { get; set; }

    public bool IsExpanded
    {
        get;
        set => Set(ref field, value);
    }

    public bool IsVisible
    {
        get;
        set => Set(ref field, value);
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