using BlogPost.Api.Models;
using BlogPost.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace BlogPost.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly JwtSettings _jwtSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(IOptions<JwtSettings> jwtSettings, IHttpClientFactory httpClientFactory)
    {
        _jwtSettings = jwtSettings.Value;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
    {
        // Validar el token con Google
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={dto.IdToken}");

        if (!response.IsSuccessStatusCode)
            return Unauthorized("Token de Google no válido");

        var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

        var email = payload.Value<string>("email");
        var name = payload.Value<string>("name");

        // Generar JWT propio
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, name ?? ""),
            new Claim(ClaimTypes.Email, email ?? ""),
            new Claim(ClaimTypes.Role, "User") 
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = jwt });
    }
    
    
}


