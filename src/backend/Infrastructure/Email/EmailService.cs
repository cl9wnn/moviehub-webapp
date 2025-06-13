using System.Net;
using System.Net.Mail;
using Domain.Abstractions.Services;
using Microsoft.Extensions.Options;

namespace Infrastructure.Email;

public class EmailService(IOptions<EmailOptions> options): IEmailService
{
    public async Task SendEmailAsync(string receptor, string subject, string body)
    {
        var smtpOptions = options.Value;
        
        var smtpClient = new SmtpClient(smtpOptions.SmtpServer, smtpOptions.SmtpPort);
        smtpClient.EnableSsl = smtpOptions.UseSsl;
        smtpClient.UseDefaultCredentials = false;
        
        smtpClient.Credentials = new NetworkCredential(smtpOptions.UserName, smtpOptions.Password);
        
        var message = new MailMessage(smtpOptions.UserName, receptor, subject, body);
        await smtpClient.SendMailAsync(message);
    }
}