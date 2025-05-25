using System.Windows;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Interface for managing window display operations.
/// </summary>
internal interface IWindowService
{
    /// <summary>
    /// Shows a window for the specified view model.
    /// </summary>
    /// <param name="parameter">Optional parameter for the view model.</param>
    void Show<TViewModel>(object parameter = null) where TViewModel : class;

    /// <summary>
    /// Shows a dialog window for the specified view model.
    /// </summary>
    /// <param name="parameter">Optional parameter for the view model.</param>
    bool? ShowDialog<TViewModel>(object parameter = null) where TViewModel : class;
}

/// <summary>
/// Interface for creating windows based on view models.
/// </summary>
internal interface IWindowFactory
{
    /// <summary>
    /// Creates a window for the given view model.
    /// </summary>
    /// <param name="viewModel">The view model for the window.</param>
    Window CreateWindow<TViewModel>(TViewModel viewModel);
}

/// <summary>
/// Interface for view models that can receive parameters.
/// </summary>
internal interface IParameterReceiver
{
    /// <summary>
    /// Sets a parameter for the view model.
    /// </summary>
    /// <param name="parameter">The parameter to set.</param>
    void SetParameter(object parameter = null);
}