using RemoteDesktop.Common;

using System.Windows;

namespace RemoteDesktop.Views.Dialogs;

public partial class MessageBoxDialog : Window, ICloseable
{
	public MessageBoxDialog()
	{
		InitializeComponent();
	}
}