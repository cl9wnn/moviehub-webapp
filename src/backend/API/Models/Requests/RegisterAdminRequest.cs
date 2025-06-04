namespace API.Models.Requests;

public record RegisterAdminRequest(string Username, string Email, string Password, string SecretKey);
    
