using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Admin.Dashboard;

public interface IDashboardService
{
    Task<ResponseViewModelGeneric<DashboardDto>> GetAllCountsAsync(CancellationToken token);
}