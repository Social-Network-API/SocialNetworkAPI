using Dapper;
using ExpenseTrackerGrupo2.Utils;
using SocialNetworkApi.DataAccess.Repositories.Interfaces;

namespace SocialNetworkApi.DataAccess.Repositories.Concretes;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{

    protected readonly IDbConnectionFactory _dbConnectionFactory;

    public BaseRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<IList<T>> GetAll()
    {
        var tableName = StringUtils.ToSnakeCase(typeof(T).Name);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return (await connection.QueryAsync<T>($"SELECT * FROM {tableName}")).ToList();
    }

    public async Task<T> GetById(Guid id)
    {
        var tableName = StringUtils.ToSnakeCase(typeof(T).Name);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {tableName} WHERE {StringUtils.ToSnakeCase(typeof(T).Name)}_id = @Id", new { Id = id });

        if (result == null)
            throw new KeyNotFoundException($"{typeof(T).Name} not found.");

        return result;
    }

    public async Task<T> CreateAsync(T entity)
    {
        var tableName = StringUtils.ToSnakeCase(typeof(T).Name);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var query = $"INSERT INTO {tableName} ({StringUtils.GetColumnNames<T>()}) VALUES ({StringUtils.GetColumnParameters<T>()})";
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<bool> UpdateAsync(Guid id, T entity)
    {
        var tableName = StringUtils.ToSnakeCase(typeof(T).Name);
        var idColumn = $"{tableName}_id";
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var query = $"UPDATE {tableName} SET {StringUtils.GetSetClause<T>()} WHERE {idColumn} = @Id";
        var parameters = new DynamicParameters(entity);
        parameters.Add("Id", id);
        return await connection.ExecuteAsync(query, parameters);
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var tableName = StringUtils.ToSnakeCase(typeof(T).Name);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync($"DELETE FROM {tableName} WHERE {StringUtils.ToSnakeCase(typeof(T).Name)}_id = @Id", new { Id = id });
        return result > 0;
    }
}
