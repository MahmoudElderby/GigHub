using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class FutureDateValidation :ValidationAttribute
    {
        //public override bool IsValid(object value)
        //{
        //    DateTime datetime;

        //    var IsValid = DateTime.TryParseExact(Convert.ToString(value),
        //        "d MMM yyyy",
        //        CultureInfo.CurrentCulture, DateTimeStyles.None,
        //        out datetime);

        //    return IsValid && datetime > DateTime.Now;
        //}


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime datetime;
            var context = validationContext.ObjectInstance as GigFormViewModel;

            var IsValid = DateTime.TryParseExact(Convert.ToString(context.Date),
                "d MMM yyyy",
                CultureInfo.CurrentCulture, DateTimeStyles.None,
                out datetime);

            if (IsValid)
            {
                if (datetime <= DateTime.Now)
                    return new ValidationResult("You Must enter a future date!");

                return ValidationResult.Success;
            }
            else
            {
                return  new ValidationResult("Not valid date format!");
            }

        }
    }
}