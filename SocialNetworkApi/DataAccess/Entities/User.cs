namespace SocialNetwork.Entities;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }  // Hasheado para seguridad
    public string ProfilePicture { get; set; }
}
