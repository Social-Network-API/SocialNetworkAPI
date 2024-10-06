using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Mappers.Request.Auth;
using SocialNetworkApi.Services.Interface;

namespace SocialNetworkApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var user = await _authService.RegisterAsync(request);
            return Ok(new { userId = user.UserId, message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authService.LoginAsync(request);
            return Ok(new { token });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Invalid email or password." });
        }

    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string token)
    {
        var tokenExists = await _authService.LogoutAsync(token);
        if (!tokenExists)
            return BadRequest(new { message = "Invalid token." });

        return Ok(new { message = "Logged out successfully." });

    }
}
