using RemoteDesktop.ViewModels.Dialogs;

using System.Windows;

namespace RemoteDesktop.Views.Dialogs;

public partial class MessageBoxWindow : Window
{
	public MessageBoxWindow()
	{
		InitializeComponent();

		Loaded += (s, e) =>
		{
			if (DataContext is MessageBoxViewModel viewModel)
			{
				viewModel.CloseAction = (result) =>
				{
					DialogResult = result;
					Close();
				};
			}
		};
	}
}
