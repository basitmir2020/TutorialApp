using System.ComponentModel.DataAnnotations;
using TutorialApp.Business.Common.Helpers.Validation;

namespace TutorialApp.Business.Admin.Exams;

public class ExamTypeDto
{
    [Required(ErrorMessage = "Please enter exam types!")]
    [NotEmptyString(ErrorMessage = "Please enter exam types!")]
    public string ExamType { get; set; } = null!;
    public string ExamTypeIcon { get; set; } = null!;
}