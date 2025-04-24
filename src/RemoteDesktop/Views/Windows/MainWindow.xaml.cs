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

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.SelectedTreeItem = e.NewValue as TreeItemViewModel;
        }
    }
}