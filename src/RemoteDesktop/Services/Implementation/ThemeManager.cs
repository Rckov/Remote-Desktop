﻿using RemoteDesktop.Models.Messages;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RemoteDesktop.Services.Implementation;

internal class ThemeManager : IThemeManager
{
    private readonly IMessenger _messenger;
    private readonly Dictionary<ThemeType, string> _themePaths = [];

    public ThemeManager(IMessenger messenger)
    {
        _messenger = messenger;

        AddTheme(ThemeType.Dark, "Resources/Themes/DarkBrushes.xaml");
        AddTheme(ThemeType.Light, "Resources/Themes/LightBrushes.xaml");
    }

    public ThemeType CurrentTheme { get; private set; }

    public void Apply(ThemeType themeType)
    {
        var dictionaries = Application.Current.Resources.MergedDictionaries;
        var currentTheme = dictionaries.FirstOrDefault(d => _themePaths.ContainsValue(d.Source.OriginalString));

        if (currentTheme != null)
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

        _messenger.Send(new ThemeMessage(themeType));
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