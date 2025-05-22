using Microsoft.Extensions.DependencyInjection;

using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Windows;

namespace RemoteDesktop.Services.Implementation;

internal class WindowService(IWindowFactory factory, IServiceProvider provider) : IWindowService
{
    public void Show<TViewModel>(object parameter = null) where TViewModel : class
    {
        var window = GetWindow<TViewModel>(parameter);
        window.Show();
    }

    public bool? ShowDialog<TViewModel>(object parameter = null) where TViewModel : class
    {
        var window = GetWindow<TViewModel>(parameter);
        return window.ShowDialog();
    }

    private Window GetWindow<TViewModel>(object parameter = null) where TViewModel : class
    {
        var viewModel = provider.GetRequiredService<TViewModel>();

        if (parameter != null && viewModel is IParameterReceiver receiver)
        {
            receiver.SetParameter(parameter);
        }

        return factory.CreateWindow(viewModel);
    }
}

internal class WindowFactory(IServiceProvider provider, Dictionary<Type, Type> views) : IWindowFactory
{
    public Window CreateWindow<TViewModel>(TViewModel viewModel)
    {
        var vmType = typeof(TViewModel);

        if (!views.TryGetValue(vmType, out var viewType))
        {
            throw new InvalidOperationException($"View not registered for {vmType.Name}");
        }

        var window = (Window)provider.GetRequiredService(viewType);
        window.DataContext = viewModel;
        return window;
    }
}