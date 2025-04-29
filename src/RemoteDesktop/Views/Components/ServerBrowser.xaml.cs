using RemoteDesktop.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Views.Components;

public partial class ServerBrowser : UserControl
{
    public ServerBrowser()
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