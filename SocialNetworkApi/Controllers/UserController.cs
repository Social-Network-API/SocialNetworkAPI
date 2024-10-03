using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Mappers.Requests;
using SocialNetwork.Mappers.Responses;

namespace SocialNetwork.Services;
[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var user = request.ToDomain();
        var response = await _userService.CreateAsync(user);
        return CreatedAtAction(
            actionName: nameof(GetUserById),
            routeValues: new { userId = user.UserId },
            value: response.Data
        );
    }

        [HttpGet]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var result = await _userService.GetByIdAsync(userId);
            return result.Data is null
                ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"User not found (userId {userId})")
                : Ok(result.Data);
        }

        [HttpPut]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid userId, [FromBody] EditUserRequest request)
        {
            var user = request.ToDomain();
            var result = await _userService.UpdateAsync(userId, user);

        return result.Success
            ? Ok(result.Data)
            : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Failed to update user");
    }

        [HttpDelete]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await _userService.DeleteAsync(userId);
            return NoContent();
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync(); 
            var userResponses = users.Select(UserResponse.FromDomain); 

        return Ok(userResponses);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] string searchUserByName)
    {
        var result = await _userService.SearchUsersAsync(searchUserByName);

        if (result == null || !result.Any())
            return NotFound(new { message = "No results found." });

        return Ok(result);
    }
}
