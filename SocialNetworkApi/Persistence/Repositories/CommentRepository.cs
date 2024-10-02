using SocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Persistence.DataBase;

namespace SocialNetwork.Persistence.Repositories
{
    public class CommentsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId)
        {
            return await _dbContext.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(Guid commentId)
        {
            return await _dbContext.Comments.FindAsync(commentId);
        }

        public async Task<Comment> UpdateAsync(Guid commentId, Comment updatedComment)
        {
            var existingComment = await _dbContext.Comments.FindAsync(commentId);
            if (existingComment == null)
                return null;

            existingComment.Content = updatedComment.Content;
            await _dbContext.SaveChangesAsync();
            return existingComment;
        }

        public async Task DeleteAsync(Guid commentId)
        {
            var comment = await _dbContext.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
