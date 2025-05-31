using Domain.Models;

namespace Domain.Dtos;

public class ActorWithUserInfoDto
{
    public Actor Actor { get; set; }
    public bool IsFavorite { get; set; }
}