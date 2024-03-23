using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Admin.Exams;

public interface IExamService
{
    Task<ResponseViewModel> SaveExamTypeAsync(ExamTypeDto model,string userId,CancellationToken token);
}