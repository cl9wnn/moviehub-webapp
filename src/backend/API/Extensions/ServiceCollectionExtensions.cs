using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using API.Filters;
using API.Mappings;
using API.Models.Requests;
using API.Options;
using API.Pipeline.Auth;
using API.Validation;
using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.BackgroundJobs;
using Infrastructure.Database.Mappings;
using Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FrontendOptions>(configuration.GetSection("FrontendOptions"));
        
        var frontendOptions = configuration.GetSection("FrontendOptions").Get<FrontendOptions>();
        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(frontendOptions!.LocalUrl, frontendOptions.PublicUrl);
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
        services.Configure<AdminOptions>(configuration.GetSection("AdminOptions"));
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
        services.AddScoped<IValidator<RegisterAdminRequest>, RegisterAdminValidator>();
        services.AddScoped<IValidator<PersonalizeUserRequest>, PersonalizeUserValidator>();
        services.AddScoped<IValidator<CreateDiscussionTopicRequest>, CreateDiscussionTopicValidator>();
        services.AddScoped<IValidator<CreateCommentRequest>, CreateCommentValidator>();

        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<IActorRepository, ActorRepository>();

        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IMovieRepository, MovieRepository>();

        services.AddScoped<IDiscussionTopicService, DiscussionTopicService>();
        services.AddScoped<IDiscussionTopicRepository, DiscussionTopicRepository>();

        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddScoped<IRecommendationService, RecommendationService>();
        services.AddScoped<IRecommendationRepository, RecommendationRepository>();
        
        services.AddScoped<IMediaService, MediaService>();

        return services;
    }

    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddScoped<TopicViewsSyncJob>();

        return services;
    }
    
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy =>
            {
                policy.RequireClaim("Admin");
            });

        return services;
    }
    
    public static IServiceCollection AddGlobalRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter("GlobalLimiter", key => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromSeconds(10),
                    QueueLimit = 2,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                }));

            options.RejectionStatusCode = 429;
        });

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