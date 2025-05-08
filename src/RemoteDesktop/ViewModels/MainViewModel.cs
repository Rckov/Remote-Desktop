using RemoteDesktop.Components.Behaviors;
using RemoteDesktop.Components.Commands;
using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;
using RemoteDesktop.ViewModels.Dialogs;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;
    private readonly IThemeService _themeService;
    private readonly IStorageService _storageService;
    private readonly IWindowService _windowService;

    public MainViewModel(
        ISettingsService settingsService,
        IThemeService themeService,
        IStorageService storageService,
        IWindowService windowService)
    {
        _storageService = storageService;
        _settingsService = settingsService;
        _windowService = windowService;

        _themeService = themeService;
        _themeService.Apply(_settingsService.Settings.ThemeType);

        DropHandler = new TreeDropHandler();
        DropHandler.ItemMoved += OnServerGroupsChanged;

        ServersGroups = [.. _storageService.GetData<IEnumerable<ServerGroup>>().Select(x => new TreeItemViewModel(x))];
    }

    public ITreeDropHandler DropHandler { get; }

    public string SearchText
    {
        get;
        set
        {
            if (Set(ref field, value))
            {
                UpdateFilter(field);
            }
        }
    }

    public TreeItemViewModel SelectedTreeItem
    {
        get;
        set => Set(ref field, value);
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; }
    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; } = [];

    public ICommand ConnectCommand { get; private set; }
    public ICommand AddServerCommand { get; private set; }
    public ICommand AddServerGroupCommand { get; private set; }
    public ICommand EditSelectedTreeItemCommand { get; private set; }
    public ICommand DeleteSelectedTreeItemCommand { get; private set; }
    public ICommand ChangeThemeCommand { get; private set; }

    protected override void InitializeCommands()
    {
        AddServerCommand = new RelayCommand(AddServer);
        AddServerGroupCommand = new RelayCommand(AddServerGroup); ;
        EditSelectedTreeItemCommand = new RelayCommand(EditSelectedTreeItem); ;
        DeleteSelectedTreeItemCommand = new RelayCommand(DeleteSelectedTreeItem); ;
        ConnectCommand = new RelayCommand(Connect);
        ChangeThemeCommand = new RelayCommand(ChangeTheme);
    }

    private void Connect()
    {
        throw new NotImplementedException();
    }

    internal void Diconnect(ConnectedServerViewModel model)
    {
        throw new NotImplementedException();
    }

    private void AddServer()
    {
        var viewModel = new ServerEditViewModel(ServersGroups.GetServerGroupNames());

        if (_windowService.ShowDialog(viewModel) != true)
        {
            return;
        }

        var selectedGroup = ServersGroups.FindByName(viewModel.SelectedGroup);

        if (selectedGroup != null)
        {
            selectedGroup.AddServer(viewModel.Server);
            OnServerGroupsChanged();
        }
    }

    private void AddServerGroup()
    {
        throw new NotImplementedException();
    }

    private void EditSelectedTreeItem()
    {
    }

    private void DeleteSelectedTreeItem()
    {
        if (SelectedTreeItem is null)
        {
            return;
        }

        switch (SelectedTreeItem.Model)
        {
            case Server serverModel:
                var serverGroup = ServersGroups.FirstOrDefault();
                break;
            default:
                break;
        }

        if (SelectedTreeItem.Model is Server server)
        {
            var parentGroup = ServersGroups.FirstOrDefault(g => g.Children.Contains(SelectedTreeItem));
            parentGroup?.Children.Remove(SelectedTreeItem);
        }

        if (SelectedTreeItem.Model is ServerGroup group)
        {
            ServersGroups.Remove(SelectedTreeItem);
        }

        OnServerGroupsChanged();
    }

    private void ChangeTheme()
    {
        var curTheme = _themeService.CurrentTheme;
        var newTheme = curTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light;

        _themeService.Apply(newTheme);

        _settingsService.Settings.ThemeType = newTheme;
        _settingsService.SaveSettings();
    }

    private void UpdateFilter(string pattern)
    {
        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(pattern);
        }
    }

    private void OnServerGroupsChanged()
    {
        var groups = ServersGroups.GetServerGroups();
        _storageService.SaveData(groups);
    }
}