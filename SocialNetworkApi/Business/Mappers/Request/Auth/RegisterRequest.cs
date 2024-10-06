namespace SocialNetworkApi.Mappers.Request.Auth;
public class RegisterRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ProfilePicture { get; set; }
}
