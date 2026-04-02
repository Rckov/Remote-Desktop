using RemoteDesktop.Services.Abstractions;
using RemoteDesktop.ViewModels.Dialogs;

namespace RemoteDesktop.Services;

internal class NotificationService(IWindowService windowService) : INotificationService
{
	public bool Show(string message, string title)
	{
		var viewModel = windowService
			.ShowWindow(new MessageBoxViewModel(title, message), true);

		return viewModel?.Result is true;
	}
}
