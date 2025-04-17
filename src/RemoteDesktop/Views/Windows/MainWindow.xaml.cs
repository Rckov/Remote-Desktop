using AvalonDock;

using RemoteDesktop.ViewModels;

using System.Windows;

namespace RemoteDesktop.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Server_Diconnect(object sender, DocumentClosedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.ServerDiconnect(e.Document.Content as ConnectedServerViewModel);
        }
    }
}