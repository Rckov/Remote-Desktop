using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace RemoteDesktop.Infrastructure.TemplateSelectors;

internal class TreeItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate GroupTemplate { get; set; }
    public DataTemplate ServerTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        var vm = item as TreeItemViewModel;
        return vm.Model is ServerGroup ? GroupTemplate : ServerTemplate;
    }
}