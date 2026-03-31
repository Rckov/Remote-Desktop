using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Models.Constans;

namespace RemoteDesktop.ViewModels;

internal partial class TabItemViewModel(string name, string icon = IconConstants.Server, bool isCloseable = true) : ObservableObject
{
	[ObservableProperty]
	private string _name = name;

	[ObservableProperty]
	private string _icon = icon;

	[ObservableProperty]
	private bool _isCloseable = isCloseable;
}