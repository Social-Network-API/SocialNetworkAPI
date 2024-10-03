using SocialNetwork.Entities;
using SocialNetworkApi.Mappers.Request.Auth;

namespace SocialNetworkApi.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
    }
}