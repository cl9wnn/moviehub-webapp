using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class TopicTagConfiguration: IEntityTypeConfiguration<TopicTagEntity>
{
    public void Configure(EntityTypeBuilder<TopicTagEntity> builder)
    {
        builder.ToTable("topic_tags");
        
        builder.Property(g => g.Name)
            .HasMaxLength(64).IsRequired();
        
        var tagEntities = new List<TopicTagEntity>();

        var tags = new[]
        {
            "Спойлеры", "Фан-теории", "Рецензия", "Сюжетные дыры", "Ошибки и ляпы", "Анализ персонажей", "Саундтрек", "Классика",
            "Новинки", "Недооценённое", "Операторская работа"
        };
        
        for (int i = 0; i < tags.Length; i++)
        {
            tagEntities.Add(new TopicTagEntity() 
            { 
                Id = i + 1, 
                Name = tags[i] 
            });
        }

        builder.HasData(tagEntities);
    }
}