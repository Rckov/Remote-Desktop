using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Common.Attributes;
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

	public MainViewModel()
	{
		_tabs = new ObservableCollection<TabItemViewModel>
		{
			new TabItemViewModel("Home", IconConstants.Home, false),
		};

		_selectedTab = _tabs[0];
	}
}