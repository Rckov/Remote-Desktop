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
	private ObservableCollection<TabItemViewModel> _tabs;

	[ObservableProperty]
	private TabItemViewModel _selectedTab;

	[ObservableProperty]
	private ObservableCollection<ServerGroup> _serverGroups;

	public MainViewModel()
	{
		_tabs = new ObservableCollection<TabItemViewModel>
		{
			new TabItemViewModel("Home", IconConstants.Home, false),
			new TabItemViewModel("Server 1"),
		};

		_selectedTab = _tabs[0];

		_serverGroups = new ObservableCollection<ServerGroup>
		{
			new ServerGroup("Production Servers")
			{
				Servers =
				{
					new Server("Web Server 01", "192.168.1.100", "administrator", "Main production web server"),
					new Server("Database Server", "192.168.1.101", "dbadmin", "Primary database instance"),
					new Server("API Gateway", "192.168.1.102", "root", "API gateway server"),
					new Server("API Gateway", "192.168.1.102", "root", "API gateway server"),
					new Server("API Gateway", "192.168.1.102", "root", "API gateway server"),
					new Server("API Gateway", "192.168.1.102", "root", "API gateway server"),
				}
			},
			new ServerGroup("Development Servers")
			{
				Servers =
				{
					new Server("Dev Server 01", "192.168.2.10", "developer", "Development environment"),
					new Server("Test Server", "192.168.2.11", "tester", "Testing environment"),
				}
			}
		};
	}
}