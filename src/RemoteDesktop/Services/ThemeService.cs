using HandyControl.Themes;
using HandyControl.Tools;

using RemoteDesktop.Services.Interfaces;

using System.Windows;

namespace RemoteDesktop.Services;

/// <summary>
/// Provides functionality to get and change the application's theme with animation support.
/// </summary>
internal class ThemeService : IThemeService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public ThemeType Current
    {
        get => (ThemeType)ThemeManager.Current.ApplicationTheme;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void ChangeTheme(ThemeType theme)
    {
        ThemeAnimationHelper.AnimateTheme(Application.Current.MainWindow, ThemeAnimationHelper.SlideDirection.Top, 0.2, 1, 0.5);
        ThemeManager.Current.ApplicationTheme = (ApplicationTheme?)theme;
        ThemeAnimationHelper.AnimateTheme(Application.Current.MainWindow, ThemeAnimationHelper.SlideDirection.Bottom, 0.2, 0.5, 1);
    }
}