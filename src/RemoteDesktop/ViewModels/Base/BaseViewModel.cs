using RemoteDesktop.Common;
using RemoteDesktop.Models.Base;

using System;
using System.Windows;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels.Base;

internal abstract class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
        InitializeCommands();
    }

    public event Action<bool> CloseRequest;

    public ICommand OkCommand { get; private set; }
    public ICommand CancelCommand { get; private set; }

    public virtual void InitializeCommands()
    {
        OkCommand = new RelayCommand(Ok);
        CancelCommand = new RelayCommand(Cancel);
    }

    public virtual void Ok()
    {
        CloseRequest?.Invoke(true);
    }

    public virtual void Cancel()
    {
        CloseRequest?.Invoke(false);
    }

    public void ErrorMessageBox(string message)
    {
        MessageBoxShow(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void InfoMessageBox(string message)
    {
        MessageBoxShow(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public bool QuestionMessageBox(string message)
    {
        return MessageBoxShow(message, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }

    public MessageBoxResult MessageBoxShow(string message, string title, MessageBoxButton messageButton, MessageBoxImage image)
    {
        return MessageBox.Show(message, title, messageButton, image);
    }
}