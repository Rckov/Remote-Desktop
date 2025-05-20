using AvalonDock;

using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RemoteDesktop.Views;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Server_Diconnect(object sender, DocumentClosingEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.Disconnect(e.Document.Content as ConnectedServerViewModel);
        }
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.SelectedItem = e.NewValue as TreeItemViewModel;
        }
    }

    private void TreeView_Drop(object sender, DragEventArgs e)
    {
        if (!TryGetDropContext(e, out var dragItem, out var targetItem))
        {
            return;
        }

        if (dragItem.Model is not Server server || targetItem.Model is not ServerGroup group)
        {
            return;
        }

        if (DataContext is MainViewModel viewModel)
        {
            viewModel.MoveServer(server, group.Name);
        }
    }

    private void TreeView_DragOver(object sender, DragEventArgs e)
    {
        if (!TryGetDropContext(e, out var dragItem, out var targetItem))
        {
            e.Effects = DragDropEffects.None;
        }
        else
        {
            e.Effects = IsDropAllowed(dragItem, targetItem) ? DragDropEffects.Move : DragDropEffects.None;
        }

        e.Handled = true;
    }

    private static bool IsDropAllowed(TreeItemViewModel dragItem, TreeItemViewModel targetItem)
    {
        return dragItem.Model is Server && targetItem.Model is ServerGroup;
    }

    private void TreeView_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed)
        {
            return;
        }

        var parentItem = GetParentTreeViewItem((DependencyObject)e.OriginalSource);

        if (parentItem?.DataContext is not TreeItemViewModel dragItem)
        {
            return;
        }

        DragDrop.DoDragDrop(parentItem, dragItem, DragDropEffects.Move);
    }

    private TreeViewItem GetParentTreeViewItem(DependencyObject source)
    {
        while (source != null && source is not TreeViewItem)
        {
            source = VisualTreeHelper.GetParent(source);
        }

        return source as TreeViewItem;
    }

    private bool TryGetDropContext(DragEventArgs e, out TreeItemViewModel dragItem, out TreeItemViewModel targetItem)
    {
        dragItem = null;
        targetItem = null;

        if (e.Data.GetData(typeof(TreeItemViewModel)) is not TreeItemViewModel drag)
        {
            return false;
        }

        var container = GetParentTreeViewItem((DependencyObject)e.OriginalSource);

        if (container?.DataContext is not TreeItemViewModel target || ReferenceEquals(drag, target))
        {
            return false;
        }

        dragItem = drag;
        targetItem = target;

        return true;
    }
}