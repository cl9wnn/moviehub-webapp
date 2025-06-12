namespace Domain.Dtos;

public class PaginatedDto<T>
{
    public ICollection<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
}