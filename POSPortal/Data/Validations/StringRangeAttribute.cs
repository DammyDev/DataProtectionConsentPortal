using System;
using System.ComponentModel.DataAnnotations;

namespace POSPortal.Data.Validations
{
    public class StringRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "Others")
            {
                return new ValidationResult("Please specify the business segment");
            }

            return ValidationResult.Success;
        }
    }
}
