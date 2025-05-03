using RemoteDesktop.Models;
using RemoteDesktop.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace RemoteDesktop.ViewModels;

[DataContract]
[KnownType(typeof(Server))]
[KnownType(typeof(ServerGroup))]
internal class TreeItemViewModel : BaseViewModel
{
    [DataMember]
    public string Name
    {
        get;
        set => Set(ref field, value);
    }

    [DataMember]
    public object Model { get; private set; }

    [DataMember]
    public ObservableCollection<TreeItemViewModel> Children { get; set; }

    [DataMember]
    public bool IsVisible
    {
        get;
        set => Set(ref field, value);
    }

    [DataMember]
    public bool IsExpanded
    {
        get;
        set => Set(ref field, value);
    }

    protected TreeItemViewModel(string name, object model)
    {
        Name = name;
        Model = model;

        if (Model is INotifyPropertyChanged onPropertyChanged)
        {
            onPropertyChanged.PropertyChanged += Model_PropertyChanged;
        }

        IsVisible = true;
        IsExpanded = true;
    }

    private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Server.Name) || e.PropertyName == nameof(ServerGroup.Name))
        {
            if (Model is Server server)
            {
                Name = server.Name;
            }
            else if (Model is ServerGroup group)
            {
                Name = group.Name;
            }
        }
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