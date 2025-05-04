using RemoteDesktop.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Views.Regions;

public partial class ServerExplorerWorkspace : UserControl
{
    public ServerExplorerWorkspace()
    {
        InitializeComponent();
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.SelectedTreeItem = e.NewValue as TreeItemViewModel;
        }
    }
}