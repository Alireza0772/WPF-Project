using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFProject
{
    class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number = 0;
            try
            {
                if (((string)value).Length > 0)
                    number = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((number > 100) || (number < 0))
            {
                return new ValidationResult(false,
                  $"Please enter a number in range 0 and 100.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
