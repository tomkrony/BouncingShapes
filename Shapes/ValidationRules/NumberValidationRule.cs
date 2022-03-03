using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Shapes.ValidationRules
{
    public sealed class NumberValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates the user input for positive number including zero
        /// </summary>
        /// <param name="value">Input</param>
        /// <param name="cultureInfo">Culture info</param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(true, null);

            if (double.TryParse(value.ToString(), out double valueAsDouble))
            {
                if (valueAsDouble >= 0.0) 
                    return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "Can not validate " + value);
        }
    }
}
