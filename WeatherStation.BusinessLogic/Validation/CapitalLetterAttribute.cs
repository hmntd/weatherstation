namespace WeatherStation.BusinessLogic.Validation;

using System.ComponentModel.DataAnnotations;

public class CapitalLetterAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null && value is string text && !string.IsNullOrWhiteSpace(text))
        {
            if (!char.IsUpper(text[0]))
            {
                return new ValidationResult(ErrorMessage ?? "Назва має обов'язково починатися з великої літери.");
            }
        }
        return ValidationResult.Success;
    }
}