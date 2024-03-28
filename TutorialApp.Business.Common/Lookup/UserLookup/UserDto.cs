namespace TutorialApp.Business.Common.Lookup.UserLookup;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? PhoneNumber { get; set; }
}