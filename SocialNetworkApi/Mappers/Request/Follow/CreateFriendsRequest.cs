using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Mappers.Request.Follow;

public record CreateFriendsRequest(Guid FollowerId)
{
    public Friend ToDomain(Guid friendId)
    {
        return new Friend
        {
            UserId = FollowerId,
            FriendId = friendId
        };
    }
}

