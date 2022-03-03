using System.Windows;

namespace Shapes.Behaviors
{
    public static class ActualSizeBehavior
    {
        public static bool GetObserveActualSizeProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(ObserveActualSizeProperty);
        }

        public static void SetObserveActualSizeProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(ObserveActualSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObserveActualSizeProperty =
            DependencyProperty.RegisterAttached(
                nameof(ObserveActualSizeProperty), 
                typeof(bool), 
                typeof(ActualSizeBehavior), 
                new PropertyMetadata(false, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement control))
                return;

            if ((bool)e.NewValue == true)
                control.SizeChanged += control_SizeChanged;
            else
                control.SizeChanged -= control_SizeChanged;
        }

        private static void control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var control = sender as FrameworkElement;
            SetActualWidthProperty(control, control.ActualWidth);
            SetActualHeightProperty(control, control.ActualHeight);
        }



        public static double GetActualWidthProperty(DependencyObject obj)
        {
            return (double)obj.GetValue(ActualWidthProperty);
        }

        public static void SetActualWidthProperty(DependencyObject obj, double value)
        {
            obj.SetValue(ActualWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for ActualWidthProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActualWidthProperty =
            DependencyProperty.RegisterAttached(
                nameof(ActualWidthProperty), 
                typeof(double), 
                typeof(ActualSizeBehavior), 
                new PropertyMetadata(400.0));

        public static double GetActualHeightProperty(DependencyObject obj)
        {
            return (double)obj.GetValue(ActualHeightProperty);
        }

        public static void SetActualHeightProperty(DependencyObject obj, double value)
        {
            obj.SetValue(ActualHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for ActualWidthProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActualHeightProperty =
            DependencyProperty.RegisterAttached(
                nameof(ActualHeightProperty),
                typeof(double), 
                typeof(ActualSizeBehavior),
                new PropertyMetadata(500.0));


    }
}
