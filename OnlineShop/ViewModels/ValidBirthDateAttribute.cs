using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime birthDate = Convert.ToDateTime(value);
                if (birthDate < DateTime.Now && birthDate > DateTime.Now.AddYears(-100))
                {
                    return new ValidationResult("Birth date can not be greater than current date.");
                }
            }
            return ValidationResult.Success;
        }
    }
}