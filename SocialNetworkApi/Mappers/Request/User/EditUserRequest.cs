using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests;
public record EditUserRequest(string Name, string ProfilePicture)
{
    public User ToDomain()
    {
        return new User
        {
            Name = Name,
            ProfilePicture = ProfilePicture
        };
    }
}