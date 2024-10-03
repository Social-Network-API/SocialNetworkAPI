using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Mappers.Requests;
using SocialNetwork.Mappers.Responses;

namespace SocialNetwork.Services
{
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

        // Obtener usuario por ID
        [HttpGet]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var result = await _userService.GetByIdAsync(userId);
            return result.Data is null
                ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"User not found (userId {userId})")
                : Ok(result.Data);
        }

        // Actualizar un usuario
        [HttpPut]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid userId, [FromBody] UpdateUserResponse request)
        {
            var user = request.ToDomain();
            var result = await _userService.UpdateAsync(userId, user);

            return result.Success
                ? Ok(result.Data)
                : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Failed to update user");
        }

        // Eliminar un usuario
        [HttpDelete]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await _userService.DeleteAsync(userId);
            return NoContent();
        }

        // Obtener todos los usuarios
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync(); // Usamos el nuevo método
            var userResponses = users.Select(UserResponse.FromDomain); // Mapeamos a UserResponse

            return Ok(userResponses);
        }
    }
}
