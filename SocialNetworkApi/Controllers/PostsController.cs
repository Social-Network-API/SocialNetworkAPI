using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers
{
    [ApiController]
    [Route("api/v1/posts")]
    public class PostsController : ControllerBase
    {
        private readonly PostsService _postsService;

        public PostsController(PostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var post = await _postsService.ReadAsync(id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpGet("users/{userId:guid}")]
        public async Task<IActionResult> GetPostsByUser([FromRoute] Guid userId)
        {
            var posts = await _postsService.GetPostsByUserAsync(userId);
            return Ok(posts);
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetUserFeed()
        {
            var feed = await _postsService.GetUserFeedAsync();
            return Ok(feed);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post post)
        {
            var createdPost = await _postsService.CreateAsync(post);
            return Ok(CreatedAtAction(nameof(Get), new { id = createdPost.Id }, createdPost).Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Post post)
        {
            var result = await _postsService.UpdateAsync(id, post);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _postsService.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
