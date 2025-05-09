using RemoteDesktop.Services;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;
using RemoteDesktop.Views.Windows;
using RemoteDesktop.Views.Windows.Dialogs;

using System;
using System.IO;

namespace RemoteDesktop;

internal static class Bootstrapper
{
    public static string DataPath { get; private set; }
    public static string SettingPath { get; private set; }

    public static void Initialize()
    {
        InitializePaths();
        InitializeServices();
        InitializeViews();
    }

    public static void Start()
    {
        // TO DO
        var windowService = ServiceLocator.Get<IWindowService>();

        var viewModel = new MainViewModel(
            ServiceLocator.Get<IThemeService>(),
            ServiceLocator.Get<IDataService>(),
            ServiceLocator.Get<ISettingsService>(),
            windowService
        );

        windowService.Show(viewModel);
    }

    private static void InitializePaths()
    {
        // TO DO
        string exeDirectory = AppContext.BaseDirectory;

        DataPath = Path.Combine(exeDirectory, "data");
        Directory.CreateDirectory(DataPath);

        SettingPath = Path.Combine(exeDirectory, "settings");
        Directory.CreateDirectory(SettingPath);
    }

    private static void InitializeServices()
    {
        ServiceLocator.Register<IThemeService>(new ThemeService());
        ServiceLocator.Register<ISettingsService>(new SettingsService());
        ServiceLocator.Register<IDataService>(new LocalDataServce());
        ServiceLocator.Register<IWindowService>(new WindowService());
    }

    private static void InitializeViews()
    {
        var service = ServiceLocator.Get<IWindowService>();

        service.Register<MainWindow, MainViewModel>();
        service.Register<ServerDialog, ServerModalViewModel>();

        //service.Register<ServerGroupDialog, ServerGroupEditViewModel>();
    }
}