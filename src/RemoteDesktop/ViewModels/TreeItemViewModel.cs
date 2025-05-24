using RemoteDesktop.Common.Base;
using RemoteDesktop.Models;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;
internal class TreeItemViewModel : BaseViewModel
{
    public string Name
    {
        get;
        set => Set(ref field, value);
    }

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

    public ObservableCollection<TreeItemViewModel> Servers = [];

    public bool ApplyFilter(string filter)
    {
        var anyChildMatch = false;

        foreach (var child in Servers)
        {
            if (child.ApplyFilter(filter))
            {
                anyChildMatch = true;
            }
        }

        IsVisible = Name.Contains(filter) || anyChildMatch;

        if (anyChildMatch)
        {
            IsExpanded = true;
        }

        return IsVisible;
    }
}
