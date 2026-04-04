using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Extensions;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Abstractions;
using RemoteDesktop.Services.Abstractions.Themes;
using RemoteDesktop.ViewModels;

using System;
using System.Windows;

namespace RemoteDesktop;

public partial class App : Application
{
	static App()
	{
		Services = ConfigureServices();
	}

	public static IServiceProvider Services { get; }

	protected override async void OnStartup(StartupEventArgs e)
	{
		Services.GetRequiredService<IThemeService>().SetTheme(ThemeType.Dark);
		Services.GetRequiredService<IWindowService>().ShowWindow<MainViewModel>();
	}

	private static IServiceProvider ConfigureServices()
	{
		var services = new ServiceCollection();

		services.AddUI();
		services.AddServices();

		return services.BuildServiceProvider();
	}
}