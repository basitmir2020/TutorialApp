using Microsoft.Extensions.DependencyInjection;
using TutorialApp.Business.Application.ExamType;

namespace TutorialApp.Business.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IExamTypeService, ExamTypeService>();
        return service;
    }
}