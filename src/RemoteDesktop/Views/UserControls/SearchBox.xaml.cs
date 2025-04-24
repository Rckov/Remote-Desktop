using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Views.UserControls;

public partial class SearchBox : UserControl
{
    public static readonly DependencyProperty SearchTextProperty =
        DependencyProperty.Register("SearchText", typeof(string), typeof(SearchBox), new PropertyMetadata(string.Empty));

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    public SearchBox()
    {
        InitializeComponent();
    }
}