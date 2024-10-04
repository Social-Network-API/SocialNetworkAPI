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

        var alreadyLiked = await _likeService.CheckIfUserAlreadyLikedAsync(postId, like.UserId);
        if (alreadyLiked)
            return Conflict($"User with ID {like.UserId} has already liked this post.");

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

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetLikesByUserId(Guid userId)
    {
        var result = await _likeService.GetLikesByUserIdAsync(userId);
        return result.Data is null || !result.Data.Any()
            ? NotFound(new { message = "No likes found for this user." })
            : Ok(result.Data);
    }


}
