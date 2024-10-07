namespace SocialNetworkApi.DataAccess.Entities;

public class User
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string Email { get; set; } = default!; 
    public string Password { get; set; } = default!; 
    public string? ProfilePicture { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public User()
    {
        UserId = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
