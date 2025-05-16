namespace RemoteDesktop.Services.Interfaces;

internal interface INotificationService
{
    void ShowInfo(string message);

    void ShowWarning(string message);

    void ShowError(string message);

    void ShowSuccess(string message);
}