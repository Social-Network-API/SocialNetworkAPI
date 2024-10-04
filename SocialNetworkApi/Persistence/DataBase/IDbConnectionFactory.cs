using System.Data;

namespace SocialNetworkApi.Persistence.DataBase;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
