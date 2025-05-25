using RemoteDesktop.Common.Base;
using RemoteDesktop.Models;

using System;

namespace RemoteDesktop.ViewModels;

internal class ConnectedServerViewModel(Server server) : BaseViewModel
{
    public event EventHandler<string> OnDisconnected;

    public string Name
    {
        get => server.Name;
    }

    public bool IsSelected
    {
        get;
        set => Set(ref field, value);
    }

    public bool IsConnected
    {
        get;
        set => Set(ref field, value);
    }

    public string ErrorReason
    {
        get;
        set
        {
            if (Set(ref field, value))
            {
                OnDisconnected?.Invoke(this, value);
            }
        }
    }

    public Server Server => server;
}