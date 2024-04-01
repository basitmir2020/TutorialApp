using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TutorialApp.Business.Admin;
using TutorialApp.Business.Application;
using TutorialApp.Business.Common;
using TutorialApp.Infrastructure;
using TutorialApp.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);
/*builder.Logging.ClearProviders();
builder.Logging
    .SetMinimumLevel(LogLevel.Information)
    .AddLog4Net("log4net.config");*/

//Bind DTO
builder.Services
    .AddBindDtoCommon();


builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddCommon()
    .AddApplication()
    .AddAdmin();

//JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(context =>
{
    context.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
    context.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = contextStaticAttribute =>
        {
            if (contextStaticAttribute.Exception.GetType() == typeof(SecurityTokenExpiredException))
                contextStaticAttribute.Response.Headers.Add("Token-Expired", "True");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        ReferenceHandler.IgnoreCycles;
});

builder.Services.AddControllers(
    options => options.Filters.Add(new ValidateInputActionFilterAttribute())
);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var result = new CustomValidationFailedResult(context.ModelState);

        // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
        result.ContentTypes.Add(MediaTypeNames.Application.Json);
        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

        return result;
    };
});

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        option.IncludeXmlComments(xmlPath);

        option.SwaggerDoc("v1", new OpenApiInfo {
            Title = "Tutorial App", Version = "v1"
        });

        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        option.OperationFilter < AuthResponsesOperationFilter > ();
    }
);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", configurePolicy =>
{
    configurePolicy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

var app = builder.Build();

/*var logger = app.Services.GetRequiredService<ILogManager>();
app.ConfigureExceptionMiddleware(logger);*/
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
    "CommonArea",
    "Common",
    "Common/{controller=Home}/{action=Index}/{id?}"
);
app.Run();