using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Views.Dialogs;

using System;

namespace RemoteDesktop.ViewModels.Dialogs;

[Window(typeof(MessageBoxWindow))]
internal partial class MessageBoxViewModel(string title, string message) : ObservableObject
{
	[ObservableProperty]
	private string _title = title;

	[ObservableProperty]
	private string _message = message;

	public bool Result { get; private set; }
	public Action<bool>? CloseAction { get; set; }

	[RelayCommand]
	private void Ok()
	{
		Result = true;
		CloseAction?.Invoke(true);
	}

	[RelayCommand]
	private void Cancel()
	{
		Result = false;
		CloseAction?.Invoke(false);
	}
}
