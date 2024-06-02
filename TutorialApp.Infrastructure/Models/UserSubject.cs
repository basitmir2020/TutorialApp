using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorialApp.Infrastructure.Models;

public class UserSubject
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("UserExamType")]
    public int UserExamTypeId { get; set; }

    public int UserSubjectId { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [MaxLength]
    public string CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [MaxLength]
    public string ModifiedBy { get; set; }

    [Required]
    public DateTime ModifiedOn { get; set; }

    public virtual UserExamType? UserExamType { get; set; }
}