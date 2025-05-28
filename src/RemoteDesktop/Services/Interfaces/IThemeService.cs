using HandyControl.Themes;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Represents the available application themes.
/// </summary>
internal enum ThemeType
{
    /// <summary>
    /// Dark theme.
    /// </summary>
    Dark = ApplicationTheme.Dark,

    /// <summary>
    /// Light theme.
    /// </summary>
    Light = ApplicationTheme.Light,
}

/// <summary>
/// Service for managing the application theme.
/// </summary>
internal interface IThemeService
{
    /// <summary>
    /// Gets the current application theme.
    /// </summary>
    ThemeType Current { get; }

    /// <summary>
    /// Changes the application theme to the specified one.
    /// </summary>
    /// <param name="theme">The theme to apply.</param>
    void ChangeTheme(ThemeType theme);
}