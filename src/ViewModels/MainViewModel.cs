using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Models;
using RemoteDesktop.Models.Constants;
using RemoteDesktop.Services.Abstractions;
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
	private readonly IWindowService windowService;
	private readonly INotificationService notificationService;

	public MainViewModel(IWindowService windowService, INotificationService notificationService)
	{
		this.windowService = windowService;
		this.notificationService = notificationService;

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
		this.windowService = windowService;
	}

	[RelayCommand]
	private void CloseTab(TabItemViewModel? tab)
	{
		if (tab is not { IsCloseable: true })
		{
			return;
		}

		TabItems.Remove(tab);

		if (SelectedTab == tab)
		{
			SelectedTab = TabItems.FirstOrDefault();
		}
	}

	[RelayCommand]
	private void EditItem(object? item)
	{
		var result = notificationService.Show("Test message", "Test Title", System.Windows.MessageBoxButton.OK);
	}

	[RelayCommand]
	private void DeleteItem(object? item)
	{
		switch (item)
		{
			case ServerGroup group:
			Groups.Remove(group);
			break;

			case Server server:
			Groups.FirstOrDefault(g => g.Servers.Remove(server));
			break;
		}
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