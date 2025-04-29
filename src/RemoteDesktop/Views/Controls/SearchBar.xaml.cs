using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Views.Controls;

public partial class SearchBar : UserControl
{
    public static readonly DependencyProperty SearchTextProperty =
        DependencyProperty.Register("SearchText", typeof(string), typeof(SearchBar), new PropertyMetadata(string.Empty));

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    public SearchBar()
    {
        InitializeComponent();
    }
}