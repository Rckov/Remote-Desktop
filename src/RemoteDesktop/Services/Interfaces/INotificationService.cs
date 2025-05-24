using HandyControl.Data;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Provides a way to show notifications with different info types.
/// </summary>
internal interface INotificationService
{
    /// <summary>
    /// Shows a notification message with the specified type.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="infoType">The type of the message (info, success, error) <see cref="InfoType"/>.</param>
    void Show(string message, InfoType infoType);
}