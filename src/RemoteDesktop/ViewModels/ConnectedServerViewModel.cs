using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Models;

namespace RemoteDesktop.ViewModels;

internal partial class ConnectedServerViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private Server _server;

    [ObservableProperty]
    private bool _isConnected;

    public ConnectedServerViewModel(Server server)
    {
        Server = server;
        Name = server.Name;
    }
}