using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;
using Moq;
using Xunit;

public class MovieServiceTests
{
    private readonly Mock<IMovieRepository> _movieRepoMock = new();
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IDistributedCacheService> _cacheServiceMock = new();
    private readonly MovieService _movieService;

    public MovieServiceTests()
    {
        _movieService = new MovieService(_movieRepoMock.Object, _userRepoMock.Object, _cacheServiceMock.Object);
    }

    [Fact]
    public async Task GetAllMoviesAsync_ReturnsFromCache()
    {
        var movies = new List<Movie> { new() { Id = Guid.NewGuid(), Title = "Cached Movie" } };
        _cacheServiceMock.Setup(c => c.GetAsync<List<Movie>>(It.IsAny<string>())).ReturnsAsync(movies);

        var result = await _movieService.GetAllMoviesAsync();

        Assert.True(result.IsSuccess);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsMovie()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Title = "Movie" };
        _movieRepoMock.Setup(r => r.GetByIdAsync(movie.Id)).ReturnsAsync(Result<Movie>.Success(movie));

        var result = await _movieService.GetByIdAsync(movie.Id);

        Assert.True(result.IsSuccess);
        Assert.Equal(movie.Id, result.Data.Id);
    }

    [Fact]
    public async Task ExistsAsync_WhenExists_ReturnsSuccess()
    {
        var id = Guid.NewGuid();
        _movieRepoMock.Setup(r => r.ExistsAsync(id)).ReturnsAsync(Result.Success());

        var result = await _movieService.ExistsAsync(id);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateMovieAsync_ReturnsCreatedMovieAndClearsCache()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Title = "New Movie" };
        _movieRepoMock.Setup(r => r.AddAsync(movie)).ReturnsAsync(Result<Movie>.Success(movie));

        var result = await _movieService.CreateMovieAsync(movie);

        Assert.True(result.IsSuccess);
        _cacheServiceMock.Verify(c => c.RemoveAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task DeleteMovieAsync_RemovesCacheOnSuccess()
    {
        var id = Guid.NewGuid();
        _movieRepoMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(Result.Success());

        var result = await _movieService.DeleteMovieAsync(id);

        Assert.True(result.IsSuccess);
        _cacheServiceMock.Verify(c => c.RemoveAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task AddActorsAsync_WithEmptyList_ReturnsFailure()
    {
        var result = await _movieService.AddActorsAsync(new List<MovieActorDto>());

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddActorsAsync_WithData_ReturnsSuccess()
    {
        var actors = new List<MovieActorDto> { new() { ActorId = Guid.NewGuid(), MovieId = Guid.NewGuid(), CharacterName = "Hero" } };
        _movieRepoMock.Setup(r => r.AddActorsAsync(actors)).ReturnsAsync(Result.Success());

        var result = await _movieService.AddActorsAsync(actors);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddOrUpdatePosterPhotoAsync_ReturnsSuccess()
    {
        var id = Guid.NewGuid();
        var url = "http://poster.jpg";
        _movieRepoMock.Setup(r => r.AddOrUpdatePosterPhotoAsync(url, id)).ReturnsAsync(Result.Success());

        var result = await _movieService.AddOrUpdatePosterPhotoAsync(url, id);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddMoviePhotoAsync_ReturnsSuccess()
    {
        var id = Guid.NewGuid();
        var photo = new Photo { ImageUrl = "http://photo.jpg" };
        _movieRepoMock.Setup(r => r.AddMoviePhotoAsync(photo, id)).ReturnsAsync(Result.Success());

        var result = await _movieService.AddMoviePhotoAsync(photo, id);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetMovieWithUserInfoAsync_ReturnsAggregatedData()
    {
        var movieId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var movie = new Movie { Id = movieId };

        _movieRepoMock.Setup(r => r.GetByIdAsync(movieId)).ReturnsAsync(Result<Movie>.Success(movie));
        _userRepoMock.Setup(r => r.IsMovieInWatchListAsync(userId, movieId)).ReturnsAsync(Result<bool>.Success(true));
        _userRepoMock.Setup(r => r.IsMovieInNotInterestedAsync(userId, movieId)).ReturnsAsync(Result<bool>.Success(false));
        _userRepoMock.Setup(r => r.GetMovieRatingAsync(userId, movieId)).ReturnsAsync(Result<int?>.Success(4));

        var result = await _movieService.GetMovieWithUserInfoAsync(userId, movieId);

        Assert.True(result.IsSuccess);
        Assert.True(result.Data.IsInWatchList);
        Assert.False(result.Data.IsInNotInterested);
        Assert.Equal(4, result.Data.OwnRating);
    }

    [Fact]
    public async Task RateMovieAsync_ReturnsSuccess()
    {
        var movieId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var rating = 5;

        _movieRepoMock.Setup(r => r.RateMovieAsync(movieId, userId, rating)).ReturnsAsync(Result.Success());

        var result = await _movieService.RateMovieAsync(movieId, userId, rating);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetTopicsByMovieIdAsync_ReturnsTopics()
    {
        var movieId = Guid.NewGuid();
        var topics = new List<DiscussionTopic> { new DiscussionTopic { Id = Guid.NewGuid(), Title = "Topic 1" } };

        _movieRepoMock.Setup(r => r.GetTopicsByMovieIdAsync(movieId)).ReturnsAsync(Result<List<DiscussionTopic>>.Success(topics));

        var result = await _movieService.GetTopicsByMovieIdAsync(movieId);

        Assert.True(result.IsSuccess);
        Assert.Single(result.Data);
    }
}
