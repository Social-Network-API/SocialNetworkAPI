using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.Mappers.Request.Auth;

namespace SocialNetworkApi.Services.Interface;
public interface IAuthService
{
    Task<User> RegisterAsync(RegisterRequest request);
    Task<(string token, Guid userId)> LoginAsync(LoginRequest request);
    Task<bool> LogoutAsync(string token);
}
