using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;

using SimpleInjector;

using System;
using System.Windows;

namespace RemoteDesktop;

public partial class App
{
    public App()
    {
        Container = ConfigureServices();
    }

    /// <summary>
    /// Base directory of the application.
    /// </summary>
    public static string BaseDirectory => AppContext.BaseDirectory;

    /// <summary>
    /// DI Container.
    /// </summary>
    public static Container Container { get; set; }

    /// <summary>
    /// Configures the Simple Injector container by registering views and services.
    /// </summary>
    private static Container ConfigureServices()
    {
        var container = new Container();

        container.RegisterViews();
        container.RegisterServices();
        container.RegisterInstance<IServiceProvider>(container);
        container.Verify();

        return container;
    }

    /// <summary>
    /// Handles application startup event.
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        var service = Container.GetInstance<IWindowService>();
        service.Show<MainViewModel>();
    }

    /// <summary>
    /// Handles application exit event.
    /// </summary>
    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        Container?.Dispose();
    }
}