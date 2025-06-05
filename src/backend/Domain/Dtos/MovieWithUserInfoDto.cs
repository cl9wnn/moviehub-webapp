using Domain.Models;

namespace Domain.Dtos;

public class MovieWithUserInfoDto
{
    public Movie Movie { get; set; }
    public bool IsInWatchList { get; set; }
    public int? OwnRating { get; set; }
}