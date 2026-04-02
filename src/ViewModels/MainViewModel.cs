using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Models;
using RemoteDesktop.Models.Constans;
using RemoteDesktop.Services.Abstractions;
using RemoteDesktop.Views;

using System.Collections.ObjectModel;

namespace RemoteDesktop.ViewModels;

[Window(typeof(MainWindow))]
internal partial class MainViewModel : ObservableObject
{
	private readonly INotificationService _notificationService;

	[ObservableProperty]
	private ObservableCollection<ServerGroup> _groups;

	[ObservableProperty]
	private ObservableCollection<TabItemViewModel> _connected;

	[ObservableProperty]
	private TabItemViewModel _selectedTab;

	public MainViewModel(INotificationService notification)
	{
		_notificationService = notification;

		_connected = new ObservableCollection<TabItemViewModel>
		{
			new TabItemViewModel("Home", Icons.Home, false),
			new TabItemViewModel("Server 1"),
		};

		_selectedTab = _connected[0];

		_groups = new ObservableCollection<ServerGroup>
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

		_groups[0].Servers[0].IsOnline = true;
	}

	[RelayCommand]
	private void CloseTab(TabItemViewModel tab)
	{
		if (tab?.IsCloseable == true && _connected.Contains(tab))
		{
		}
	}

	[RelayCommand]
	private void EditItem(object item)
	{
		_notificationService.Show("Тестовое всооо", "sdfasfd");
	}

	[RelayCommand]
	private void DeleteItem(object item)
	{
		if (item is Server server)
		{
			foreach (var group in _groups)
			{
				if (group.Servers.Contains(server))
				{
					group.Servers.Remove(server);
					break;
				}
			}
		}
		else if (item is ServerGroup group)
		{
			_groups.Remove(group);
		}
	}
}