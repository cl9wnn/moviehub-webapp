using Domain.Models;

namespace Domain.Dtos;

public class PersonalizeUserDto
{
    public string Bio { get; set; }
    public List<Genre> Genres { get; set; } = new();
}