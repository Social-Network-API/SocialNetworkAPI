using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests;
public record CreateUserRequest(string name, string email, string password, string profileUser)
{
    public User ToDomain()
    {
        return new User
        {
            UserId = Guid.NewGuid(),
            Name = name,
            Email = email,
            Password = password,
            ProfilePicture = profileUser,
            CreatedAt = DateTime.UtcNow
        };
    }
}
