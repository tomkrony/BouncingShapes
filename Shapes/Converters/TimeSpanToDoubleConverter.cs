﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Shapes.Converters
{
    /// <summary>
    /// Converts the TimeSpan to MilliSeconds and back.
    /// </summary>
    public sealed class TimeSpanToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan timeSpan))
                return null;

            return timeSpan.TotalMilliseconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(value.ToString(), out double valueAsDouble))
                return null;

            return TimeSpan.FromMilliseconds(valueAsDouble);
        }
    }
}
