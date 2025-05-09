using RemoteDesktop.Services.Implementation;

namespace RemoteDesktop.Services.Interfaces;

public interface IThemeService
{
    ThemeType CurrentTheme { get; }

    void ApplyTheme(ThemeType themeType);
}