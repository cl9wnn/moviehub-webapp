namespace Domain.DTOs;

public class AuthModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public static AuthModel Create(string accessToken, string refreshToken)
    {
        return new AuthModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}