﻿using RemoteDesktop.Services.Interfaces;

namespace RemoteDesktop.Models;

internal class Settings
{
    public ThemeType ThemeType { get; set; }

    public Settings()
    {
        ThemeType = ThemeType.Light;
    }
}