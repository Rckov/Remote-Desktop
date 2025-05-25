using DryIoc;

using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;

using System;
using System.Windows;

namespace RemoteDesktop;

public partial class App
{
    private readonly IContainer _container;

    public App()
    {
        _container = ConfigureContainer();
    }

    /// <summary>
    /// Base directory of the application.
    /// </summary>
    public static string BaseDirectory => AppContext.BaseDirectory;

    /// <summary>
    /// Configures the DryIoc container by registering views and services.
    /// </summary>
    private IContainer ConfigureContainer()
    {
        var container = new Container();

        container.RegisterViews();
        container.RegisterServices();

        return container;
    }

    /// <summary>
    /// Handles application startup event.
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        var service = _container.Resolve<IWindowService>();
        service.Show<MainViewModel>();
    }
}