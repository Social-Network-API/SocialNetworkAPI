using SocialNetwork.Entities;

namespace SocialNetworkApi.Services;
public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<IList<User>> GetUsersAsync();
    Task<User?> GetUserAsync(Guid id);
    Task<bool> UpdateUserAsync(Guid id, User updatedUser);
    Task<bool> DeleteUserAsync(Guid id);
}
