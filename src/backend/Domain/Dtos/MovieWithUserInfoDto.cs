using Domain.Models;

namespace Domain.Dtos;

public class MovieWithUserInfoDto
{
    public Movie Movie { get; set; }
    public bool IsInWatchList { get; set; }
}