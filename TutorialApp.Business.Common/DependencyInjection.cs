using Microsoft.Extensions.DependencyInjection;
using Nelibur.ObjectMapper;
using TutorialApp.Business.Common.Authentication;
using TutorialApp.Business.Common.Token;
using TutorialApp.Business.Common.Lookup.CountryLookup;
using TutorialApp.Business.Common.Lookup.ExamTypeLookup;
using TutorialApp.Business.Common.Lookup.UserLookup;
using TutorialApp.Business.Common.Middleware.Exception;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection service)
    {
        service.AddSingleton<ILogManager, LogManager>();
        service.AddTransient<ITokenService, TokenService>();
        service.AddScoped<ICountryService, CountryService>();
        service.AddScoped<IExamTypeService, ExamTypeService>();
        service.AddScoped<IAuthService, AuthService>();
        service.AddScoped<IUserService, UserService>();
        return service;
    }
    
    public static IServiceCollection AddBindDtoCommon(this IServiceCollection service)
    {
        TinyMapper.Bind<List<LkpCountry>,List<CountryDto>>();
        TinyMapper.Bind<List<LkpExamTypes>,List<ExamTypeDto>>();
        return service;
    }
}