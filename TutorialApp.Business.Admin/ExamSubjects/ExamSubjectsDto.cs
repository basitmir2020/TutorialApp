using System.ComponentModel.DataAnnotations;
using TutorialApp.Business.Common.Helpers.Validation;

namespace TutorialApp.Business.Admin.ExamSubjects;

public class ExamSubjectsDto
{
    [Required(ErrorMessage = "Select exam type!")]
    public int? ExamTypeId { get; set; }
    [Required(ErrorMessage = "Please enter subject name!")]
    [NotEmptyString(ErrorMessage = "Please enter subject name!")]
    public string SubjectName { get; set; } = null!;
}

public class GetAllExamTypeSubjects
{
    public int Id { get; set; }
    public string ExamType { get; set; } = null!;
    public string SubjectsName { get; set; } = null!;
    public int? Sequence { get; set; }
    public string Status { get; set; } = null!;
}

public class GetExamSubject
{
    public int Id { get; set; }
    public int ExamTypeId { get; set; }
    public string? ExamType { get; set; } = null!;
    public string SubjectsName { get; set; } = null!;
}

public class ExamSubjectStatus
{
    public int ExamSubjectsId { get; set; }
    public int StatusId { get; set; }
}

public class DeleteExamSubjects
{
    public int ExamSubjectsId { get; set; }
}

public class ExamSubjectsVM
{
    public int Id { get; set; }
    public string SubjectName { get; set; } = null!;
}