namespace SocialNetworkApi.Business.Mappers.Request.Post;

public record CreatePostRequest(Guid UserId, string Content, string ImageUrl)
{
    public DataAccess.Entities.Post ToDomain()
    {
        return new DataAccess.Entities.Post
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            Content = Content,
            ImageUrl = ImageUrl,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
