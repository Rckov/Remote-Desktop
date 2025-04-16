using RemoteDesktop.Helpers;
using RemoteDesktop.Models.Base;

using System.Collections.ObjectModel;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        ConnectedServers = [];

        foreach (var item in TestGenerated.GenerateServers())
        {
            ConnectedServers.Add(new ConnectedServerViewModel(item));
        }
    }

    public ObservableCollection<ConnectedServerViewModel> ConnectedServers { get; set; }
}