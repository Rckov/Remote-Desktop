using CommunityToolkit.Mvvm.ComponentModel;

using System;

namespace RemoteDesktop.Models;

internal partial class Server : ObservableObject
{
	[ObservableProperty]
	private Guid _id = Guid.NewGuid();

	[ObservableProperty]
	private string _name = string.Empty;

	[ObservableProperty]
	private string _host = string.Empty;

	[ObservableProperty]
	private string _username = string.Empty;

	[ObservableProperty]
	private string _password = string.Empty;

	[ObservableProperty]
	private int _port = 3389;

	[ObservableProperty]
	private Guid? _groupId;

	[ObservableProperty]
	private bool _isOnline;
}

internal partial class ServerGroup : ObservableObject
{
	[ObservableProperty]
	private Guid _id = Guid.NewGuid();

	[ObservableProperty]
	private string _name = string.Empty;
}