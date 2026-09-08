using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ListaCompras.Core.Entities;

namespace ListaCompras.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Credential))
        {
            return BadRequest(new { error = "Credential token is required" });
        }

        var payload = await ValidateGoogleTokenAsync(request.Credential);
        if (payload == null)
        {
            return Unauthorized(new { error = "Invalid Google token" });
        }

        var user = await _userManager.FindByEmailAsync(payload.Email!);
        
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = payload.Email,
                Email = payload.Email,
                Name = payload.Name,
                GoogleId = payload.Subject,
                PictureUrl = payload.Picture,
                EmailConfirmed = payload.EmailVerified
            };
            
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { error = "Failed to create user", details = result.Errors });
            }
        }
        else
        {
            if (string.IsNullOrEmpty(user.GoogleId))
            {
                user.GoogleId = payload.Subject;
                user.PictureUrl = payload.Picture;
                await _userManager.UpdateAsync(user);
            }
        }

        var token = GenerateJwtToken(user);
        
        return Ok(new AuthResponse
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                Name = user.Name,
                PictureUrl = user.PictureUrl
            }
        });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { error = "User not found" });
        }

        return Ok(new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            Name = user.Name,
            PictureUrl = user.PictureUrl
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logged out successfully. Please remove token from client storage." });
    }

    private async Task<GooglePayload?> ValidateGoogleTokenAsync(string credential)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(credential);
            
            var payload = new GooglePayload
            {
                Subject = jsonToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
                Email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                Name = jsonToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
                Picture = jsonToken.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                EmailVerified = bool.Parse(jsonToken.Claims.FirstOrDefault(c => c.Type == "email_verified")?.Value ?? "false")
            };

            return payload;
        }
        catch
        {
            return null;
        }
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var jwtSettings = _configuration.GetSection("Authentication:Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.Name ?? user.Email!),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpirationMinutes"]!)),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public record GoogleLoginRequest(string Credential);

public record GooglePayload
{
    public string? Subject { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public bool EmailVerified { get; set; }
}

public record AuthResponse
{
    public string Token { get; init; } = string.Empty;
    public UserDto User { get; init; } = new();
}

public record UserDto
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Name { get; init; }
    public string? PictureUrl { get; init; }
}