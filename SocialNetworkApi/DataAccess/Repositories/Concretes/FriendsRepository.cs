using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.Persistence.DataBase;

namespace SocialNetworkApi.DataAccess.Repositories.Concretes;

public class FriendsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FriendsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task FollowUserAsync(Guid userId, Guid friendId)
    {
        if (userId == friendId) throw new ArgumentException("Users cannot follow themselves");

        var alreadyFriends = await _dbContext.Friends
            .AnyAsync(f => f.UserId == userId && f.FriendId == friendId);

        if (!alreadyFriends)
        {
            var friendForUser = new Friend { UserId = userId, FriendId = friendId };
            var friendForFriend = new Friend { UserId = friendId, FriendId = userId };

            await _dbContext.Friends.AddAsync(friendForUser);
            await _dbContext.Friends.AddAsync(friendForFriend);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UnfollowUserAsync(Guid userId, Guid friendId)
    {
        var friendRelationForUser = await _dbContext.Friends
            .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

        if (friendRelationForUser != null)
        {
            _dbContext.Friends.Remove(friendRelationForUser);
        }

        var friendRelationForFriend = await _dbContext.Friends
            .FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId);

        if (friendRelationForFriend != null)
        {
            _dbContext.Friends.Remove(friendRelationForFriend);
        }

        await _dbContext.SaveChangesAsync();
    }


    public async Task<IEnumerable<User>> GetFriendsListAsync(Guid userId)
    {
        return await _dbContext.Friends
            .Where(f => f.UserId == userId)
            .Select(f => f.FriendId)
            .Join(_dbContext.Users, friendId => friendId, user => user.UserId, (friendId, user) => user)
            .ToListAsync();
    }

    public async Task<bool> AreFriendsAsync(Guid userId, Guid friendId)
    {
        return await _dbContext.Friends
            .AnyAsync(f => f.UserId == userId && f.FriendId == friendId);
    }
}