namespace API.Pipeline.Auth;
public class AuthOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int AccessTokenExpiredAtMinutes { get; set; }
    public int RefreshTokenExpiredAtDays { get; set; }
}