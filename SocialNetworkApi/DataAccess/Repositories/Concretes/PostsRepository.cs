using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.Business.Mappers.Response;
using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.Persistence.DataBase;

namespace SocialNetworkApi.DataAccess.Repositories.Concretes;

public class PostsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PostsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post> CreateAsync(Post post)

    {
        Console.WriteLine($"Image value: {post.ImageUrl}");

        await _dbContext.Posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> GetByIdAsync(Guid postId)
    {
        return await _dbContext.Posts.FindAsync(postId);
    }

    public async Task<Post?> EditAsync(Guid postId, Post updatedPost)
    {
        var existingPost = await _dbContext.Posts.FindAsync(postId);
        if (existingPost == null)
            return null;

        existingPost.Content = updatedPost.Content;
        existingPost.ImageUrl = updatedPost.ImageUrl;

        _dbContext.Posts.Update(existingPost);
        await _dbContext.SaveChangesAsync();
        return existingPost;
    }

    public async Task DeleteAsync(Guid postId)
    {
        var post = await _dbContext.Posts.FindAsync(postId);
        if (post != null)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Post>> GetUserPostsAsync(Guid userId)
    {
        return await _dbContext.Posts
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetHomePostsAsync(Guid userId)
    {
        var followedUserIds = await _dbContext.Followers
            .Where(f => f.FollowerId == userId)
            .Select(f => f.FollowedId)
            .ToListAsync();

        followedUserIds.Add(userId);

        return await _dbContext.Posts
            .Where(p => followedUserIds.Contains(p.UserId))
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Like>> GetLikesForPostAsync(Guid postId)
    {
        return await _dbContext.Likes
            .Where(l => l.PostId == postId)
            .ToListAsync();
    }

}
