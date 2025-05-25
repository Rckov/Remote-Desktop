using DryIoc;

using RemoteDesktop.Services.Interfaces;
using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Windows;

namespace RemoteDesktop.Services;

/// <summary>
/// Shows windows or dialogs for specific ViewModels using a factory and DI container.
/// </summary>
internal class WindowService(IWindowFactory factory, IContainer container) : IWindowService
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
        var viewModel = container.Resolve(typeof(TViewModel));

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
internal class WindowFactory(Dictionary<Type, Type> views, IContainer container) : IWindowFactory
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Window CreateWindow<TViewModel>(TViewModel viewModel)
    {
        var vmType = viewModel.GetType();

        if (!views.TryGetValue(vmType, out var viewType))
        {
            throw new InvalidOperationException($"View not registered for {vmType.Name}");
        }

        var window = (Window)container.Resolve(viewType);
        window.DataContext = viewModel;
        return window;
    }
}