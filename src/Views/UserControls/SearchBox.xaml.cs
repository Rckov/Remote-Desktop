using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Views.UserControls;

public partial class SearchBox : UserControl
{
	public SearchBox()
	{
		InitializeComponent();
		SearchTextBox.TextChanged += (s, e) => SearchText = SearchTextBox.Text;
	}

	public static readonly DependencyProperty SearchTextProperty =
		DependencyProperty.Register("SearchText", typeof(string), typeof(SearchBox),
			new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

	public string SearchText
	{
		get => (string)GetValue(SearchTextProperty);
		set => SetValue(SearchTextProperty, value);
	}
}