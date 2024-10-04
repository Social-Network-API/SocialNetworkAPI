using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.Models;
using SocialNetworkApi.Persistence.DataBase;

namespace SocialNetworkApi.DataAccess.Repositories.Concretes;

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

    public async Task<ServiceResult<Comment>> GetByIdAsync(Guid commentId) 
    {
        var comment = await _dbContext.Comments.FindAsync(commentId);
        return comment == null 
            ? new ServiceResult<Comment> { Success = false }
            : new ServiceResult<Comment> { Data = comment, Success = true };
    }

    public async Task<ServiceResult<Comment>> UpdateAsync(Guid commentId, Comment updatedComment)
    {
        var existingComment = await _dbContext.Comments.FindAsync(commentId);
        if (existingComment == null)
            return new ServiceResult<Comment> { Success = false };

        existingComment.Content = updatedComment.Content;
        await _dbContext.SaveChangesAsync();
        return new ServiceResult<Comment> { Data = existingComment, Success = true };
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
        
    public async Task<IEnumerable<Comment>> GetCommentsForPostAsync(Guid postId)
    { 
        return await _dbContext.Comments
            .Where(c => c.PostId == postId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }
}

