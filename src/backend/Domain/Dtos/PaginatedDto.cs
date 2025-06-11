namespace Domain.Dtos;

public class PaginatedDto<T>
{
    public ICollection<T> Items { get; set; }
    public int TotalCount { get; set; }
}