namespace Domain.Dtos;

public class AuthDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public static AuthDto Create(string accessToken, string refreshToken)
    {
        return new AuthDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}