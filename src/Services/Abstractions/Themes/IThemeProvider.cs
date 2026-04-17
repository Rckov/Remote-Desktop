using RemoteDesktop.Models.Themes;

namespace RemoteDesktop.Services.Abstractions.Themes;

internal interface IThemeProvider
{
	ThemeDescriptor Get(ThemeType type);
}