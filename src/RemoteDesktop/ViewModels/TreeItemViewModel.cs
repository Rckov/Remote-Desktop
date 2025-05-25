using RemoteDesktop.Common.Base;
using RemoteDesktop.Extensions;
using RemoteDesktop.Models;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal class TreeItemViewModel : BaseViewModel
{
    public TreeItemViewModel(string name, object model)
    {
        Name = name;
        ItemModel = model;
        IsVisible = true;
        IsExpanded = true;

        SetIcon(ItemModel);
    }

    public TreeItemViewModel(Server server) : this(server.Name, server)
    {
        Servers = [];
    }

    public TreeItemViewModel(ServerGroup group) : this(group.Name, group)
    {
        Servers = [.. group.Servers.ToTreeItems()];
        group.Servers.CollectionChanged += Servers_CollectionChanged;
    }

    public object ItemModel
    {
        get;
        set => Set(ref field, value);
    }

    public string Name
    {
        get;
        set => Set(ref field, value);
    }

    public string Icon
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

    public ObservableCollection<TreeItemViewModel> Servers { get; }

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

    private void Servers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (Server newServer in e.NewItems)
            {
                Servers.Add(new TreeItemViewModel(newServer));
            }

            return;
        }

        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (Server removedServer in e.OldItems)
            {
                var item = Servers.FirstOrDefault(c => c.ItemModel == removedServer);
                if (item != null)
                {
                    Servers.Remove(item);
                }
            }

            return;
        }
    }

    // Create/Move to IIconService
    private void SetIcon(object model)
    {
        if (model is Server)
        {
            Icon = "\uE7F4";
        }
    }
}