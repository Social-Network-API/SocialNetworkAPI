namespace SocialNetworkApi.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Persistence.DataBase;
using SocialNetwork.Persistence.Repositories;
using SocialNetwork.Services;
using SocialNetworkApi.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSocialNetworkServices(this IServiceCollection services)
    {
        services.AddScoped<PostsService>();
        services.AddScoped<UserService>();
        services.AddScoped<CommentsService>();
        services.AddScoped<LikeService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection AddSocialNetworkRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<CommentsRepository>();
        services.AddScoped<PostsRepository>();
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
