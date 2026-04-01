using CommunityToolkit.Mvvm.ComponentModel;

namespace RemoteDesktop.Models;

internal partial class Server : ObservableObject
{
	[ObservableProperty]
	private string _name;

	[ObservableProperty]
	private string _host;

	[ObservableProperty]
	private string _username;

	[ObservableProperty]
	private string? _description;

	public Server(string name, string host, string username, string? description = null)
	{
		_name = name;
		_host = host;
		_username = username;
		_description = description;
	}
}