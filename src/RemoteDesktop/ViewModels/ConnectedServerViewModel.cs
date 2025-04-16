using RemoteDesktop.Models;
using RemoteDesktop.Models.Base;

namespace RemoteDesktop.ViewModels;

internal class ConnectedServerViewModel : ObservableObject
{
    public string Name
    {
        get => Server?.Name;
    }

    public Server Server { get; }

    public ConnectedServerViewModel(Server server)
    {
        Server = server;
    }
}