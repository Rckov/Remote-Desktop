using RemoteDesktop.Models.Themes;

namespace RemoteDesktop.Services.Abstractions.Themes;

internal interface IThemeService
{
	ThemeType CurrentTheme { get; }

	void SetTheme(ThemeType theme);
}