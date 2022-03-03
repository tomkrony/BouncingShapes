using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Shapes.Behaviors
{
    public static class VMErrorValidation
    {
        public static bool GetCheckValidity(DependencyObject obj)
        {
            return (bool)obj.GetValue(CheckValidityProperty);
        }

        public static void SetCheckValidity(DependencyObject obj, bool value)
        {
            obj.SetValue(CheckValidityProperty, value);
        }

        public static bool GetHasErrors(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasErrorsProperty);
        }

        public static void SetHasErrors(DependencyObject obj, bool value)
        {
            obj.SetValue(HasErrorsProperty, value);
        }

        public static ISet<object> GetErrorList(DependencyObject obj)
        {
            return (ISet<object>)obj.GetValue(ErrorListProperty);
        }

        public static void SetErrorList(DependencyObject obj, ISet<object> value)
        {
            obj.SetValue(ErrorListProperty, value);
        }

        // Using a DependencyProperty as the backing store for ErrorList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorListProperty =
            DependencyProperty.RegisterAttached("ErrorList", typeof(ISet<object>), typeof(VMErrorValidation), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for HasErrors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorsProperty =
            DependencyProperty.RegisterAttached("HasErrors",
                typeof(bool), typeof(VMErrorValidation),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        // Using a DependencyProperty as the backing store for IsValid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckValidityProperty =
            DependencyProperty.RegisterAttached("CheckValidity",
                typeof(bool), typeof(VMErrorValidation),
                new FrameworkPropertyMetadata(
                                        false,
                                        new PropertyChangedCallback(IsValidPropertyChanged)));

        private static void IsValidPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                System.Windows.Controls.Validation.AddErrorHandler(d, ErrorHandler); // NotifyOnValidationError=True should be set in View inorder to access this error routed event.
            else
                System.Windows.Controls.Validation.RemoveErrorHandler(d, ErrorHandler);
        }

        private static void ErrorHandler(object sender, ValidationErrorEventArgs e)
        {
            if (!(sender is DependencyObject obj))
                return;

            if (e.Action == ValidationErrorEventAction.Added)
            {
                var invalidItems = GetErrorList(obj);
                if (invalidItems == null)
                    invalidItems = new HashSet<object>();

                invalidItems.Add(e.OriginalSource);
                SetErrorList(obj, invalidItems);
                SetHasErrors(obj, true);
            }
            else
            {
                var hasErrors = System.Windows.Controls.Validation.GetHasError(e.OriginalSource as DependencyObject);
                if (!hasErrors)
                {
                    var invalidItems = GetErrorList(obj);
                    if (invalidItems != null)
                    {
                        invalidItems.Remove(e.OriginalSource);
                        SetHasErrors(obj, invalidItems.Count > 0);
                    }
                }

            }
        }
    }
}
