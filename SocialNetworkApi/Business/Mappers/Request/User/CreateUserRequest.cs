namespace SocialNetworkApi.Business.Mappers.Request.User;

public record CreateUserRequest(string name, string email, string password, string profileUser)
{
    public DataAccess.Entities.User ToDomain()
    {
        return new DataAccess.Entities.User
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
