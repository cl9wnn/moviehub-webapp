using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovieRatingConfiguration: IEntityTypeConfiguration<MovieRatingEntity>
{
    public void Configure(EntityTypeBuilder<MovieRatingEntity> builder)
    {
        builder.ToTable("movie_ratings", tb =>
        {
            tb.HasCheckConstraint("CK_MovieRatings_Rating", "\"Rating\" >= 1 AND \"Rating\" <= 10");
        });
        
        builder.HasKey(x => new { x.UserId, x.MovieId });

        builder.Property(x => x.Rating)
            .IsRequired();
    }
}