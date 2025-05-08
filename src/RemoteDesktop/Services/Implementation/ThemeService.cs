using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RemoteDesktop.Services.Implementation;

internal class ThemeService : IThemeService
{
    private readonly Dictionary<ThemeType, string> _themePaths = [];

    public ThemeService()
    {
        AddTheme(ThemeType.Dark, "Resources/Themes/DarkBrushes.xaml");
        AddTheme(ThemeType.Light, "Resources/Themes/LightBrushes.xaml");
    }

    public ThemeType CurrentTheme { get; private set; } = ThemeType.Light;

    public void Apply(ThemeType themeType)
    {
        var dictionaries = Application.Current.Resources.MergedDictionaries;
        var currentTheme = dictionaries.FirstOrDefault(d => _themePaths.ContainsValue(d.Source.OriginalString));

        if (currentTheme is not null)
        {
            dictionaries.Remove(currentTheme);
        }

        if (_themePaths.TryGetValue(themeType, out var path))
        {
            CurrentTheme = themeType;

            var newTheme = new ResourceDictionary
            {
                Source = new Uri(path, UriKind.Relative)
            };

            dictionaries.Add(newTheme);
        }
    }

    private void AddTheme(ThemeType type, string path)
    {
        _themePaths.Add(type, path);
    }
}

public enum ThemeType
{
    Dark,
    Light
}