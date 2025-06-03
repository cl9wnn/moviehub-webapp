namespace API.Models.Requests;

public record PersonalizeUserRequest(string Bio, List<int> Genres);
