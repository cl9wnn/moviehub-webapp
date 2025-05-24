namespace API.Models.Requests;

public record CreateMovieActorByIdRequest(Guid ActorId, string CharacterName);
