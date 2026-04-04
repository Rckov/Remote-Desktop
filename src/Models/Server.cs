using CommunityToolkit.Mvvm.ComponentModel;

namespace RemoteDesktop.Models;

internal partial class Server(string name, string host, string username, string? description = null) : ObservableObject
{
	[ObservableProperty]
	private string _name = name;

	[ObservableProperty]
	private string _host = host;

	[ObservableProperty]
	private string _username = username;

	[ObservableProperty]
	private string? _description = description;

	[ObservableProperty]
	private bool _isOnline = false;

	[ObservableProperty]
	private bool _isVisible = true;
}