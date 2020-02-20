﻿using System;
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
            // Validate if entered date is in the future
            var dateValue = objValue as DateTime? ?? new DateTime();
            if (dateValue.Date < DateTime.Now.Date)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}