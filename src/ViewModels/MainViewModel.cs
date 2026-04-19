using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Models;
using RemoteDesktop.Views;

using System.Collections.ObjectModel;

namespace RemoteDesktop.ViewModels;

[Window(typeof(MainWindow))]
internal partial class MainViewModel : ObservableObject
{
	private const int MaxTabs = 10;

	[ObservableProperty]
	private TabItemViewModel? _selectedTab;

	[ObservableProperty]
	private ObservableCollection<TabItemViewModel>? _tabItems;

	[ObservableProperty]
	private ObservableGroupedCollection<string, Server> _groupedServers =
	[
		new("Production",
		[
			new Server { Name = "Web Server",  Host = "192.168.1.10", Username = "admin",     IsOnline = true  },
			new Server { Name = "Database",    Host = "192.168.1.11", Username = "dbadmin",   IsOnline = true  },
			new Server { Name = "Mail Server", Host = "192.168.1.12", Username = "admin",     IsOnline = false },
		]),
		new("Development",
		[
			new Server { Name = "Dev Box",     Host = "10.0.0.5",    Username = "developer", IsOnline = true  },
			new Server { Name = "Test Server", Host = "10.0.0.6",    Username = "tester",    IsOnline = false },
		]),
	];

	public MainViewModel()
	{
		TabItems =
		[
			new TabItemViewModel("Home", false),
			new TabItemViewModel("Server1"),
		];

		SelectedTab = TabItems[0];
	}

	public bool CanAddTab() => TabItems?.Count < MaxTabs;

	public void AddTab(TabItemViewModel tab)
	{
		if (CanAddTab())
			TabItems?.Add(tab);
	}
}
