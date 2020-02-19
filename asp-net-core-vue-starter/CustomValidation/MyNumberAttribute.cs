using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreVueStarter.CustomValidation
{
    // Custom Number validator, 
    public class MyNumberAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Kood peab olema numbrites";
        }
        protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
        {
            // Validate if entered id code is a number
            bool isNumber = int.TryParse(objValue as string, out _);
            if (!isNumber)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
