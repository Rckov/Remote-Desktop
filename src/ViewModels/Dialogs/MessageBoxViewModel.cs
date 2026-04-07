using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using RemoteDesktop.Common;
using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Views.Dialogs;

using System.Windows;

namespace RemoteDesktop.ViewModels.Dialogs;

[Window(typeof(MessageBoxDialog))]
internal partial class MessageBoxViewModel : ObservableObject
{
	[ObservableProperty]
	private string _title = string.Empty;

	[ObservableProperty]
	private string _message = string.Empty;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ShowCancelButton))]
	private MessageBoxButton _button = MessageBoxButton.OK;

	public bool ShowCancelButton => Button == MessageBoxButton.OKCancel;

	public MessageBoxResult Result { get; private set; } = MessageBoxResult.None;

	[RelayCommand]
	private void Ok(ICloseable? closeable)
	{
		Result = MessageBoxResult.OK;
		closeable?.Close();
	}

	[RelayCommand]
	private void Cancel(ICloseable? closeable)
	{
		Result = MessageBoxResult.Cancel;
		closeable?.Close();
	}
}