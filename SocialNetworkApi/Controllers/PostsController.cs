using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Mappers.Request.Post;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Controllers;

[ApiController]
[Route("api/v1/posts")]
public class PostsController(PostsService postsService) : ControllerBase
{
    private readonly PostsService _postsService = postsService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
    {
        var post = request.ToDomain(); 
        var response = await _postsService.CreateAsync(post);  
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { postId = post.Id },
            value: response.Data
            );
    }

    [HttpGet("{postId:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid postId)
    {
        var result = await _postsService.GetByIdAsync(postId);
        return result.Data is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Post not found (postId {postId})")
            : Ok(result.Data);
    }

    [HttpPut("{postId:guid}")]
    public async Task<IActionResult> Edit([FromRoute] Guid postId, [FromBody] EditPostRequest request)
    {
        var post = request.ToDomain();
        var result = await _postsService.UpdateAsync(postId, post);

        return result.Success
            ? Ok(result.Data) 
            : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Failed to update post");
    }
 
    [HttpDelete("{postId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid postId)
    {
        await _postsService.DeleteAsync(postId);
        return NoContent();
    }

    [HttpGet("/users/{userId:guid}/posts")]
    public async Task<IActionResult> GetUserPosts([FromRoute] Guid userId)
    {
        var result = await _postsService.GetUserPostsAsync(userId);
        return Ok(result.Data);
    }
        
    [HttpGet("home")]
    public async Task<IActionResult> GetHomePosts([FromQuery] Guid userId)
    {
        var result = await _postsService.GetHomePostsWithCommentsAsync(userId);
        return Ok(result.Data);
    }
}

