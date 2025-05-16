using System.Collections.Generic;
using System.Windows;

namespace RemoteDesktop.Services.Interfaces;

internal interface IWindowService
{
    void Show<TViewModel>(object parameter = null) where TViewModel : class;

    bool? ShowDialog<TViewModel>(object parameter = null) where TViewModel : class;
}

internal interface IWindowFactory
{
    Window CreateWindow<TViewModel>(TViewModel viewModel);
}

internal interface IParameterReceiver
{
    void SetParameter(IDictionary<string, object> parameter);
}