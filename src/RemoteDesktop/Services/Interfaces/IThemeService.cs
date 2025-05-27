using HandyControl.Themes;

namespace RemoteDesktop.Services.Interfaces;

internal enum ThemeType
{
    Dark = ApplicationTheme.Dark,
    Light = ApplicationTheme.Light,
}

internal interface IThemeService
{
    ThemeType Current { get; }

    void ChangeTheme(ThemeType theme);
}