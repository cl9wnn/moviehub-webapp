namespace Infrastructure.Database.Entities;

public class GenreEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<MovieEntity> Movies { get; set; }
}