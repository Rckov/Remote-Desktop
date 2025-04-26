using RemoteDesktop.Services.Implementation;

namespace RemoteDesktop.Models.Messages;

internal class ThemeMessage
{
    public ThemeMessage(ThemeType themeType)
    {
        ThemeType = themeType;
    }

    public ThemeType ThemeType { get; }
}