using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests;
public record UpdatePostResponse(string Content, string ImageUrl)
{
    public Post ToDomain()
    {
        return new Post
        {
            Content = Content,
            ImageUrl = ImageUrl
        };
    }
}