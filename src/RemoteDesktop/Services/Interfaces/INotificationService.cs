using HandyControl.Data;

namespace RemoteDesktop.Services.Interfaces;

internal interface INotificationService
{
    bool Ask(string message, string caption = "Question");

    void Show(string message, InfoType infoType);
}