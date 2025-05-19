using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class ActorConfiguration: IEntityTypeConfiguration<ActorEntity>
{
    public void Configure(EntityTypeBuilder<ActorEntity> builder)
    {
        builder.ToTable("actors");
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(32)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(x => x.Biography)
            .HasMaxLength(512)
            .IsRequired();
        
        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(256)
            .IsRequired();
    }
}