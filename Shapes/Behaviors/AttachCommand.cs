using System.Windows;
using System.Windows.Input;

namespace Shapes.Behaviors
{
    internal class AttachCommand
    {


        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(nameof(CommandProperty), typeof(ICommand), typeof(AttachCommand), new PropertyMetadata(null));


    }
}
