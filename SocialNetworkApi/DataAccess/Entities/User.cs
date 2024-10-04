namespace SocialNetworkApi.DataAccess.Entities;

public class User
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public required string ProfilePicture { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public User()
    {
        UserId = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
