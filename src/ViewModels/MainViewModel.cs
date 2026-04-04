using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Models;
using RemoteDesktop.Models.Constans;
using RemoteDesktop.Views;

using System.Collections.ObjectModel;

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
}