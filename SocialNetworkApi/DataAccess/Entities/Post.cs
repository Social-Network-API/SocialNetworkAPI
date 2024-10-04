namespace SocialNetworkApi.DataAccess.Entities;

public class Post
{
    public Guid Id { get; init; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = default!; 
    public DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; } = default!; 

    public Post()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}

