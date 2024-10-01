using SocialNetwork.Entities;
using SocialNetworkApi.DataAccess.Repositories.Interfaces;

namespace SocialNetworkApi.Services;
public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepository;

    public UserService(IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task<IList<User>> GetUsersAsync()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<bool> UpdateUserAsync(Guid id, User updatedUser)
    {
        return await _userRepository.UpdateAsync(id, updatedUser);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}