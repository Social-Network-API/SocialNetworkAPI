using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests;
public record UpdateCommentResponse(string Content)
{
    public Comment ToDomain()
    {
        return new Comment
        {
            Content = Content,
            CreatedAt = DateTime.UtcNow
        };
    }
}
