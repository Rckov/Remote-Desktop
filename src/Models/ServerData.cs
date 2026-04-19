using System.Collections.Generic;

namespace RemoteDesktop.Models;

internal class ServerData
{
	public List<ServerGroup> Groups { get; set; } = [];
	public List<Server> Servers { get; set; } = [];
}