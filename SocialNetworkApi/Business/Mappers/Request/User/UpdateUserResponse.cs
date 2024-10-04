namespace SocialNetworkApi.Business.Mappers.Request.User;

public record UpdateUserResponse(string Name, string ProfilePicture)
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