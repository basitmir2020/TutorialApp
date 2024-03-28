using System.ComponentModel.DataAnnotations;
using TutorialApp.Business.Common.Helpers.Validation;

namespace TutorialApp.Business.Admin.Exams;

public class ExamTypeDto
{
    [Required(ErrorMessage = "Please select country!!")]
    public int CountryId { get; set; }
    [Required(ErrorMessage = "Please enter exam types!")]
    [NotEmptyString(ErrorMessage = "Please enter exam types!")]
    public string ExamType { get; set; } = null!;
    [Required(ErrorMessage = "Please enter exam sub types!")]
    [NotEmptyString(ErrorMessage = "Please enter exam sub types!")]
    public string ExamSubType { get; set; } = null!;
}

public class GetAllExamTypes
{
    public int Id { get; set; }
    public int? Sequence { get; set; }
    public string CountryName { get; set; } = null!;
    public string ExamType { get; set; } = null!;
    public string ExamSubType { get; set; } = null!;
    public string Status { get; set; } = null!;
}

public class ChangeStatus
{
    public int ExamTypeId { get; set; }
    public int StatusId { get; set; }
}

public class DeleteExamType
{
    public int ExamTypeId { get; set; }
}

public class GetExamType
{
    public int Id { get; set; }
    public int? CountryId { get; set; }
    public string ExamType { get; set; } = null!;
    public string ExamSubType { get; set; } = null!;
}

