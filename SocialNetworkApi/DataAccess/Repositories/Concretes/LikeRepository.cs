using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.Persistence.DataBase;

namespace SocialNetworkApi.DataAccess.Repositories.Concretes;

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

    public async Task<Like?> GetByIdAsync(Guid likeId)
    {
        var like = await _dbContext.Likes.FindAsync(likeId);
        return like;
    }

    public async Task UpdateAsync(Guid likeId, Like updatedLike)
    {
        await Task.CompletedTask;
    }

    public async Task<bool> CheckIfUserExistsAsync(Guid userId)
    {
        return await _dbContext.Users.AnyAsync(u => u.UserId == userId);
    }

    public async Task<bool> CheckIfUserAlreadyLikedAsync(Guid postId, Guid userId)
    {
        return await _dbContext.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
    }

    public async Task<IEnumerable<Like>> GetLikesByUserIdAsync(Guid userId)
    {
        return await _dbContext.Likes
            .Where(l => l.UserId == userId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Like>> GetLikesForPostsAsync(IEnumerable<Guid> postIds)
    {
        return await _dbContext.Likes
            .Where(l => postIds.Contains(l.PostId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsLikedByUserAsync(Guid userId)
    {
        return await _dbContext.Likes
            .Where(like => like.UserId == userId)
            .Join(_dbContext.Posts, 
                like => like.PostId, 
                post => post.Id, 
                (like, post) => post)
            .ToListAsync();
    }

  }
