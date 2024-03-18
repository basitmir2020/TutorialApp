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
    public static IServiceCollection AddInfrastructure(this IServiceCollection service,
        ConfigurationManager configurationManager)
    {
        //DBContext
        service.AddDbContextPool<TutorialAppContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("DBConnection"),
                sqlServerOptionsAction=>sqlServerOptionsAction.CommandTimeout(60)));
        
        //Identity DBContext
        service.AddDbContextPool<TutorialAppIdentityContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("DBConnection"),
                sqlServerOptionsAction=>sqlServerOptionsAction.CommandTimeout(60)));
        
        service.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<TutorialAppIdentityContext>()
            .AddDefaultTokenProviders();

        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(context =>
        {
            context.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
            context.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = ContextStaticAttribute =>
                {
                    if(ContextStaticAttribute.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        ContextStaticAttribute.Response.Headers.Add("Token-Expired","True");
                    return Task.CompletedTask;
                }
            };
        });
        return service;
    }
}