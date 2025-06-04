using System.Reflection;
using System.Text;
using API.Filters;
using API.Mappings;
using API.Models;
using API.Models.Requests;
using API.Pipeline.Auth;
using API.Validation;
using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using FluentValidation;
using Infrastructure.Database.Mappings;
using Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173", "http://localhost:3000");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            });
        });

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(expression =>
        {
            expression.AddProfile<ApiMappingProfile>();
            expression.AddProfile<InfrastructureMappingProfile>();
        });
        
        return services;
    }

    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddScoped(typeof(EntityExistsFilter<,>));
        services.AddScoped<ValidateImageFileFilter>();
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IAuthService, AuthService>();
        
        var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions!.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions!.SecretKey))
                };
            });
        
        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateActorRequest>, CreateActorValidator>();
        services.AddScoped<IValidator<CreateMovieRequest>, CreateMovieValidator>();
        services.AddScoped<IValidator<CreateMovieActorRequest>, CreateMovieActorValidator>();
        services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
        services.AddScoped<IValidator<PersonalizeUserRequest>, PersonalizeUserValidator>();
        return services;
    }
    
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IWebHostEnvironment env)
    {
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = env.ApplicationName, Version = "v1" });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                { 
                    Description = "JWT Bearer Authentication Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}