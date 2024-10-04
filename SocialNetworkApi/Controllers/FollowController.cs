using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Mappers.Request.Follow;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Controllers;

[Route("api/v1/users")]
public class FollowController : ControllerBase
{
    private readonly FollowService _followService;

    public FollowController(FollowService followService)
    {
        _followService = followService;
    }

    [HttpPost("{userId:guid}/follow")]
    public async Task<IActionResult> Follow(Guid userId, [FromBody] CreateFollowUserRequest request)
    {
        if (userId == request.FollowerId)
            return BadRequest("You cannot follow yourself.");

        var result = await _followService.FollowUserAsync(request.FollowerId, userId);
        return result.Success ? Created("", new { message = "User followed successfully." }) : BadRequest(result.Errors);
    }

    [HttpDelete("{userId:guid}/unfollow")]
    public async Task<IActionResult> Unfollow(Guid userId, [FromBody] CreateFollowUserRequest request)
    {
        var result = await _followService.UnfollowUserAsync(request.FollowerId, userId);
        return result.Success ? NoContent() : BadRequest(result.Errors);
    }

    [HttpGet("{userId:guid}/followers")]
    public async Task<IActionResult> GetFollowers(Guid userId)
    {
        var result = await _followService.GetFollowersAsync(userId);
        return result.Success ? Ok(result.Data) : NotFound();
    }

    [HttpGet("{userId:guid}/following")]
    public async Task<IActionResult> GetFollowing(Guid userId)
    {
        var result = await _followService.GetFollowingAsync(userId);
        return result.Success ? Ok(result.Data) : NotFound();
    }
}


