namespace Infrastructure.Email;

public class EmailOptions
{
    public string SmtpServer { get; set; } = "";
    public int SmtpPort { get; set; }
    public bool UseSsl { get; set; }
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}