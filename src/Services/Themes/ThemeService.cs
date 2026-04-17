using RemoteDesktop.Models.Themes;
using RemoteDesktop.Services.Abstractions.Themes;

using System;
using System.Windows;

namespace RemoteDesktop.Services.Themes;

internal class ThemeService(IThemeProvider provider) : IThemeService
{
	private ResourceDictionary? _currentBrushes;

	public ThemeType CurrentTheme { get; private set; }

	public void SetTheme(ThemeType theme)
	{
		var descriptor = provider.Get(theme);

		ApplyBrushes(descriptor.Url);
		CurrentTheme = theme;
	}

	private void ApplyBrushes(string uri)
	{
		var merged = Application.Current.Resources.MergedDictionaries;

		if (_currentBrushes is not null)
		{
			merged.Remove(_currentBrushes);
		}

		_currentBrushes = new ResourceDictionary
		{
			Source = new Uri(uri, UriKind.Absolute)
		};

		merged.Add(_currentBrushes);
	}
}