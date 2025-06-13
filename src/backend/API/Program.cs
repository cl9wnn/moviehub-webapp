using API.Extensions;
using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Infrastructure.Database.Repositories;
using Infrastructure.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddCustomCors();
builder.Services.AddFilters();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation(builder.Environment);
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddMinioStorage(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddGlobalRateLimiting();
builder.Services.AddValidators();
builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(builder.Environment);
    app.Services.ApplyMigrations();
    app.SeedDatabase();
}

app.UseRateLimiter();
app.UseRequestResponseLogging();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();