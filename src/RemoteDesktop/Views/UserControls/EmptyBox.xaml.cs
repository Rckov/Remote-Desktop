using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Views.UserControls;

public partial class EmptyBox : UserControl
{
    public static readonly DependencyProperty DesctiptionProperty =
        DependencyProperty.Register("Desctiption", typeof(string), typeof(SearchBox), new PropertyMetadata(string.Empty));

    public string Desctiption
    {
        get => (string)GetValue(DesctiptionProperty);
        set => SetValue(DesctiptionProperty, value);
    }

    public EmptyBox()
    {
        InitializeComponent();
    }
}