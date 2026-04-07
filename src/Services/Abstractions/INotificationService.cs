using System.Windows;

namespace RemoteDesktop.Services.Abstractions;

internal interface INotificationService
{
	MessageBoxResult Show(string message, string title, MessageBoxButton button = MessageBoxButton.OK);
}