using RemoteDesktop.Models;
using RemoteDesktop.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal class TreeItemViewModel : BaseViewModel
{
    public string Name { get; }
    public object Model { get; }
    public ObservableCollection<TreeItemViewModel> Children { get; }

    public bool IsVisible
    {
        get;
        set => Set(ref field, value);
    }

    public bool IsExpanded
    {
        get;
        set => Set(ref field, value);
    }

    protected TreeItemViewModel(string name, object model)
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