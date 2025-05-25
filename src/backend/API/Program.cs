using API.Extensions;
using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Infrastructure.Database.Repositories;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddCustomCors();
builder.Services.AddFilters();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation(builder.Environment);
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddMinioStorage(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddValidators();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(builder.Environment);
}

app.UseRequestResponseLogging();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Services.ApplyMigrations();

app.Run();