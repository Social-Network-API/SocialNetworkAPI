using RestApi.Services.interfaces;
using SocialNetwork.Entities;
using SocialNetwork.Mappers.Responses;
using SocialNetwork.Persistence.Repositories;
using SocialNetwork.Models;

namespace SocialNetwork.Services;
public class UserService : IService<User, UserResponse>
{
    private readonly UserRepository _UsersRepository;

    public UserService(UserRepository UserRepository)
    {
        _UsersRepository = UserRepository;
    }

    public async Task<ServiceResult<UserResponse>> CreateAsync(User User)
    {
        await _UsersRepository.CreateAsync(User);
        var response = UserResponse.FromDomain(User);
        return new ServiceResult<UserResponse> { Data = response, Success = true };
    }

    public async Task<ServiceResult<UserResponse>> GetByIdAsync(Guid UserId)
    {
        var User = await _UsersRepository.GetByIdAsync(UserId);
        return User is null
            ? new ServiceResult<UserResponse> { Data = null, Success = false }
            : new ServiceResult<UserResponse> { Data = UserResponse.FromDomain(User), Success = true };
    }

    public async Task<ServiceResult<UserResponse>> EditAsync(Guid UserId, User updatedUser)
    {
        var existingUser = await _UsersRepository.GetByIdAsync(UserId);

        if (existingUser == null)
        {
            return new ServiceResult<UserResponse> { Success = false };
        }

        existingUser.Name = updatedUser.Name;
        existingUser.ProfilePicture = updatedUser.ProfilePicture;

        await _UsersRepository.EditAsync(UserId, existingUser);

        var response = UserResponse.FromDomain(existingUser);
        return new ServiceResult<UserResponse> { Data = response, Success = true };

    }

    public async Task DeleteAsync(Guid UserId)
    {
        await _UsersRepository.DeleteAsync(UserId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _UsersRepository.GetAllAsync(); // Llama al método en el repositorio
    }

}
