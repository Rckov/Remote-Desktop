using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RemoteDesktop.Common.Base;

/// <summary>
/// Base class for all ViewModels providing property change notification.
/// </summary>
internal abstract class BaseViewModel : INotifyPropertyChanged
{
    protected BaseViewModel()
    {
        InitializeCommands();
    }

    /// <summary>
    /// Event raised when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Override this method to initialize commands in the derived ViewModel.
    /// </summary>
    public virtual void InitializeCommands()
    { }

    /// <summary>
    /// Logs an informational message along with the calling method name.
    /// </summary>
    public void LogInfo(string message, [CallerMemberName] string method = null)
    {
        Debug.WriteLine(
            $"Method: {method}\r\n" +
            $"Message: {message}"
        );
    }

    /// <summary>
    /// Logs an error message with optional exception details and calling method name.
    /// </summary>
    public void LogError(string message, Exception exception = null, [CallerMemberName] string method = null)
    {
        Debug.WriteLine(
            $"Method: {method}\r\n" +
            $"Message: {message}\r\n" +
            $"Exception: {exception?.Message ?? "No exception"}"
        );
    }

    /// <summary>
    /// Raises the PropertyChanged event for UI updates.
    /// </summary>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets the backing field value and raises PropertyChanged if the value has changed.
    /// </summary>
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}