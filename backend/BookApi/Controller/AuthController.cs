using Microsoft.AspNetCore.Mvc;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BookApi.Data;

namespace BookApi.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(ApiDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

      [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
            return BadRequest("Användarnamn finns redan.");

        var user = new User
        {
            Username = userDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registrering lyckades" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            return Unauthorized("Fel användarnamn eller lösenord.");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_config["JwtKey"] ?? "secret_key_007!" );
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ClaimsIdentity(new[] { new Claim("id", user.Id.ToString())}),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}

public class UserDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!; 
}