using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class GenreConfiguration: IEntityTypeConfiguration<GenreEntity>
{
    public void Configure(EntityTypeBuilder<GenreEntity> builder)
    {
        builder.ToTable("genres");
        
        builder.Property(g => g.Name)
            .HasMaxLength(64).IsRequired();
        
        var genres = new List<GenreEntity>();
        var popularGenres = new[] { "Action", "Adventure", "Comedy", "Drama", "Horror", "Sci-Fi", "Fantasy", "Romance", 
            "Thriller", "Crime", "Mystery", "Animation", "Documentary", "Western" };

        for (int i = 0; i < popularGenres.Length; i++)
        {
            genres.Add(new GenreEntity 
            { 
                Id = i + 1, 
                Name = popularGenres[i] 
            });
        }

        builder.HasData(genres);
    }
}