namespace SocialNetworkApi.Mappers.Request.Post;

public record EditPostRequest(string Content, string ImageUrl)
{
    public DataAccess.Entities.Post ToDomain()
    {
        return new DataAccess.Entities.Post
        {
            Content = Content,
            ImageUrl = ImageUrl
        };
    }
}