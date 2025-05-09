using RemoteDesktop.ViewModels;

using System.Windows;

namespace RemoteDesktop.Views.Windows.Dialogs;

public partial class ServerGroupDialog : Window
{
    public ServerGroupDialog()
    {
        InitializeComponent();
        Loaded += OnWindowLoaded;
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        SubscribeViewModelEvents();
    }

    private void SubscribeViewModelEvents()
    {
        if (DataContext is ServerModalViewModel viewModel)
        {
            viewModel.CloseRequest += OnCloseRequest;
        }
    }

    private void OnCloseRequest(bool dialogResult)
    {
        DialogResult = dialogResult;
        Close();
    }
}