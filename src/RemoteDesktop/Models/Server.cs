using RemoteDesktop.Models.Base;

namespace RemoteDesktop.Models;

internal class Server : ObservableObject
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string Host { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
}