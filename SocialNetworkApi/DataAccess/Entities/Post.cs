namespace SocialNetwork.Entities;
public class Post
{
    public Guid Id { get; init; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<User> LikedByUsers { get; set; } = new List<User>();

    public Post()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}

