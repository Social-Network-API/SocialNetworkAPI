using System.Data;

namespace SocialNetwork.Persistence.DataBase;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
