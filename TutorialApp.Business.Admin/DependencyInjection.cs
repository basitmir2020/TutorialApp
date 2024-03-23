using Microsoft.Extensions.DependencyInjection;
using TutorialApp.Business.Admin.Exams;

namespace TutorialApp.Business.Admin;

public static class DependencyInjection
{
    public static IServiceCollection AddAdmin(this IServiceCollection service)
    {
        service.AddScoped<IExamService, ExamService>();
        return service;
    }
}