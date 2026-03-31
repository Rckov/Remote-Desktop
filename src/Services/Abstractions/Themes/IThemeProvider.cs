using RemoteDesktop.Models;

namespace RemoteDesktop.Services.Abstractions.Themes;

internal interface IThemeProvider
{
	ThemeDescriptor Get(ThemeType type);
}