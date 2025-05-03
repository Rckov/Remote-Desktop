using RemoteDesktop.Infrastructure.Behaviors;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;
using RemoteDesktop.Views.Windows;

using SimpleInjector;

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

        Services = ConfigureServices(new Container());
    }

    public static string DataPath { get; private set; }
    public static Container Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var window = new MainWindow()
        {
            DataContext = Services.GetInstance<MainViewModel>()
        };
        window.Show();
    }

    private static Container ConfigureServices(Container container)
    {
        container.Register<IStorageService, JsonStorageService>();
        container.Register<ISettingsService, SettingsService>();
        container.Register<IThemeManager, ThemeManager>();

        container.Register<ITreeDropHandler, TreeDropHandler>();

        container.Register<MainViewModel>();
        container.Verify();

        return container;
    }
}