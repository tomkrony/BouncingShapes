using Shapes.ViewModels;
using System.Windows;
using System.Windows.Shapes;

namespace Shapes.Behaviors
{
    public class SelectedShapeAttachedProperty
    {
        public static ShapeType GetSelectedShapeProperty(DependencyObject obj)
        {
            return (ShapeType)obj.GetValue(SelectedShapeProperty);
        }

        public static void SetSelectedShapeProperty(DependencyObject obj, ShapeType value)
        {
            obj.SetValue(SelectedShapeProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedShapeProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedShapeProperty =
            DependencyProperty.RegisterAttached(
                nameof(SelectedShapeProperty), 
                typeof(ShapeType), 
                typeof(SelectedShapeAttachedProperty),
                new PropertyMetadata(ShapeType.None, SelectedShapeAttachedPropertyChanged));

        private static void SelectedShapeAttachedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is ShapeType shape))
                return;

            if (!(d is Rectangle rectangle))
                return;

            switch (shape)
            {
                case ShapeType.None:
                    break;
                case ShapeType.Circle: 
                    // Making rectangle a circle
                    rectangle.RadiusX = rectangle.Height;
                    rectangle.RadiusY = rectangle.Width;
                    break;
                case ShapeType.Rectangle: 
                    // Making a circle a rectangle
                    rectangle.RadiusX = 0;
                    rectangle.RadiusY = 0;
                    break;
            }
        }
    }
}
