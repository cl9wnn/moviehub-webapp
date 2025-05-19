using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovieActorConfiguration: IEntityTypeConfiguration<MovieActorEntity>
{
    public void Configure(EntityTypeBuilder<MovieActorEntity> builder)
    {
        builder.ToTable("movie_actors");
        
        builder.HasKey(ma => new {ma.MovieId, ma.ActorId});

        builder.HasOne(ma => ma.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);
        
        builder.HasOne(ma => ma.Actor)
            .WithMany(a => a.MovieActors)
            .HasForeignKey(ma => ma.ActorId);
        
        builder.Property(ma => ma.CharacterName)
            .IsRequired()
            .HasMaxLength(128);
    }
}