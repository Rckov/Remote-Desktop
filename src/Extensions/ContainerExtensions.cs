using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Services;
using RemoteDesktop.Services.Abstractions;
using RemoteDesktop.Services.Abstractions.Themes;
using RemoteDesktop.Services.Themes;
using RemoteDesktop.ViewModels;
using RemoteDesktop.ViewModels.Dialogs;
using RemoteDesktop.Views;
using RemoteDesktop.Views.Dialogs;

namespace RemoteDesktop.Extensions;

internal static class ContainerExtensions
{
	extension(IServiceCollection services)
	{
		public void AddUI()
		{
			services.AddView<MainViewModel, MainWindow>();
			services.AddView<MessageBoxViewModel, MessageBoxDialog>();
		}

		public void AddServices()
		{
			services.AddTransient<IWindowService, WindowService>();
			services.AddSingleton<IThemeProvider, ThemeProvider>();
			services.AddSingleton<IThemeService, ThemeService>();
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