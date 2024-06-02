using Microsoft.Extensions.DependencyInjection;
using TutorialApp.Business.Application.UserExamSubjects;
using TutorialApp.Business.Application.UserExamType;

namespace TutorialApp.Business.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IExamTypeService, ExamTypeService>();
        service.AddScoped<IUserExamSubjects, UserExamSubjects.UserExamSubjects>();
        return service;
    }
}