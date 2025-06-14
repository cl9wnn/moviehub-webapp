using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;
using Moq;

namespace Tests;

public class ActorServiceTests
{
    private readonly Mock<IActorRepository> _actorRepoMock = new();
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IDistributedCacheService> _cacheMock = new();

    private readonly ActorService _service;

    public ActorServiceTests()
    {
        _service = new ActorService(_actorRepoMock.Object, _userRepoMock.Object, _cacheMock.Object);
    }

    [Fact]
    public async Task GetAllActorsAsync_ReturnsFromCache_IfExists()
    {
        // Arrange
        var cachedActors = new List<Actor> { new() { Id = Guid.NewGuid(), FirstName = "Test Actor" } };
        _cacheMock.Setup(c => c.GetAsync<List<Actor>>("actors:all")).ReturnsAsync(cachedActors);

        // Act
        var result = await _service.GetAllActorsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(cachedActors, result.Data);
        _actorRepoMock.Verify(r => r.GetAllAsync(), Times.Never);
    }

    [Fact]
    public async Task GetAllActorsAsync_FetchesFromRepo_IfCacheEmpty()
    {
        // Arrange
        _cacheMock.Setup(c => c.GetAsync<List<Actor>>("actors:all")).ReturnsAsync((List<Actor>?)null);

        var actorsFromRepo = new List<Actor> { new() { Id = Guid.NewGuid(), FirstName = "Repo Actor" } };
        _actorRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(Result<ICollection<Actor>>.Success(actorsFromRepo));

        // Act
        var result = await _service.GetAllActorsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(actorsFromRepo, result.Data);
        _cacheMock.Verify(c => c.SetAsync<ICollection<Actor>>(
            "actors:all", 
            It.IsAny<ICollection<Actor>>(), 
            TimeSpan.FromMinutes(10)
        ), Times.Once);    }

    [Fact]
    public async Task GetByIdAsync_ReturnsActor_IfExists()
    {
        var id = Guid.NewGuid();
        var actor = new Actor { Id = id, FirstName = "Test" };
        _actorRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(Result<Actor>.Success(actor));

        var result = await _service.GetByIdAsync(id);

        Assert.True(result.IsSuccess);
        Assert.Equal(actor, result.Data);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsSuccess_IfExists()
    {
        var id = Guid.NewGuid();
        _actorRepoMock.Setup(r => r.ExistsAsync(id)).ReturnsAsync(Result.Success());

        var result = await _service.ExistsAsync(id);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateActorAsync_AddsActor_AndClearsCache()
    {
        var actor = new Actor { Id = Guid.NewGuid(), FirstName = "New Actor" };
        _actorRepoMock.Setup(r => r.AddAsync(actor)).ReturnsAsync(Result<Actor>.Success(actor));

        var result = await _service.CreateActorAsync(actor);

        Assert.True(result.IsSuccess);
        _cacheMock.Verify(c => c.RemoveAsync("actors:all"), Times.Once);
    }

    [Fact]
    public async Task DeleteActorAsync_DeletesActor_AndClearsCache()
    {
        var id = Guid.NewGuid();
        _actorRepoMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(Result.Success());

        var result = await _service.DeleteActorAsync(id);

        Assert.True(result.IsSuccess);
        _cacheMock.Verify(c => c.RemoveAsync("actors:all"), Times.Once);
    }

    [Fact]
    public async Task AddOrUpdatePortraitPhotoAsync_ReturnsSuccess_IfOk()
    {
        var id = Guid.NewGuid();
        _actorRepoMock.Setup(r => r.AddOrUpdatePortraitAsync("url.jpg", id)).ReturnsAsync(Result.Success());

        var result = await _service.AddOrUpdatePortraitPhotoAsync("url.jpg", id);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddActorPhotoAsync_ReturnsSuccess_IfOk()
    {
        var id = Guid.NewGuid();
        var photo = new Photo { ImageUrl = "photo.jpg" };
        _actorRepoMock.Setup(r => r.AddActorPhotoAsync(photo, id)).ReturnsAsync(Result.Success());

        var result = await _service.AddActorPhotoAsync(photo, id);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetActorWithUserInfoAsync_ReturnsCorrectDto()
    {
        var userId = Guid.NewGuid();
        var actorId = Guid.NewGuid();
        var actor = new Actor { Id = actorId, FirstName = "Actor" };

        _actorRepoMock.Setup(r => r.GetByIdAsync(actorId)).ReturnsAsync(Result<Actor>.Success(actor));
        _userRepoMock.Setup(r => r.IsActorFavoriteAsync(userId, actorId)).ReturnsAsync(Result<bool>.Success(true));

        var result = await _service.GetActorWithUserInfoAsync(userId, actorId);

        Assert.True(result.IsSuccess);
        Assert.True(result.Data.IsFavorite);
        Assert.Equal(actor, result.Data.Actor);
    }
}