using RemoteDesktop.ViewModels;

using System.Windows;

namespace RemoteDesktop.Views.Dialogs;

public partial class ServerDialog
{
    public ServerDialog()
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