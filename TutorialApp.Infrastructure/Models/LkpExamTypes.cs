using System.ComponentModel.DataAnnotations;

namespace TutorialApp.Infrastructure.Models;

public class LkpExamTypes
{
    [Key]
    public int Id { get; set; }
    public string ExamType { get; set; } = null!;
    public string? ExamTypeIcon { get; set; }
    public int? Sequence { get; set; }
    public bool IsActive { get; set; }
    public string? Status { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    
}