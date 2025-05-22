using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Extensions;
using RemoteDesktop.Models;

using System;

namespace RemoteDesktop.ViewModels;

internal partial class ConnectedServerViewModel(Server server) : ObservableObject
{
    [ObservableProperty]
    private bool _isConnected;

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private string _errorReason;

    public event EventHandler<string> OnDisconnected;

    public string Name
    {
        get => server.Name;
    }

    public Server Server
    {
        get => server.Clone();
    }

    partial void OnErrorReasonChanged(string value)
    {
        OnDisconnected?.Invoke(this, value);
    }
}