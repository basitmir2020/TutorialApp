using System.ComponentModel.DataAnnotations;

namespace TutorialApp.Business.Common.Validators;

public class GreaterThanZeroAttribute :  ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return value is > 0 ? ValidationResult.Success : new ValidationResult("Exam Type Id should be greater than 0");
    }
}