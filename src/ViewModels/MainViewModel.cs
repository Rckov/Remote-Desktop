using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Models;
using RemoteDesktop.Models.Constans;
using RemoteDesktop.Views;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RemoteDesktop.ViewModels;

[Window(typeof(MainWindow))]
internal partial class MainViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<ServerGroup> _groups;

	[ObservableProperty]
	private ObservableCollection<TabItemViewModel> _tabItems;

	[ObservableProperty]
	private TabItemViewModel? _selectedTab;

	[ObservableProperty]
	private string _searchText = string.Empty;

	public MainViewModel()
	{
		TabItems =
		[
			new TabItemViewModel("Home", Icons.Home, false),
			new TabItemViewModel("Home"),
		];

		SelectedTab = TabItems[0];

		_groups =
		[
			new ServerGroup("Production Servers")
			{
				Servers =
				{
					new Server("Web Server 01", "192.168.1.100", "administrator", "Main production web server"),
					new Server("Database Server", "192.168.1.101", "dbadmin", "Primary database instance"),
				}
			},
			new ServerGroup("Development Servers")
			{
				Servers =
				{
					new Server("Dev Server 01", "192.168.2.10", "developer", "Development environment"),
				}
			}
		];

		_groups[0].Servers[0].IsOnline = true;
	}

	partial void OnSearchTextChanged(string value)
	{
		var isEmpty = string.IsNullOrWhiteSpace(value);

		foreach (var group in Groups)
		{
			var groupMatches = !isEmpty && group.Name.Contains(value, StringComparison.OrdinalIgnoreCase);

			for (int i = 0; i < group.Servers.Count; i++)
			{
				var server = group.Servers[i];
				server.IsVisible = isEmpty || groupMatches || server.Name.Contains(value, StringComparison.OrdinalIgnoreCase);
			}

			group.IsVisible = isEmpty || groupMatches || group.Servers.Any(s => s.IsVisible);
		}
	}
}