namespace RemoteDesktop.Models;

/// <summary>
/// Represents a remote desktop server.
/// </summary>
internal class Server
{
    /// <summary>
    /// Display name of the server.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Optional description for the server.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Host address (IP or domain).
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Username for the RDP connection.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Password for the RDP connection.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Port number, default is 3389.
    /// </summary>
    public int Port { get; set; } = 3389;

    /// <summary>
    /// Name of the group this server belongs to.
    /// </summary>
    public string GroupName { get; set; }

    public override string ToString()
    {
        return "Server";
    }
}