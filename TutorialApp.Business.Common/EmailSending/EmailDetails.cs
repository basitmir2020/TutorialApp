using Microsoft.AspNetCore.Http;

namespace TutorialApp.Business.Common.EmailSending;

public class EmailDetails
{
    public List<UserDetails>? EmailTo { get; set; }
    public List<UserDetails>? EmailCC { get; set; }
    public List<UserDetails>? EmailBCC { get; set; }
    public string? EmailSubject { get; set; }
    public string? EmailBody { get; set; }
    public int? EmailTemplateId { get; set; }
    public int? PriorityTypeId { get; set; }
    public string? SentBy { get; set; }
    public bool IsHtml = false;
    public List<IFormFile>? Attachments { get; set; }
    public string? EmailToUserId { get; set; }
    public string? AttachmentPath { get; set; }
    public int? CommunicationTypeId { get; set; }
}

public class EmailDetailsDto
{
    public List<string> toEmailsUserIds { get; set; } = null!;
    public string? templateTag { get; set; }
    public IDictionary<string, string>? emailBodyPlaceHolders { get; set; }
    public string? currentUserId { get; set; }
    public List<IFormFile>? files { get; set; }
    public string? filesPath { get; set; }
    public int? CommunicationTypeId { get; set; }
}