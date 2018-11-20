using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class TimeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime;

            var IsValid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture, DateTimeStyles.None,
                out datetime);

            return IsValid;
        }
    }
}