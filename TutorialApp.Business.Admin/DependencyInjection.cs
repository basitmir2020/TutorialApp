using Microsoft.Extensions.DependencyInjection;

namespace TutorialApp.Business.Admin;

public static class DependencyInjection
{
    public static IServiceCollection AddAdmin(this IServiceCollection service)
    {
        return service;
    }
}