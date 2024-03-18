using System.ComponentModel.DataAnnotations;

namespace TutorialApp.Infrastructure.Models;

public class LkpCountry
{
    [Key]
    public int Id { get; set; }
    public string CountryName { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
    public int Sequence { get; set; }
    public bool IsActive { get; set; }
    public string Status { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public DateTime ModifiedOn { get; set; }
}