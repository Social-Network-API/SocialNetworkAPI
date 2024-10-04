using SocialNetworkApi.Business.Mappers.Response;
using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.DataAccess.Repositories.Concretes;
using SocialNetworkApi.Models;
using SocialNetworkApi.Services.Interface;

namespace SocialNetworkApi.Services;
public class UserService : IService<User, UserResponse>
{
    private readonly UserRepository _usersRepository;

    public UserService(UserRepository userRepository)
    {
        _usersRepository = userRepository;
    }

    public async Task<ServiceResult<UserResponse>> CreateAsync(User User)
    {
        await _usersRepository.CreateAsync(User);
        var response = UserResponse.FromDomain(User);
        return new ServiceResult<UserResponse> { Data = response, Success = true };
    }

    public async Task<ServiceResult<UserResponse>> GetByIdAsync(Guid UserId)
    {
        var User = await _usersRepository.GetByIdAsync(UserId);
        return User is null
            ? new ServiceResult<UserResponse> { Data = null, Success = false }
            : new ServiceResult<UserResponse> { Data = UserResponse.FromDomain(User), Success = true };
    }

    public async Task<ServiceResult<UserResponse>> UpdateAsync(Guid UserId, User updatedUser)
    {
        var existingUser = await _usersRepository.GetByIdAsync(UserId);

        if (existingUser == null)
        {
            return new ServiceResult<UserResponse> { Success = false };
        }

        existingUser.Name = updatedUser.Name;
        existingUser.ProfilePicture = updatedUser.ProfilePicture;

        await _usersRepository.EditAsync(UserId, existingUser);

        var response = UserResponse.FromDomain(existingUser);
        return new ServiceResult<UserResponse> { Data = response, Success = true };

    }

    public async Task<ServiceResult> DeleteAsync(Guid UserId)
    {
        await _usersRepository.DeleteAsync(UserId);
        return new ServiceResult { Success = true };
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _usersRepository.GetAllAsync();
    }

    public async Task<IEnumerable<UserResponse>> SearchUsersAsync(string searchTerm)
    {
        var users = await _usersRepository.SearchAsync(searchTerm);
        return users.Select(UserResponse.FromDomain);
    }

}
