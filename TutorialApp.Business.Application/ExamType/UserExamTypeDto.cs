using System.ComponentModel.DataAnnotations;
using TutorialApp.Business.Common.Validators;

namespace TutorialApp.Business.Application.ExamType;

public class UserExamTypeDto
{
    [Required(ErrorMessage = "Exam Type Not Selected!")]
    [GreaterThanZero(ErrorMessage = "Exam Type Not Valid Selected!")]
    public int ExamTypeId { get; set; }
}