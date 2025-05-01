using RemoteDesktop.Models;

namespace RemoteDesktop.Services.Interfaces;

internal interface ISettingsService
{
    Settings Settings { get; }

    Settings LoadSettings();

    void SaveSettings();
}