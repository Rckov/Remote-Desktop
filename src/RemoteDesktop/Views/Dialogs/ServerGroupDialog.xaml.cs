using RemoteDesktop.ViewModels;

using System.Windows;

namespace RemoteDesktop.Views.Dialogs;

public partial class ServerGroupDialog
{
    public ServerGroupDialog()
    {
        InitializeComponent();
        Loaded += OnWindowLoaded;
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ServerViewModel viewModel)
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