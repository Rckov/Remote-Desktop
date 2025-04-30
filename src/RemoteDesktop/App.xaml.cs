using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;

using SimpleInjector;

using System.Windows;

namespace RemoteDesktop;

public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices(new Container());
    }

    public static Container Services { get; private set; }

    private static Container ConfigureServices(Container container)
    {
        container.Register<IStorageService, JsonStorageService>();
        container.Register<IMessenger, Messanger>(Lifestyle.Singleton);
        container.Register<IThemeManager, ThemeManager>(Lifestyle.Singleton);

        container.Register<MainViewModel>(Lifestyle.Transient);
        container.Verify();

        return container;
    }
}