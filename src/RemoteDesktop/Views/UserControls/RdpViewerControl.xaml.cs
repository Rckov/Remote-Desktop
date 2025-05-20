using AxMSTSCLib;

using MSTSCLib;

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Threading;

using MessageBox = System.Windows.MessageBox;

namespace RemoteDesktop.Views.UserControls;

public partial class RdpViewerControl : System.Windows.Controls.UserControl
{
    private readonly AxMsRdpClient11NotSafeForScripting _client = new();

    public RdpViewerControl()
    {
        InitializeComponent();
        Loaded += RdpViewerControl_Loaded;
        Unloaded += RdpViewerControl_Unloaded;
    }

    private void RdpViewerControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (rdpHost.Child == null)
        {
            rdpHost.Child = _client;

            _client.OnDisconnected += Client_OnDisconnected;
            _client.OnFatalError += Client_OnFatalError;
            _client.OnLogonError += Client_OnLogonError;
        }

        Connect();
    }

    private void RdpViewerControl_Unloaded(object sender, RoutedEventArgs e)
    {
        Disconnect();
    }

    private void Connect()
    {
        try
        {
            if (_client.Connected == 1)
                return;

            IMsRdpClientNonScriptable7 ocx = (IMsRdpClientNonScriptable7)_client.GetOcx();

            ocx.EnableCredSspSupport = true;
            ocx.AllowCredentialSaving = false;
            ocx.PromptForCredentials = false;
            ocx.PromptForCredsOnClient = false;

            _client.Server = Host;
            _client.UserName = Username;
            _client.AdvancedSettings9.RDPPort = Port;
            _client.AdvancedSettings9.ClearTextPassword = Password;
            _client.ClientSize = new System.Drawing.Size((int)this.ActualWidth, (int)this.ActualHeight);

            _client.Connect();
        }
        catch (COMException ex)
        {
            MessageBox.Show($"RDP COM error: {ex.Message}", "RDP Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"RDP connection failed: {ex.Message}", "RDP Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Disconnect()
    {
        try
        {
            if (_client.Connected == 1)
                _client.Disconnect();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"RDP disconnection error: {ex.Message}", "RDP Disconnect Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void Client_OnDisconnected(object sender, IMsTscAxEvents_OnDisconnectedEvent e)
    {
        //MessageBox.Show($"RDP disconnected (reason: {e.discReason})", "Disconnected", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Client_OnFatalError(object sender, IMsTscAxEvents_OnFatalErrorEvent e)
    {
        MessageBox.Show($"RDP fatal error (code: {e.errorCode})", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void Client_OnLogonError(object sender, IMsTscAxEvents_OnLogonErrorEvent e)
    {
        MessageBox.Show($"RDP logon error (code: {e.lError})", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public static readonly DependencyProperty HostProperty =
        DependencyProperty.Register(nameof(Host), typeof(string), typeof(RdpViewerControl), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PortProperty =
        DependencyProperty.Register(nameof(Port), typeof(int), typeof(RdpViewerControl), new PropertyMetadata(3389));

    public static readonly DependencyProperty UsernameProperty =
        DependencyProperty.Register(nameof(Username), typeof(string), typeof(RdpViewerControl), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register(nameof(Password), typeof(string), typeof(RdpViewerControl), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty IsConnectedProperty =
        DependencyProperty.Register(nameof(IsConnected), typeof(bool), typeof(RdpViewerControl),
            new PropertyMetadata(false, OnIsConnectedChanged));

    private static void OnIsConnectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RdpViewerControl control)
            return;

        var shouldConnect = (bool)e.NewValue;

        if (shouldConnect)
        {
            control.Dispatcher.BeginInvoke(control.Connect, DispatcherPriority.ApplicationIdle);
        }
        else
        {
            control.Dispatcher.BeginInvoke(control.Disconnect, DispatcherPriority.ApplicationIdle);
        }
    }

    public string Host
    {
        get => (string)GetValue(HostProperty);
        set => SetValue(HostProperty, value);
    }

    public int Port
    {
        get => (int)GetValue(PortProperty);
        set => SetValue(PortProperty, value);
    }

    public string Username
    {
        get => (string)GetValue(UsernameProperty);
        set => SetValue(UsernameProperty, value);
    }

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    public bool IsConnected
    {
        get => (bool)GetValue(IsConnectedProperty);
        set => SetValue(IsConnectedProperty, value);
    }
}
