using RemoteDesktop.Infrastructure;
using RemoteDesktop.Infrastructure.Behaviors;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;
    private readonly IThemeManager _themeService;
    private readonly IStorageService _storageService;

    public MainViewModel(
        ISettingsService settingsService,
        IThemeManager themeService,
        IStorageService storageService,
        ITreeDropHandler dropTarget)
    {
        _themeService = themeService;
        _settingsService = settingsService;
        _storageService = storageService;

        _themeService.Apply(_settingsService.Settings.ThemeType);

        DropHandler = dropTarget;
        DropHandler.ItemMoved += ServerGroups_Changed;

        ServersGroups = [.. _storageService.LoadData()];
    }

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

    public ITreeDropHandler DropHandler { get; }

    public TreeItemViewModel SelectedTreeItem
    {
        get;
        set => Set(ref field, value);
    }

    public ConnectedServerViewModel ActiveConnect
    {
        get;
        set => Set(ref field, value);
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; } = [];
    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; } = [];

    public ICommand ConnectCommand { get; private set; }
    public ICommand ThemeChangeCommand { get; private set; }

    protected override void InitializeCommands()
    {
        ConnectCommand = new RelayCommand(ServerConnect);
        ThemeChangeCommand = new RelayCommand(ThemeChange);
    }

    private void UpdateFilter(string pattern)
    {
        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(pattern);
        }
    }

    private void ThemeChange()
    {
        var curTheme = _themeService.CurrentTheme;
        var newTheme = curTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light;

        _themeService.Apply(newTheme);

        _settingsService.Settings.ThemeType = newTheme;
        _settingsService.SaveSettings();
    }

    private void ServerGroups_Changed()
    {
        _storageService.SaveData(ServersGroups);
    }

    public void ServerConnect()
    {
    }

    public void ServerDiconnect(ConnectedServerViewModel model)
    {
    }
}