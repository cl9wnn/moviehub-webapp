using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class DiscussionTopicConfiguration: IEntityTypeConfiguration<DiscussionTopicEntity>
{
    public void Configure(EntityTypeBuilder<DiscussionTopicEntity> builder)
    {
        builder.ToTable("discussion_topics");

        builder.Property(d => d.Title)
            .IsRequired()
            .HasMaxLength(128);
        
        builder.Property(d => d.Content)
            .HasMaxLength(1024)
            .IsRequired();
        
        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.Property(d => d.IsDeleted)
            .HasDefaultValue(false);
        
        builder.Property(d => d.Views)
            .HasDefaultValue(0);
    }
}