using RemoteDesktop.Services;
using RemoteDesktop.Services.Implementation;
using RemoteDesktop.Services.Interfaces;

using System;
using System.IO;

namespace RemoteDesktop;

internal static class Bootstrapper
{
    public static string DataPath { get; private set; }
    public static string SettingPath { get; private set; }

    public static void Initialize()
    {
        InitializePaths();
        InitializeServices();
    }

    private static void InitializePaths()
    {
        string exeDirectory = AppContext.BaseDirectory;

        DataPath = Path.Combine(exeDirectory, "data");
        SettingPath = Path.Combine(exeDirectory, "settings");

        Directory.CreateDirectory(DataPath);
        Directory.CreateDirectory(SettingPath);
    }

    private static void InitializeServices()
    {
        ServiceLocator.Register<IThemeService>(new ThemeService());
        ServiceLocator.Register<ISettingsService>(new SettingsService());
        ServiceLocator.Register<IStorageService>(new JsonStorageService());
    }
}