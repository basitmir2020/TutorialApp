using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;

namespace TutorialApp.Business.Admin.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly TutorialAppContext _tutorialAppContext;

    public DashboardService(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }

    public async Task<ResponseViewModelGeneric<DashboardDto>> GetAllCountsAsync(CancellationToken token)
    {
        var response = new DashboardDto
        {
            UserCount = await _tutorialAppContext.AspNetUsers
                .Include(x => x.Roles)
                .Where(x => x.Roles.Any(r => r.Name == "Applicant"))
                .CountAsync(cancellationToken: token),
            ExamTypeCount = await _tutorialAppContext.ExamTypes
                .CountAsync(x =>x.IsActive, 
                    cancellationToken: token),
            ExamSubjectCount = await _tutorialAppContext.ExamSubjects
                .CountAsync(x=>x.IsActive,
                    cancellationToken: token),
            SubjectChaptersCount = await _tutorialAppContext.SubjectChapters
                .CountAsync(x=>x.IsActive,cancellationToken : token)
            
        };

        return new ResponseViewModelGeneric<DashboardDto>(response)
        {
            Success = true,
            StatusCode = 200,
            Message = "All Dashboard Counts"
        };
    }
}