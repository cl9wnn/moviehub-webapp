using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class CommentLikeConfiguration: IEntityTypeConfiguration<CommentLikeEntity>
{
    public void Configure(EntityTypeBuilder<CommentLikeEntity> builder)
    {
        builder.ToTable("comment_likes");
    }
}