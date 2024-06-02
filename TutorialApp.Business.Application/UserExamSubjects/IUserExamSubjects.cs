using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Application.UserExamSubjects;

public interface IUserExamSubjects
{
    Task<ResponseViewModelGeneric<List<UserExamSubjectDto>>> GetAllUserExamSubjectsAsync
        (string userId,CancellationToken token);

    Task<ResponseViewModel> SaveUserExamSubjectsAsync(string userId,SaveUserExamSubjectDto  model,CancellationToken token);
}