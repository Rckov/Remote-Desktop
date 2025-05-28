using RemoteDesktop.Models;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Provides access to application settings.
/// </summary>
internal interface ISettingsService
{
    /// <summary>
    /// Gets the current application settings.
    /// </summary>
    Settings Settings { get; }

    /// <summary>
    /// Save <see cref="Models.Settings"/>
    /// </summary>
    void SaveSettings();

    /// <summary>
    /// Load <see cref="Models.Settings"/>
    /// </summary>
    void LoadSettings();
}