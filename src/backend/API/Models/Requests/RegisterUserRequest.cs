namespace API.Models.Requests;

public record RegisterUserRequest(string Username, string Email, string Password);