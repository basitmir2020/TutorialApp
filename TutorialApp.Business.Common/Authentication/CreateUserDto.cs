using System.ComponentModel.DataAnnotations;
using TutorialApp.Business.Common.Helpers.Validation;

namespace TutorialApp.Business.Common.Authentication;

public class CreateUserDto
{
    [Required(ErrorMessage = "Please provide First Name!")]
    [NotEmptyString(ErrorMessage = "Please provide First Name!")]
    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    [Required(ErrorMessage = "Please Provide Phone Number")]
    [NotEmptyString(ErrorMessage = "Please provide firstName!")]
    [StringLength(20, MinimumLength = 10,
        ErrorMessage = "The Phone Number must be between {2} and {1} characters long!")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please provide email address!")]
    [EmailAddress(ErrorMessage = "Please provide valid email address!")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please provide password!")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Please provide Country Code!")]
    public string CountryCode { get; set; } = null!;

    [Required(ErrorMessage = "Please provide UserType!")]
    public string UserType { get; set; } = null!;
}

public class LoginUserDto
{
    [Required(ErrorMessage = "Please provide email address!")]
    [EmailAddress(ErrorMessage = "Please provide valid email address!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Please provide password!")]
    public string Password { get; set; }
}

public class OtpDto
{
    [Required(ErrorMessage = "Please provide otp!")]
    public string Otp { get; set; } = null!;
}

public class EmailDto
{
    [Required(ErrorMessage = "Please provide Email!")]
    public string Email { get; set; } = null!;
}