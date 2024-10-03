using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests;
public record CreateLikeRequest(Guid UserId)
{
    public Like ToDomain()
    {
        return new Like
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
