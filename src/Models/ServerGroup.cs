using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace RemoteDesktop.Models;

internal partial class ServerGroup(string name) : ObservableObject
{
	[ObservableProperty]
	private string _name = name;

	[ObservableProperty]
	private ObservableCollection<Server> _servers = [];

	[ObservableProperty]
	private bool _isExpanded = true;
}