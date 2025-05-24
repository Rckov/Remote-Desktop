using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Windows;

namespace RemoteDesktop.Services;

/// <summary>
/// Service for displaying windows using view models and a factory.
/// </summary>
internal class WindowService(IWindowFactory factory, IServiceProvider provider) : IWindowService
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Show<TViewModel>(object parameter = null) where TViewModel : class
    {
        var window = GetWindow<TViewModel>(parameter);

        if (typeof(TViewModel) == typeof(MainViewModel))
        {
            Application.Current.MainWindow = window;
        }

        window.Show();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? ShowDialog<TViewModel>(object parameter = null) where TViewModel : class
    {
        var window = GetWindow<TViewModel>(parameter);
        return window.ShowDialog();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    private Window GetWindow<TViewModel>(object parameter = null) where TViewModel : class
    {
        var viewModel = (TViewModel)provider.GetService(typeof(TViewModel));

        if (parameter != null && viewModel is IParameterReceiver receiver)
        {
            receiver.SetParameter(parameter);
        }

        return factory.CreateWindow(viewModel);
    }
}

/// <summary>
/// Factory for creating windows based on view models and registered view types.
/// </summary>
internal class WindowFactory(IServiceProvider provider, Dictionary<Type, Type> views) : IWindowFactory
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Window CreateWindow<TViewModel>(TViewModel viewModel)
    {
        var vmType = typeof(TViewModel);

        if (!views.TryGetValue(vmType, out var viewType))
        {
            throw new InvalidOperationException($"View not registered for {vmType.Name}");
        }

        var window = (Window)provider.GetService(viewType);
        window.DataContext = viewModel;
        return window;
    }
}