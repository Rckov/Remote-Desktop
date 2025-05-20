using HandyControl.Controls;
using HandyControl.Data;

using RemoteDesktop.Services.Interfaces;

using System.Windows;

using MessageBox = HandyControl.Controls.MessageBox;

namespace RemoteDesktop.Services.Implementation;

internal class NotificationService : INotificationService
{
    public bool Ask(string message, string caption = "Question")
    {
        var info = new MessageBoxInfo
        {
            Message = message,
            Caption = caption,
            Button = MessageBoxButton.YesNo,
            IconBrushKey = ResourceToken.AccentBrush,
            IconKey = ResourceToken.AskGeometry
        };

        return MessageBox.Show(info) == MessageBoxResult.Yes;
    }

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
            WaitTime = 1
        };
    }
}