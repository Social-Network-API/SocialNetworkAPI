using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities;
using SocialNetwork.Persistence.DataBase;

namespace SocialNetwork.Persistence.Repositories;
public class LikeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LikeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Like> CreateAsync(Like like)
    {
        await _dbContext.Likes.AddAsync(like);
        await _dbContext.SaveChangesAsync();
        return like;
    }

    public async Task DeleteAsync(Guid postId, Guid userId)
    {
        var like = await _dbContext.Likes
            .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
        if (like != null)
        {
            _dbContext.Likes.Remove(like);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Like>> GetLikeByPostIdAsync(Guid postId)
    {
        return await _dbContext.Likes
            .Where(l => l.PostId == postId)
            .ToListAsync();
    }


    public async Task<IEnumerable<Like>> GetUserLikesAsync(Guid userId)
    {
        return await _dbContext.Likes
            .Where(l => l.UserId == userId)
            .ToListAsync();
    }

    public async Task<Like?> GetByIdAsync(Guid likeId)
    {
        var like = await _dbContext.Likes.FindAsync(likeId);
        return like;
    }

    public async Task UpdateAsync(Guid likeId, Like updatedLike)
    {
        await Task.CompletedTask;  
    }
}
