namespace API.Models.Requests;

public record CreateMovieActorRequest(CreateActorRequest Actor, string CharacterName);