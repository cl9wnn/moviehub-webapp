using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class PhotoConfiguration: IEntityTypeConfiguration<PhotoEntity>
{
    public void Configure(EntityTypeBuilder<PhotoEntity> builder)
    {
        builder.ToTable("photos");

        builder.HasDiscriminator<string>("PhotoType")
            .HasValue<ActorPhotoEntity>("Actor")
            .HasValue<MoviePhotoEntity>("Movie");
    }
}