using Domain.Abstractions.Services;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Email;

public class EmailService(IOptions<EmailOptions> options, ILogger<EmailService> logger): IEmailService
{
    public async Task SendEmailAsync(string receptor, string subject, string body, bool isBodyHtml = false)
    {
        var smtpOptions = options.Value;
        
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(smtpOptions.UserName));
        message.To.Add(MailboxAddress.Parse(receptor));
        message.Subject = subject;
        message.Body = new TextPart(isBodyHtml ? "html" : "plain") { Text = body };
        
        try
        {
            using var smtpClient = new SmtpClient();
            
            var socketOptions = smtpOptions.SmtpPort switch
            {
                465 => SecureSocketOptions.SslOnConnect,
                587 => SecureSocketOptions.StartTls,
                _ => SecureSocketOptions.Auto
            };

            await smtpClient.ConnectAsync(smtpOptions.SmtpServer, smtpOptions.SmtpPort, socketOptions);
            
            await smtpClient.AuthenticateAsync(smtpOptions.UserName, smtpOptions.Password);
            await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);
        }
        catch (SmtpCommandException ex)
        {
            logger.LogError(ex, "SMTP command error while sending email to {@Recipient}: {@Message}", receptor, ex.Message);
        }
        catch (SmtpProtocolException ex)
        {
            logger.LogError(ex, "SMTP protocol error while sending email to {@Recipient}: {@Message}", receptor, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while sending email to {@Recipient}: {@Message}", receptor, ex.Message);
        }
    }
}