using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddDbContext<TutorialAppContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("DBConnection"),
                sqlServerOptionsAction => sqlServerOptionsAction.CommandTimeout(60))
        );
        
        //Identity DBContext
        services.AddDbContext<TutorialAppIdentityContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("DBConnection"),
                sqlServerOptionsAction => sqlServerOptionsAction.CommandTimeout(60)));
        
        //Identity Configuration
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<TutorialAppIdentityContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}