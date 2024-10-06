using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Business.Mappers.Response;

public record UserResponse(Guid Id, string Name, string Email, DateTime CreatedAt, string ProfilePicture)
{
    public static UserResponse FromDomain(User user)
    {
        return new UserResponse(user.UserId, user.Name, user.Email, user.CreatedAt, user.ProfilePicture);
    }
}
