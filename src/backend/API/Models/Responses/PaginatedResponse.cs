namespace API.Models.Responses;

public class PaginatedResponse<T>
{
    public ICollection<T> Items { get; set; }
    public int TotalCount { get; set; }
}