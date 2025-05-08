using System.Windows;

namespace RemoteDesktop.Services.Interfaces;

internal interface IWindowService
{
    void Register<TView, TViewModel>() where TView : Window;

    void Show<TViewModel>(TViewModel viewModel) where TViewModel : class;

    bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class;
}