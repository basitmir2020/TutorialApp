using Microsoft.AspNetCore.Identity;

namespace TutorialApp.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; } = DateTime.Now;
}