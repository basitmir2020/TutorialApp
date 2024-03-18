using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TutorialApp.Infrastructure.Identity;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Infrastructure.DB;

public class TutorialAppContext : DbContext
{
    public TutorialAppContext(DbContextOptions<TutorialAppContext> options) :
        base(options) { }

    public virtual DbSet<LkpCountry> LkpCountry { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    
}