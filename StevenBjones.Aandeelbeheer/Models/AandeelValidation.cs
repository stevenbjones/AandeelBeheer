using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StevenBjones.Aandeelbeheer.Models
{
    public class AandeelValidation : ValidationRule
    {
        public int Min { get; set; }       

        public AandeelValidation()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((age < Min))
            {
                return new ValidationResult(false,
                  $"Please enter an age in the range: {Min}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
