using RemoteDesktop.Models;

using System;
using System.Globalization;
using System.Windows.Data;

namespace RemoteDesktop.Common.Converters;

internal class TreeItemIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ServerGroup)
        {
            return null;
        }

        if (value is Server)
        {
            return "\uE7F4";
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}