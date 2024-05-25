using Microsoft.Extensions.DependencyInjection;
using TutorialApp.Business.Admin.Dashboard;
using TutorialApp.Business.Admin.Exams;
using TutorialApp.Business.Admin.ExamSubjects;
using TutorialApp.Business.Admin.SubjectChapters;

namespace TutorialApp.Business.Admin;

public static class DependencyInjection
{
    public static IServiceCollection AddAdmin(this IServiceCollection service)
    {
        service.AddScoped<IDashboardService, DashboardService>();
        service.AddScoped<IExamService, ExamService>();
        service.AddScoped<IExamSubjectsService, ExamSubjectsService>();
        service.AddScoped<ISubjectChapters, SubjectChaptersService>();
        return service;
    }
}