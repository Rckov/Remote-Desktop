using RemoteDesktop.ViewModels;

using System;
using System.Globalization;
using System.Windows.Data;

namespace RemoteDesktop.Common.Converters;

internal class TabTypeIconConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return value is TabItemViewModel { IsCloseable: true } ? "\uE7F4" : "\uE80F";
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}