using System;
using System.Windows.Input;

namespace RemoteDesktop.Common;

/// <summary>
/// A command that executes a delegate when Execute is called and determines
/// whether it can execute in the current state using the CanExecute delegate.
/// </summary>
public class RelayCommand(Action<object> execute, Predicate<object> canExecute) : ICommand
{
    private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    /// <summary>
    /// Creates a new instance of RelayCommand.
    /// </summary>
    /// <param name="execute">The delegate to execute when the command is invoked.</param>
    public RelayCommand(Action execute)
        : this(_ => execute(), null)
    {
    }

    /// <summary>
    /// Creates a new instance of RelayCommand.
    /// </summary>
    /// <param name="execute">The delegate to execute when the command is invoked.</param>
    /// <param name="canExecute">The delegate that determines whether the command can execute.</param>
    public RelayCommand(Action execute, Func<bool> canExecute)
        : this(_ => execute(), _ => canExecute())
    {
    }

    /// <summary>
    /// Creates a new instance of RelayCommand.
    /// </summary>
    /// <param name="execute">The delegate to execute when the command is invoked.</param>
    public RelayCommand(Action<object> execute)
        : this(execute, null)
    {
    }

    /// <summary>
    /// Occurs when changes occur that affect whether the command can execute.
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">The parameter for the command.</param>
    /// <returns>true if the command can be executed; otherwise false.</returns>
    public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

    /// <summary>
    /// Executes the command with the given parameter.
    /// </summary>
    /// <param name="parameter">The parameter for the command.</param>
    public void Execute(object parameter) => _execute(parameter);

    /// <summary>
    /// Notifies that the ability of the command to execute has changed.
    /// </summary>
    public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
}