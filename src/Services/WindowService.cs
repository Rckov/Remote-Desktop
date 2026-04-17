using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Services.Abstractions;

using System;
using System.Windows;

namespace RemoteDesktop.Services;

internal class WindowService(IServiceProvider service) : IWindowService
{
	public T? ShowWindow<T>(T? context = null, bool dialog = false) where T : ObservableObject
	{
		var window = GetWindow(context);

		if (dialog)
		{
			window.Owner = Application.Current.MainWindow;
			window.ShowDialog();
		}
		else
		{
			window.Show();
		}

		return context;
	}

	private Window GetWindow<T>(T? context) where T : ObservableObject
	{
		context ??= service.GetRequiredService<T>();

		if (Attribute.GetCustomAttribute(typeof(T), typeof(WindowAttribute)) is not WindowAttribute attr)
		{
			throw new InvalidOperationException($"Window type not specified for {typeof(T).Name}");
		}

		var window = (Window)service.GetRequiredService(attr.WindowType);
		window.DataContext = context;

		return window;
	}
}