using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace RemoteDesktop.Models;

internal partial class ServerGroup : ObservableObject
{
	[ObservableProperty]
	private string _name;

	[ObservableProperty]
	private ObservableCollection<Server> _servers;

	[ObservableProperty]
	private bool _isExpanded = true;

	public ServerGroup(string name)
	{
		_name = name;
		_servers = new ObservableCollection<Server>();
	}
}
