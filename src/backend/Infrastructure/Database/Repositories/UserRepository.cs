using AutoMapper;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class UserRepository(AppDbContext context, IMapper mapper): IUserRepository
{
    public IQueryable<UserEntity> ActiveUsers => context.Users.Where(u => !u.IsDeleted);
    public async Task<Result<ICollection<User>>> GetAllAsync()
    {
        var usersEntities = await ActiveUsers
            .Include(u => u.WatchList)
            .Include(u => u.FavoriteActors)
            .Include(u => u.PreferredGenres)
            .AsNoTracking()
            .ToListAsync();
        
        var users = mapper.Map<List<User>>(usersEntities);
            
        return Result<ICollection<User>>.Success(users);
    }
    
    public async Task<Result<User>> GetByIdAsync(Guid userId)
    {
        var userEntity = await context.Users
            .FirstOrDefaultAsync(u => u.Id == userId );

        return userEntity == null 
            ? Result<User>.Failure("User not found!")! 
            : Result<User>.Success(mapper.Map<User>(userEntity));
    }
    
    public async Task<Result<User>> GetByUsernameAsync(string username)
    {
        var userEntity = await context.Users
            .FirstOrDefaultAsync(u => u.Username == username );

        return userEntity == null
            ? Result<User>.Failure("User not found!")! 
            : Result<User>.Success(mapper.Map<User>(userEntity));
    }

    public async Task<Result<User>> AddAsync(User userDto)
    {
        if (await context.Users.AnyAsync(u => u.Username == userDto.Username))
        {
            return Result<User>.Failure("User with this username already exists!")!;
        }
        
        var userEntity = mapper.Map<UserEntity>(userDto);

        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();
        
        var user = mapper.Map<User>(userEntity);
        
        return Result<User>.Success(user);
    }

    public async Task<Result<User>> UpdateAsync(User userDto)
    {
        var userEntity = await ActiveUsers
            .FirstOrDefaultAsync(u => u.Id == userDto.Id);
        
        if (userEntity == null)
        {
            return Result<User>.Failure("Actor not found")!;
        }    
        
        mapper.Map(userDto, userEntity);
        await context.SaveChangesAsync();
        
        var updated = mapper.Map<User>(userEntity);
        
        return Result<User>.Success(updated);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var userEntity = await ActiveUsers
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if (userEntity == null)
        {
            return Result.Failure("Actor not found")!;
        }
        
        userEntity.IsDeleted = true;
        await context.SaveChangesAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> AddPreferredGenresAsync(Guid userId, List<Genre> genres)
    {
        var genreIds = genres.Select(g => g.Id).ToList();
            
        var genreEntities = await context.Genres
            .Where(g => genreIds.Contains(g.Id))
            .ToListAsync();
            
        var missingIds = genreIds.Except(genreEntities.Select(g => g.Id)).ToList();
            
        if (missingIds.Count != 0)
        {
            return Result.Failure("Genres not found")!;
        }
        
        var userEntity = await ActiveUsers
            .Include(u => u.PreferredGenres)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
        {
            return Result.Failure("User not found")!;
        }

        userEntity.PreferredGenres = genreEntities;
        await context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<bool>> IsActorFavoriteAsync(Guid userId, Guid actorId)
    {
        var userEntity = await ActiveUsers
            .Include(u => u.FavoriteActors)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
        {
            return Result<bool>.Failure("User not found")!;
        }
        
        if (userEntity.FavoriteActors.Any(a => a.Id == actorId))
        {
            return Result<bool>.Success(true);
        }
        
        return Result<bool>.Success(false);
    }
    
    public async Task<Result<bool>> IsMovieInWatchListAsync(Guid userId, Guid movieId)
    {
        var userEntity = await ActiveUsers
            .Include(u => u.WatchList)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
        {
            return Result<bool>.Failure("User not found")!;
        }
        
        if (userEntity.WatchList.Any(a => a.Id == movieId))
        {
            return Result<bool>.Success(true);
        }
        
        return Result<bool>.Success(false);
    }
    
    public async Task<Result> AddFavoriteActorAsync(Guid userId, Guid actorId)
    {
        var userEntity = await ActiveUsers
            .Include(u => u.FavoriteActors)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
        {
            return Result.Failure("User not found")!;
        }

        if (userEntity.FavoriteActors.Any(a => a.Id == actorId))
        {
            return Result.Failure("Actor is already in favorites");
        }
        
        var actorEntity = await context.Actors.FirstOrDefaultAsync(a => a.Id == actorId);

        if (actorEntity == null)
        {
            return Result.Failure("Actor not found")!;
        }
        
        userEntity.FavoriteActors.Add(actorEntity);
        await context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> DeleteFavoriteActorAsync(Guid userId, Guid actorId)
    {
        var userEntity = await ActiveUsers
            .Include(u => u.FavoriteActors)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
        {
            return Result.Failure("User not found")!;
        }
        
        var actorEntity = userEntity.FavoriteActors.FirstOrDefault(a => a.Id == actorId);

        if (actorEntity == null)
        {
            return Result.Failure("Actor not found in favorites")!;
        }
        
        userEntity.FavoriteActors.Remove(actorEntity);
        await context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> AddToWatchListAsync(Guid userId, Guid movieId)
    {
        var userEntity = await ActiveUsers
            .Include(u => u.WatchList)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
        {
            return Result.Failure("User not found")!;
        }

        if (userEntity.WatchList.Any(m => m.Id == movieId))
        {
            return Result.Failure("Movie is already in watchlist");
        }

        var movieEntity = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

        if (movieEntity == null)
        {
            return Result.Failure("Movie not found")!;
        }

        userEntity.WatchList.Add(movieEntity);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteFromWatchListAsync(Guid userId, Guid movieId)
    {
        var userEntity = await ActiveUsers
            .Include(u => u.WatchList)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity == null)
        {
            return Result.Failure("User not found")!;
        }

        var movieEntity = userEntity.WatchList.FirstOrDefault(m => m.Id == movieId);

        if (movieEntity == null)
        {
            return Result.Failure("Movie not found in watchlist")!;
        }

        userEntity.WatchList.Remove(movieEntity);
        await context.SaveChangesAsync();

        return Result.Success();
    }
}