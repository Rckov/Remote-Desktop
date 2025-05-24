using DryIoc;

using RemoteDesktop.Services;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;
using RemoteDesktop.Views;
using RemoteDesktop.Views.Dialogs;

using System;
using System.Collections.Generic;
using System.IO;

namespace RemoteDesktop;

/// <summary>
/// Configures dependency injection registrations for views and services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all views and their view models in the container.
    /// Also creates a mapping between view model types and dialog types.
    /// </summary>
    public static void RegisterViews(this IContainer container)
    {
        var views = new Dictionary<Type, Type>();

        container.RegisterDialog<MainViewModel, MainWindow>(views);
        container.RegisterDialog<ServerViewModel, ServerDialog>(views);
        container.RegisterDialog<ServerGroupViewModel, ServerGroupDialog>(views);

        container.RegisterInstance<IWindowFactory>(new WindowFactory(views, container));
    }

    /// <summary>
    /// Registers core application services as singletons.
    /// </summary>
    public static void RegisterServices(this IContainer container)
    {
        container.Register<IWindowService, WindowService>();
        container.Register<INotificationService, NotificationService>();
        container.Register<IMessengerService, MessengerService>();

        container.RegisterDelegate<IDataService>(r =>
        {
            var path = Path.Combine(AppContext.BaseDirectory, "data");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return new JsonDataService(Path.Combine(path, "servers.dat"));
        }, Reuse.Singleton);
    }

    /// <summary>
    /// Registers the specified view model and dialog types in the container
    /// and adds the mapping between them to the provided dictionary.
    /// </summary>
    private static void RegisterDialog<TViewModel, TDialog>(this IContainer container, IDictionary<Type, Type> viewMap)
        where TViewModel : class
        where TDialog : class
    {
        container.Register<TViewModel>(Reuse.Transient);
        container.Register<TDialog>(Reuse.Transient);

        viewMap[typeof(TViewModel)] = typeof(TDialog);
    }
}