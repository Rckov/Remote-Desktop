using System;
using System.Windows.Input;

namespace RemoteDesktop.Components.Commands;

internal class RelayCommand(Action<object> execute, Predicate<object> canExecute = null) : ICommand
{
    public RelayCommand(Action execute) : this(_ => execute(), null)
    {
    }

    public RelayCommand(Action<object> execute) : this(execute, null)
    {
    }

    public RelayCommand(Action execute, Func<bool> canExecute) : this(_ => execute(), _ => canExecute())
    {
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        execute(parameter);
    }
}