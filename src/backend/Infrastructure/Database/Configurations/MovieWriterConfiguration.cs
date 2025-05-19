using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovieWriterConfiguration: IEntityTypeConfiguration<MovieWriterEntity>
{
    public void Configure(EntityTypeBuilder<MovieWriterEntity> builder)
    {
        builder.ToTable("movie_writers");
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(32)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .HasMaxLength(64)
            .IsRequired();
    }
}