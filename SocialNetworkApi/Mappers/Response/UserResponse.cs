using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Mappers.Response;
public record UserResponse(Guid Id, string Name, string Email, DateTime CreatedAt)
{
    public static UserResponse FromDomain(User User)
    {
        return new UserResponse(User.UserId, User.Name, User.Email, User.CreatedAt);
    }
}

