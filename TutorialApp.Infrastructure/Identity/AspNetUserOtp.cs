namespace TutorialApp.Infrastructure.Identity;

public class AspNetUserOtp
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? Otp { get; set; }
    public int? OtpType { get; set; }
    public DateTime Expiry { get; set; }
    public int StatusId { get; set; }
    public bool IsActive { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}