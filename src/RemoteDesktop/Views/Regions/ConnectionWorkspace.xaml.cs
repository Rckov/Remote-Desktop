using AvalonDock;

using RemoteDesktop.ViewModels;

using System.Windows.Controls;

namespace RemoteDesktop.Views.Regions;

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
            viewModel.Diconnect(e.Document.Content as ConnectedServerViewModel);
        }
    }
}