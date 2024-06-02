using System.ComponentModel.DataAnnotations;
using TutorialApp.Business.Common.Validators;

namespace TutorialApp.Business.Application.UserExamSubjects;

public class UserExamSubjectDto
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
}

public class SaveUserExamSubjectDto
{
    [Required(ErrorMessage = "Subject Not Selected!")]
    [GreaterThanZero(ErrorMessage = "Select valid Subject!")]
    public int SubjectId { get; set; } 
}

