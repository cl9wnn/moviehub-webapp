using API.Extensions;
using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;
using Infrastructure.Auth;
using Infrastructure.Database.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddCustomCors();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagerDocumentation(builder.Environment);
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(builder.Environment);
}

app.UseSerilogRequestLogging();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Services.ApplyMigrations();

app.Run();