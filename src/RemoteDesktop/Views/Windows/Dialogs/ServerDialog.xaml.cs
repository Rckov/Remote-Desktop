using RemoteDesktop.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RemoteDesktop.Views.Windows.Dialogs;

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
