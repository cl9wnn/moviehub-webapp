using System.Net;
using System.Net.Mail;
using Domain.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Email;

public class EmailService(IOptions<EmailOptions> options, ILogger<EmailService> logger): IEmailService
{
    public async Task SendEmailAsync(string receptor, string subject, string body)
    {
        var smtpOptions = options.Value;
        
        var smtpClient = new SmtpClient(smtpOptions.SmtpServer, smtpOptions.SmtpPort);
        smtpClient.EnableSsl = smtpOptions.UseSsl;
        smtpClient.UseDefaultCredentials = false;
        
        smtpClient.Credentials = new NetworkCredential(smtpOptions.UserName, smtpOptions.Password);
        
        var message = new MailMessage(smtpOptions.UserName, receptor, subject, body);
        
        try
        {
            await smtpClient.SendMailAsync(message);
            logger.LogInformation("Email sent successfully to {Recipient}", receptor);
        }
        catch (SmtpException ex)
        {
            logger.LogError(ex, "SMTP error while sending email to {@Recipient}: {@Message}", receptor, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while sending email to {@Recipient}: {@Message}", receptor, ex.Message);
        }
    }
}