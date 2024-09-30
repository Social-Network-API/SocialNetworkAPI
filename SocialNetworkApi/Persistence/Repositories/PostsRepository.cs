using System;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using SocialNetwork.Domain;
using SocialNetwork.Persistence.DataBase;

namespace SocialNetwork.Persistence.Repositories
{
    public class PostsRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PostsRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Post> CreateAsync(Post post)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                string sql = @"INSERT INTO Posts (post_id, user_id, content, image, created_at)
                               VALUES (@PostId, @UserId, @Content, @Image, @CreatedAt)
                               RETURNING *;";

                return await connection.QuerySingleAsync<Post>(sql, post);
            }
        }

        public async Task<Post> GetByIdAsync(Guid postId)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                string sql = "SELECT * FROM Posts WHERE post_id = @PostId;";
                return await connection.QuerySingleOrDefaultAsync<Post>(sql, new { PostId = postId });
            }
        }

        public async Task<Post> EditAsync(Guid postId, Post post)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                string sql = @"UPDATE Posts SET content = @Content, image = @Image
                               WHERE post_id = @PostId
                               RETURNING *;";

                return await connection.QuerySingleAsync<Post>(sql, new
                {
                    PostId = postId,
                    Content = post.Content,
                    Image = post.Image
                });
            }
        }

        public async Task DeleteAsync(Guid postId)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                string sql = "DELETE FROM Posts WHERE post_id = @PostId;";
                await connection.ExecuteAsync(sql, new { PostId = postId });
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByUserAsync(Guid userId)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                string sql = "SELECT * FROM Posts WHERE user_id = @UserId;";
                return await connection.QueryAsync<Post>(sql, new { UserId = userId });
            }
        }
    }
}
