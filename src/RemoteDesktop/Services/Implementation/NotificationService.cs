using HandyControl.Controls;
using HandyControl.Data;

using RemoteDesktop.Services.Interfaces;

namespace RemoteDesktop.Services.Implementation;

internal class NotificationService : INotificationService
{
    public void ShowInfo(string message)
    {
        Growl.Info(GetGrowInfo(message));
    }

    public void ShowWarning(string message)
    {
        Growl.Warning(GetGrowInfo(message));
    }

    public void ShowError(string message)
    {
        Growl.Error(GetGrowInfo(message));
    }

    public void ShowSuccess(string message)
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
            WaitTime = 1
        };
    }
}