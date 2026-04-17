using CommunityToolkit.Mvvm.ComponentModel;

namespace RemoteDesktop.ViewModels;

internal partial class TabItemViewModel(string name, bool isCloseable = true) : ObservableObject
{
	[ObservableProperty]
	private string _name = name;

	[ObservableProperty]
	private bool _isCloseable = isCloseable;

	[ObservableProperty]
	private object? _content;
}