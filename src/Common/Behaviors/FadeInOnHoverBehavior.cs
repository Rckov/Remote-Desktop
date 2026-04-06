using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace RemoteDesktop.Common.Behaviors
{
    public static class FadeInOnHoverBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(FadeInOnHoverBehavior),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element) return;

            if ((bool)e.NewValue)
            {
                element.Loaded += OnLoaded;
            }
            else
            {
                element.Loaded -= OnLoaded;
                DetachParentEvents(element);
            }
        }

        private static void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element) return;
            
            var parent = element.Parent as FrameworkElement;
            if (parent == null) return;

            parent.MouseEnter += (s, args) => AnimateOpacity(element, 1.0);
            parent.MouseLeave += (s, args) => AnimateOpacity(element, 0.0);
        }

        private static void DetachParentEvents(FrameworkElement element)
        {
            if (element.Parent is FrameworkElement parent)
            {
                parent.MouseEnter -= (s, args) => AnimateOpacity(element, 1.0);
                parent.MouseLeave -= (s, args) => AnimateOpacity(element, 0.0);
            }
        }

        private static void AnimateOpacity(FrameworkElement element, double toValue)
        {
            var animation = new DoubleAnimation
            {
                To = toValue,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
