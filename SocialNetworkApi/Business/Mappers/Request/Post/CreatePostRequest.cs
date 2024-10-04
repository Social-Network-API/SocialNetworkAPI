using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests;
public record CreatePostRequest(Guid UserId, string Content, string ImageUrl)
{
    public Post ToDomain()
    {
        return new Post
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            Content = Content,
            ImageUrl = ImageUrl,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
