using Microsoft.AspNetCore.Identity;

namespace TutorialApp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public int CountryId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}