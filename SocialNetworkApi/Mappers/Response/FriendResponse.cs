using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Mappers.Response;

public record FriendResponse(Guid FriendId, string FriendName)
{
    public static FriendResponse FromDomain(User user)
    {
        return new FriendResponse(user.UserId, user.Name);
    }
}
