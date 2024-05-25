using TutorialApp.Business.Admin.Exams;
using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Admin.SubjectChapters;

public interface ISubjectChapters
{
    Task<ResponseViewModel> SaveSubjectChaptersAsync(SubjectChaptersDto model,string userId,CancellationToken token);
    Task<ResponseViewModelGeneric<List<GetAllSubjectChapters>>> 
        GetAllSubjectChaptersAsync(string userId,CancellationToken token,string? filter = null, 
            string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10);
    Task<ResponseViewModelGeneric<GetSubjectChapters>> GetSubjectChaptersByIdAsync(string userId,int examTypeId,
        CancellationToken token);
    Task<ResponseViewModel> UpdateSubjectChaptersAsync(string userId,GetSubjectChapters model,CancellationToken token);
    Task<ResponseViewModel> ChangeSubjectChaptersStatusAsync(string userId,SubjectChapterStatus model,CancellationToken token);
    Task<ResponseViewModel> DeleteSubjectChaptersAsync(string userId,DeleteSubjectChapters model,CancellationToken token);

    Task<ResponseViewModelGeneric<List<SubjectChaptersVM>>>
        SelectSubjectChaptersAsync(CancellationToken token);
}