using System;
using System.ComponentModel.DataAnnotations;

namespace NetPOC.Backend.Domain.Validations
{
    public class CheckBirthDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dt = (DateTime) value;
            return dt <= DateTime.UtcNow
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ?? "Data de nascimento deve ser menor que hoje");
        }
    }
}