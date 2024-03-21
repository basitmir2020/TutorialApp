using System.ComponentModel.DataAnnotations;

namespace TutorialApp.Business.Common.Helpers.Validation;

public class NotEmptyStringAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return value switch
        {
            null => new ValidationResult("The field is required."),
            string stringValue when string.IsNullOrWhiteSpace(stringValue) => new ValidationResult(
                "The field must not be empty."),
            _ => ValidationResult.Success
        };
    }
}