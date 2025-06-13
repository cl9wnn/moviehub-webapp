namespace API.Models.Responses;

public class MovieWithUserInfoResponse
{
    public MovieResponse Movie { get; set; }
    public bool IsInWatchList { get; set; }
    public bool IsInNotInterested { get; set; }
    public int? OwnRating { get; set; }
}