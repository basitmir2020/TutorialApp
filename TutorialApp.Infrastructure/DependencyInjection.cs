using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<TutorialAppIdentityContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}