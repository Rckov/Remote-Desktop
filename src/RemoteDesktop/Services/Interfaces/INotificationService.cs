using HandyControl.Data;

namespace RemoteDesktop.Services.Interfaces;

internal interface INotificationService
{
    void Show(string message, InfoType infoType);
}