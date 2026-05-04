namespace WeatherStation.BusinessLogic.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.DTOs;
using WeatherStation.BusinessLogic.DTOs.AuthDTOs;
using WeatherStation.DataAccess.Entities;
using WeatherStation.DataAccess.Repositories;
using WeatherStation.DataAccess.Contracts;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(UserRegisterDto registerDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
        if (existingUser != null)
            throw new Exception("Користувач з таким email вже існує.");

        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = BCrypt.HashPassword(registerDto.Password)
        };

        var createdUser = await _userRepository.AddAsync(user);

        var token = GenerateJwtToken(createdUser);

        return new AuthResponseDto { Token = token, Message = "Реєстрація успішна" };
    }

    public async Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);

        if (user == null || !BCrypt.Verify(loginDto.Password, user.Password))
            throw new Exception("Невірний email або пароль.");

        var token = GenerateJwtToken(user);
        return new AuthResponseDto { Token = token, Message = "Вхід успішний" };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.Name)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
