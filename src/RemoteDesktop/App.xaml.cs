using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;

using System;
using System.Windows;

namespace RemoteDesktop;

public partial class App : Application
{
    private readonly IServiceProvider _provider;

    public App()
    {
        _provider = ConfigureServices(new ServiceCollection());
    }

    public static string BaseDirectory => AppContext.BaseDirectory;

    private ServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddViews();
        services.AddServices();

        return services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var service = _provider.GetRequiredService<IWindowService>();
        service.Show<MainViewModel>();
    }
}