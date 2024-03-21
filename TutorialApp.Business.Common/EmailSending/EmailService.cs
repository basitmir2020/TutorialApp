using System.Net;
using System.Net.Mail;

namespace TutorialApp.Business.Common.EmailSending;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailService()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("your-email@gmail.com", "your-password"),
            EnableSsl = true,
        };
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MailMessage("your-email@gmail.com", toEmail, subject, body)
        {
            IsBodyHtml = true
        };

        await _smtpClient.SendMailAsync(message);
    }
}