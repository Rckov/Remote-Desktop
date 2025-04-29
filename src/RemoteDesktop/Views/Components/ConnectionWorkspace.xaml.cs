using AvalonDock;

using RemoteDesktop.ViewModels;

using System.Windows.Controls;

namespace RemoteDesktop.Views.Components;

public partial class ConnectionWorkspace : UserControl
{
    public ConnectionWorkspace()
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