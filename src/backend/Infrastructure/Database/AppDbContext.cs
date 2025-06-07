using Domain.Models;
using Infrastructure.Database.Configurations;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<ActorEntity> Actors { get; set; }
    public DbSet<ActorPhotoEntity> ActorPhotos { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<MovieActorEntity> MovieActors { get; set; }
    public DbSet<MovieDirectorEntity> MovieDirectors { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<MoviePhotoEntity> MoviePhotos { get; set; }
    public DbSet<MovieWriterEntity> MovieWriters { get; set; }
    public DbSet<MovieRatingEntity> MovieRatings { get; set; }
    public DbSet<PhotoEntity> Photos { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<DiscussionTopicEntity> DiscussionTopics { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<CommentLikeEntity> CommentLikes { get; set; }
    public DbSet<TopicTagEntity> TopicTags { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ActorConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new MovieActorConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new MovieDirectorConfiguration());
        modelBuilder.ApplyConfiguration(new MovieWriterConfiguration());
        modelBuilder.ApplyConfiguration(new PhotoConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new MovieRatingConfiguration());
        modelBuilder.ApplyConfiguration(new DiscussionTopicConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new TopicTagConfiguration());
        modelBuilder.ApplyConfiguration(new CommentLikeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}