using AxMSTSCLib;

using MSTSCLib;

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace RemoteDesktop.Views.UserControls;

public partial class RdpViewerControl : System.Windows.Controls.UserControl
{
    private AxMsRdpClient11NotSafeForScripting _client;

    public RdpViewerControl()
    {
        InitializeComponent();
        Loaded += RdpViewerControl_Loaded;
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
        DependencyProperty.Register(nameof(IsConnected), typeof(bool), typeof(RdpViewerControl), new PropertyMetadata(false, OnIsConnectedChanged));

    public static readonly DependencyProperty ErrorReasonProperty =
        DependencyProperty.Register(nameof(ErrorReason), typeof(string), typeof(RdpViewerControl), new PropertyMetadata(string.Empty));

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

    public string ErrorReason
    {
        get => (string)GetValue(ErrorReasonProperty);
        set => SetValue(ErrorReasonProperty, value);
    }

    private void RdpViewerControl_Loaded(object sender, RoutedEventArgs e)
    {
        _client = new AxMsRdpClient11NotSafeForScripting();
        _client.OnDisconnected += Client_OnDisconnected;

        rdpHost.Child ??= _client;
    }

    private void Client_OnDisconnected(object sender, IMsTscAxEvents_OnDisconnectedEvent e)
    {
        var reason = (uint)e.discReason;
        var extendedreason = (uint)_client.ExtendedDisconnectReason;

        if (reason != 1)
        {
            ErrorReason = _client.GetErrorDescription(reason, extendedreason);
        }
    }

    private static void OnIsConnectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RdpViewerControl control)
        {
            return;
        }

        var shouldConnect = (bool)e.NewValue;

        control.Dispatcher.BeginInvoke(() =>
        {
            if (shouldConnect)
            {
                control.Connect();
            }
            else
            {
                control.Disconnect();
            }
        }, DispatcherPriority.ApplicationIdle);
    }

    private void Connect()
    {
        if (_client.Connected == 1)
        {
            return;
        }

        var ocx = (IMsRdpClientNonScriptable7)_client.GetOcx();

        ocx.EnableCredSspSupport = true;
        ocx.AllowCredentialSaving = false;
        ocx.PromptForCredentials = false;
        ocx.PromptForCredsOnClient = false;

        _client.Server = Host;
        _client.UserName = Username;
        _client.AdvancedSettings9.RDPPort = Port;
        _client.AdvancedSettings9.ClearTextPassword = Password;

        var rect = Screen.PrimaryScreen.WorkingArea;

        _client.DesktopWidth = Math.Max(800, rect.Width);
        _client.DesktopHeight = Math.Max(600, rect.Height);
        _client.AdvancedSettings9.SmartSizing = true;

        try
        {
            _client.Connect();
        }
        catch (COMException ex)
        {
            ErrorReason = $"RDP COM error: {ex.Message}";
        }
        catch (Exception ex)
        {
            ErrorReason = $"RDP connection failed: {ex.Message}";
        }
    }

    private void Disconnect()
    {
        try
        {
            _client.Disconnect();
        }
        catch
        {
            // error stub
        }
    }
}