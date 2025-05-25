using HandyControl.Controls;
using HandyControl.Data;

using RemoteDesktop.Services.Interfaces;

using System.Windows;

using MessageBox = HandyControl.Controls.MessageBox;

namespace RemoteDesktop.Services;

/// <summary>
/// Provides user notifications and message dialogs using HandyControl.
/// </summary>
internal class NotificationService : INotificationService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void Show(string message, InfoType infoType)
    {
        var info = GetGrowInfo(message);

        switch (infoType)
        {
            case InfoType.Success:
                Growl.Success(info);
                break;

            case InfoType.Info:
                Growl.Info(info);
                break;

            case InfoType.Error:
                Growl.Error(info);
                break;
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
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

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void MessageShow(string message)
    {
        var info = new MessageBoxInfo
        {
            Message = message,
            Caption = "Error",
            Button = MessageBoxButton.OK,
            IconBrushKey = ResourceToken.AccentBrush,
            IconKey = ResourceToken.ErrorGeometry
        };

        MessageBox.Show(info);
    }

    /// <summary>
    /// Creates a standardized notification configuration.
    /// </summary>
    /// <param name="message">The message content.</param>
    /// <returns>A configured <see cref="GrowlInfo"/> instance.</returns>
    private GrowlInfo GetGrowInfo(string message) => new()
    {
        Message = message,
        StaysOpen = false,
        ShowCloseButton = true,
        WaitTime = 1
    };
}