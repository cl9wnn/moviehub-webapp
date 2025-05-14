namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public static User Create(string username, string email, string password)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            Password = password,

        };
    }

}