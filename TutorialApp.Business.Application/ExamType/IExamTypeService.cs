using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Application.ExamType;

public interface IExamTypeService
{
    Task<ResponseViewModel> SaveUserExamTypeAsync(UserExamTypeDto model,string userId,CancellationToken token);
}