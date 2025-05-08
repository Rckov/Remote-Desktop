using System.Windows;

namespace RemoteDesktop;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Bootstrapper.Initialize();
        Bootstrapper.Start();
    }
}