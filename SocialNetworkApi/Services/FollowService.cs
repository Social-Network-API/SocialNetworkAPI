using SocialNetworkApi.Business.Mappers.Response;
using SocialNetworkApi.DataAccess.Repositories.Concretes;
using SocialNetworkApi.Models;

namespace SocialNetworkApi.Services;

public class FollowService
{
    private readonly FollowRepository _friendsRepository;

    public FollowService(FollowRepository friendsRepository)
    {
        _friendsRepository = friendsRepository;
    }

    public async Task<ServiceResult> FollowUserAsync(Guid followerId, Guid followedId)
    {
        try
        {
            await _friendsRepository.FollowUserAsync(followerId, followedId);
            return new ServiceResult { Success = true };
        }
        catch (ArgumentException ex)
        {
            return new ServiceResult { Success = false, Errors = new[] { new ValidationResponse { ErrorMessage = ex.Message } } };
        }
    }

    public async Task<ServiceResult> UnfollowUserAsync(Guid followerId, Guid followedId)
    {
        var success = await _friendsRepository.UnfollowUserAsync(followerId, followedId);
        return new ServiceResult { Success = success };
    }

    public async Task<ServiceResult<IEnumerable<FollowerResponse>>> GetFollowersAsync(Guid userId)
    {
        var followers = await _friendsRepository.GetFollowersAsync(userId);
        return new ServiceResult<IEnumerable<FollowerResponse>> { Data = followers, Success = true };
    }

    public async Task<ServiceResult<IEnumerable<FollowingResponse>>> GetFollowingAsync(Guid userId)
    {
        var following = await _friendsRepository.GetFollowingAsync(userId);
        return new ServiceResult<IEnumerable<FollowingResponse>> { Data = following, Success = true };
    }


}
