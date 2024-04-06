using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Common.EmailSending;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly EmailSettings _emailSettings;
    private readonly TutorialAppContext _tutorialAppContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> options, TutorialAppContext tutorialAppContext,
        UserManager<ApplicationUser> userManager, ILogger<EmailService> logger)
    {
        _emailSettings = options.Value;
        _tutorialAppContext = tutorialAppContext;
        _userManager = userManager;
        _logger= logger;
        _smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("acetutorialapp@gmail.com", "dfzv nubq esfd rewt")
        };
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        using var message = new MailMessage("acetutorialapp@gmail.com", toEmail);
        message.IsBodyHtml = true;
        message.Subject = subject;
        message.Body = body;

        await _smtpClient.SendMailAsync(message);
    }
}