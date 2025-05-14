using Application.Utils;
using AutoMapper;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class UserRepository(AppDbContext context, IMapper mapper): IUserRepository
{
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

    public async Task<Result> AddAsync(User user)
    {
        if (await context.Users.AnyAsync(u => u.Username == user.Username))
        {
            return Result.Failure("User with this username already exists!");
        }
        
        var userEntity = mapper.Map<UserEntity>(user);

        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();
        
        return Result.Success();
    }
}