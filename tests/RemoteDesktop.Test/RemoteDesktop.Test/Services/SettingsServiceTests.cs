using DryIoc;

using RemoteDesktop.Services.Interfaces;

using System.IO;

using Xunit;

namespace RemoteDesktop.Test.Services;

public class SettingsServiceTests(ServiceFixture fixture) : IClassFixture<ServiceFixture>
{
    private readonly ISettingsService _settingsService = fixture.Container.Resolve<ISettingsService>();

    [Fact(DisplayName = "LoadSettings should return default settings if file does not exist")]
    public void Test0()
    {
        // Arrange
        if (File.Exists(fixture.SettingsPath))
        {
            File.Delete(fixture.SettingsPath);
        }

        // Act
        _settingsService.LoadSettings();

        // Assert
        Assert.NotNull(_settingsService.Settings);
        Assert.Equal(ThemeType.Light, _settingsService.Settings.ThemeType);
    }

    [Fact(DisplayName = "SaveSettings should persist settings to disk")]
    public void Test1()
    {
        // Arrange
        _settingsService.LoadSettings();
        _settingsService.Settings.ThemeType = ThemeType.Dark;

        // Act
        _settingsService.SaveSettings();
        _settingsService.LoadSettings();

        // Assert
        Assert.Equal(ThemeType.Dark, _settingsService.Settings.ThemeType);
    }
}