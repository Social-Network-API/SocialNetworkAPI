namespace SocialNetworkApi.Mappers.Request.User;

public record EditUserRequest(string Name, string ProfilePicture)
{
    public DataAccess.Entities.User ToDomain()
    {
        return new DataAccess.Entities.User
        {
            Name = Name,
            ProfilePicture = ProfilePicture
        };
    }
}