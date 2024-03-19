using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TutorialApp.Business.Admin;
using TutorialApp.Business.Application;
using TutorialApp.Business.Common;
using TutorialApp.Business.Common.Middleware.Exception;
using TutorialApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging
        .SetMinimumLevel(LogLevel.Information)
        .AddLog4Net("log4net.config");
    
    
    //Bind DTO
    builder.Services
        .AddBindDtoCommon();
    
    
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddCommon()
        .AddApplication()
        .AddAdmin();
    
    
    
    builder.Services.AddControllers().AddJsonOptions(options => 
    { 
        options.JsonSerializerOptions.ReferenceHandler = 
            ReferenceHandler.IgnoreCycles; 
    });
    
    builder.Services.AddMemoryCache();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
        
        options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", configurePolicy =>
{
    configurePolicy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

var app = builder.Build();
{
    var logger = app.Services.GetRequiredService<ILogManager>();
    app.ConfigureExceptionMiddleware(logger);
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers()
        .RequireAuthorization();
    app.MapAreaControllerRoute(
        name: "CommonArea",
        areaName:"Common",
        pattern:"Common/{controller=Home}/{action=Index}/{id?}"
        
    );
    app.Run();
}
