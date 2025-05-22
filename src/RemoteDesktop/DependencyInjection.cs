using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;
using RemoteDesktop.Views;
using RemoteDesktop.Views.Dialogs;

using System;
using System.Collections.Generic;
using System.IO;

namespace RemoteDesktop;

internal static class DependencyInjection
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        var views = new Dictionary<Type, Type>();

        services.AddDialog<MainViewModel, MainWindow>(views);
        services.AddDialog<ServerViewModel, ServerDialog>(views);
        services.AddDialog<ServerGroupViewModel, ServerGroupDialog>(views);

        services.AddSingleton<IWindowFactory>(sp => new WindowFactory(sp, views));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
        services.AddSingleton<IDataService>(sp =>
        {
            var path = Path.Combine(AppContext.BaseDirectory, "data");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return new JsonDataService(Path.Combine(path, "servers.json"));
        });

        return services;
    }

    private static IServiceCollection AddDialog<TViewModel, TDialog>(this IServiceCollection services, IDictionary<Type, Type> viewMap)
        where TViewModel : class
        where TDialog : class
    {
        services.AddTransient<TViewModel>();
        services.AddTransient<TDialog>();

        viewMap[typeof(TViewModel)] = typeof(TDialog);
        return services;
    }
}