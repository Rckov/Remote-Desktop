namespace RemoteDesktop.Services.Abstractions;

internal interface INotificationService
{
	bool Show(string message, string title);
}