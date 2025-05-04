using RemoteDesktop.Components.Behaviors;
using RemoteDesktop.Components.Commands;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;

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

    public MainViewModel(ISettingsService settingsService, IThemeService themeService, IStorageService storageService)
    {
        _storageService = storageService;
        _settingsService = settingsService;

        _themeService = themeService;
        _themeService.Apply(_settingsService.Settings.ThemeType);

        DropHandler = new TreeDropHandler();
        DropHandler.ItemMoved += OnServersGroupsChanged;

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

    public ICommand ThemeChangeCommand { get; private set; }

    protected override void InitializeCommands()
    {
        ThemeChangeCommand = new RelayCommand(ThemeChange);
    }

    private void ThemeChange()
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

    private void OnServersGroupsChanged()
    {
        var groupModel = ServersGroups.Where(x => x.Model is ServerGroup);
        var group = groupModel.Select(x => x.Model).OfType<ServerGroup>();

        _storageService.SaveData(group);
    }
}