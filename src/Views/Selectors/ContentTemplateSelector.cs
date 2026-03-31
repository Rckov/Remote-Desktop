using RemoteDesktop.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Views.Selectors;

public class ContentTemplateSelector : DataTemplateSelector
{
	public DataTemplate? HomeContentTemplate { get; set; }
	public DataTemplate? ServerContentTemplate { get; set; }

	public override DataTemplate? SelectTemplate(object item, DependencyObject container)
	{
		if (item is TabItemViewModel tabItem)
		{
			return tabItem.IsCloseable ? ServerContentTemplate : HomeContentTemplate;
		}

		return base.SelectTemplate(item, container);
	}
}