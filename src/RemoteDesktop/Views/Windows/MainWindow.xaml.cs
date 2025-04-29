using RemoteDesktop.ViewModels;

using System.Windows;

namespace RemoteDesktop.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Services.GetInstance<MainViewModel>();
    }
}