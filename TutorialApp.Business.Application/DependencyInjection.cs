using Microsoft.Extensions.DependencyInjection;

namespace TutorialApp.Business.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        return service;
    }
}