using HandyControl.Controls;
using HandyControl.Data;

using RemoteDesktop.Services.Interfaces;

namespace RemoteDesktop.Services.Implementation;

internal class NotificationService : INotificationService
{
    public void Show(string message, InfoType infoType)
    {
        switch (infoType)
        {
            case InfoType.Success:
                ShowSuccess(message);
                break;

            case InfoType.Info:
                ShowInfo(message);
                break;

            case InfoType.Error:
                ShowError(message);
                break;

            default:
                break;
        }
    }

    private void ShowInfo(string message)
    {
        Growl.Info(GetGrowInfo(message));
    }

    private void ShowError(string message)
    {
        Growl.Error(GetGrowInfo(message));
    }

    private void ShowSuccess(string message)
    {
        Growl.Success(GetGrowInfo(message));
    }

    private GrowlInfo GetGrowInfo(string message)
    {
        return new GrowlInfo
        {
            Message = message,
            StaysOpen = false,
            ShowCloseButton = true,
            WaitTime = 1,
        };
    }
}