using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class CommentConfiguration: IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("comments");

        builder.Property(c => c.Content)
            .HasMaxLength(1024)
            .IsRequired();
        
        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);
        
        builder.Property(c => c.CreatedAt)
            .IsRequired();
    }
}