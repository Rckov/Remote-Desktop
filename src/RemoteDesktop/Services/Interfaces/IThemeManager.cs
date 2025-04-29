using RemoteDesktop.Services.Implementation;

namespace RemoteDesktop.Services.Interfaces;

public interface IThemeManager
{
    ThemeType CurrentTheme { get; }

    void Apply(ThemeType themeType);
}