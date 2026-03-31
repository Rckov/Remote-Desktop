using RemoteDesktop.Models;
using RemoteDesktop.Services.Abstractions.Themes;

using System;
using System.Collections.Generic;

namespace RemoteDesktop.Services.Themes;

internal class ThemeProvider : IThemeProvider
{
	private static readonly IDictionary<ThemeType, ThemeDescriptor> _themes =
		new Dictionary<ThemeType, ThemeDescriptor>
		{
			[ThemeType.Dark] = new(ThemeType.Dark, "pack://application:,,,/Resources/Themes/Brushes/DarkBrushes.xaml"),
			[ThemeType.Default] = new(ThemeType.Default, "pack://application:,,,/Resources/Themes/Brushes/DefaultBrushes.xaml"),
		};

	public ThemeDescriptor Get(ThemeType type)
	{
		return _themes.TryGetValue(type, out ThemeDescriptor? descriptor)
			? descriptor
			: throw new InvalidOperationException($"Theme '{type}' is not registered.");
	}
}