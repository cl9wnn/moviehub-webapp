namespace Infrastructure.Database.Entities;

public class MovieDirectorEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}