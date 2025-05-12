using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;
using RemoteDesktop.Views;
using RemoteDesktop.Views.Dialogs;

using System;
using System.Collections.Generic;
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
        services.AddTransient<MainWindow>();
        services.AddTransient<MainViewModel>();

        services.AddTransient<ServerViewModel>();
        services.AddTransient<ServerDialog>();

        services.AddTransient<ServerGroupViewModel>();
        //services.AddTransient<ServerGroupDialog>();

        var views = new Dictionary<Type, Type>
        {
            { typeof(MainViewModel), typeof(MainWindow) },

            { typeof(ServerViewModel), typeof(ServerDialog) },
            //{ typeof(ServerGroupViewModel), typeof(ServerGroupDialog) },
        };

        services.AddSingleton<IWindowFactory>(sp => new WindowFactory(sp, views));
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        return services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _provider.GetRequiredService<IWindowService>().Show<MainViewModel>();
    }
}