using BlogPost.Api.Models;
using BlogPost.Api.DTOs;
using BlogPost.Api.Interfaces;
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
    private readonly IUserRepository _userRepository;

    public AuthController(IOptions<JwtSettings> jwtSettings, IHttpClientFactory httpClientFactory, IUserRepository userRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _httpClientFactory = httpClientFactory;
        _userRepository = userRepository;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={dto.IdToken}");

        if (!response.IsSuccessStatusCode)
            return Unauthorized("Token de Google no válido");

        var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

        var email = payload.Value<string>("email");
        var name = payload.Value<string>("name");

        if (email == null)
            return Unauthorized("No se pudo obtener el correo electrónico del token de Google");

        string role = email == "neoxxx9717@gmail.com" ? "Admin" : "User";

        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
        {
            user = new User
            {
                Email = email,
                Name = name ?? "",
                Role = role
            };

            await _userRepository.CreateAsync(user);
        }

        // Generar JWT propio
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, name ?? ""),
            new Claim(ClaimTypes.Email, email ?? ""),
            new Claim(ClaimTypes.Role, role)
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


