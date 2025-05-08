using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Windows;

namespace RemoteDesktop.Services.Implementation;

internal class WindowService : IWindowService
{
    private readonly Dictionary<Type, Type> _windows = [];

    public void Register<TView, TViewModel>() where TView : Window
    {
        var type = typeof(TViewModel);

        if (_windows.ContainsKey(type))
        {
            _windows[type] = typeof(TView);
        }
        else
        {
            _windows.Add(type, typeof(TView));
        }
    }

    public void Show<TViewModel>(TViewModel viewModel) where TViewModel : class
    {
        if (!_windows.TryGetValue(typeof(TViewModel), out var viewType))
        {
            throw new InvalidOperationException($"No view registered for '{typeof(TViewModel).Name}'.");
        }

        var win = (Window)Activator.CreateInstance(viewType);
        win.DataContext = viewModel;
        win.Show();
    }

    public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class
    {
        if (!_windows.TryGetValue(typeof(TViewModel), out var viewType))
        {
            throw new InvalidOperationException($"No view registered for '{typeof(TViewModel).Name}'.");
        }

        var win = (Window)Activator.CreateInstance(viewType);
        win.DataContext = viewModel;
        return win.ShowDialog();
    }
}