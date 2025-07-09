namespace Domain.Abstractions.Services;

public interface IEmailService
{
    Task SendEmailAsync(string receptor, string subject, string body, bool isBodyHtml = false);
}