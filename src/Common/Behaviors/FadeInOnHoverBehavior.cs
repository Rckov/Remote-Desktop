using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RemoteDesktop.Common.Behaviors;

internal static class FadeInOnHoverBehavior
{
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(FadeInOnHoverBehavior),
            new PropertyMetadata(false, OnIsEnabledChanged));

    public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
    public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element || !(bool)e.NewValue)
            return;

        element.Loaded += (_, _) =>
        {
            var container = FindContainer(element);
            if (container is null) return;

            container.MouseEnter += (_, _) => Animate(element, 1);
            container.MouseLeave += (_, _) => Animate(element, 0);
        };
    }

    private static FrameworkElement? FindContainer(FrameworkElement element)
    {
        var current = System.Windows.Media.VisualTreeHelper.GetParent(element) as FrameworkElement;
        while (current is Panel or ContentPresenter)
            current = System.Windows.Media.VisualTreeHelper.GetParent(current) as FrameworkElement;
        return current;
    }

    private static void Animate(UIElement element, double to) =>
        element.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(to, TimeSpan.FromSeconds(0.15)));
}
