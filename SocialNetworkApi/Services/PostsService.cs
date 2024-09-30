using Microsoft.EntityFrameworkCore;
using RestApi.Services.interfaces;
using SocialNetwork.Domain;
using SocialNetwork.Persistence.DataBase;

namespace SocialNetwork.Services
{
    public class PostsService : IBaseService<Post>
    {
        private readonly ApplicationDbContext _context;

        public PostsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreateAsync(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<List<Post>> ReadAsync()
        {
            return await _context.Post
                .Where(p => p.UserId == UserContext.CurrentUserId)
                .ToListAsync();
        }

        public async Task<Post?> ReadAsync(Guid id)
        {
            return await _context.Post
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> UpdateAsync(Guid id, Post updatedPost)
        {
            var existingPost = await _context.Post.FindAsync(id);
            if (existingPost == null) return false;

            existingPost.Content = updatedPost.Content;
            existingPost.ImageUrl = updatedPost.ImageUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null) return false;

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Post>> GetPostsByUserAsync(Guid userId)
        {
            return await _context.Post
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<Post>> GetUserFeedAsync()
        {
            return await _context.Post
                .Where(p => p.UserId == UserContext.CurrentUserId || p.User.Friends.Contains(UserContext.CurrentUserId))
                .ToListAsync();
        }


    }
}
