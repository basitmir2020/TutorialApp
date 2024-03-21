
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutorialApp.Infrastructure.Identity;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Infrastructure.DB;

public partial class TutorialAppIdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public TutorialAppIdentityContext(DbContextOptions<TutorialAppIdentityContext> options)
        : base(options)
    {
    }
}