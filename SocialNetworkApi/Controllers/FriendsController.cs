using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Mappers.Request.Follow;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class FriendsController : ControllerBase
{
    private readonly FriendService _friendsService;

    public FriendsController(FriendService friendsService)
    {
        _friendsService = friendsService;
    }


    [HttpPost("{userId:guid}/follow")]
    public async Task<IActionResult> Follow(Guid userId, [FromBody] CreateFriendsRequest request)
    {
        if (userId == request.FollowerId)
            return BadRequest("You cannot follow yourself.");

        var result = await _friendsService.FollowUserAsync(request.FollowerId, userId);
        return result.Success ? Created("", new { message = "User followed successfully." }) : BadRequest(result.Errors);
    }

    [HttpDelete("{userId:guid}/unfollow")]
    public async Task<IActionResult> Unfollow(Guid userId, [FromBody] CreateFriendsRequest request)
    {
        var result = await _friendsService.UnfollowUserAsync(request.FollowerId, userId);
        return result.Success ? NoContent() : BadRequest();
    }

    [HttpGet("{userId:guid}/friends")]
    public async Task<IActionResult> GetFriends(Guid userId)
    {
        var result = await _friendsService.GetFriendsListAsync(userId);
        return result.Success ? Ok(result.Data) : NotFound();
    }
}

