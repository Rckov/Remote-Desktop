using RemoteDesktop.Services;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;
using RemoteDesktop.Views.Windows;

using System;
using System.IO;
using System.Windows;

namespace RemoteDesktop;

public partial class App : Application
{
    public App()
    {
        DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RemoteDesktop");

        if (!Directory.Exists(DataPath))
        {
            Directory.CreateDirectory(DataPath);
        }
    }

    public static string DataPath { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        Bootstrapper.Initialize();
        base.OnStartup(e);

        var viewModel = new MainViewModel(
            ServiceLocator.Get<ISettingsService>(),
            ServiceLocator.Get<IThemeService>(),
            ServiceLocator.Get<IStorageService>()
        );

        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();
    }
}