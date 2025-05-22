namespace RemoteDesktop.Models;

internal class Server
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Port { get; set; } = 3389;
    public string GroupName { get; set; }

    public override string ToString()
    {
        return "Server";
    }
}