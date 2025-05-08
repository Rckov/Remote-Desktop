using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RemoteDesktop.Components.Converters;

internal class TreeItemMarginConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var left = 0.0;
        UIElement element = value as TreeViewItem;

        while (element is not null && element.GetType() != typeof(TreeView))
        {
            element = (UIElement)VisualTreeHelper.GetParent(element);
            if (element is TreeViewItem)
            {
                left += 19.0;
            }
        }

        return new Thickness(left, 0, 0, 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}