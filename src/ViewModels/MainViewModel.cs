using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Common.Attributes;
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
		{
			TabItems?.Add(tab);
		}
	}
}