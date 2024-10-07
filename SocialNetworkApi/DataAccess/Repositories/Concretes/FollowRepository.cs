using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.Business.Mappers.Response;
using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.Persistence.DataBase;

namespace SocialNetworkApi.DataAccess.Repositories.Concretes;

public class FollowRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FollowRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> FollowUserAsync(Guid followerId, Guid followedId)
    {
        var alreadyFollowing = await _dbContext.Followers
            .AnyAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);

        if (alreadyFollowing)
            return false;

        var follow = new Follower { FollowerId = followerId, FollowedId = followedId, CreatedAt = DateTime.UtcNow };
        await _dbContext.Followers.AddAsync(follow);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnfollowUserAsync(Guid followerId, Guid followedId)
    {
        var follow = await _dbContext.Followers
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);

        if (follow == null)
            return false;

        _dbContext.Followers.Remove(follow);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<FollowerResponse>> GetFollowersAsync(Guid userId)
    {
        var followers = await _dbContext.Followers
            .Where(f => f.FollowedId == userId)
            .Join(_dbContext.Users, f => f.FollowerId, u => u.UserId, (f, u) => new FollowerResponse
            {
                FollowerId = f.FollowerId,
                FollowerName = u.Name, 
                FollowedAt = f.CreatedAt
            })
            .ToListAsync();

        return followers;
    }

    public async Task<IEnumerable<FollowingResponse>> GetFollowingAsync(Guid userId)
    {
        var following = await _dbContext.Followers
            .Where(f => f.FollowerId == userId)
            .Join(_dbContext.Users, f => f.FollowedId, u => u.UserId, (f, u) => new FollowingResponse
            {
                FollowedId = f.FollowedId,
                FollowedName = u.Name, 
                FollowedAt = f.CreatedAt
            })
            .ToListAsync();

        return following;
    }
    
    public async Task<bool> AreFriendsAsync(Guid userId, Guid friendId)
    {
        return await _dbContext.Followers
            .AnyAsync(f => f.FollowerId == userId && f.FollowedId == friendId);
    }
   

}