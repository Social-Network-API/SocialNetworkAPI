using SocialNetworkApi.DataAccess.Repositories.Concretes;
using SocialNetworkApi.Mappers.Response;
using SocialNetworkApi.Models;

namespace SocialNetworkApi.Services;

public class FriendService
{
    private readonly FriendsRepository _friendsRepository;

    public FriendService(FriendsRepository friendsRepository)
    {
        _friendsRepository = friendsRepository;
    }

    public async Task<ServiceResult> FollowUserAsync(Guid userId, Guid friendId)
    {
        try
        {
            await _friendsRepository.FollowUserAsync(userId, friendId);
            return new ServiceResult { Success = true };
        }
        catch (ArgumentException ex)
        {
            return new ServiceResult { Success = false, Errors = new[] { new ValidationResponse { ErrorMessage = ex.Message } } };
        }
    }

    public async Task<ServiceResult> UnfollowUserAsync(Guid userId, Guid friendId)
    {
        await _friendsRepository.UnfollowUserAsync(userId, friendId);
        return new ServiceResult { Success = true };
    }

    public async Task<ServiceResult<IEnumerable<FriendResponse>>> GetFriendsListAsync(Guid userId)
    {
        var friends = await _friendsRepository.GetFriendsListAsync(userId);
        var response = friends.Select(FriendResponse.FromDomain);
        return new ServiceResult<IEnumerable<FriendResponse>> { Data = response, Success = true };
    }
}
    

