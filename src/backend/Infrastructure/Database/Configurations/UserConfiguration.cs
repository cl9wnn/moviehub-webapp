using Domain.Enums;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(64);
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(64);
        
        builder.Property(u => u.Password)
            .IsRequired();
        
        builder.Property(u => u.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.AvatarUrl)
            .IsRequired();
        
        builder.Property(u => u.RegistrationDate)
            .IsRequired();

        builder.Property(u => u.Bio)
            .HasMaxLength(256);
        
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired()
            .HasDefaultValue(UserRole.User);
        
        builder
            .HasMany(u => u.WatchList)
            .WithMany(m => m.UsersWatchList)
            .UsingEntity(j => j.ToTable("WatchList"));

        builder
            .HasMany(u => u.NotInterestedMovies)
            .WithMany(m => m.UsersNotInterested)
            .UsingEntity(j => j.ToTable("NotInterestedMovies"));
    }
}