﻿using AvalonDock;

using RemoteDesktop.ViewModels;

using System.Windows;

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
}