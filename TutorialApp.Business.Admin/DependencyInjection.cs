using Microsoft.Extensions.DependencyInjection;
using TutorialApp.Business.Admin.Exams;
using TutorialApp.Business.Admin.ExamSubjects;

namespace TutorialApp.Business.Admin;

public static class DependencyInjection
{
    public static IServiceCollection AddAdmin(this IServiceCollection service)
    {
        service.AddScoped<IExamService, ExamService>();
        service.AddScoped<IExamSubjectsService, ExamSubjectsService>();
        return service;
    }
}