using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Mappers.Requests;
using SocialNetwork.Services;

namespace SocialNetwork.Controllers;

[ApiController]
[Route("api/v1/posts/{postId:guid}/likes")]
public class LikeController : ControllerBase
{
    private readonly LikeService _likeService;

    public LikeController(LikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid postId, [FromBody] CreateLikeRequest request)
    {
        var like = request.ToDomain();
        like.PostId = postId;
        var userExists = await _likeService.CheckIfUserExistsAsync(like.UserId);
        if (!userExists)
            return BadRequest($"User with ID {like.UserId} does not exist.");

        var result = await _likeService.CreateAsync(like);
        return CreatedAtAction(nameof(GetLikesByPostId), new { postId }, result.Data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid postId, Guid userId)
    {
        await _likeService.DeleteAsync(postId, userId);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetLikesByPostId(Guid postId)
    {
        var result = await _likeService.GetLikesByPostIdAsync(postId);
        return Ok(result.Data);
    }

}
