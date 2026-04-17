using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Services;
using RemoteDesktop.Services.Abstractions;
using RemoteDesktop.Services.Abstractions.Themes;
using RemoteDesktop.Services.Themes;
using RemoteDesktop.ViewModels;
using RemoteDesktop.Views;

namespace RemoteDesktop.Extensions;

internal static class ContainerExtensions
{
	extension(IServiceCollection services)
	{
		public void AddUI()
		{
			services.AddView<MainViewModel, MainWindow>();
		}

		public void AddServices()
		{
			services.AddSingleton<IThemeService, ThemeService>();
			services.AddSingleton<IThemeProvider, ThemeProvider>();
			services.AddTransient<IWindowService, WindowService>();
		}

		private void AddView<TViewModel, TView>()
			where TViewModel : class
			where TView : class
		{
			services.AddTransient<TViewModel>();
			services.AddTransient<TView>();
		}
	}
}