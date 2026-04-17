using RemoteDesktop.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Resources.DataTemplates.Selectors;

internal class ContentTemplateSelector : DataTemplateSelector
{
    public DataTemplate? HomeTemplate { get; set; }
    public DataTemplate? ServerTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (item is TabItemViewModel vm)
		{
			return vm.IsCloseable ? ServerTemplate : HomeTemplate;
		}

		return base.SelectTemplate(item, container);
    }
}
