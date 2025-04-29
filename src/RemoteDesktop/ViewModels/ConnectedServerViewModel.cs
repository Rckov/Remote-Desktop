using RemoteDesktop.Models;
using RemoteDesktop.ViewModels.Base;

namespace RemoteDesktop.ViewModels;

internal class ConnectedServerViewModel : BaseViewModel
{
    public string Name
    {
        get => Server?.Name;
    }

    public Server Server { get; }
    public bool IsConnected { get; internal set; }

    public ConnectedServerViewModel(Server server)
    {
        Server = server;
    }

    public void Connect()
    {
    }

    public void Disconnect()
    {
    }
}