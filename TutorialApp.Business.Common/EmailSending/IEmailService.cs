namespace TutorialApp.Business.Common.EmailSending;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}