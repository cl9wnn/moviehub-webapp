using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovieDirectorConfiguration: IEntityTypeConfiguration<MovieDirectorEntity>
{
    public void Configure(EntityTypeBuilder<MovieDirectorEntity> builder)
    {
        builder.ToTable("movie_directors");
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(32)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .HasMaxLength(64)
            .IsRequired();
    }
}