using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Services.Abstractions;
using RemoteDesktop.ViewModels.Dialogs;

using System;
using System.Windows;

namespace RemoteDesktop.Services;

internal class NotificationService(IServiceProvider serviceProvider, IWindowService windowService) : INotificationService
{
	public MessageBoxResult Show(string message, string title, MessageBoxButton button = MessageBoxButton.OK)
	{
		var viewModel = serviceProvider.GetRequiredService<MessageBoxViewModel>();
		viewModel.Title = title;
		viewModel.Message = message;
		viewModel.Button = button;

		windowService.ShowWindow(viewModel, dialog: true);
		return viewModel.Result;
	}
}