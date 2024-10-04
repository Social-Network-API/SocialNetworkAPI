using SocialNetworkApi.DataAccess.Repositories.Concretes;
using SocialNetworkApi.Services;
using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.Persistence.DataBase;

namespace SocialNetworkApi.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSocialNetworkServices(this IServiceCollection services)
    {
        services.AddScoped<PostsService>();
        services.AddScoped<UserService>();
        services.AddScoped<CommentsService>();
        services.AddScoped<FollowService>();
        services.AddScoped<LikeService>();
        return services;
    }

    public static IServiceCollection AddSocialNetworkRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<CommentsRepository>();
        services.AddScoped<PostsRepository>();
        services.AddScoped<FollowRepository>();
        services.AddScoped<LikeRepository>();
        return services;
    }

    public static IServiceCollection AddSocialNetworkDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
