using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Admin.ExamSubjects;

public interface IExamSubjectsService
{
    Task<ResponseViewModel> SaveExamSubjectsAsync(ExamSubjectsDto model,string userId,CancellationToken token);

    Task<ResponseViewModelGeneric<List<GetAllExamTypeSubjects>>> GetAllExamTypeSubjectsAsync(
        string userId, CancellationToken token, string? filter = null,
        string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10);
    Task<ResponseViewModelGeneric<GetExamSubject>> GetExamSubjectsByIdAsync(string userId,int examTypeId,CancellationToken token);
    Task<ResponseViewModel> UpdateExamSubjectsAsync(string userId,GetExamSubject model,CancellationToken token);
    Task<ResponseViewModel> ChangeExamSubjectsStatusAsync(string userId,ExamSubjectStatus model,CancellationToken token);
    Task<ResponseViewModel> DeleteExamSubjectsAsync(string userId,DeleteExamSubjects model,CancellationToken token);
    Task<ResponseViewModelGeneric<List<ExamSubjectsVM>>>
        SelectExamSubjectsAsync(CancellationToken token);
}