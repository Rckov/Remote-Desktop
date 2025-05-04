using RemoteDesktop.Models;
using RemoteDesktop.Models.Base;

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal class TreeItemViewModel : ObservableObject
{
    public TreeItemViewModel(string name, object model)
    {
        Name = name;
        Model = model;

        if (Model is INotifyPropertyChanged notify)
        {
            PropertyChangedEventManager.AddHandler(notify, OnNamePropertyChanged, nameof(Name));
        }

        IsVisible = true;
        IsExpanded = true;
    }

    public TreeItemViewModel(Server server) : this(server.Name, server)
    {
        Children = [];
        CollectionChangedEventManager.AddHandler(Children, OnChildrenCollectionChanged);
    }

    public TreeItemViewModel(ServerGroup group) : this(group.Name, group)
    {
        Children = [.. group.Servers.Select(s => new TreeItemViewModel(s))];
        CollectionChangedEventManager.AddHandler(Children, OnChildrenCollectionChanged);
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

    private void OnNamePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Name))
        {
            switch (Model)
            {
                case Server m:
                    Name = m.Name; 
                    break;
                case ServerGroup m:
                    Name = m.Name;
                    break;

                default:
                    break;
            }
        }
    }

    private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (Model is not ServerGroup group)
        {
            return;
        }

        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var item = (TreeItemViewModel)e.NewItems[0];
            var newIndex = e.NewStartingIndex < 0 ? 0 : e.NewStartingIndex;

            if (item.Model is Server server)
            {
                group.Servers.Insert(newIndex, server);
            }
        }

        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            var item = (TreeItemViewModel)e.OldItems[0];

            if (item.Model is Server server)
            {
                group.Servers.Remove(server);
            }
        }
    }
}