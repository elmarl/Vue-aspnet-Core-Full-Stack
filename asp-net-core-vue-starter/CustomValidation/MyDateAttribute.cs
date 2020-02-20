using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreVueStarter.CustomValidation
{
    public class MyDateAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Kuupäev peab olema tulevikus";
        }
        protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
        {
            // Validate if entered date is in the future, taking into consideration the year, month, day, hours, minutes.
            var dateValue = objValue as DateTime? ?? new DateTime();
            if (dateValue < DateTime.Now)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
