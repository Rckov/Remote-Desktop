using RemoteDesktop.Common;
using RemoteDesktop.Common.Behaviors;
using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private readonly IThemeService _themeService;
    private readonly IDataService _dataService;
    private readonly ISettingsService _settingsService;
    private readonly IWindowService _windowService;

    public MainViewModel(IThemeService themeService, IDataService dataService, ISettingsService settingsService, IWindowService windowService)
    {
        _themeService = themeService;
        _dataService = dataService;
        _settingsService = settingsService;
        _windowService = windowService;

        _dataService.Load();
        _themeService.ApplyTheme(settingsService.Settings.ThemeType);

        DropHandler = new TreeDropHandler();
        DropHandler.ItemMoved += dataService.Save;

        ServersGroups = [.. dataService.Groups.Select(x => new TreeItemViewModel(x))];
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
    public ICommand ChangeThemeCommand { get; private set; }
    public ICommand CreateServerCommand { get; private set; }
    public ICommand CreateGroupCommand { get; private set; }

    public override void InitializeCommands()
    {
        ConnectCommand = new RelayCommand(Connect);
        ChangeThemeCommand = new RelayCommand(ChangeTheme);
        CreateServerCommand = new RelayCommand(CreateServer);
        CreateGroupCommand = new RelayCommand(CreateGroup);
    }

    private void Connect()
    {
        throw new NotImplementedException();
    }

    internal void Diconnect(ConnectedServerViewModel model)
    {
        throw new NotImplementedException();
    }

    private void CreateGroup()
    {
        var viewModel = new ServerGroupModalViewModel(_dataService);

        if (_windowService.ShowDialog(viewModel) != true)
        {
            return;
        }

        ServersGroups.Add(new(new ServerGroup()
        {
            Name = viewModel.NameGroup
        }));

        _dataService.Save();
    }

    private void CreateServer()
    {
        var viewModel = new ServerModalViewModel(_dataService);

        if (_windowService.ShowDialog(viewModel) != true)
        {
            return;
        }

        var selectedGroup = ServersGroups.FindByName(viewModel.SelectedGroup);

        if (selectedGroup != null)
        {
            selectedGroup.Children.Add(new(viewModel.Server));
        }

        _dataService.Save();
    }

    private void ChangeTheme()
    {
        var curTheme = _themeService.CurrentTheme;
        var newTheme = curTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light;

        _themeService.ApplyTheme(newTheme);

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
}