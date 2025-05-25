using HandyControl.Data;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Provides a way to show notifications and dialogs to the user.
/// </summary>
internal interface INotificationService
{
    /// <summary>
    /// Shows a notification message with the specified type.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="infoType">The type of the message (info, success, error). See <see cref="InfoType"/>.</param>
    void Show(string message, InfoType infoType);

    /// <summary>
    /// Shows a confirmation dialog with Yes/No buttons.
    /// </summary>
    /// <param name="message">The question to display.</param>
    /// <param name="caption">The caption of the dialog. Default is "Question".</param>
    /// <returns>True if the user selects Yes, otherwise false.</returns>
    bool Ask(string message, string caption = "Question");

    /// <summary>
    /// Shows an error message in a modal dialog with an OK button.
    /// </summary>
    /// <param name="message">The message to display.</param>
    void MessageShow(string message);
}