using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovieConfiguration: IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.ToTable("movies");
        
        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(m => m.Year)
            .IsRequired();
        
        builder.Property(m => m.DurationAtMinutes)
            .IsRequired();
        
        builder.Property(m => m.RatingCount)
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.Property(m => m.RatingSum)
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.Property(m => m.AgeRating)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(m => m.PosterUrl)
            .HasMaxLength(256);
        
        builder.Property(m => m.IsDeleted)
            .HasDefaultValue(false);
    }
}