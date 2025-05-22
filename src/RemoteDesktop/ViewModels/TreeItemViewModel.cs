using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Extensions;
using RemoteDesktop.Models;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

internal partial class TreeItemViewModel : ObservableObject
{
    [ObservableProperty]
    private object _model;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _icon;

    [ObservableProperty]
    private bool _isExpanded;

    [ObservableProperty]
    private bool _isVisible;

    [ObservableProperty]
    private ObservableCollection<TreeItemViewModel> _children;

    public TreeItemViewModel(string name, object model)
    {
        Name = name;
        Model = model;

        IsVisible = true;
        IsExpanded = true;

        SetIcon(Model);
    }

    public TreeItemViewModel(Server server) : this(server.Name, server)
    {
        Children = [];
    }

    public TreeItemViewModel(ServerGroup group) : this(group.Name, group)
    {
        Children = [.. group.Servers.ToTreeItems()];
    }

    public void AddServer(Server server)
    {
        if (Model is not ServerGroup groupModel)
        {
            return;
        }

        groupModel.Servers.Add(server);
        Children.Add(new TreeItemViewModel(server));
    }

    public void UpdateServer(Server server)
    {
        if (Model is not Server serverModel)
        {
            return;
        }

        Name = server.Name;
        serverModel.Name = server.Name;
        serverModel.Description = server.Description;
        serverModel.Host = server.Host;
        serverModel.Port = server.Port;
        serverModel.Username = server.Username;
        serverModel.Password = server.Password;
        serverModel.GroupName = server.GroupName;
    }

    public void UpdateServer(Server value, Server oldValue)
    {
        if (Model is not ServerGroup groupModel)
        {
            return;
        }

        var target = Children.GetByName(oldValue.Name);
        target?.UpdateServer(value);
    }

    public void RemoveServer(Server server)
    {
        if (Model is not ServerGroup groupModel)
        {
            return;
        }

        groupModel.Servers.Remove(server);

        var item = Children.GetByName(server.Name);

        if (item != null)
        {
            Children.Remove(item);
        }
    }

    public void UpdateGroup(ServerGroup group)
    {
        if (Model is not ServerGroup groupModel)
        {
            return;
        }

        Name = group.Name;
        groupModel.Name = group.Name;
        groupModel.Description = group.Description;

        foreach (var item in groupModel.Servers)
        {
            item.GroupName = group.Name;
        }
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

        IsVisible = Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || anyChildMatch;

        if (anyChildMatch)
        {
            IsExpanded = true;
        }

        return IsVisible;
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