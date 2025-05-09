using RemoteDesktop.ViewModels;

using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace RemoteDesktop.Views.Windows.Dialogs;

public partial class ServerDialog : Window
{
    private readonly Regex _regex = new("[^0-9]+");

    public ServerDialog()
    {
        InitializeComponent();
        Loaded += OnWindowLoaded;
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        e.Handled = _regex.IsMatch(e.Text);
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